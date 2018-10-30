using System;
using System.Windows.Forms;
using TI.Configuration.Logic;
using TI.Configuration.Logic._internals.Configs;
using TI.Configuration.Logic.Abstracts;

namespace Sandbox
{
   

    public partial class Form1 : Form,INeedConfigUpdates{ 
        //TestConfig cfg = new TestConfig();
        public Form1()
        {
           // ConfigurationManager.Instance.AddWatcher<JimenaConfi>(this);

            InitializeComponent();
        }

       

        public void OnConfigurationUpdate<T>(T instance) where T : ConfigurationBase
        {
            JimenaConfi c = instance as JimenaConfi;
            
            var d = Controls[0] as JimenaConfigDisplay;
            if (d != null)
                d.LoadConfiguration(c);

            //MessageBox.Show(c.JimenasNewname);
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

            //Controls.Add(ConfigurationManager.Instance.GetMappedDisplay<JimenaConfi>());
            //Controls.Add(ConfigurationManager.Instance.GetMappedDisplay<MasterConfig>());
        }
    }
}
