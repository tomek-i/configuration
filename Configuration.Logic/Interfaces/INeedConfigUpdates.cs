using TI.Configuration.Logic.Abstracts;

namespace TI.Configuration.Logic
{
    /// <summary>
    /// Observeable 
    /// </summary>
    public interface INeedConfigUpdates
    {
        //TODO: shouldnt that be a delegate or something
        void OnConfigurationUpdate<T>(T instance) where T : ConfigurationBase;
    }
}