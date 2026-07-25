using OfflineTextTranslatorApp.Properties;
using System.Diagnostics;

namespace OfflineTextTranslatorApp
{
    public partial class Form1 : Form
    {
        private Paoldev.OfflineTextTranslator.ITranslator? Translator = null;
        private string EnglishCodeForUI = "en";
        private Dictionary<string, string> LanguagePrefixes = [];
        private bool Initialized = false;

        private readonly string _untranslatedEnglishTitle = "Offline Text Translator";
        private readonly string _untranslatedEnglishCheckBox = "Translate UI";
        private readonly string _untranslatedEnglishButton = "Translate";
        private readonly string _untranslatedEnglishElapsedTime = "Elapsed time";

        private ModelDescriptor? _CurrentModel = null;
        private bool bInitializingComboBoxModels = false;

        public Form1()
        {
            InitializeComponent();

            ReadInitialAppSettings();
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (_CurrentModel == null)
            {
                await ShowConfigurationForm();
            }
            else
            {
                await InitializeTranslatorAsync();
            }
        }

        private async Task InitializeTranslatorAsync()
        {
            Initialized = false;

            UseWaitCursor = true;

            //Force an immediate garbage collection, since the Translator may take a lot of memory.
            Translator = null;
            GC.Collect();

            List<Tuple<Control, bool>> controlStates = [];
            foreach (Control c in Controls)
            {
                if (c != null)
                {
                    controlStates.Add(new Tuple<Control, bool>(c, c.Enabled));
                    c.Enabled = false;
                }
            }

            ResetUIEnglish();

            var languages = new List<string>();

            LanguagePrefixes = [];

            //Configuration error condition: there is no need to try to initialize the Translator in this case.
            string sExtraError = string.Empty;
            bool bError = (_CurrentModel == null) ||
                (_CurrentModel is not CT2ModelDescriptor);    // NB: only CT2ModelDescriptor is currently supported

            if (!bError)
            {
                // NB: only CT2ModelDescriptor is currently supported
                var ct2Model = (_CurrentModel as CT2ModelDescriptor)!;

                var options = new Paoldev.OfflineTextTranslator.TranslatorOptions
                {
                    Device = ct2Model.Device,
                    TokenizerModelType = ct2Model.TokenizerType,
                    TokenizerModelFilePath = ct2Model.TokenizerModelFilePath,
                    CTranslate2ModelDirectoryPath = ct2Model.ModelDirectory
                };

                try
                {
                    Translator = await Paoldev.OfflineTextTranslator.Translator.CreateAsync(options);

                    var languageDescription = await ReadLanguageDescriptionFile(ct2Model.LanguageDescriptionFile);

                    languages = languageDescription.Item1;
                    LanguagePrefixes = languageDescription.Item2;
                    EnglishCodeForUI = languageDescription.Item3;
                }
                catch (Exception ex)
                {
                    bError = true;
                    sExtraError = ex.Message;
                }
            }

            languages.Insert(0, "none");
            LanguagePrefixes.Add("none", "");

            comboBoxInput.Items.Clear();
            comboBoxOutput.Items.Clear();
            comboBoxOutput2.Items.Clear();

            comboBoxInput.Items.AddRange([.. languages]);
            comboBoxOutput.Items.AddRange([.. languages]);
            comboBoxOutput2.Items.AddRange([.. languages]);

            comboBoxInput.SelectedIndex = 0;
            comboBoxOutput.SelectedIndex = 0;
            comboBoxOutput2.SelectedIndex = 0;

            comboBoxOutput2.Enabled = checkBoxEnable2.Checked;
            textBoxOutput2.Enabled = checkBoxEnable2.Checked;

            //textBoxInput.Text = string.Empty; //Preserve input text when changing the Translator?
            textBoxOutput.Text = string.Empty;
            textBoxOutput2.Text = string.Empty;

            foreach (var t in controlStates)
            {
                t.Item1.Enabled = t.Item2;
            }

            UseWaitCursor = false;

            Initialized = true;

            if (bError)
            {
                MessageBox.Show(
                    (!string.IsNullOrEmpty(sExtraError) ? (sExtraError + Environment.NewLine + Environment.NewLine) : string.Empty) +
                    $"Error initializing the translator with following parameters:" + Environment.NewLine + Environment.NewLine +
                    (_CurrentModel?.GetDescription() ?? "Missing model") + Environment.NewLine + Environment.NewLine +
                    $"Please change settings in File -> Configuration dialog.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void OncomboBoxModels_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!bInitializingComboBoxModels)
            {
                var currentModel = comboBoxModels.SelectedItem as string;
                SelectCurrentModel(currentModel);

                await InitializeTranslatorAsync();
            }
        }

        private async void OnbuttonTranslate_Click(object sender, EventArgs e)
        {
            await TranslateText();
        }

        private async void OncomboBoxOutput_SelectedIndexChanged(object sender, EventArgs e)
        {
            await TranslateUI();
            await TranslateText();
        }

        private async void OncheckBoxTranslateUI_CheckedChanged(object sender, EventArgs e)
        {
            await TranslateUI();
        }

        private async Task TranslateText()
        {
            if (Initialized && (Translator != null))
            {
                var timer = Stopwatch.StartNew();
                UseWaitCursor = true;
                string sourceLang = LanguagePrefixes[comboBoxInput.Text];
                string destLang = LanguagePrefixes[comboBoxOutput.Text];
                string destLang2 = LanguagePrefixes[comboBoxOutput2.Text];
                if (!string.IsNullOrWhiteSpace(textBoxInput.Text))
                {
                    try
                    {
                        if ((destLang == destLang2) || !checkBoxEnable2.Checked)
                        {
                            var res = await Translator.TranslateAsync(textBoxInput.Text, sourceLang, destLang);
                            textBoxOutput.Text = res;
                            textBoxOutput2.Text = checkBoxEnable2.Checked ? res : string.Empty;
                        }
                        else
                        {
                            var res = await Translator.MultiTranslateAsync([textBoxInput.Text], [sourceLang], [destLang, destLang2]);
                            textBoxOutput.Text = res[0];
                            textBoxOutput2.Text = res[1];
                        }
                    }
                    catch { }
                }
                else
                {
                    textBoxOutput.Text = textBoxInput.Text;
                    textBoxOutput2.Text = textBoxInput.Text;
                }
                UseWaitCursor = false;
                timer.Stop();
                if (timer.Elapsed.TotalSeconds > 0.001)
                {
                    labelSeconds.Text = timer.Elapsed.TotalSeconds.ToString() + " s";
                }
                else
                {
                    labelSeconds.Text = "";
                }
            }
        }

        private async Task TranslateUI()
        {
            if (Initialized && (Translator != null))
            {
                string destLang = LanguagePrefixes[comboBoxOutput.Text];
                if (checkBoxTranslateUI.Checked && !string.IsNullOrEmpty(destLang) && !string.IsNullOrEmpty(EnglishCodeForUI))
                {
                    try
                    {
                        var source = new string[] { _untranslatedEnglishTitle, _untranslatedEnglishCheckBox, _untranslatedEnglishButton, _untranslatedEnglishElapsedTime };
                        var results = await Translator.TranslateAsync(source, EnglishCodeForUI, destLang);
                        this.Text = results[0];
                        checkBoxTranslateUI.Text = results[1];
                        buttonTranslate.Text = results[2];
                        labelElapsedTime.Text = results[3];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    ResetUIEnglish();
                }
            }
            else
            {
                ResetUIEnglish();
            }
        }

        private void ResetUIEnglish()
        {
            this.Text = _untranslatedEnglishTitle;
            checkBoxTranslateUI.Text = _untranslatedEnglishCheckBox;
            buttonTranslate.Text = _untranslatedEnglishButton;
            labelElapsedTime.Text = _untranslatedEnglishElapsedTime;
        }

        private async void OncomboBoxOutput2_SelectedIndexChanged(object sender, EventArgs e)
        {
            await TranslateText();
        }

        private async void OncheckBoxEnable2_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxOutput2.Enabled = checkBoxEnable2.Checked;
            textBoxOutput2.Enabled = checkBoxEnable2.Checked;
            if (!textBoxOutput2.Enabled)
            {
                textBoxOutput2.Text = string.Empty;
            }
            else
            {
                await TranslateText();
            }
        }

        private void OnaboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutBox = new AboutBox1
            {
                StartPosition = FormStartPosition.CenterParent
            };
            aboutBox.ShowDialog(this);
        }

        private void OnexitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void OnconfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await ShowConfigurationForm();
        }

