using System.Windows.Forms;

namespace TI.Configuration.Logic.Interfaces
{
    /// <summary>
    /// Configuration Manager API
    /// </summary>
    public interface IConfigurationManager<TStore>
    {

        TStore Storage { get; }

        //bool Exist<T>(string name);
        //  T Load<T>(string name) where T : class, IConfiguration;
        //  void Save<T>(T instance) where T : class, IConfiguration;

        void MapToDisplay<T, TDisplay>() where T : IConfiguration
                                         where TDisplay : Control;
        Control GetMappedDisplay<T>() where T : IConfiguration;


    }
}