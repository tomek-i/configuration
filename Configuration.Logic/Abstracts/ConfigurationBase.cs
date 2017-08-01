using System;
using TI.Configuration.Logic._internals.Configs;
using TI.Configuration.Logic.API;

namespace TI.Configuration.Logic.Abstracts
{
    [Serializable]
    public abstract class ConfigurationBase : 
        PropertyChangedNotificationClass,
        IConfiguration
    {
       
        public string Name => GetType().Name;
        public DateTime Created { get; }

      
        protected ConfigurationBase()
        {
             Created = DateTime.Now;
        }

        public virtual ConfigurationBase Default()
        {
            throw new Exception("You need to override this method.");
        }

        protected override void OnPropertyChanged(string propertyName, Action onChanged = null)
        {
            base.OnPropertyChanged(propertyName, onChanged);
        }

        #region Overrides of Object
        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}
