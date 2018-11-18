using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EyeTracker
{
    public partial class KeybindingConfigurationForm : Form
    {
        Form1 form;
        public KeybindingConfigurationForm(Form1 form)
        {
            InitializeComponent();
            this.form = form;
        }
    }
}
