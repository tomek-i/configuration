using TI.Configuration.Logic.Abstracts;
using TI.Configuration.Logic.Interfaces;

namespace TI.Configuration.Logic._internals.Configs
{

    [InternalConfiguration]
    public sealed class MasterConfig : ConfigurationBase
    {
        public const string ConfigDirectory = @".\config";

        private string _modeName;

    
        public string ModeName
        {
            get { return _modeName?.ToLowerInvariant(); }
            set
            {
                SetValue(nameof(ModeName), ref _modeName, ref value, ()=> { /*(De-)Serialize again? or call UI updates?*/ });
            }
        }

      

        public override IConfiguration Default()
        {
            string modename;
#if DEBUG
            modename = "debug";
#else
            modename = "live";
#endif
            return new MasterConfig() { ModeName = modename };
        }
    }
}