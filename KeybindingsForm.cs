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
        List<List<string>> commands;

        public KeybindingsForm(Form1 form, List<List<string>> commands)
        {
            InitializeComponent();
            this.form = form;
            this.commands = commands;

            button1.Text = commands[2][0];
            button2.Text = commands[3][0];
            button3.Text = commands[4][0];
            button4.Text = commands[5][0];
            button5.Text = commands[6][0];
            button6.Text = commands[7][0];
            button7.Text = commands[8][0];
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //form.SendCommand(110);
            form.SendCommand(commands[2][1]);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //form.SendCommand(010);
            form.SendCommand(commands[3][1]);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //form.SendCommand(000);
            form.SendCommand(commands[4][1]);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //form.SendCommand(110);
            form.SendCommand(commands[5][1]);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //form.SendCommand(220);
            form.SendCommand(commands[6][1]);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            //form.SendCommand(120);
            form.SendCommand(commands[7][1]);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            //form.SendCommand(210);
            form.SendCommand(commands[8][1]);
        }


        private void KeybindingsForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
