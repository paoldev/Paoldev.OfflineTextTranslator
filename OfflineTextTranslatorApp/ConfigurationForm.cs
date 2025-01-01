using Paoldev.OfflineTextTranslator;
using System.ComponentModel;

namespace OfflineTextTranslatorApp
{
    public partial class ConfigurationForm : Form
    {
        public ConfigurationForm()
        {
            InitializeComponent();

            comboBoxTokenizerModelFile.DataSource = Enum.GetValues<TokenizerType>();
            comboBoxTokenizerModelFile.SelectedItem = TokenizerType.None;

            comboBoxDeviceType.DataSource = Enum.GetValues<DeviceType>();
            comboBoxDeviceType.SelectedItem = DeviceType.Auto;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string LanguageFile { get => textBoxLanguageFile.Text; set => textBoxLanguageFile.Text = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DeviceType Device { get => (DeviceType)(comboBoxDeviceType.SelectedItem ?? DeviceType.Auto); set => comboBoxDeviceType.SelectedItem = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string TokenizerModelFile { get => textBoxTokenizerModelFile.Text; set => textBoxTokenizerModelFile.Text = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TokenizerType TokenizerModelFileType { get => (TokenizerType)(comboBoxTokenizerModelFile.SelectedItem ?? TokenizerType.None); set => comboBoxTokenizerModelFile.SelectedItem = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CTranslate2Directory { get => textBoxCTranslate2Directory.Text; set => textBoxCTranslate2Directory.Text = value; }

        private void OnbuttonLanguageFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.FileName = textBoxLanguageFile.Text;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxLanguageFile.Text = openFileDialog1.FileName;
            }
        }

        private void OnbuttonTokenizerModelFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.FileName = textBoxTokenizerModelFile.Text;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxTokenizerModelFile.Text = openFileDialog1.FileName;
            }
        }

        private void OnbuttonCTranslate2Directory_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.InitialDirectory = textBoxCTranslate2Directory.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxCTranslate2Directory.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void OnbuttonOk_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.LanguageDescriptionFile = LanguageFile;
            Properties.Settings.Default.Device = Device;
            Properties.Settings.Default.TokenizerModelFilePath = TokenizerModelFile;
            Properties.Settings.Default.CTranslate2ModelDirectory = CTranslate2Directory;
            Properties.Settings.Default.TokenizerType = TokenizerModelFileType;
            Properties.Settings.Default.Save();

            Close();
        }

        private void OnbuttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
