﻿using TI.Configuration.Logic.Interfaces;
using System.Threading.Tasks;

namespace TI.Configuration.Logic
{
    public abstract class ConfigurationStorage : DbContext
    {
        public abstract T Get<T>(string name) where T : class, IConfiguration;
        public abstract T Set<T>(T instance) where T : class,IConfiguration;
    }
}