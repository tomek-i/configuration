using TI.Configuration.Logic.Interfaces;

namespace TI.Configuration.Logic
{
    public interface IConfigStorage
    {
        T Get<T>(string name) where T : class, IConfiguration;
        void Set<T>(T instance) where T : class,IConfiguration;
    }

}