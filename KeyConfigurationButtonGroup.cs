using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EyeTracker
{
    internal class KeyConfigurationButtonGroup
    {
        private static SortedDictionary<string, int> eyeStates = new SortedDictionary<string, int>
{
  {"LR", 0},
  {"L", 1},
  {"R", 2}
};


        private readonly ComboBox FirstComboBox;
        private readonly ComboBox SecondComboBox;
        private readonly ComboBox ThirdComboBox;
        private readonly Label NameLabel;

        private bool beingEdited = false;

        public Command Command { get; private set; }

        public KeyConfigurationButtonGroup(Command command, ComboBox firstComboBox, ComboBox secondComboBox, ComboBox thirdComboBox, Label nameLabel)
        {
            Command = command;
            FirstComboBox = firstComboBox;
            SecondComboBox = secondComboBox;
            ThirdComboBox = thirdComboBox;
            NameLabel = nameLabel;

            BindComboBoxSource(firstComboBox, eyeStates);
            BindComboBoxSource(secondComboBox, eyeStates);
            BindComboBoxSource(thirdComboBox, eyeStates);

            ImportValues();
            DisableEditing();
        }

        private void ImportValues()
        {
            NameLabel.Text = Command.Name;
            FirstComboBox.SelectedValue = (int)char.GetNumericValue(Command.Actions[0]); 
            SecondComboBox.SelectedValue = (int)char.GetNumericValue(Command.Actions[1]);
            ThirdComboBox.SelectedValue = (int)char.GetNumericValue(Command.Actions[2]);
        }

        private string ExportActions()
        {
            return string.Format("{0}{1}{2}", FirstComboBox.SelectedValue, SecondComboBox.SelectedValue, ThirdComboBox.SelectedValue);
        }

        private int[] GetIntArray(int num)
        {
            List<int> listOfInts = new List<int>();
            while (num > 0)
            {
                listOfInts.Add(num % 10);
                num = num / 10;
            }
            listOfInts.Reverse();
            return listOfInts.ToArray();
        }

        internal bool InEditMode()
        {
            return beingEdited;
        }

        public void ToggleMode()
        {
            if (beingEdited)
            {
                SaveChanges();
                DisableEditing();
            }
            else
            {
                EnableEditing();
            }
            beingEdited = !beingEdited;
        }

        private void EnableEditing()
        {
            FirstComboBox.Enabled = true;
            SecondComboBox.Enabled = true;
            ThirdComboBox.Enabled = true;
        }

        private void DisableEditing()
        {
            FirstComboBox.Enabled = false;
            SecondComboBox.Enabled = false;
            ThirdComboBox.Enabled = false;
        }

        private void SaveChanges()
        {
            Command.Actions = ExportActions();
        }


        private void BindComboBoxSource(ComboBox comboBox, SortedDictionary<string, int> keyValuePairs)
        {
            comboBox.DataSource = new BindingSource(keyValuePairs, null);
            comboBox.DisplayMember = "Key";
            comboBox.ValueMember = "Value";
        }

    }
}
