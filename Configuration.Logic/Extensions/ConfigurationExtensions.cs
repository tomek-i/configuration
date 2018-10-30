using System;
using System.IO;
using System.Reflection;
using TI.Configuration.Logic.API;
using TI.Configuration.Logic._internals.Configs;
using System.Linq.Expressions;

namespace TI.Configuration.Logic
{
    public static class ConfigurationExtensions
    {
        public static string GetTooltip<T, TProp>(this T o, Expression<Func<T, TProp>> propertySelector) where T : Configuration.Logic.Abstracts.ConfigurationBase
        {
            string propertyName = ((MemberExpression)propertySelector.Body).Member.Name;
            return ((MemberExpression)propertySelector.Body).Expression.Type.GetProperty(propertyName).GetCustomAttribute<ConfigurationToolTipAttribute>()?.Tooltip;
        }

        public static string GetTooltip<T>(this T o, string propertyName) where T : Configuration.Logic.Abstracts.ConfigurationBase
        {
            return o.GetType().GetProperty(propertyName).GetCustomAttribute<ConfigurationToolTipAttribute>()?.Tooltip;
        }
      
    }
}