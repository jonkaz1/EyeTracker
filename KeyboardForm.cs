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
    public partial class KeyboardForm : Form
    {
        public static Dictionary<string, int> eyeState = new Dictionary<string, int>();

        public KeyboardForm()
        {
            eyeState.Add("L", 1);
            eyeState.Add("R", 2);
            eyeState.Add("LR", 3);
            InitializeComponent();
            label1.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
