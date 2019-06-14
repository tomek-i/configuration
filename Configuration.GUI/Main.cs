using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TI.Configuration.Logic;
using System.Data.Entity;
using Configuration.SQL;
using System.Threading.Tasks;

namespace Configuration.GUI
{
    public partial class Main : Form
    {
        List<SQLAppConfigSetting> currentSettings = new List<SQLAppConfigSetting>();
        static internal ConfigurationContext db = new WarburnEstateDatabaseFactory().Create();

        public Main()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {

            comboBox1.Items.AddRange(Enum.GetNames(typeof(ConfigMode)));
            comboBox2.Items.AddRange(Enum.GetNames(typeof(ConfigMode)));
            comboBox1.SelectedIndex = 0;

            await db.AppConfigs.Include(x => x.Settings).LoadAsync();
            sQLAppConfigBindingSource.DataSource = db.AppConfigs.Local;
        }

        private async void sQLAppConfigBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            
            flowLayoutPanel1.SuspendLayout();
            var currentCfg = (SQLAppConfig)sQLAppConfigBindingSource.Current;
            var settings = (currentCfg).Settings;
            textBoxCode.Text = currentCfg.Code;
            List<Control> controls = new List<Control>();
            foreach (var setting in settings)
            {
                var ctrl = new ConfigSettingsControl(setting);
                ctrl.VisibleIf(comboBox1.SelectedItem.ToString());
                controls.Add(ctrl);
            }
            
            await Task.Factory.StartNew(() =>
            {
                Action action = () =>
                {
                    flowLayoutPanel1.Controls.Clear();
                    flowLayoutPanel1.Controls.AddRange(controls.ToArray());
                };
                if (flowLayoutPanel1.InvokeRequired)
                {
                    flowLayoutPanel1.Invoke(action);

                }
                else
                    action.Invoke();
            });
            flowLayoutPanel1.ResumeLayout();

        }


        private async void sQLAppConfigBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            int count = await db.SaveChangesAsync();

            MessageBox.Show(count.ToString());
        }

        private async void buttonSaveAll_Click(object sender, EventArgs e)
        {
            foreach (ConfigSettingsControl control in flowLayoutPanel1.Controls)
            {
                control.UpdateChanges();
            }
            int count = await db.SaveChangesAsync();

            MessageBox.Show(count.ToString());
        }

        private void sQLAppConfigBindingSource_AddingNew(object sender, System.ComponentModel.AddingNewEventArgs e)
        {
            using (var dlg = new ConfigNameForm())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    e.NewObject = new SQL.SQLAppConfig(dlg.AppConfigName) { Code = dlg.AppConfigCode };
                else
                    throw new ArgumentException("need to fix this, there need to be a name + code");

            }

        }

        private void buttonAddNewSetting_Click(object sender, EventArgs e)
        {
            var current = ((SQL.SQLAppConfig)sQLAppConfigBindingSource.Current);
            var setting = new SQLAppConfigSetting($"Example Setting {current.Settings.Count}", "Test", ConfigMode.Default)
            {
                AppConfigId = current.Id,
                Config = current
            };
            current.Add(setting);
            flowLayoutPanel1.Controls.Add(new ConfigSettingsControl(setting));
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ConfigSettingsControl control in flowLayoutPanel1.Controls)
            {
                control.VisibleIf(comboBox1.SelectedItem.ToString());
            }
        }

        private void cloneConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var current = (SQLAppConfig)sQLAppConfigBindingSource.Current;
            using (var dlg = new ConfigNameForm())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var cloned = new SQL.SQLAppConfig(dlg.AppConfigName) { Code = dlg.AppConfigCode };
                    foreach (var setting in current.Settings)
                    {
                        cloned.Add(new SQL.SQLAppConfigSetting(setting.Name, setting.Value, setting.Mode) { Code = setting.Code,Description = setting.Description });
                    }
                    sQLAppConfigBindingSource.Add(cloned);
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonCloneToMode.Enabled = true;
        }

        private void buttonCloneToMode_Click(object sender, EventArgs e)
        {
            List<ConfigSettingsControl> clones = new List<ConfigSettingsControl>();
            foreach (ConfigSettingsControl control in flowLayoutPanel1.Controls)
            {
                var cloned = control.CloneControl();

                cloned.SetModeCombobox((ConfigMode)Enum.Parse(typeof(ConfigMode), (string)comboBox2.SelectedItem));
                cloned.UpdateChanges();
                ((SQL.SQLAppConfig)sQLAppConfigBindingSource.Current).Add(cloned.currentSetting);
                clones.Add(cloned);
            }
            flowLayoutPanel1.Controls.AddRange(clones.ToArray());
        }
        bool expand = false;
        private void buttonToggle_Click(object sender, EventArgs e)
        {
            foreach (ConfigSettingsControl control in flowLayoutPanel1.Controls)
            {
                control.PerformToggle(expand);
            }
            expand = !expand;
        }
    }
}
