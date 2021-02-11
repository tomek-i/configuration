using TI.Configuration.Logic.Interfaces;

namespace TI.Configuration.Logic
{
    public interface DbContext
    {
        T Get<T>(string name) where T : class, IConfiguration;
        T Set<T>(T instance) where T : class, IConfiguration;
    }

}