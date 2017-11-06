using TI.Configuration.Logic.Abstracts;
using TI.Configuration.Logic.API;

namespace TI.Configuration.Logic._internals.Configs
{
    public sealed class JimenaConfi : ConfigurationBase
    {
        string name;
        public string JimenasNewname {
            get { return name; }
            set {
                SetValue(nameof(JimenasNewname), ref name, ref value); }}

        public override IConfiguration Default()
        {
            return new JimenaConfi() { JimenasNewname = "Default New Name" };
        }
        //need to use the set method of the notification property


    }
}