﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tobii.Interaction;
using System.Threading;

namespace EyeTracker
{
    public partial class Form1 : Form
    {
        delegate void StringArgReturningVoidDelegate(string text);
        DateTime dateL1, dateL2, dateR1, dateR2, dateB1, dateB2;          //variables to track dates of left/right/both last opened/closed eye
        List<int> commands = new List<int>();   //0 - Both are closed;   1- Left is closed;    2 - Right is closed
        List<int> inputs = new List<int>();                 //0 - Both are closed;   1- Left is closed;    2 - Right is closed
        private bool leftEyeClosed, rightEyeClosed, bothEyeClosed;        //variables to track whether eye is opened/closed
        bool fYouVariable = false;          //Check if next inputs are for commands
        int leftEyeBlinkTime, rightEyeBlinkTime, BothEyeBlinkTime = 30;        //variables to store eye blinking time
        KeyboardForm keyboardForm = new KeyboardForm();
        SettingsForm settingsForm = new SettingsForm();
        KeybindingsForm keybindingsForm;
        KeybindingConfigurationForm keybindingConfigurationForm;


        //start of import for gaze postion
        Mouse mouse = new Mouse();

        //pratestuoti, tada galima bus trinti
        static Host host = new Host();                                  //changed from var to Host
        //end of import for gaze postion

        public Form1()
        {

            InitializeComponent();

            keybindingsForm = new KeybindingsForm(this);
            keybindingConfigurationForm = new KeybindingConfigurationForm(this);

            mouse.isCursorActive = settingsForm.isGazeOn;

            commands.Add(110); 
            commands.Add(010);
            commands.Add(000);
            commands.Add(110);
            commands.Add(220);
            commands.Add(120);
            commands.Add(210);
            commands.Add(100); //Left click
            commands.Add(200); //Right click


            var positionss = host.Streams.CreateEyePositionStream();
            // var ts = new Thread(() => waitingForEyeInput(positionss));
            //ts.Start();
            waitingForEyeInput(positionss);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (mouse.isCursorActive)
            {
                button1.Text = "Stop mouse controlling";
            }
            else
            {
                button1.Text = "Control mouse with eyes";
            }
            mouse.ToggleCursorGaze();
            
        }


        //method to stream information about eyes
        private void waitingForEyeInput(EyePositionStream positionss)
        {
            //Bothy stream
            positionss.EyePosition(posti =>
            {
                if (posti.HasRightEyePosition != true && posti.HasLeftEyePosition != true) BothEyeClosed(false, posti.Timestamp);
                else BothEyeClosed(true, posti.Timestamp);
            });


            //Lefty stream
            positionss.EyePosition(posti =>
            {
                if (posti.HasLeftEyePosition != true && posti.HasRightEyePosition == true) LeftEyeClosed(false, posti.Timestamp);
                else LeftEyeClosed(true, posti.Timestamp);
            });


            //Righty stream
            positionss.EyePosition(posti =>
            {
                if (posti.HasRightEyePosition != true && posti.HasLeftEyePosition == true) RightEyeClosed(false, posti.Timestamp);
                else RightEyeClosed(true, posti.Timestamp);
            });
        }



        #region left/right/both closed eye time calculator


        /// <summary>
        /// Method to calculate time both eye was closed
        /// </summary>
        /// <param name="eye">variable that indicates whether eye is closed, closed - false; open - true</param>
        /// <param name="data">date eye was closed/opened</param>
        private void BothEyeClosed(bool eye, double data)
        {
            //If left eye wasn't closed before do ... else if was closed and now is open
            if (!bothEyeClosed && !eye)
            {
                dateB1 = DateTime.Now;
                bothEyeClosed = true;
                leftEyeClosed = false;
                rightEyeClosed = false;
            }
            else if (eye && bothEyeClosed)
            {
                dateB2 = DateTime.Now;
                var x = dateB2 - dateB1;
                //if (x.Ticks > 3500000)
                if (x.Ticks > BothEyeBlinkTime)
                {

                    inputs.Add(0);
                    //If we don't expect commands THEN check random inputs for specific line ELSE check if given inputs matches one of commands
                    //if (fYouVariable != true)
                    if (x.Ticks > BothEyeBlinkTime*1.5)
                        CheckInputs();
                    //else
                    //    CheckCommand();
                }

                bothEyeClosed = false;
                leftEyeClosed = false;
                rightEyeClosed = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (mouse.isMoveSlowly)
            {
                mouse.moveCursorSlowly();
            }
            mouse.SetCursorPosition();
        }


        /// <summary>
        /// Method to calculate time left eye was closed
        /// </summary>
        /// <param name="eye">variable that indicates whether eye is closed, closed - false; open - true</param>
        /// <param name="data">date eye was closed/opened</param>
        private void LeftEyeClosed(bool eye, double data)
        {
            //If left eye wasn't closed before do ... else if was closed and now is open
            if (!leftEyeClosed && !eye && !rightEyeClosed && !bothEyeClosed)
            {
                dateL1 = DateTime.Now;
                leftEyeClosed = true;
            }
            else if (eye && leftEyeClosed && !rightEyeClosed && !bothEyeClosed)
            {
                dateL2 = DateTime.Now;
                var x = dateL2 - dateL1;
                //if (x.Ticks > 1000000)
                if (x.Ticks > leftEyeBlinkTime)
                    inputs.Add(1);
                //setText("L" + x.Ticks);
                leftEyeClosed = false;
            }
        }


        /// <summary>
        /// Method to calculate time right eye was closed
        /// </summary>
        /// <param name="eye">variable that indicates whether eye is closed, closed - false; open - true</param>
        /// <param name="data">date eye was closed/opened</param>
        private void RightEyeClosed(bool eye, double data)
        {
            //If right eye wasn't closed before do ... else if was closed and now is open
            if (!rightEyeClosed && !eye && !leftEyeClosed && !bothEyeClosed)
            {
                dateR1 = DateTime.Now;
                rightEyeClosed = true;
            }
            else if (eye && rightEyeClosed && !leftEyeClosed && !bothEyeClosed)
            {
                dateR2 = DateTime.Now;
                var x = dateR2 - dateR1;
                //if (x.Ticks > 1000000)
                if (x.Ticks > rightEyeBlinkTime)
                    inputs.Add(2);
                //setText("R" + x.Ticks);
                rightEyeClosed = false;
            }
        }

        private void editKeybindingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            keybindingConfigurationForm.ShowDialog();
        }

