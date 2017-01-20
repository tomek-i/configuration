using TI.Configuration.Logic.Abstracts;
using TI.Configuration.Logic.API;

namespace TI.Configuration.Logic._internals.Configs
{
    [InternalConfiguration]
    internal sealed class MasterConfig : ConfigurationBase
    {
        public const string ConfigDirectory = @".\configs";


        private string _modeName = "debug";
       
        //public string ConfigDirectory { get; internal set; } = @".\configs";

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
    }
}