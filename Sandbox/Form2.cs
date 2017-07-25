using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TI.Configuration.Logic;
using TI.Configuration.Logic._internals.Configs;
using TI.Configuration.Logic.Abstracts;

namespace Sandbox
{
    public partial class Form2 : Form,INeedConfigUpdates
    {
        JimenaConfi cfg;
        public Form2()
        {
            InitializeComponent();

            ConfigurationManager.Instance.AddWatcher<JimenaConfi>(this);
            cfg = ConfigurationManager.Instance.Read<JimenaConfi>();

        }

        public void OnConfigurationUpdate<T>(T instance) where T : ConfigurationBase
        {
            cfg = instance as JimenaConfi;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var dlg = new Form1())
            {
                dlg.ShowDialog();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine($"{DateTime.Now.ToShortTimeString()} {cfg?.JimenasNewname}");
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
