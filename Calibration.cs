using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTracker
{
    public class Calibration
    {
        public long leftEyeBlinkTime, rightEyeBlinkTime, BothEyeBlinkTime;
        public bool isCalibrating;
        public List<long> leftEyeTimeList, rightEyeTimeList, BothEyeTimeList;
        public Calibration()
        {
            leftEyeBlinkTime = 10000;
            rightEyeBlinkTime = 10000;
            BothEyeBlinkTime = 10000;
            leftEyeTimeList = new List<long>();
            rightEyeTimeList = new List<long>();
            BothEyeTimeList = new List<long>();
            isCalibrating = false;
        }
        public void toggleCalinbrating()
        {
            isCalibrating = !isCalibrating;
        }
        public void calculateAverageBlinkTime()
        {
            if (leftEyeTimeList.Count > 0)
            {
                foreach (var time in leftEyeTimeList)
                {
                    leftEyeBlinkTime += time;
                }
                leftEyeBlinkTime /= leftEyeTimeList.Count;
            }

            if (rightEyeTimeList.Count > 0)
            {
                foreach (var time in rightEyeTimeList)
                {
                    rightEyeBlinkTime += time;
                }
                rightEyeBlinkTime /= rightEyeTimeList.Count;
            }

            if (BothEyeTimeList.Count > 0)
            {
                foreach (var time in BothEyeTimeList)
                {
                    BothEyeBlinkTime += time;
                }
                BothEyeBlinkTime /= BothEyeTimeList.Count;
            }
            
            Console.WriteLine("L{0}  R{1}  B{2}", leftEyeTimeList.Count, rightEyeTimeList.Count, BothEyeTimeList.Count);

            leftEyeTimeList.Clear();
            rightEyeTimeList.Clear();
            BothEyeTimeList.Clear();
        }

    }
}