        #endregion

        /// <summary>
        /// Checks inputs for specific commands TO DO STUFF
        /// </summary>
        private void CheckCommand(int c)
        {
            if (c.Equals(commands[0]))
            {
                fYouVariable = false;
                if (Application.OpenForms["KeyboardForm"] == null)
                    setText("0");
                else
                    keyboardForm.Close();
                inputs.RemoveAll(y => y < 3);
                return;
            }

            if (c.Equals(commands[1]))
            {
                SendKeys.Send("^%{TAB}");
                //System.Diagnostics.Process process = new System.Diagnostics.Process();
                //System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                //startInfo.FileName = "cmd.exe";
                //startInfo.Arguments = "alt+tab";
                //process.StartInfo = startInfo;
                //process.Start();

                inputs.RemoveAll(y => y < 3);
                return;
            }

            if (c.Equals(commands[2]))
            {
                SendKeys.Send("+%");

                inputs.RemoveAll(y => y < 3);
                return;
            }

            if (c.Equals(commands[3]))
            {
                SendKeys.Send("%{LEFT}");

                inputs.RemoveAll(y => y < 3);
                return;
            }

            if (c.Equals(commands[4]))
            {
                SendKeys.Send("%{RIGHT}");

                inputs.RemoveAll(y => y < 3);
                return;
            }

            if (c.Equals(commands[5]))
            {
                SendKeys.Send("^{C}");

                inputs.RemoveAll(y => y < 3);
                return;
            }

            if (c.Equals(commands[6]))
            {
                SendKeys.Send("^{P}");

                inputs.RemoveAll(y => y < 3);
                return;
            }

            if (c.Equals(commands[7]))
            {
                setText("Left mouse click");
                mouse.saveCursorPosition();


                //mouse.LeftClick(Cursor.Position.X, Cursor.Position.Y);

                inputs.RemoveAll(y => y < 3);
                return;
            }

            if (c.Equals(commands[8]))
            {
                setText("Right mouse click");
                mouse.saveCursorPosition();

                //mouse.RightClick(mouse.posX, mouse.posY);

                inputs.RemoveAll(y => y < 3);
                return;
            }
        }


        //To receive command from others Forms
        public void SendCommand(int command)
        {
            CheckCommand(command);
        }



        /// <summary>
        /// Checks randoms inputs for specific command in order to allow use of all commands
        /// </summary>
        private void CheckInputs()
        {
            int x = inputs.Count;
            if (x < 3)
            {
                return;
            }
            int c = inputs[x - 3] * 100 + inputs[x - 2] * 10 + inputs[x - 1];
            CheckCommand(c);
            //if (inputs[0] == commands[0][0] && inputs[1] == commands[0][1] && inputs[2] == commands[0][2])
            //{
            //    fYouVariable = true;
            //    setText("Waiting for command");
            //}
            //inputs.RemoveAll(x => x < 3);
        }


        //Random metod for testing which eye is visible 
        private void setText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox1.InvokeRequired)
            {
                try
                {
                    StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(setText);
                    this.Invoke(d, new object[] { text });
                }
                catch (ObjectDisposedException e)
                {
                    //If somebody finds me, this poor unloved exception that was thrown to a corner and forgotten, REMEMBER ME!!!
                }
            }
            else
            {
                if (text == "0")
                    keyboardForm.Show();
                else
                    this.textBox1.Text = text;
                Console.WriteLine(text);
            }
        }
        

        private void keybindingsToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            keybindingsForm.ShowDialog();
        }


        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingsForm.ShowDialog();
        }

    }

    //Cancer 3rd stage
    public class cancer
    {
        double closedTime;
        double opendTime;
        char eye;
        bool x = false;

        public void runOutOfIdeas(double dateTime, char eye)
        {
            if (!x)
            {
                closedTime = dateTime;
                this.eye = eye;
                x = true;
            }
            else if (closedTime != null)
            {
                opendTime = dateTime;
                this.eye = eye;
                x = false;
            }
        }
    }


}
