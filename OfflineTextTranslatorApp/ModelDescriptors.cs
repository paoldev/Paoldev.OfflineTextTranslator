using Paoldev.OfflineTextTranslator;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Reflection;
using System.Xml.Serialization;

namespace OfflineTextTranslatorApp
{
    [XmlInclude(typeof(CT2ModelDescriptor))]
    //[XmlInclude(typeof(AnotherModelDescriptor))]
    public abstract class ModelDescriptor
    {
        [Category("Common")]
        [DisplayName("Model name")]
        public string Name { get; set; } = string.Empty;

        // ToString() is automatically called by CollectionEditor.GetDisplayText, if not overridden by ModelDescriptorsEditor,
        // to show the model name instead of its type; if "ToString()" returns an empty string, CollectionEditor may show the Type name.
        public override string ToString() => Name;
        public virtual string GetDescription()
        {
            string Description = $"Model name: {Name}";
            var props = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var prop in props)
            {
                if (prop.Name != "Name")
                {
                    Description += Environment.NewLine + Environment.NewLine;

                    var displayNameAttr = prop.GetCustomAttribute<DisplayNameAttribute>();
                    Description += $"{displayNameAttr?.DisplayName ?? prop.Name}: {prop.GetValue(this)}";
                }
            }
            return Description;
        }

