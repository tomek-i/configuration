using TI.Configuration.Logic.Abstracts;

namespace TI.Configuration.Logic
{
    /// <summary>
    /// Observeable 
    /// </summary>
    public interface INeedConfigUpdates
    {
        void OnConfigurationUpdate<T>(T instance) where T : ConfigurationBase;
    }
}