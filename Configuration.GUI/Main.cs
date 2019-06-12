using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TI.Configuration.Logic;
using System.Data.Entity;
using Configuration.SQL;

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
          
            await db.AppConfigs.Include(x => x.Settings).LoadAsync();
            sQLAppConfigBindingSource.DataSource = db.AppConfigs.Local;
        }

        private void sQLAppConfigBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var settings =  ((SQLAppConfig)sQLAppConfigBindingSource.Current).Settings;
            flowLayoutPanel1.Controls.Clear();

            foreach (var setting in settings)
            {
                flowLayoutPanel1.Controls.Add(new ConfigSettingsControl(setting));
            }
        }

      
        private async void sQLAppConfigBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            int count = await db.SaveChangesAsync();

            MessageBox.Show(count.ToString());
        }

        private async void button1_Click(object sender, EventArgs e)
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
                    e.NewObject = new SQL.SQLAppConfig(dlg.AppConfigName);
                else
                    e.NewObject = new SQL.SQLAppConfig("New Config"); ;
                
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

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }
    }
}
