using System;
using System.ComponentModel;

namespace TIConfiguration.Logic.API
{
    //[TypeConverter(typeof(ConfigurationTypeConverter))]
    public interface IConfiguration: INotifyPropertyChanged
    {
        string Name { get; }
        DateTime Created { get; }
        DateTime Changed { get; }
    }
}