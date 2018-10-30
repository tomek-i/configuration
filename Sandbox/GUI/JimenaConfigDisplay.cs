using System;

using TI.Configuration.Logic._internals.Configs;

namespace TI.Configuration.Logic
{
    public partial class JimenaConfigDisplay : JimenaConfiConfigurationDisplayBase
    {

        public JimenaConfigDisplay()
        {
            InitializeComponent();
        }

        //TODO: instead of overwriting this function the base control should have a function which associates the values to the display controls
        public override void LoadConfiguration(JimenaConfi instance = null)
        {
            base.LoadConfiguration(instance);
            
            this.label1.Text = cfg.Name;
        }

        //TODO: this should also be separated to assign the display control values back to the config fro mthe actual saving/writing part
        private void button1_Click(object sender, EventArgs e)
        {
            cfg.JimenasNewname = textBox1.Text;

            ConfigurationManager.Instance.Save(cfg);
        }
    }
}
