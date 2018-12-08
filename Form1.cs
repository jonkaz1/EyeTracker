using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Tobii.Interaction;

namespace EyeTracker
{
    public partial class Form1 : Form
    {
        private static Form1 form;
        public static SortedDictionary<string, int> EyeStates = new SortedDictionary<string, int>
{
  {"LR", 0},
  {"L", 1},
  {"R", 2}
};
        private delegate void StringArgReturningVoidDelegate(string text);

        private DateTime dateL1, dateL2, dateR1, dateR2, dateB1, dateB2;          //variables to track dates of left/right/both last opened/closed eye
        private List<Command> commands = new List<Command>();   //0 - Both are closed;   1- Left is closed;    2 - Right is closed
                                                                // List<string> commandsName = new List<string>();

        private List<int> inputs = new List<int>();                 //0 - Both are closed;   1- Left is closed;    2 - Right is closed
        private bool leftEyeClosed, rightEyeClosed, bothEyeClosed;        //variables to track whether eye is opened/closed
        private bool commandWasDisplayed = false;


        //int leftEyeBlinkTime, rightEyeBlinkTime, BothEyeBlinkTime = 10000;        //variables to store eye blinking time
        private KeyboardForm keyboardForm = new KeyboardForm();
        private SettingsForm settingsForm = new SettingsForm();
        private CalibrationForm calibrationForm;
        public Calibration calibration = new Calibration();
        private KeybindingsForm keybindingsForm;
        private KeybindingConfigurationForm keybindingConfigurationForm;


        //start of import for gaze postion
        private Mouse mouse = new Mouse();
        private const int constLongGazeTimeTrigger = 2000; //2000 ms

        private ClickConfirmationForm clickConfirmationForm = new ClickConfirmationForm();
        private static Host host = new Host();                                  //changed from var to Host
        //end of import for gaze postion

        public Form1()
        {

            InitializeComponent();

            ReadCommandFile();
            //HideExecutedCommandLabel();
            //HideActionLabels();


            keybindingsForm = new KeybindingsForm(this, commands);
            calibrationForm = new CalibrationForm(this);
            keybindingConfigurationForm = new KeybindingConfigurationForm(this, commands);

            mouse.isCursorActive = settingsForm.isGazeOn;


            var positionss = host.Streams.CreateEyePositionStream();

            waitingForEyeInput(positionss);
            form = this;

        }

        public static Form1 GetInstance()
        {
            return form;
        }

        #region read/write

        public void SaveCommandsToFile()
        {
            List<string> commandLines = new List<string>();

            foreach (Command c in commands)
            {
                commandLines.Add(string.Format("{0},{1},{2}", c.Name, c.Actions, c.ResultingAction));
            }
            File.WriteAllLines(@".\Commands.txt", commandLines);
        }

        private void ReadCommandFile()
        {
            // Example #2
            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.
            string[] lines = File.ReadAllLines(@".\Commands.txt");

            // Display the file contents by using a foreach loop.
            foreach (string line in lines)
            {
                List<string> x = new List<string>();
                string[] spl = line.Split(',');
                commands.Add(new Command(spl[0], spl[1], spl[2]));
            }
        }

        internal List<Command> GetDefaultCommands()
        {
            string[] lines = File.ReadAllLines(@".\Commands.default.txt");
            commands.Clear();
            // Display the file contents by using a foreach loop.
            foreach (string line in lines)
            {
                List<string> x = new List<string>();
                string[] spl = line.Split(',');
                commands.Add(new Command(spl[0], spl[1], spl[2]));
            }

            return commands;
        }


        #endregion

        public List<Command> GetCurrentCommands()
        {
            return commands;
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
                if (posti.HasRightEyePosition != true && posti.HasLeftEyePosition != true)
                {
                    BothEyeClosed(false, posti.Timestamp); //closed
                }
                else
                {
                    BothEyeClosed(true, posti.Timestamp); //opened eyes
                }
            });


            //Lefty stream
            positionss.EyePosition(posti =>
            {
                if (posti.HasLeftEyePosition != true && posti.HasRightEyePosition == true)
                {
                    LeftEyeClosed(false, posti.Timestamp);
                }
                else
                {
                    LeftEyeClosed(true, posti.Timestamp);
                }
            });


