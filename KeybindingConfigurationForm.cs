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
       

        Form1 form;
        KeyConfigurationButtonGroup copyBindingButtonGroup;

        public KeybindingConfigurationForm(Form1 form, List<Command> commands)
        {
            InitializeComponent();

            copyBindingButtonGroup = new KeyConfigurationButtonGroup(commands[1], copyBindingFirstAction, copyBindingSecondAction, copyBindingThirdAction);
            this.form = form;
        }

        private void copyBindingButton_Click(object sender, EventArgs e)
        {
            copyBindingButtonGroup.ToggleMode();
            if (copyBindingButtonGroup.InEditMode())
            {
                copyBindingButton.Text = "Save";
            } else
            {
                copyBindingButton.Text = "Edit";
            }
        }
    }
}
