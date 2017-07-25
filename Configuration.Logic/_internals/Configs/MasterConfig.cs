using TI.Configuration.Logic.Abstracts;

namespace TI.Configuration.Logic._internals.Configs
{

    [InternalConfiguration]
    public sealed class MasterConfig : ConfigurationBase
    {
        public const string ConfigDirectory = @".\configs";
        private string _modeName = "debug";
        
        public string ModeName
        {
            get { return _modeName?.ToLowerInvariant(); }
            set
            {
                SetValue(nameof(ModeName), ref _modeName, ref value, ()=> { /*(De-)Serialize again? or call UI updates?*/ });
            }
        }

        public new MasterConfig Default()
        {
            return new MasterConfig() { ModeName = "debug" };
        }
    }
}