            //Righty stream
            positionss.EyePosition(posti =>
            {
                if (posti.HasRightEyePosition != true && posti.HasLeftEyePosition == true)
                {
                    RightEyeClosed(false, posti.Timestamp);
                }
                else
                {
                    RightEyeClosed(true, posti.Timestamp);
                }
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
                if (!calibration.isCalibrating)
                {
                    if (x.Ticks > calibration.BothEyeBlinkTime)
                    {

                        AddInput(0);
                        CheckInputs();
                        //If we don't expect commands THEN check random inputs for specific line ELSE check if given inputs matches one of commands
                        //if (fYouVariable != true)
                        //else
                        //    CheckCommand();
                    }
                }
                else
                {
                    if (!calibration.isSecondTime)
                    {
                        calibration.BothEyeTimeList.Add((int)x.Ticks);
                    }
                    else
                    {
                        calibration.BothEyeTimeList2.Add((int)x.Ticks);
                    }
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
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (mouse.isClickActive)
            {
                if (mouse.isClickActive2)
                {
                    if (!mouse.isInSquareAlways)    //if gaze moved out of square
                    {
                        mouse.resetCount++;
                        if (mouse.isLeftClick)
                        {
                            mouse.LeftClick(Cursor.Position.X, Cursor.Position.Y);
                            mouse.ResetDefault();
                        }
                        else if (mouse.isRightClick)
                        {
                            mouse.RightClick(Cursor.Position.X, Cursor.Position.Y);
                            mouse.ResetDefault();
                        }
                        else if (mouse.resetCount >= 100)//100ms * 100 = 10secs
                        {
                            mouse.ResetDefault();
                        }
                        //if (mouse.longGazeCount * 25 >= constLongGazeTimeTrigger)   //waits some time
                        //{
                        //}
                    }
                }
                else if (mouse.longGazeCount * 25 >= constLongGazeTimeTrigger)
                {
                    mouse.saveCursorPosition(); //starts moving slowly
                    mouse.isClickActive2 = true;//starts to wait for real click
                    mouse.longGazeCount = 0;    //resets trigger count
                }

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
                if (!calibration.isCalibrating)
                {
                    if (x.Ticks > calibration.leftEyeBlinkTime)
                    {
                        AddInput(1);
                        CheckInputs();
                    }
                }
                else
                {
                    if (!calibration.isSecondTime)
                    {
                        calibration.leftEyeTimeList.Add((int)x.Ticks);
                    }
                    else
                    {
                        calibration.leftEyeTimeList2.Add((int)x.Ticks);
                    }
                }
                leftEyeClosed = false;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            calibration.toggleCalinbrating();
            calibrationForm.ShowDialog();
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
                if (!calibration.isCalibrating)
                {
                    if (x.Ticks > calibration.rightEyeBlinkTime)
                    {
                        AddInput(2);
                        CheckInputs();
                    }
                }
                else
                {
                    if (!calibration.isSecondTime)
                    {
                        calibration.rightEyeTimeList.Add((int)x.Ticks);
                    }
                    else
                    {
                        calibration.rightEyeTimeList2.Add((int)x.Ticks);
                    }
                }
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
        private void CheckCommand(string c)
        {
            if (c.Equals(commands[0].Actions))
            {
                keybindingsForm.ShowDialog();
            }
            else if (c.Equals(commands[1].Actions))
            {
                DisplayExecutedCommand("Left mouse click");
                mouse.isLeftClick = true;
            }
            else if (c.Equals(commands[2].Actions))
            {
                DisplayExecutedCommand("Right mouse click");
                mouse.isRightClick = true;
            }
            else if (c.Equals(commands[3].Actions))   //starts click procedure
            {
                mouse.ResetDefault();
                DisplayExecutedCommand("Starts click procedure");
                mouse.isCursorActive = true; //lets cursor move
                mouse.isClickActive = true;  //lets gaze time counting and slow movement and timer 2
            }
            else
            {
                for (int i = 4; i < commands.Count(); i++)
                {
                    if (c.Equals(commands[i].Actions))
                    {
                        mouse.ResetDefault();
                        DisplayExecutedCommand(commands[i].Name);
                        InvokeCommand(commands[i].ResultingAction);
                    }
                }
            }
            ClearInputs();
        }

        private void DisplayExecutedCommand(String commandName)
        {
            ExecutedCommandLabel.Invoke((MethodInvoker)(() =>
            {
                ExecutedCommandLabel.Text = commandName;
                ExecutedCommandLabel.Show();
            }));
            commandWasDisplayed = true;
        }

        private void HideExecutedCommandLabel()
        {
            ExecutedCommandLabel.Invoke((MethodInvoker)(() =>
            {
                ExecutedCommandLabel.Hide();
            }));
        }

        private void HideActionLabels()
        {
            Action1Label.Invoke((MethodInvoker)(() =>
            {
                Action1Label.Hide();
                Action2Label.Hide();
                Action3Label.Hide();
            }));
        }

        private void AddInput(int eyeState)
        {
            if (commandWasDisplayed)
            {
                HideActionLabels();
                HideExecutedCommandLabel();
                commandWasDisplayed = false;
            }

            inputs.Add(eyeState);
            if (inputs.Count == 1)
            {
                string key = EyeStates.FirstOrDefault(x => x.Value == eyeState).Key;

                Action1Label.Invoke((MethodInvoker)(() =>
                {
                    Action1Label.Text = key;
                    Action1Label.Show();
                }));
            }
            else if (inputs.Count == 2)
            {
                string key = EyeStates.FirstOrDefault(x => x.Value == eyeState).Key;

                Action2Label.Invoke((MethodInvoker)(() =>
                {
                    Action2Label.Text = key;
                    Action2Label.Show();
                }));

            }
            else if (inputs.Count == 3)
            {

                string key = EyeStates.FirstOrDefault(x => x.Value == eyeState).Key;

                Action3Label.Invoke((MethodInvoker)(() =>
                {
                    Action3Label.Text = key;
                    Action3Label.Show();
                }));
            }

        }

        private void ClearInputs()
        {
            inputs.Clear();
            HideActionLabels();
        }


        //To receive command from others Forms
        public void SendCommand(int command)
        {
            string c = command.ToString();
            CheckCommand(c);
        }

        //To receive command from others Forms
        public void SendCommand(string command)
        {
            CheckCommand(command);
        }

        //To receive command from others Forms
        public void SendCommand(Command command)
        {
            CheckCommand(command.Actions);
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
            string c = string.Format("{0}{1}{2}", inputs[x - 3], inputs[x - 2], inputs[x - 1]);
            CheckCommand(c);
        }

        private void InvokeCommand(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                try
                {
                    StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(InvokeCommand);
                    this.Invoke(d, new object[] { text });
                }
                catch (ObjectDisposedException e)
                {
                    //If somebody finds me, this poor unloved exception that was thrown to a corner and forgotten, REMEMBER ME!!!
                }
            }
            else
            {
                SendKeys.Send(text);
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
}
