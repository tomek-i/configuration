using TI.Configuration.Logic.Interfaces;
using System.Threading.Tasks;
using System;
using System.Windows.Forms;

namespace TI.Configuration.Logic
{
    /// <summary>
    /// Configuration Manager, you have to call <c>ConfigurationManager.Create()</c> first.
    /// TODO: maybe there is a better solution to the usage of this
    /// </summary>
    public sealed class ConfigurationManager<TStore> : IConfigurationManager<TStore> where TStore: IConfigStorage
    {
        public TStore Storage { get; set; }

        public ConfigurationManager(TStore storage)
        {
            Storage = storage;
        }
        public Control GetMappedDisplay<T>() where T : IConfiguration
        {
            throw new NotImplementedException();
        }

        

        public void MapToDisplay<T, TDisplay>()
            where T : IConfiguration
            where TDisplay : Control
        {
            throw new NotImplementedException();
        }

       
    }
}