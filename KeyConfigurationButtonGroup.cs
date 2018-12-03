using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EyeTracker
{
    internal class KeyConfigurationButtonGroup
    {
        private readonly ComboBox firstComboBox;
        private readonly ComboBox secondComboBox;
        private readonly ComboBox thirdComboBox;

        private bool beingEdited;

        public KeyConfigurationButtonGroup(ComboBox firstComboBox, ComboBox secondComboBox, ComboBox thirdComboBox)
        {
            this.firstComboBox = firstComboBox;
            this.secondComboBox = secondComboBox;
            this.thirdComboBox = thirdComboBox;
            beingEdited = false;
        }

        public void LoadCommand(int command)
        {
            firstComboBox.SelectedValue = GetIntArray(command)[0];
            secondComboBox.SelectedValue = GetIntArray(command)[1];
            thirdComboBox.SelectedValue = GetIntArray(command)[2];
        }

        public int getCommand()
        {
            return ((int)firstComboBox.SelectedValue * 100) + ((int)secondComboBox.SelectedValue * 10) + (int)thirdComboBox.SelectedValue;
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
            } else
            {
                EnableEditing();
            }
            beingEdited = !beingEdited;
        }

        private void EnableEditing()
        {
            firstComboBox.Enabled = true;
            secondComboBox.Enabled = true;
            thirdComboBox.Enabled = true;
        }

        private void DisableEditing()
        {
            firstComboBox.Enabled = false;
            secondComboBox.Enabled = false;
            thirdComboBox.Enabled = false;
        }

        private void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
