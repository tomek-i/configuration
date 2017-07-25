using System;

namespace TI.Configuration.Logic.API
{
    public interface IConfiguration
    {
        string Name { get; }
        DateTime Created { get; }
    }
}