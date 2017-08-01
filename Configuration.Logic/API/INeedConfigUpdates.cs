using TI.Configuration.Logic.Abstracts;

namespace TI.Configuration.Logic
{
    public interface INeedConfigUpdates
    {
        void OnConfigurationUpdate<T>(T instance) where T : ConfigurationBase;
    }
}