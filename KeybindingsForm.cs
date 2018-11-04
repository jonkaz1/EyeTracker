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
    public partial class KeybindingsForm : Form
    {
        Form1 form;

        public KeybindingsForm(Form1 form)
        {
            InitializeComponent();
            this.form = form;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            form.SendCommand(110);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            form.SendCommand(010);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            form.SendCommand(000);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            form.SendCommand(110);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            form.SendCommand(220);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            form.SendCommand(120);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            form.SendCommand(210);
        }


        private void KeybindingsForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
