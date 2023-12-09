namespace OfflineTextTranslatorApp
{
    partial class ConfigurationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            textBoxLanguageFile = new TextBox();
            buttonLanguageFile = new Button();
            openFileDialog1 = new OpenFileDialog();
            folderBrowserDialog1 = new FolderBrowserDialog();
            buttonTokenizerModelFile = new Button();
            textBoxTokenizerModelFile = new TextBox();
            label2 = new Label();
            buttonCTranslate2Directory = new Button();
            textBoxCTranslate2Directory = new TextBox();
            label3 = new Label();
            buttonOk = new Button();
            buttonCancel = new Button();
            comboBoxTokenizerModelFile = new ComboBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 41);
            label1.Name = "label1";
            label1.Size = new Size(117, 25);
            label1.TabIndex = 0;
            label1.Text = "Language file";
            // 
            // textBoxLanguageFile
            // 
            textBoxLanguageFile.Location = new Point(31, 85);
            textBoxLanguageFile.Name = "textBoxLanguageFile";
            textBoxLanguageFile.Size = new Size(970, 31);
            textBoxLanguageFile.TabIndex = 1;
            // 
            // buttonLanguageFile
            // 
            buttonLanguageFile.Location = new Point(1007, 83);
            buttonLanguageFile.Name = "buttonLanguageFile";
            buttonLanguageFile.Size = new Size(112, 34);
            buttonLanguageFile.TabIndex = 2;
            buttonLanguageFile.Text = "Select";
            buttonLanguageFile.UseVisualStyleBackColor = true;
            buttonLanguageFile.Click += OnbuttonLanguageFile_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonTokenizerModelFile
            // 
            buttonTokenizerModelFile.Location = new Point(1007, 204);
            buttonTokenizerModelFile.Name = "buttonTokenizerModelFile";
            buttonTokenizerModelFile.Size = new Size(112, 34);
            buttonTokenizerModelFile.TabIndex = 5;
            buttonTokenizerModelFile.Text = "Select";
            buttonTokenizerModelFile.UseVisualStyleBackColor = true;
            buttonTokenizerModelFile.Click += OnbuttonTokenizerModelFile_Click;
            // 
            // textBoxTokenizerModelFile
            // 
            textBoxTokenizerModelFile.Location = new Point(31, 206);
            textBoxTokenizerModelFile.Name = "textBoxTokenizerModelFile";
            textBoxTokenizerModelFile.Size = new Size(970, 31);
            textBoxTokenizerModelFile.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(31, 162);
            label2.Name = "label2";
            label2.Size = new Size(169, 25);
            label2.TabIndex = 3;
            label2.Text = "Tokenizer model file";
            // 
            // buttonCTranslate2Directory
            // 
            buttonCTranslate2Directory.Location = new Point(1007, 323);
            buttonCTranslate2Directory.Name = "buttonCTranslate2Directory";
            buttonCTranslate2Directory.Size = new Size(112, 34);
            buttonCTranslate2Directory.TabIndex = 8;
            buttonCTranslate2Directory.Text = "Select";
            buttonCTranslate2Directory.UseVisualStyleBackColor = true;
            buttonCTranslate2Directory.Click += OnbuttonCTranslate2Directory_Click;
            // 
            // textBoxCTranslate2Directory
            // 
            textBoxCTranslate2Directory.Location = new Point(31, 325);
            textBoxCTranslate2Directory.Name = "textBoxCTranslate2Directory";
            textBoxCTranslate2Directory.Size = new Size(970, 31);
            textBoxCTranslate2Directory.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(31, 281);
            label3.Name = "label3";
            label3.Size = new Size(232, 25);
            label3.TabIndex = 6;
            label3.Text = "CTranslate2 model directory";
            // 
            // buttonOk
            // 
            buttonOk.DialogResult = DialogResult.OK;
            buttonOk.Location = new Point(397, 436);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(112, 34);
            buttonOk.TabIndex = 9;
            buttonOk.Text = "Ok";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += OnbuttonOk_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Location = new Point(613, 436);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(112, 34);
            buttonCancel.TabIndex = 10;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += OnbuttonCancel_Click;
            // 
            // comboBoxTokenizerModelFile
            // 
            comboBoxTokenizerModelFile.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTokenizerModelFile.FormattingEnabled = true;
            comboBoxTokenizerModelFile.Location = new Point(265, 159);
            comboBoxTokenizerModelFile.Name = "comboBoxTokenizerModelFile";
            comboBoxTokenizerModelFile.Size = new Size(253, 33);
            comboBoxTokenizerModelFile.TabIndex = 11;
            // 
            // ConfigurationForm
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(1131, 503);
            Controls.Add(comboBoxTokenizerModelFile);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOk);
            Controls.Add(buttonCTranslate2Directory);
            Controls.Add(textBoxCTranslate2Directory);
            Controls.Add(label3);
            Controls.Add(buttonTokenizerModelFile);
            Controls.Add(textBoxTokenizerModelFile);
            Controls.Add(label2);
            Controls.Add(buttonLanguageFile);
            Controls.Add(textBoxLanguageFile);
            Controls.Add(label1);
            Name = "ConfigurationForm";
            Text = "Configuration";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBoxLanguageFile;
        private Button buttonLanguageFile;
        private OpenFileDialog openFileDialog1;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button buttonTokenizerModelFile;
        private TextBox textBoxTokenizerModelFile;
        private Label label2;
        private Button buttonCTranslate2Directory;
        private TextBox textBoxCTranslate2Directory;
        private Label label3;
        private Button buttonOk;
        private Button buttonCancel;
        private ComboBox comboBoxTokenizerModelFile;
    }
}