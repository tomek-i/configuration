using System;

namespace TI.Configuration.Logic.Interfaces
{
    /// <summary>
    /// Basic configuration interface with minimal required properties
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// The name of the configuration (file)
        /// </summary>
        string Name { get; }
        string Code { get; }

        /// <summary>
        /// The date when the file was created
        /// </summary>
        DateTime Created { get; }

        /// <summary>
        /// Creates a new instance with default values.
        /// </summary>
        /// <returns>A new instance with valid default values.</returns>
        IConfiguration Default();
    }
}