        private async Task ShowConfigurationForm()
        {
            var config = new ConfigurationForm
            {
                StartPosition = FormStartPosition.CenterParent
            };
            if (config.ShowDialog(this) == DialogResult.OK)
            {
                var currentModel = comboBoxModels.SelectedItem as string;
                InitComboBoxModels(currentModel);

                await InitializeTranslatorAsync();
            }
        }

        private void InitComboBoxModels(string? selectedModelName)
        {
            bInitializingComboBoxModels = true;
            comboBoxModels.SelectedItem = null; // This call triggers OncomboBoxModels_SelectedValueChanged event.
            comboBoxModels.Items.Clear();
            comboBoxModels.Items.AddRange([.. Settings.Default.ModelsContainer.Models.Where(m => m.IsValid()).Select(m => m.Name.Trim())]);
            SelectCurrentModel(selectedModelName);
            comboBoxModels.SelectedItem = _CurrentModel?.Name;  // This call triggers OncomboBoxModels_SelectedValueChanged event.
            bInitializingComboBoxModels = false;
        }

        private void SelectCurrentModel(string? modelName)
        {
            _CurrentModel = null;
            if (!string.IsNullOrEmpty(modelName))
            {
                _CurrentModel = Settings.Default.ModelsContainer.Models.FirstOrDefault(m => m.Name == modelName);
            }
            if ((_CurrentModel == null) && Settings.Default.ModelsContainer.Models.Count > 0)
            {
                _CurrentModel = Settings.Default.ModelsContainer.Models[0];
            }
            if (_CurrentModel != null)
            {
                Settings.Default.CurrentModel = _CurrentModel.Name;
                Settings.Default.Save();
            }
        }

