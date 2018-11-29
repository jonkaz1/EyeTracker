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
    public partial class KeybindingConfigurationForm : Form
    {
        public static SortedDictionary<string, int> eyeStates = new SortedDictionary<string, int>
{
  {"L", 1},
  {"R", 2},
  {"LR", 3}
};

        Form1 form;
        public KeybindingConfigurationForm(Form1 form)
        {
            InitializeComponent();
            BindComboBoxSource(copyBindingFirstAction, eyeStates);
            BindComboBoxSource(copyBindingSecondAction, eyeStates);
            BindComboBoxSource(copyBindingThirdAction, eyeStates);
           

            this.form = form;
        }

        private void BindComboBoxSource(ComboBox comboBox, SortedDictionary<string, int> keyValuePairs)
        {
            comboBox.DataSource = new BindingSource(keyValuePairs, null);
            comboBox.DisplayMember = "Key";
            comboBox.ValueMember = "Value";
        }
    }
}
