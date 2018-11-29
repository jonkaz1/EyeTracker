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
        List<string> commands;
        List<string> commandsName;

        public KeybindingsForm(Form1 form, List<string> commands, List<string> commandsName)
        {
            InitializeComponent();
            this.form = form;
            this.commands = commands;
            this.commandsName = commandsName;

            button1.Text = commandsName[0];
            button2.Text = commandsName[1];
            button3.Text = commandsName[2];
            button4.Text = commandsName[3];
            button5.Text = commandsName[4];
            button6.Text = commandsName[5];
            button7.Text = commandsName[6];
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //form.SendCommand(110);
            form.SendCommand(commands[0]);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //form.SendCommand(010);
            form.SendCommand(commands[1]);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //form.SendCommand(000);
            form.SendCommand(commands[2]);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //form.SendCommand(110);
            form.SendCommand(commands[3]);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //form.SendCommand(220);
            form.SendCommand(commands[4]);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            //form.SendCommand(120);
            form.SendCommand(commands[5]);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            //form.SendCommand(210);
            form.SendCommand(commands[6]);
        }


        private void KeybindingsForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
