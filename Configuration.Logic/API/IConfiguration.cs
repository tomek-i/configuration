using System;

namespace TI.Configuration.Logic.API
{
    public interface IConfiguration
    {
        string Name { get; }
        DateTime Created { get; }

        /// <summary>
        /// Creates a new instance with default values.
        /// </summary>
        /// <returns>A new instance with valid default values.</returns>
        IConfiguration Default();
    }
}