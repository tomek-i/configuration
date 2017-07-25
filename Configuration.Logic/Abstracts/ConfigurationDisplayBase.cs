using System;
using System.ComponentModel;
using System.Windows.Forms;
using TI.Configuration.Logic.API;
using TI.Configuration.Logic.Abstracts;

namespace TI.Configuration.Logic
{
    /// <summary>
    /// Abstract Control which should be inherited from when createing a display 
    /// which represents the configuration
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ConfigurationDisplayBase<T>:UserControl where T:ConfigurationBase
    {
        private static IConfigurationManager configManager;

        protected T cfg { get; private set; }

        public static void Initialize(IConfigurationManager manager)
        {
            configManager = manager;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (configManager == null)
                configManager = ConfigurationManager.Instance;

            LoadConfiguration();
        }

        public virtual void SaveConfiguration()
        {
            configManager.Write(cfg);
        }
        public virtual void LoadConfiguration(T instance=null)
        {
            if(instance==null)
            {
                cfg = configManager.Read<T>();
            }
            else
            {
                cfg = instance;
            }
        }
       
    }
}
