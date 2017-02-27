using System;

namespace TI.Configuration.Logic
{
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