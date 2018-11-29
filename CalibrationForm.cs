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

        public CalibrationForm(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private void CalibrationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            form1.calibration.isCalibrating = false;
        }
    }
}
