using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using TI.Configuration.Logic.API;

namespace TI.Configuration.Logic._internals.Experimental.GUI
{
    public partial class ConfigurationPropertyControl : UserControl
    {
        private IConfiguration _configuration;

        [Editor(typeof (ConfigurationTypeEditor), typeof (UITypeEditor))]
        public IConfiguration Configuration
        {
            get { return _configuration; }
            set
            {
                _configuration = value;
                Controls.Clear();
                UpdateControls();
            }
        }

        private void UpdateControls()
        {
            var cfg = Configuration;
            if(cfg==null) return;
            
            var publicProperties = cfg.GetType().GetProperties();

            Label lastLabel = new Label();
            int longestWidth=0;
            foreach (PropertyInfo property in publicProperties)
            {
                Label l = new Label
                {
                    AutoSize = true,
                    Text = property.Name
                };
                l.Location = new Point(lastLabel.Location.X,  (l.Size.Height*Controls.Count) + 3);
                longestWidth = Math.Max(longestWidth, l.PreferredSize.Width);
                Controls.Add(l);
                

                lastLabel = l;
            }
            Controls.Add(new Label() {Text = longestWidth.ToString(),Location = new Point(longestWidth,lastLabel.Location.Y+3)});

        }


        public ConfigurationPropertyControl()
        {
            InitializeComponent();
        }
    }
}