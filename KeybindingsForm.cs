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
        List<Command> commands;

        public KeybindingsForm(Form1 form, List<Command> commands)
        {
            InitializeComponent();
            this.form = form;
            this.commands = commands;

            button1.Text = commands[2].Name;
            button2.Text = commands[3].Name;
            button3.Text = commands[4].Name;
            button4.Text = commands[5].Name;
            button5.Text = commands[6].Name;
            button6.Text = commands[7].Name;
            button7.Text = commands[8].Name;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //form.SendCommand(110);
            form.SendCommand(commands[2]);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //form.SendCommand(010);
            form.SendCommand(commands[3]);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //form.SendCommand(000);
            form.SendCommand(commands[4]);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //form.SendCommand(110);
            form.SendCommand(commands[5]);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //form.SendCommand(220);
            form.SendCommand(commands[6]);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            //form.SendCommand(120);
            form.SendCommand(commands[7]);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            //form.SendCommand(210);
            form.SendCommand(commands[8]);
        }


        private void KeybindingsForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
