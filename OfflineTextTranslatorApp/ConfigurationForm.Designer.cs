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
            label4 = new Label();
            comboBoxDeviceType = new ComboBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 25);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(78, 15);
            label1.TabIndex = 0;
            label1.Text = "Language file";
            // 
            // textBoxLanguageFile
            // 
            textBoxLanguageFile.Location = new Point(22, 51);
            textBoxLanguageFile.Margin = new Padding(2, 2, 2, 2);
            textBoxLanguageFile.Name = "textBoxLanguageFile";
            textBoxLanguageFile.Size = new Size(680, 23);
            textBoxLanguageFile.TabIndex = 1;
            // 
            // buttonLanguageFile
            // 
            buttonLanguageFile.Location = new Point(705, 50);
            buttonLanguageFile.Margin = new Padding(2, 2, 2, 2);
            buttonLanguageFile.Name = "buttonLanguageFile";
            buttonLanguageFile.Size = new Size(78, 24);
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
            buttonTokenizerModelFile.Location = new Point(705, 154);
            buttonTokenizerModelFile.Margin = new Padding(2, 2, 2, 2);
            buttonTokenizerModelFile.Name = "buttonTokenizerModelFile";
            buttonTokenizerModelFile.Size = new Size(78, 24);
            buttonTokenizerModelFile.TabIndex = 5;
            buttonTokenizerModelFile.Text = "Select";
            buttonTokenizerModelFile.UseVisualStyleBackColor = true;
            buttonTokenizerModelFile.Click += OnbuttonTokenizerModelFile_Click;
            // 
            // textBoxTokenizerModelFile
            // 
            textBoxTokenizerModelFile.Location = new Point(20, 155);
            textBoxTokenizerModelFile.Margin = new Padding(2, 2, 2, 2);
            textBoxTokenizerModelFile.Name = "textBoxTokenizerModelFile";
            textBoxTokenizerModelFile.Size = new Size(680, 23);
            textBoxTokenizerModelFile.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 129);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(112, 15);
            label2.TabIndex = 3;
            label2.Text = "Tokenizer model file";
            // 
            // buttonCTranslate2Directory
            // 
            buttonCTranslate2Directory.Location = new Point(705, 226);
            buttonCTranslate2Directory.Margin = new Padding(2, 2, 2, 2);
            buttonCTranslate2Directory.Name = "buttonCTranslate2Directory";
            buttonCTranslate2Directory.Size = new Size(78, 24);
            buttonCTranslate2Directory.TabIndex = 8;
            buttonCTranslate2Directory.Text = "Select";
            buttonCTranslate2Directory.UseVisualStyleBackColor = true;
            buttonCTranslate2Directory.Click += OnbuttonCTranslate2Directory_Click;
            // 
            // textBoxCTranslate2Directory
            // 
            textBoxCTranslate2Directory.Location = new Point(22, 227);
            textBoxCTranslate2Directory.Margin = new Padding(2, 2, 2, 2);
            textBoxCTranslate2Directory.Name = "textBoxCTranslate2Directory";
            textBoxCTranslate2Directory.Size = new Size(680, 23);
            textBoxCTranslate2Directory.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 200);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(154, 15);
            label3.TabIndex = 6;
            label3.Text = "CTranslate2 model directory";
            // 
            // buttonOk
            // 
            buttonOk.DialogResult = DialogResult.OK;
            buttonOk.Location = new Point(276, 293);
            buttonOk.Margin = new Padding(2, 2, 2, 2);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(78, 25);
            buttonOk.TabIndex = 9;
            buttonOk.Text = "Ok";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += OnbuttonOk_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Location = new Point(428, 293);
            buttonCancel.Margin = new Padding(2, 2, 2, 2);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(78, 25);
            buttonCancel.TabIndex = 10;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += OnbuttonCancel_Click;
            // 
            // comboBoxTokenizerModelFile
            // 
            comboBoxTokenizerModelFile.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTokenizerModelFile.FormattingEnabled = true;
            comboBoxTokenizerModelFile.Location = new Point(184, 127);
            comboBoxTokenizerModelFile.Margin = new Padding(2, 2, 2, 2);
            comboBoxTokenizerModelFile.Name = "comboBoxTokenizerModelFile";
            comboBoxTokenizerModelFile.Size = new Size(178, 23);
            comboBoxTokenizerModelFile.TabIndex = 11;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(22, 85);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(68, 15);
            label4.TabIndex = 12;
            label4.Text = "Device type";
            // 
            // comboBoxDeviceType
            // 
            comboBoxDeviceType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxDeviceType.FormattingEnabled = true;
            comboBoxDeviceType.Location = new Point(184, 83);
            comboBoxDeviceType.Margin = new Padding(2, 2, 2, 2);
            comboBoxDeviceType.Name = "comboBoxDeviceType";
            comboBoxDeviceType.Size = new Size(178, 23);
            comboBoxDeviceType.TabIndex = 13;
            // 
            // ConfigurationForm
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(792, 329);
            Controls.Add(comboBoxDeviceType);
            Controls.Add(label4);
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
            Margin = new Padding(2, 2, 2, 2);
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
        private Label label4;
        private ComboBox comboBoxDeviceType;
    }
}