using System.Windows.Forms;

namespace TI.Configuration.Logic.API
{
    public interface IConfigurationDisplayMapper
    {
        void MapToDisplay<TConfig, TDisplay>() where TConfig : IConfiguration
                                             where TDisplay : Control;

        Control GetMappedDisplay<TConfig>() where TConfig : IConfiguration;
    }
}