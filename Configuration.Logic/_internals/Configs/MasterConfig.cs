using System;
using System.ComponentModel;
using TI.Configuration.Logic.Abstracts;
using TI.Configuration.Logic.API;

namespace TI.Configuration.Logic._internals.Configs
{
    [InternalConfiguration]
    public sealed class MasterConfig : ConfigurationBase
    {
        private string _modeName = "debug";
        private ConfigurationMode _currentMode = ConfigurationMode.Debug;

        public ConfigurationMode CurrentMode
        {
            get { return _currentMode; }
            set
            {
                if (value == _currentMode) return;
                _currentMode = value;
                OnPropertyChanged();
            }
        }
        public string ConfigDirectory { get; internal set; } = @".\configs";

        public string ModeName
        {
            get { return _modeName?.ToLowerInvariant(); }
            set
            {
                if (value == _modeName) return;
                _modeName = value;
                OnPropertyChanged();
            }
        }

        #region Overrides of Configuration

        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == nameof(CurrentMode))
            {
                switch (CurrentMode)
                {
                    case ConfigurationMode.Debug:
                    case ConfigurationMode.Release:
                        ModeName = nameof(CurrentMode).ToLowerInvariant();
                        break;
                    case ConfigurationMode.Custom:
                        ModeName = null;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                return;
            }

            base.OnPropertyChanged(sender, propertyChangedEventArgs);
        }

        #endregion
    }
}