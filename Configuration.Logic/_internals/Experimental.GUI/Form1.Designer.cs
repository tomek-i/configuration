using System.ComponentModel;
using TI.Configuration.Logic.API;
using TI.Configuration.Logic._internals.Configs;

namespace TI.Configuration.Logic._internals.Experimental.GUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            MasterConfig masterConfig1 = new MasterConfig();
            this.configurationPropertyControl1 = new ConfigurationPropertyControl();
            this.SuspendLayout();
            // 
            // configurationPropertyControl1
            // 
            masterConfig1.ModeName = "debug";
            this.configurationPropertyControl1.Configuration = masterConfig1;
            this.configurationPropertyControl1.Location = new System.Drawing.Point(94, 108);
            this.configurationPropertyControl1.Name = "configurationPropertyControl1";
            this.configurationPropertyControl1.Size = new System.Drawing.Size(268, 256);
            this.configurationPropertyControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 405);
            this.Controls.Add(this.configurationPropertyControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ConfigurationPropertyControl configurationPropertyControl1;
    }
}