using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Configuration.GUI
{
    public partial class ConfigNameForm : Form
    {
        public ConfigNameForm()
        {
            InitializeComponent();
        }

        public string AppConfigName => textBox1.Text;

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
