using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using TI.Configuration.Logic.API;

namespace TI.Configuration.Logic._internals.Experimental.GUI
{
    public class ConfigurationTypeEditor : UITypeEditor
    {
        private IWindowsFormsEditorService _editorService;

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            // drop down mode (we'll host a listbox in the drop down)
            return UITypeEditorEditStyle.DropDown;
        }

        private List<Type> obj = new List<Type>();

        public ConfigurationTypeEditor()
        {
            var type = typeof (IConfiguration);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                try
                {
                    var types = assembly.GetTypes();
                    foreach (var t in types)
                    {
                        if (type.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                        {
                            obj.Add(t);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
                _editorService = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));

            // use a list box
            ListBox lb = new ListBox();
            lb.SelectionMode = SelectionMode.One;
            lb.SelectedValueChanged += OnListBoxSelectedValueChanged;
            
            lb.DisplayMember = "Name";

            lb.Items.AddRange(obj.ToArray());
            
            // show this model stuff
            _editorService.DropDownControl(lb);
            if (lb.SelectedItem == null)
            {
                // no selection, return the passed-in value as is  
                if (value != null)
                {
                    IConfiguration ret = Activator.CreateInstance(value.GetType()) as IConfiguration;
                    return ret;
                }
                
            }

            var ttt = lb.SelectedItem as Type;
            if(ttt!=null)
                return Activator.CreateInstance(ttt) as IConfiguration;

            return value;
        }

        private void OnListBoxSelectedValueChanged(object sender, EventArgs e)
        {
            // close the drop down as soon as something is clicked
            _editorService.CloseDropDown();
        }
    }
}