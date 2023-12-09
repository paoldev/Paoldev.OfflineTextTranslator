namespace OfflineTextTranslatorApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBoxInput = new TextBox();
            textBoxOutput = new TextBox();
            comboBoxInput = new ComboBox();
            comboBoxOutput = new ComboBox();
            buttonTranslate = new Button();
            checkBoxTranslateUI = new CheckBox();
            comboBoxOutput2 = new ComboBox();
            textBoxOutput2 = new TextBox();
            checkBoxEnable2 = new CheckBox();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            configToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            labelElapsedTime = new Label();
            labelSeconds = new Label();
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxInput
            // 
            textBoxInput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxInput.Location = new Point(3, 41);
            textBoxInput.Multiline = true;
            textBoxInput.Name = "textBoxInput";
            textBoxInput.ScrollBars = ScrollBars.Vertical;
            textBoxInput.Size = new Size(848, 813);
            textBoxInput.TabIndex = 0;
            // 
            // textBoxOutput
            // 
            textBoxOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxOutput.Location = new Point(3, 42);
            textBoxOutput.Multiline = true;
            textBoxOutput.Name = "textBoxOutput";
            textBoxOutput.ReadOnly = true;
            textBoxOutput.ScrollBars = ScrollBars.Vertical;
            textBoxOutput.Size = new Size(840, 383);
            textBoxOutput.TabIndex = 1;
            // 
            // comboBoxInput
            // 
            comboBoxInput.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxInput.FormattingEnabled = true;
            comboBoxInput.Location = new Point(3, 3);
            comboBoxInput.Name = "comboBoxInput";
            comboBoxInput.Size = new Size(182, 33);
            comboBoxInput.TabIndex = 2;
            // 
            // comboBoxOutput
            // 
            comboBoxOutput.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxOutput.FormattingEnabled = true;
            comboBoxOutput.Location = new Point(3, 3);
            comboBoxOutput.Name = "comboBoxOutput";
            comboBoxOutput.Size = new Size(182, 33);
            comboBoxOutput.TabIndex = 3;
            comboBoxOutput.SelectedIndexChanged += OncomboBoxOutput_SelectedIndexChanged;
            // 
            // buttonTranslate
            // 
            buttonTranslate.Location = new Point(237, 1);
            buttonTranslate.Name = "buttonTranslate";
            buttonTranslate.Size = new Size(112, 34);
            buttonTranslate.TabIndex = 4;
            buttonTranslate.Text = "Translate";
            buttonTranslate.UseVisualStyleBackColor = true;
            buttonTranslate.Click += OnbuttonTranslate_Click;
            // 
            // checkBoxTranslateUI
            // 
            checkBoxTranslateUI.AutoSize = true;
            checkBoxTranslateUI.Location = new Point(396, 5);
            checkBoxTranslateUI.Name = "checkBoxTranslateUI";
            checkBoxTranslateUI.Size = new Size(128, 29);
            checkBoxTranslateUI.TabIndex = 6;
            checkBoxTranslateUI.Text = "Translate UI";
            checkBoxTranslateUI.UseVisualStyleBackColor = true;
            checkBoxTranslateUI.CheckedChanged += OncheckBoxTranslateUI_CheckedChanged;
            // 
            // comboBoxOutput2
            // 
            comboBoxOutput2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxOutput2.Enabled = false;
            comboBoxOutput2.FormattingEnabled = true;
            comboBoxOutput2.Location = new Point(0, 3);
            comboBoxOutput2.Name = "comboBoxOutput2";
            comboBoxOutput2.Size = new Size(182, 33);
            comboBoxOutput2.TabIndex = 8;
            comboBoxOutput2.SelectedIndexChanged += OncomboBoxOutput2_SelectedIndexChanged;
            // 
            // textBoxOutput2
            // 
            textBoxOutput2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxOutput2.Enabled = false;
            textBoxOutput2.Location = new Point(3, 42);
            textBoxOutput2.Multiline = true;
            textBoxOutput2.Name = "textBoxOutput2";
            textBoxOutput2.ReadOnly = true;
            textBoxOutput2.ScrollBars = ScrollBars.Vertical;
            textBoxOutput2.Size = new Size(840, 377);
            textBoxOutput2.TabIndex = 7;
            // 
            // checkBoxEnable2
            // 
            checkBoxEnable2.AutoSize = true;
            checkBoxEnable2.Location = new Point(233, 5);
            checkBoxEnable2.Name = "checkBoxEnable2";
            checkBoxEnable2.Size = new Size(90, 29);
            checkBoxEnable2.TabIndex = 9;
            checkBoxEnable2.Text = "Enable";
            checkBoxEnable2.UseVisualStyleBackColor = true;
            checkBoxEnable2.CheckedChanged += OncheckBoxEnable2_CheckedChanged;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1700, 33);
            menuStrip1.TabIndex = 10;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { configToolStripMenuItem, toolStripSeparator2, aboutToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(54, 29);
            fileToolStripMenuItem.Text = "File";
            // 
            // configToolStripMenuItem
            // 
            configToolStripMenuItem.Name = "configToolStripMenuItem";
            configToolStripMenuItem.Size = new Size(223, 34);
            configToolStripMenuItem.Text = "Configuration";
            configToolStripMenuItem.Click += OnconfigToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(220, 6);
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(223, 34);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += OnaboutToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(220, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(223, 34);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += OnexitToolStripMenuItem_Click;
            // 
            // labelElapsedTime
            // 
            labelElapsedTime.AutoSize = true;
            labelElapsedTime.Location = new Point(233, 6);
            labelElapsedTime.Name = "labelElapsedTime";
            labelElapsedTime.Size = new Size(113, 25);
            labelElapsedTime.TabIndex = 11;
            labelElapsedTime.Text = "Elapsed time";
            // 
            // labelSeconds
            // 
            labelSeconds.AutoSize = true;
            labelSeconds.Location = new Point(404, 6);
            labelSeconds.Name = "labelSeconds";
            labelSeconds.Size = new Size(0, 25);
            labelSeconds.TabIndex = 12;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 33);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(comboBoxInput);
            splitContainer1.Panel1.Controls.Add(textBoxInput);
            splitContainer1.Panel1.Controls.Add(checkBoxTranslateUI);
            splitContainer1.Panel1.Controls.Add(buttonTranslate);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(1700, 854);
            splitContainer1.SplitterDistance = 850;
            splitContainer1.TabIndex = 13;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(comboBoxOutput);
            splitContainer2.Panel1.Controls.Add(labelSeconds);
            splitContainer2.Panel1.Controls.Add(labelElapsedTime);
            splitContainer2.Panel1.Controls.Add(textBoxOutput);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(comboBoxOutput2);
            splitContainer2.Panel2.Controls.Add(textBoxOutput2);
            splitContainer2.Panel2.Controls.Add(checkBoxEnable2);
            splitContainer2.Size = new Size(846, 854);
            splitContainer2.SplitterDistance = 428;
            splitContainer2.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1700, 887);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            Name = "Form1";
            Text = "Offline Text Translator";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxInput;
        private TextBox textBoxOutput;
        private ComboBox comboBoxInput;
        private ComboBox comboBoxOutput;
        private Button buttonTranslate;
        private CheckBox checkBoxTranslateUI;
        private ComboBox comboBoxOutput2;
        private TextBox textBoxOutput2;
        private CheckBox checkBoxEnable2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private Label labelElapsedTime;
        private Label labelSeconds;
        private ToolStripMenuItem configToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
    }
}