using System;

namespace TI.Configuration.Logic
{
    /// <summary>
    /// Add a tool or helptext to your configuration attributes.
    /// </summary>
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