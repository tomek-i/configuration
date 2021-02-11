using System;
using System.Linq.Expressions;
using System.Reflection;

namespace TI.Configuration.Logic
{
    /// <summary>
    /// Extension class with utility functions for Configurations
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="o"></param>
        /// <param name="propertySelector"></param>
        /// <returns></returns>
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