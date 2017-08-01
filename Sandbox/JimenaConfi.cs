using TI.Configuration.Logic.Abstracts;

namespace TI.Configuration.Logic._internals.Configs
{
    public sealed class JimenaConfi : ConfigurationBase
    {
        string name;
        public string JimenasNewname {
            get { return name; }
            set {
                SetValue(nameof(JimenasNewname), ref name, ref value); }}
        //need to use the set method of the notification property
    }
}