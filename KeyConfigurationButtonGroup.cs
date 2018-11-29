using System.Collections.Generic;
using System.Windows.Forms;

namespace EyeTracker
{
    internal class KeyConfigurationButtonGroup
    {
        private readonly ComboBox firstComboBox;
        private readonly ComboBox secondComboBox;
        private readonly ComboBox thirdComboBox;

        public KeyConfigurationButtonGroup(ComboBox firstComboBox, ComboBox secondComboBox, ComboBox thirdComboBox)
        {
            this.firstComboBox = firstComboBox;
            this.secondComboBox = secondComboBox;
            this.thirdComboBox = thirdComboBox;
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
    }
}
