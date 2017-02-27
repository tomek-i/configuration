﻿using System;

namespace TI.Configuration.Logic.API
{
    public interface IConfigurationManager
    {
        IConfiguration MasterConfig { get; }
        T Read<T>(bool rewriteIfExists=true) where T : class, IConfiguration;
        T Update<T>(Action<T> exp) where T : class, IConfiguration;
        bool Write<T>(T instance) where T : IConfiguration;
    }
}