        public virtual ModelDescriptor SimpleClone()
        {
            var type = GetType();
            var ctor = type.GetConstructor(Type.EmptyTypes) ?? throw new InvalidOperationException($"Type {type.FullName} must have a parameterless constructor.");
            var clone = ctor.Invoke(null) as ModelDescriptor ?? throw new InvalidOperationException($"Type {type.FullName} is not a ModelDescriptor.");
            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var prop in props)
            {
                if (prop.CanRead && prop.CanWrite)
                {
                    prop.SetValue(clone, prop.GetValue(this));
                }
            }
            return clone;
        }

        public abstract bool IsValid();
        public abstract string GetInvalidDescription();
    }

    [Serializable]
    public class CT2ModelDescriptor : ModelDescriptor
    {
        [DisplayName("Device type")]
        public DeviceType Device { get; set; } = DeviceType.Auto;

        [DisplayName("Language file")]
        [Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(UITypeEditor))]
        public string LanguageDescriptionFile { get; set; } = string.Empty;

        [DisplayName("Model directory")]
        [Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(UITypeEditor))]
        public string ModelDirectory { get; set; } = string.Empty;

        [DisplayName("Tokenizer model file type")]
        public TokenizerType TokenizerType { get; set; } = TokenizerType.None;

        [DisplayName("Tokenizer model file")]
        [Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(UITypeEditor))]
        public string TokenizerModelFilePath { get; set; } = string.Empty;

        public override bool IsValid() => !string.IsNullOrEmpty(Name) &&
            !string.IsNullOrEmpty(LanguageDescriptionFile) &&
            !string.IsNullOrEmpty(ModelDirectory) &&
            ((TokenizerType == TokenizerType.None) || !string.IsNullOrEmpty(TokenizerModelFilePath));

        public override string GetInvalidDescription()
        {
            List<string> errors = [];
            if (string.IsNullOrEmpty(Name))
            {
                errors.Add("Missing Name.");
            }
            if (string.IsNullOrEmpty(LanguageDescriptionFile))
            {
                errors.Add("Missing Language file.");
            }
            if (string.IsNullOrEmpty(ModelDirectory))
            {
                errors.Add("Missing Model directory.");
            }
            if ((TokenizerType != TokenizerType.None) && string.IsNullOrEmpty(TokenizerModelFilePath))
            {
                errors.Add($"Missing Tokenizer model file for {TokenizerType}.");
            }
            return string.Join(Environment.NewLine, errors);
        }
    }

    /*
    [Serializable]
    public class AnotherModelDescriptor : ModelDescriptor
    {
        [DisplayName("Test A")]
        public string TestA { get; set; } = string.Empty;

        [DisplayName("Test B")]
        public int TestB { get; set; } = 0;

        public override bool IsValid() => true;
        public override string GetInvalidDescription() => string.Empty;
    }
    */

    [Serializable]
    public class ModelDescriptors
    {
        [Editor(typeof(ModelDescriptorsEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(ModelDescriptorsConverter))]
        public List<ModelDescriptor> Models { get; set; } = [];

        public bool IsValid()
        {
            bool bOk = Models != null;
            var duplicates = Models?
                        .GroupBy(m => m.Name.Trim(), StringComparer.OrdinalIgnoreCase)
                        .Where(g => (g.Count() > 1) || string.IsNullOrEmpty(g.Key));
            var invalids = Models?.Where(m => !m.IsValid());
            if ((duplicates?.Count() > 0) || (invalids?.Count() > 0))
            {
                bOk = false;
            }
            return bOk;
        }

        public bool ValidateOnLoad()
        {
            Models ??= [];

            // Always trim model names
            foreach (var model in Models)
            {
                model.Name = model.Name.Trim();
            }

            return IsValid();
        }

        public void TryAutoRepair()
        {
            if (Models == null)
            {
                Models = [];
                return;
            }

            void MakeUniqueNames(IEnumerable<ModelDescriptor> modelList, string prefix)
            {
                foreach (var model in modelList)
                {
                    do
                    {
                        model.Name = prefix + Guid.NewGuid().ToString();
                    }
                    while (Models.Where(m => m != model).FirstOrDefault(m => m.Name.Equals(model.Name, StringComparison.OrdinalIgnoreCase)) != null);
                }
            }

            // Step 1: trim model names
            foreach (var model in Models)
            {
                model.Name = model.Name.Trim();
            }

            // Step 2: repair empty model names
            var emptyNamesModels = Models.Where(m => string.IsNullOrEmpty(m.Name));
            MakeUniqueNames(emptyNamesModels, string.Empty);

            // Step 3: remove invalid models
            // This step has to be done after repairing empty model names, to avoid removing "valid" models with just an empty name.
            Models = [.. Models.Where(m => m.IsValid())];

            // Step 4: create unique model names
            var duplicatedModels = Models
                        .GroupBy(m => m.Name, StringComparer.OrdinalIgnoreCase)
                        .Where(g => g.Count() > 1)
                        .Select(g => g.Key);
            foreach (var groupName in duplicatedModels)
            {
                var models = Models.Where(m => m.Name.Equals(groupName, StringComparison.OrdinalIgnoreCase)).ToList();
                models[0].Name = groupName; // Normalize the name of the first model...
                models.RemoveAt(0); // ... then preserve its name.
                MakeUniqueNames(models, groupName);
            }
        }
    }

    public class ModelDescriptorsEditor(Type type) : CollectionEditor(type)
    {
        protected override Type[] CreateNewItemTypes() => [typeof(CT2ModelDescriptor)/*, typeof(AnotherModelDescriptor)*/];

        protected override string GetDisplayText(object? value)
        {
            if (value is ModelDescriptor model)
            {
                if (string.IsNullOrWhiteSpace(model.Name))
                {
                    return "Specify a valid model name";
                }

                return model.Name;
            }
            return base.GetDisplayText(value);
        }

        public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
        {
            bool bOk;
            object? result;
            // Duplicate "value" to avoid editing original items also when calling "cancel".
            object? copy = (value is IList<ModelDescriptor> descriptors) ? descriptors.Select(m => m.SimpleClone()).ToList() : value;
            do
            {
                bOk = true;
                result = base.EditValue(context, provider, copy);
                if (!bCancelled && (result is IList<ModelDescriptor> items))
                {
                    var duplicates = items
                        .Select(m => m.Name.Trim())
                        .GroupBy(s => s, StringComparer.OrdinalIgnoreCase)
                        .Where(g => (g.Count() > 1) || string.IsNullOrEmpty(g.Key))
                        .Select(g => g.Key)
                        .ToList();
                    var invalids = items
                        .Where(m => !m.IsValid())
                        .Select(m => $"{m.Name.Trim()}:{Environment.NewLine}{m.GetInvalidDescription()}")
                        .ToList();
                    if (duplicates?.Count > 0)
                    {
                        if (duplicates.Remove(string.Empty))
                        {
                            duplicates.Insert(0, "<empty name>");
                        }
                        MessageBox.Show(
                            "Empty model names or duplicated model names are not allowed:\n" + string.Join(Environment.NewLine, duplicates),
                            "Validation Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        bOk = false;
                    }
                    else if (invalids?.Count > 0)
                    {
                        MessageBox.Show(
                            "Invalid models:\n" + string.Join(Environment.NewLine, invalids),
                            "Validation Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        bOk = false;
                    }
                }
            }
            while (!bOk);

            // Automatically refresh the collection when closing its editor.
            var ownerGridProp = provider.GetType().GetProperty("OwnerGrid", BindingFlags.Instance | BindingFlags.Public);
            var ownerGrid = ownerGridProp?.GetValue(provider) as PropertyGrid;
            ownerGrid?.Refresh();

            return bCancelled ? value : result;
        }

        protected override CollectionForm CreateCollectionForm()
        {
            var form = base.CreateCollectionForm();

            bCancelled = false;
            form.FormClosing += Form_FormClosing;
            return form;
        }

        private void Form_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (sender is CollectionForm form)
            {
                bCancelled = form.DialogResult == DialogResult.Cancel;
            }
        }

        private bool bCancelled = false;
    }

    public class ModelDescriptorsConverter : ExpandableObjectConverter
    {
        public override bool GetPropertiesSupported(ITypeDescriptorContext? context) => true;

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext? context, object value, Attribute[]? attributes)
        {
            if (value is IEnumerable enumerable)
            {
                var props = enumerable.Cast<object>().Select(item => new ModelNamePropertyDescriptor(item)).ToArray();
                return new PropertyDescriptorCollection(props);
            }

            return base.GetProperties(context, value, attributes);
        }

        public override object? ConvertTo(ITypeDescriptorContext? context,
                                     System.Globalization.CultureInfo? culture,
                                     object? value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is IList list)
                return (list.Count == 1 ? $"[{list.Count} item]" : $"[{list.Count} items]") + " - Click here to view or edit models.";
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    public class ModelNamePropertyDescriptor(object item) : PropertyDescriptor($"{item}", null)
    {
        public override Type ComponentType => typeof(ModelDescriptor);
        public override bool IsReadOnly => true;
        public override Type PropertyType => typeof(string);

        public override bool CanResetValue(object component) => false;
        public override object GetValue(object? component) => string.Empty;
        public override void ResetValue(object component) { }
        public override void SetValue(object? component, object? value) { }
        public override bool ShouldSerializeValue(object component) => false;
    }
}
