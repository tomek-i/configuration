using System;

namespace TI.Configuration.Logic.Properties
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class AspMvcAreaViewLocationFormatAttribute : Attribute
    {
        public AspMvcAreaViewLocationFormatAttribute(string format)
        {
            Format = format;
        }

        public string Format { get; private set; }
    }
}