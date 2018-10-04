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

namespace EyeTracker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            int posX = 0, posY = 0;

            InitializeComponent();
            var host = new Host();
            var gazePointDataStream = host.Streams.CreateGazePointDataStream();
            gazePointDataStream.GazePoint((gazePointX, gazePointY, _) => textBox1.Text = String.Format("X: {0} Y:{1}", gazePointX, gazePointY));
            gazePointDataStream.GazePoint((gazePointX, gazePointY, _) => { posX = (int)gazePointX; posY = (int)gazePointY; Cursor.Position = new Point(posX, posY); });

        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