        private void ReadInitialAppSettings()
        {
            _CurrentModel = null;
            try
            {
                string currentModel = Settings.Default.CurrentModel;
                InitComboBoxModels(currentModel);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading applications settings." + Environment.NewLine + Environment.NewLine +
                      ex.Message + Environment.NewLine + Environment.NewLine +
                      $"Please change settings in File -> Configuration dialog.",
                      "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static async Task<Tuple<List<string>, Dictionary<string, string>, string>> ReadLanguageDescriptionFile(string fileName)
        {
            //File format
            //language_prefix:__language__
            //english_code_for_ui:en
            //de
            //en
            //es
            //fr
            //it
            var languageLines = await File.ReadAllLinesAsync(fileName);
            languageLines = [.. languageLines.Where(x => !string.IsNullOrWhiteSpace(x))];
            if (languageLines.Length >= 2)
            {
                if (languageLines[0].StartsWith("language_prefix:", StringComparison.InvariantCultureIgnoreCase) &&
                    languageLines[1].StartsWith("english_code_for_ui:", StringComparison.InvariantCultureIgnoreCase))
                {
                    var language_prefix = languageLines[0]["language_prefix:".Length..].Trim();
                    var english_code_for_ui = languageLines[1]["english_code_for_ui:".Length..].Trim();

                    var languages = languageLines.Where((x, i) => i > 1).Select(x => x.Trim()).ToList();

                    var languagePrefixes = languages.Select(x => new KeyValuePair<string, string>(x, language_prefix.Replace("language", x, StringComparison.InvariantCultureIgnoreCase))).ToDictionary(p => p.Key, p => p.Value);

                    var englishCodeForUI = string.IsNullOrEmpty(english_code_for_ui) ?
                        string.Empty :
                        language_prefix.Replace("language", english_code_for_ui, StringComparison.InvariantCultureIgnoreCase);

                    return new Tuple<List<string>, Dictionary<string, string>, string>(languages, languagePrefixes, englishCodeForUI);
                }
            }

            throw new InvalidDataException($"Invalid content in {fileName}");
        }
    }
}