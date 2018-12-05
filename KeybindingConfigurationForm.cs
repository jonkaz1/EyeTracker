using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EyeTracker
{
    public partial class KeybindingConfigurationForm : Form
    {
        private Form1 form;
        private List<Command> commands;
        private KeyBindingUIElementGroup row1ButtonGroup;
        private KeyBindingUIElementGroup row2ButtonGroup;
        private KeyBindingUIElementGroup row3ButtonGroup;
        private KeyBindingUIElementGroup row4ButtonGroup;
        private KeyBindingUIElementGroup row5ButtonGroup;
        private KeyBindingUIElementGroup row6ButtonGroup;
        private KeyBindingUIElementGroup row7ButtonGroup;
        private KeyBindingUIElementGroup row8ButtonGroup;
        private KeyBindingUIElementGroup row9ButtonGroup;

        public KeybindingConfigurationForm(Form1 form, List<Command> commands)
        {
            InitializeComponent();
            LoadCommands(commands);

            this.commands = commands;
            this.form = form;
        }

        private void LoadCommands(List<Command> commands)
        {
            row1ButtonGroup = RegisterUIElementGroup(0, commands, Row1Label, Row1Action1, Row1Action2, Row1Action3, Row1Button);
            row2ButtonGroup = RegisterUIElementGroup(1, commands, Row2Label, Row2Action1, Row2Action2, Row2Action3, Row2Button);
            row3ButtonGroup = RegisterUIElementGroup(2, commands, Row3Label, Row3Action1, Row3Action2, Row3Action3, Row3Button);
            row4ButtonGroup = RegisterUIElementGroup(3, commands, Row4Label, Row4Action1, Row4Action2, Row4Action3, Row4Button);
            row5ButtonGroup = RegisterUIElementGroup(4, commands, Row5Label, Row5Action1, Row5Action2, Row5Action3, Row5Button);
            row6ButtonGroup = RegisterUIElementGroup(5, commands, Row6Label, Row6Action1, Row6Action2, Row6Action3, Row6Button);
            row7ButtonGroup = RegisterUIElementGroup(6, commands, Row7Label, Row7Action1, Row7Action2, Row7Action3, Row7Button);
            row8ButtonGroup = RegisterUIElementGroup(7, commands, Row8Label, Row8Action1, Row8Action2, Row8Action3, Row8Button);
            row9ButtonGroup = RegisterUIElementGroup(8, commands, Row9Label, Row9Action1, Row9Action2, Row9Action3, Row9Button);
        }

        private KeyBindingUIElementGroup RegisterUIElementGroup(int index, List<Command> commands, Label label, ComboBox action1, ComboBox action2, ComboBox action3, Button button)
        {
            if (commands.Count >= index)
            {
                return new KeyBindingUIElementGroup(commands[index], label, action1, action2, action3, button);
            }
            else
            {
                return new KeyBindingUIElementGroup(null, label, action1, action2, action3, button);
            }
        }

        private void Row1Button_Click(object sender, EventArgs e)
        {
            row1ButtonGroup.ToggleMode();
        }

        private void Row2Button_Click(object sender, EventArgs e)
        {
            row2ButtonGroup.ToggleMode();
        }

        private void Row3Button_Click(object sender, EventArgs e)
        {
            row3ButtonGroup.ToggleMode();
        }

        private void Row4Button_Click(object sender, EventArgs e)
        {
            row4ButtonGroup.ToggleMode();
        }

        private void Row5Button_Click(object sender, EventArgs e)
        {
            row5ButtonGroup.ToggleMode();
        }

        private void Row6Button_Click(object sender, EventArgs e)
        {
            row6ButtonGroup.ToggleMode();
        }

        private void Row7Button_Click(object sender, EventArgs e)
        {
            row7ButtonGroup.ToggleMode();
        }
  
        private void Row8Button_Click(object sender, EventArgs e)
        {
            row8ButtonGroup.ToggleMode();
        }

        private void Row9Button_Click(object sender, EventArgs e)
        {
            row9ButtonGroup.ToggleMode();
        }

        private void RestoreDefaultsButton_Click(object sender, EventArgs e)
        {
            // Restore defaults and emediatelly save them to file, for future use.
            LoadCommands(Form1.GetInstance().GetDefaultCommands());
            Form1.GetInstance().SaveCommandsToFile();
        }
    }
}
