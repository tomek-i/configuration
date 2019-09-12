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
    public sealed class ConfigurationManager<TStore> : IConfigurationManager<TStore> where TStore : DbContext
    {
        //NOTE: need to be specifiedwith COnfiguration Manger to make it work for SQLConfigStorage
        public TStore Storage { get; set; }

        public ConfigurationManager(DbContext storage)
        {
            Storage = (TStore)storage;
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