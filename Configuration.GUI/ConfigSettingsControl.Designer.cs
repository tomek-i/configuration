namespace Configuration.GUI
{
    partial class ConfigSettingsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label codeLabel;
            System.Windows.Forms.Label modeLabel;
            System.Windows.Forms.Label nameLabel;
            System.Windows.Forms.Label valueLabel;
            System.Windows.Forms.Label label1;
            this.codeTextBox = new System.Windows.Forms.TextBox();
            this.modeComboBox = new System.Windows.Forms.ComboBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.valueTextBox = new System.Windows.Forms.TextBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.buttonToggle = new System.Windows.Forms.Button();
            codeLabel = new System.Windows.Forms.Label();
            modeLabel = new System.Windows.Forms.Label();
            nameLabel = new System.Windows.Forms.Label();
            valueLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // codeLabel
            // 
            codeLabel.AutoSize = true;
            codeLabel.Location = new System.Drawing.Point(14, 48);
            codeLabel.Name = "codeLabel";
            codeLabel.Size = new System.Drawing.Size(35, 13);
            codeLabel.TabIndex = 0;
            codeLabel.Text = "Code:";
            codeLabel.Click += new System.EventHandler(this.codeLabel_Click);
            // 
            // modeLabel
            // 
            modeLabel.AutoSize = true;
            modeLabel.Location = new System.Drawing.Point(14, 74);
            modeLabel.Name = "modeLabel";
            modeLabel.Size = new System.Drawing.Size(37, 13);
            modeLabel.TabIndex = 2;
            modeLabel.Text = "Mode:";
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new System.Drawing.Point(14, 22);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new System.Drawing.Size(38, 13);
            nameLabel.TabIndex = 4;
            nameLabel.Text = "Name:";
            // 
            // valueLabel
            // 
            valueLabel.AutoSize = true;
            valueLabel.Location = new System.Drawing.Point(14, 101);
            valueLabel.Name = "valueLabel";
            valueLabel.Size = new System.Drawing.Size(37, 13);
            valueLabel.TabIndex = 6;
            valueLabel.Text = "Value:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(14, 125);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(63, 13);
            label1.TabIndex = 8;
            label1.Text = "Description:";
            // 
            // codeTextBox
            // 
            this.codeTextBox.Location = new System.Drawing.Point(94, 45);
            this.codeTextBox.Name = "codeTextBox";
            this.codeTextBox.Size = new System.Drawing.Size(121, 20);
            this.codeTextBox.TabIndex = 1;
            this.codeTextBox.TextChanged += new System.EventHandler(this.codeTextBox_TextChanged);
            // 
            // modeComboBox
            // 
            this.modeComboBox.FormattingEnabled = true;
            this.modeComboBox.Location = new System.Drawing.Point(94, 71);
            this.modeComboBox.Name = "modeComboBox";
            this.modeComboBox.Size = new System.Drawing.Size(121, 21);
            this.modeComboBox.TabIndex = 3;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(94, 19);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(121, 20);
            this.nameTextBox.TabIndex = 5;
            this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
            // 
            // valueTextBox
            // 
            this.valueTextBox.Location = new System.Drawing.Point(94, 98);
            this.valueTextBox.Name = "valueTextBox";
            this.valueTextBox.Size = new System.Drawing.Size(121, 20);
            this.valueTextBox.TabIndex = 7;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonDelete.Location = new System.Drawing.Point(0, 230);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(232, 40);
            this.buttonDelete.TabIndex = 10;
            this.buttonDelete.Text = "DELETE";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(17, 141);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(198, 83);
            this.textBoxDescription.TabIndex = 9;
            // 
            // buttonToggle
            // 
            this.buttonToggle.Location = new System.Drawing.Point(3, 0);
            this.buttonToggle.Name = "buttonToggle";
            this.buttonToggle.Size = new System.Drawing.Size(228, 13);
            this.buttonToggle.TabIndex = 11;
            this.buttonToggle.UseVisualStyleBackColor = true;
            this.buttonToggle.Click += new System.EventHandler(this.buttonToggle_Click);
            // 
            // ConfigSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonToggle);
            this.Controls.Add(label1);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(codeLabel);
            this.Controls.Add(this.codeTextBox);
            this.Controls.Add(modeLabel);
            this.Controls.Add(this.modeComboBox);
            this.Controls.Add(nameLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(valueLabel);
            this.Controls.Add(this.valueTextBox);
            this.Name = "ConfigSettingsControl";
            this.Size = new System.Drawing.Size(232, 270);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox codeTextBox;
        private System.Windows.Forms.ComboBox modeComboBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox valueTextBox;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Button buttonToggle;
    }
}
