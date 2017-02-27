using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using TI.Configuration.Logic.API;
using TI.Configuration.Logic.Properties;

namespace TI.Configuration.Logic.Abstracts
{
    [Serializable]
    public abstract class ConfigurationBase : IConfiguration
    {
        [ExcludeFromCodeCoverage]
        public event PropertyChangedEventHandler PropertyChanged;
        public string Name => GetType().Name;

        [ExcludeFromCodeCoverage]
        public DateTime Created { get; }
        [ExcludeFromCodeCoverage]
        public DateTime Changed { get; private set; }

        [NotifyPropertyChangedInvocator]
        [ExcludeFromCodeCoverage]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected ConfigurationBase()
        {
            Created = DateTime.Now;
            PropertyChanged += OnPropertyChanged;
        }
        [ExcludeFromCodeCoverage]
        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            Changed = DateTime.Now;
        }

        #region Overrides of Object
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return Name;
        }

        #endregion

        
    }
}
