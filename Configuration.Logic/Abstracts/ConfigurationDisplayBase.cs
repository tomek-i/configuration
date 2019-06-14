using System;
using System.ComponentModel;
using System.Windows.Forms;
using TI.Configuration.Logic.Interfaces;
using TI.Configuration.Logic.Abstracts;

namespace TI.Configuration.Logic
{
    /// <summary>
    /// Abstract Control which should be inherited from when createing a display 
    /// which represents the configuration
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ConfigurationDisplayBase<T>:UserControl where T:class,IConfiguration
    {
        //private static ConfigurationManager configManager;

        protected T cfg { get; private set; }

        //public static void Initialize(ConfigurationManager manager)
        //{
       //     configManager = manager;
        //}

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadConfiguration();
        }

        public virtual void SaveConfiguration()
        {
          //  configManager.Save(cfg);
        }
        public virtual void LoadConfiguration(T instance=null)
        {
            if(instance==null)
            {
               // cfg = (T)configManager.Load<T>(instance.Name);
            }
            else
            {
                cfg = instance;
            }
        }
       
    }
}
