using System;
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
        List<List<int>> commands = new List<List<int>>();   //0 - Both are closed;   1- Left is closed;    2 - Right is closed
        List<int> inputs = new List<int>();                 //0 - Both are closed;   1- Left is closed;    2 - Right is closed
        private bool leftEyeClosed, rightEyeClosed, bothEyeClosed;        //variables to track whether eye is opened/closed
        bool fYouVariable = false;          //Check if next inputs are for commands
        int leftEyeBlinkTime, rightEyeBlinkTime, BothEyeBlinkTime = 30;        //variables to store eye blinking time
        KeyboardForm keyboardForm = new KeyboardForm();
        SettingsForm settingsForm = new SettingsForm();


        //start of import for gaze postion
        bool isGazeAutoStart = false;
        int posX = 0, posY = 0;                                         //Eye gaze x and y  coordinates
        static Host host = new Host();                                  //changed from var to Host
        GazePointDataStream gazePointDataStream = host.Streams.CreateGazePointDataStream(); //changed from var to GazePointDataStream
        //end of import for gaze postion

        //start of imports for mouse clicks
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        public const int MOUSEEVENTF_RIGHTUP = 0x10;
        //end of imports for mouse clicks

        public Form1()
        {

            InitializeComponent();
            isGazeAutoStart = settingsForm.isGazeOn;

            if (isGazeAutoStart)
            {
                gazePointDataStream.GazePoint((gazePointX, gazePointY, _) => textBox1.Text = String.Format("X: {0} Y:{1}", gazePointX, gazePointY));
                gazePointDataStream.GazePoint((gazePointX, gazePointY, _) => { posX = (int)gazePointX; posY = (int)gazePointY; Cursor.Position = new Point(posX, posY); });
            }

            List<int> command = new List<int>();
            command.Add(1);
            command.Add(1);
            command.Add(0);
            commands.Add(command);
            commands.Add(command);

            var positionss = host.Streams.CreateEyePositionStream();
            // var ts = new Thread(() => waitingForEyeInput(positionss));
            //ts.Start();
            waitingForEyeInput(positionss);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Patikrinti ar čia persimeta pelė į vietą kurią žiūri ir ar sukasi ciklas ar tiesiog 1 kartą tik paspaudus mygtuką
            isGazeAutoStart = true;
            //setText("1"); //just to test
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
                    if (fYouVariable != true)
                        CheckInputs();
                    else
                        CheckCommand();
                }

                bothEyeClosed = false;
                leftEyeClosed = false;
                rightEyeClosed = false;
            }
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

        #endregion


        /// <summary>
        /// Checks inputs for specific commands TO DO STUFF
        /// </summary>
        private void CheckCommand()
        {
            if (inputs[0] == commands[1][0] && inputs[1] == commands[1][1] && inputs[2] == commands[1][2])
            {
                //setText("OK");
                fYouVariable = false;
                if (Application.OpenForms["KeyboardForm"] == null)
                    setText("0");
                //this.Close();
                //keyboardForm.Show();
                else
                    keyboardForm.Close();
            }
            else if (inputs[0] == commands[1][0])
            {
                fYouVariable = false;
                setText("1");
            }
            else if (inputs[0] == commands[0][1])
            {
                fYouVariable = false;
                setText("2");
            }
            else inputs.RemoveAll(x => x < 3);
        }


        //Because program always waits for command input, this method isn't needed and only calls for other method.
        //If end program doesn't change please remove this method and change all calls to this method to "CkeckCommand();
        /// <summary>
        /// Checks randoms inputs for specific command in order to allow use of all commands
        /// </summary>
        private void CheckInputs()
        {
            CheckCommand();
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
                else if (text == "1")
                {
                    this.textBox1.Text = "Left mouse click";
                    LeftClick(Cursor.Position.X, Cursor.Position.Y);
                }
                else if (text == "2")
                {
                    this.textBox1.Text = "Right mouse click";
                    RightClick(Cursor.Position.X, Cursor.Position.Y);
                }
                else
                    this.textBox1.Text = text;
                Console.WriteLine(text);
            }
        }


        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingsForm.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public static void LeftClick(int PositionX, int PositionY)
        {
            SetCursorPos(PositionX, PositionY);
            mouse_event(MOUSEEVENTF_LEFTDOWN, PositionX, PositionY, 0, 0);
            System.Threading.Thread.Sleep(50);
            mouse_event(MOUSEEVENTF_LEFTUP, PositionX, PositionY, 0, 0);
        }
        public static void RightClick(int PositionX, int PositionY)
        {
            SetCursorPos(PositionX, PositionY);
            mouse_event(MOUSEEVENTF_RIGHTDOWN, PositionX, PositionY, 0, 0);
            System.Threading.Thread.Sleep(50);
            mouse_event(MOUSEEVENTF_RIGHTUP, PositionX, PositionY, 0, 0);
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
