using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTracker
{
    public class Calibration
    {
        public int leftEyeBlinkTime, rightEyeBlinkTime, BothEyeBlinkTime;
        public bool isCalibrating;
        public List <int> timeList;
        public Calibration()
        {
            leftEyeBlinkTime = 60;
            rightEyeBlinkTime = 60;
            BothEyeBlinkTime = 60;
            isCalibrating = false;
        }
        public void toggleCalinbrating()
        {
            isCalibrating = !isCalibrating;
        }

    }
}
