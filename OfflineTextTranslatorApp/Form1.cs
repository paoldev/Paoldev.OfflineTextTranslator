using System.Diagnostics;

namespace OfflineTextTranslatorApp
{
    public partial class Form1 : Form
    {
        private Paoldev.OfflineTextTranslator.ITranslator? Translator = null;
        private string EnglishCodeForUI = "en";
        private Dictionary<string, string> LanguagePrefixes = new();
        private bool Initialized = false;

        private readonly string _untranslatedEnglishTitle = "Offline Text Translator";
        private readonly string _untranslatedEnglishCheckBox = "Translate UI";
        private readonly string _untranslatedEnglishButton = "Translate";
        private readonly string _untranslatedEnglishElapsedTime = "Elapsed time";

        private string _LanguageFilePath = string.Empty;
        private string _TokenizerModelFilePath = string.Empty;
        private string _CTranslate2ModelDirectoryPath = string.Empty;
        private Paoldev.OfflineTextTranslator.TokenizerType _TokenizerModelFileType = Paoldev.OfflineTextTranslator.TokenizerType.None;
        public Form1()
        {
            InitializeComponent();

            ReadInitialAppSettings();
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (IsDefaultConfiguration())
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

            List<Tuple<Control, bool>> controlStates = new();
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

            LanguagePrefixes = new();

            //Configuration error condition: there is no need to try to initialize the Translator in this case.
            string sExtraError = string.Empty;
            bool bError = IsDefaultConfiguration() ||
                //(string.IsNullOrEmpty(_TokenizerModelFilePath) && _TokenizerModelFileType != Paoldev.OfflineTextTranslator.TokenizerType.None) ||  //This condition is valid
                string.IsNullOrEmpty(_CTranslate2ModelDirectoryPath) ||
                string.IsNullOrEmpty(_LanguageFilePath);

            if (!bError)
            {
                var options = new Paoldev.OfflineTextTranslator.TranslatorOptions
                {
                    TokenizerModelType = _TokenizerModelFileType,
                    TokenizerModelFilePath = _TokenizerModelFilePath,
                    CTranslate2ModelDirectoryPath = _CTranslate2ModelDirectoryPath
                };

                try
                {
                    Translator = await Paoldev.OfflineTextTranslator.Translator.CreateAsync(options);

                    var languageDescription = await ReadLanguageDescriptionFile(_LanguageFilePath);

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

            comboBoxInput.Items.AddRange(languages.ToArray());
            comboBoxOutput.Items.AddRange(languages.ToArray());
            comboBoxOutput2.Items.AddRange(languages.ToArray());

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
                    $"Language Description File: {_LanguageFilePath}" + Environment.NewLine + Environment.NewLine +
                    $"Tokenizer Model Type: {_TokenizerModelFileType}" + Environment.NewLine + Environment.NewLine +
                    $"Tokenizer Model File: {_TokenizerModelFilePath}" + Environment.NewLine + Environment.NewLine +
                    $"CTranslate2 Model Directory: {_CTranslate2ModelDirectoryPath}" + Environment.NewLine + Environment.NewLine +
                    $"Please change settings in File -> Configuration dialog.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            var res = await Translator.MultiTranslateAsync(new string[] { textBoxInput.Text }, new string[] { sourceLang }, new string[] { destLang, destLang2 });
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
                StartPosition = FormStartPosition.CenterParent,
                LanguageFile = _LanguageFilePath,
                TokenizerModelFile = _TokenizerModelFilePath,
                TokenizerModelFileType = _TokenizerModelFileType,
                CTranslate2Directory = _CTranslate2ModelDirectoryPath
            };
            if (config.ShowDialog(this) == DialogResult.OK)
            {
                _LanguageFilePath = config.LanguageFile;
                _TokenizerModelFilePath = config.TokenizerModelFile;
                _TokenizerModelFileType = config.TokenizerModelFileType;
                _CTranslate2ModelDirectoryPath = config.CTranslate2Directory;

                await InitializeTranslatorAsync();
            }
        }

        private bool IsDefaultConfiguration()
        {
            return
                string.IsNullOrEmpty(_LanguageFilePath) &&
                string.IsNullOrEmpty(_TokenizerModelFilePath) &&
                (_TokenizerModelFileType == Paoldev.OfflineTextTranslator.TokenizerType.None) &&
                string.IsNullOrEmpty(_CTranslate2ModelDirectoryPath);
        }

        private void ReadInitialAppSettings()
        {
            try
            {
                _LanguageFilePath = Properties.Settings.Default.LanguageDescriptionFile;
                _TokenizerModelFilePath = Properties.Settings.Default.TokenizerModelFilePath;
                _CTranslate2ModelDirectoryPath = Properties.Settings.Default.CTranslate2ModelDirectory;
                _TokenizerModelFileType = Properties.Settings.Default.TokenizerType;
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
            languageLines = languageLines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
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