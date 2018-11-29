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
    public partial class CalibrationForm : Form
    {
        private readonly Form1 form1;
        public string[] text;
        int i = 0;

        public CalibrationForm(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
            text = new string[4];
            text[0] = "Calibrating your eyes";
            text[1] = "Calibrating your eyes.";
            text[2] = "Calibrating your eyes..";
            text[3] = "Calibrating your eyes...";
        }

        private void CalibrationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            form1.calibration.isCalibrating = false;
            form1.calibration.calculateAverageBlinkTime();
            form1.calibration.saveBlinkTimeToMemory();
            form1.calibration.time = form1.calibration.time = 20;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (form1.calibration.isCalibrating == true)
            {
                if (!form1.calibration.isSecondTime)
                {
                    if (form1.calibration.time > 9)
                    {
                        richTextBox1.Text = String.Format("0:{0}", form1.calibration.time);
                    }
                    else
                    {
                        richTextBox1.Text = String.Format("0:0{0}", form1.calibration.time);
                    }
                    form1.calibration.time = form1.calibration.time - 1;
                    if (i < 4)
                    {
                        label1.Text = text[i];
                        i++;
                        if (i == 4)
                        {
                            i = 0;
                        }
                    }
                    if (form1.calibration.time <= 0)
                    {
                        form1.calibration.isSecondTime = true;
                        form1.calibration.time = form1.calibration.time = 20;
                    }
                }
                else
                {
                    if (form1.calibration.time > 9)
                    {
                        richTextBox1.Text = String.Format("0:{0}", form1.calibration.time);
                    }
                    else
                    {
                        richTextBox1.Text = String.Format("0:0{0}", form1.calibration.time);
                    }
                    form1.calibration.time = form1.calibration.time - 1;
                    if (i < 4)
                    {
                        label1.Text = text[i];
                        i++;
                        if (i == 4)
                        {
                            i = 0;
                        }
                    }
                    label2.Text = "Blink these commands, until the time ends:";
                    label3.Visible = true;
                    label4.Visible = true;
                    label5.Visible = true;
                    label6.Visible = true;
                    if (form1.calibration.time <= 0)
                    {
                        form1.calibration.isSecondTime = false;
                        form1.calibration.isCalibrating = false;
                        form1.calibration.time = form1.calibration.time = 0;
                        label1.Visible = false;
                        label2.Text = "Your eyes finished calibrating, you can close this window now.";
                        label3.Visible = false;
                        label4.Visible = false;
                        label5.Visible = false;
                        label6.Visible = false;
                        richTextBox1.Visible = false;
                    }
                }

            }
        }
    }
}
