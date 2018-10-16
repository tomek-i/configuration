using System;

namespace TI.Configuration.Logic
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ConfigurationToolTipAttribute : Attribute
    {
        public ConfigurationToolTipAttribute(string tooltiptext)
        {
            TooltipText = tooltiptext;
        }

        public string TooltipText { get; }
    }
}