using System;

namespace TI.Configuration.Logic.API
{
    public interface IConfiguration
    {
        /// <summary>
        /// A name for the configuration.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Timestamp when it was created.
        /// </summary>
        DateTime Created { get; }

        /// <summary>
        /// Creates a new instance with default values.
        /// </summary>
        /// <returns>A new instance with valid default values.</returns>
        IConfiguration Default();
    }
}