using Configuration.SQL;
using System;
using System.Windows.Forms;
using TI.Configuration.Logic;
using TI.Configuration.Logic._internals.Configs;
using TI.Configuration.Logic.Abstracts;

namespace Sandbox
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var cfg = ConfigurationManager.Instance.Read<JimenaConfi>();
            ConfigurationManager.Instance.MapToDisplay<JimenaConfi, JimenaConfigDisplay>();
            var i = ConfigurationManagerSQL.Instance;
            var s = i.Get("L1FILLER");
            s.Add(new SQLAppConfigSetting("RUNNUMBERTAG", "L1_Run_Number", ConfigMode.Live));
            var cf = i.Read("L1FILLER");
            MessageBox.Show($"{i.Write(s)}");
            s.Mode = ConfigMode.Live;
           var x1 = s.Get("RUNNUMBERTAG");
            var x2= s.Get("RUNNUMBERTAG", ConfigMode.Default);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form2());
        }
    }
}
