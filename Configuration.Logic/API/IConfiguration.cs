using System;
using System.ComponentModel;

namespace TI.Configuration.Logic.API
{
    public interface IConfiguration: INotifyPropertyChanged
    {
        string Name { get; }
        DateTime Created { get; }
        DateTime Changed { get; }
    }
}