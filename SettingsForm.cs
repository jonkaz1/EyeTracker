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
    public partial class SettingsForm : Form
    {
        public bool isGazeOn;
        public SettingsForm()
        {
            InitializeComponent();
            if (Properties.Settings.Default["autoStartProgram"].Equals(true))
            {
                checkBox1.Checked = true;
            }
            if (Properties.Settings.Default["autoStartGaze"].Equals(true))
            {
                checkBox2.Checked = true;
            }

            isGazeOn = checkBox2.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox1.Text = "Program will start with windows";
                Properties.Settings.Default["autoStartProgram"] = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                checkBox1.Text = "Program will not start with windows";
                Properties.Settings.Default["autoStartProgram"] = false;
                Properties.Settings.Default.Save();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox2.Text = "Eyes controls cursor";
                Properties.Settings.Default["autoStartGaze"] = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                checkBox2.Text = "Mouse controls cursor";
                Properties.Settings.Default["autoStartGaze"] = false;
                Properties.Settings.Default.Save();
            }
        }

        
    }
}
