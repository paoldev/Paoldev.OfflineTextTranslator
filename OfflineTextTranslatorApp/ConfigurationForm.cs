using OfflineTextTranslatorApp.Properties;

namespace OfflineTextTranslatorApp
{
    public partial class ConfigurationForm : Form
    {
        public ConfigurationForm()
        {
            InitializeComponent();

            Properties.Settings.Default.ModelsContainer ??= new();
            propertyGrid1.SelectedObject = Properties.Settings.Default.ModelsContainer;
            propertyGrid1.ExpandAllGridItems();
        }

        private void OnbuttonOk_Click(object sender, EventArgs e)
        {
            SaveSettings();

            Close();
        }

        private void OnbuttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.ModelsContainer = propertyGrid1.SelectedObject as ModelDescriptors;
            Properties.Settings.Default.Save();
        }
    }
}
