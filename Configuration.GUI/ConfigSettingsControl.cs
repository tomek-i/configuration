using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TI.Configuration.Logic;

namespace Configuration.GUI
{
    public partial class ConfigSettingsControl : UserControl
    {
        private bool isToggled = false;
        private Size original;
        SQL.SQLAppConfigSetting currentSetting;
        public ConfigSettingsControl()
        {
            InitializeComponent();
            original = Size;
            modeComboBox.Items.AddRange(Enum.GetNames(typeof(ConfigMode)));

        }
        public ConfigSettingsControl(SQL.SQLAppConfigSetting setting) : this()
        {
            currentSetting = setting;
            assignToGui();
        }

        private void assignToGui()
        {
            nameTextBox.Text = currentSetting.Name;
            codeTextBox.Text = currentSetting.Code;
            modeComboBox.SelectedItem = currentSetting.Mode.ToString();
            valueTextBox.Text = currentSetting.Value;
            textBoxDescription.Text = currentSetting.Description;
        }

        public void UpdateChanges()
        {
            currentSetting.Code = codeTextBox.Text.ToUpper().Replace(" ",string.Empty);
            currentSetting.Mode = (ConfigMode)Enum.Parse(typeof(ConfigMode), (string)modeComboBox.SelectedItem);
            currentSetting.Name = nameTextBox.Text;
            currentSetting.Value = valueTextBox.Text;
            currentSetting.Description = textBoxDescription.Text;
           //MessageBox.Show(Main.db.SaveChanges().ToString());
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

            //currentSetting.Config = null;
            Main.db.AppConfigSettings.Remove(currentSetting);
            this.Parent.Controls.Remove(this);
        }

        private void buttonToggle_Click(object sender, EventArgs e)
        {
            if(!isToggled)
            {
                //make small
                Size = new Size(Size.Width, 130);
                isToggled = true;
            }
            else
            {
                //make big
                Size = original;
                isToggled = false;
            }
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            codeTextBox.Text = nameTextBox.Text.ToUpper().Replace(" ", string.Empty);
        }

        private void codeLabel_Click(object sender, EventArgs e)
        {

        }

        private void codeTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
