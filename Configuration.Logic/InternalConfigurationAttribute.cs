using System;

namespace TI.Configuration.Logic
{
    /// <summary>
    /// Attribute to decorate an internal configuration. These configurations are stored into a seperate folder
    /// and are loaded regardless of which mode the configuration manager is currently set to.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Struct,Inherited = false)]
    public sealed class InternalConfigurationAttribute : Attribute
    {
        public string Foldername { get; }
        public string FilePrefix { get; }
        public InternalConfigurationAttribute(string foldername,string fileprefix)
        {
            Foldername = foldername;
            FilePrefix = fileprefix;
        }
        public InternalConfigurationAttribute():this("_internals","_")
        {
        }
    }
}