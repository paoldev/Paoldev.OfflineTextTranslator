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
            buttonOk = new Button();
            buttonCancel = new Button();
            propertyGrid1 = new PropertyGrid();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // buttonOk
            // 
            buttonOk.Anchor = AnchorStyles.None;
            buttonOk.DialogResult = DialogResult.OK;
            buttonOk.Location = new Point(113, 461);
            buttonOk.Margin = new Padding(2);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(96, 26);
            buttonOk.TabIndex = 9;
            buttonOk.Text = "Ok";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += OnbuttonOk_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.None;
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Location = new Point(435, 461);
            buttonCancel.Margin = new Padding(2);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(96, 26);
            buttonCancel.TabIndex = 10;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += OnbuttonCancel_Click;
            // 
            // propertyGrid1
            // 
            propertyGrid1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            propertyGrid1.BackColor = SystemColors.Control;
            tableLayoutPanel1.SetColumnSpan(propertyGrid1, 2);
            propertyGrid1.HelpVisible = false;
            propertyGrid1.Location = new Point(3, 3);
            propertyGrid1.Name = "propertyGrid1";
            propertyGrid1.PropertySort = PropertySort.NoSort;
            propertyGrid1.Size = new Size(639, 453);
            propertyGrid1.TabIndex = 17;
            propertyGrid1.ToolbarVisible = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(buttonOk, 0, 1);
            tableLayoutPanel1.Controls.Add(propertyGrid1, 0, 0);
            tableLayoutPanel1.Controls.Add(buttonCancel, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(645, 489);
            tableLayoutPanel1.TabIndex = 18;
            // 
            // ConfigurationForm
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(645, 489);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(2);
            MinimumSize = new Size(300, 200);
            Name = "ConfigurationForm";
            Text = "Configuration";
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button buttonOk;
        private Button buttonCancel;
        private PropertyGrid propertyGrid1;
        private TableLayoutPanel tableLayoutPanel1;
    }
}