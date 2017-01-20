using System;
using System.Linq.Expressions;
using TI.Configuration.Logic.API;

namespace TI.Configuration.Logic
{
    public interface IConfigurationManager
    {
        IConfiguration MasterConfig { get; }
        T Read<T>(bool rewriteIfExists=true) where T : class, IConfiguration;
        T Update<T>(Action<T> exp) where T : class, IConfiguration;
        bool Write<T>(T instance) where T : IConfiguration;
    }
}