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
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form2());
        }
    }
}
