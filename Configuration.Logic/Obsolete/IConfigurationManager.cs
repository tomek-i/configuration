using System;
using System.Windows.Forms;
using TI.Configuration.Logic.Interfaces;

namespace TI.Configuration.Obsolete
{
    public interface IConfigurationManager
    {
        /// <summary>
        /// Instance to the internal Master configuration file
        /// </summary>
        //IConfiguration MasterConfig { get; }

        /// <summary>
        /// Reads a configuration into memory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Read<T>() where T : class, IConfiguration;

        T Update<T>(Action<T> exp) where T : class, IConfiguration;

        /// <summary>
        /// writes a configuration out 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        bool Write<T>(T instance) where T : IConfiguration;


        /// <summary>
        /// map a configuration to a GUI display
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <typeparam name="TDisplay"></typeparam>
        void MapToDisplay<TConfig, TDisplay>() where TConfig : IConfiguration
                                               where TDisplay : Control;

        /// <summary>
        /// Retrives the configuration GUI
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <returns></returns>
        Control GetMappedDisplay<TConfig>() where TConfig : IConfiguration;
    }
}
