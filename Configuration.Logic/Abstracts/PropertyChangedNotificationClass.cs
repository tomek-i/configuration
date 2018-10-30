using System;
using System.ComponentModel;

namespace TI.Configuration.Logic._internals.Configs
{
    /// <summary>
    /// Class wich should be used for inheritance where the interface <c>INotifyPropertyChanged</c> 
    /// is used.
    /// </summary>
    public abstract class PropertyChangedNotificationClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetValue<T>(string name, ref T value, ref T newValue, Action onChanged = null)
        {
            if (newValue != null)
            {
                if (!newValue.Equals(value))
                {
                    value = newValue;
                    OnPropertyChanged(name, onChanged);
                }
            }
            else
            {
                value = default(T);
            }
        }
        protected virtual void OnPropertyChanged(string propertyName, Action onChanged = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
                onChanged?.Invoke();
            }

        }
    }
}