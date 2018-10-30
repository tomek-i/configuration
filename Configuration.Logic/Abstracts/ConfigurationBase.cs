using System;
using System.Diagnostics;
using TI.Configuration.Logic._internals.Configs;
using TI.Configuration.Logic.API;

namespace TI.Configuration.Logic.Abstracts
{
    [Serializable]
    [DebuggerDisplay("{Name}")]
    public abstract class ConfigurationBase : PropertyChangedNotificationClass, IConfiguration
    {
        public string Name { get; protected set; }
        public DateTime Created { get; }

        protected ConfigurationBase(string name)
        {
            if(name == null)
                Name=GetType().Name;
            else
             Name = name;

            Created = DateTime.Now;
        }

        protected ConfigurationBase():this(null)
        {
          
        }

        public abstract IConfiguration Default();

        protected override void OnPropertyChanged(string propertyName, Action onChanged = null)
        {
            base.OnPropertyChanged(propertyName, onChanged);
        }
    }
}
