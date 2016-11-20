using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TIConfiguration.Logic.API;
using TIConfiguration.Logic.Properties;

namespace TIConfiguration.Logic._internals.Abstracts
{
    [Serializable]
    public abstract class ConfigurationBase : IConfiguration
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Name => GetType().Name;
        public DateTime Created { get; }
        public DateTime Changed { get; private set; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected ConfigurationBase()
        {
            Created = DateTime.Now;
            PropertyChanged += OnPropertyChanged;
        }

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            Changed = DateTime.Now;
        }

        #region Overrides of Object

        public override string ToString()
        {
            return Name;
        }

        #endregion

        
    }
}
