using System;

namespace TI.Configuration.Logic
{
    /// <summary>
    /// Add tooltips/descripotions to configuration properties
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ConfigurationToolTipAttribute : Attribute
    {
        public ConfigurationToolTipAttribute(string tooltip, string description)
        {
            Tooltip = tooltip;
            Description = description;
        }

        public string Tooltip { get; }
        public string Description { get; }
    }
}