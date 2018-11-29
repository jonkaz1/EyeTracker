using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTracker
{
    public class Calibration
    {
        public long leftEyeBlinkTime, rightEyeBlinkTime, BothEyeBlinkTime, leftMax, rightMax, BothMax;
        public bool isCalibrating;
        public List<long> leftEyeTimeList, rightEyeTimeList, BothEyeTimeList;
        public Calibration()
        {
            leftEyeBlinkTime = 4500000;
            rightEyeBlinkTime = 4500000;
            BothEyeBlinkTime = 2000000;
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
                leftMax = leftEyeTimeList.Max();

                foreach (var time in leftEyeTimeList)
                {
                    leftEyeBlinkTime += time;
                }
                leftEyeBlinkTime /= leftEyeTimeList.Count;

                if (leftMax > leftEyeBlinkTime * 1.3)
                {
                    leftMax = leftEyeBlinkTime + (leftEyeBlinkTime / 3);
                }
            }

            if (rightEyeTimeList.Count > 0)
            {
                rightMax = rightEyeTimeList.Max();

                foreach (var time in rightEyeTimeList)
                {
                    rightEyeBlinkTime += time;
                }
                rightEyeBlinkTime /= rightEyeTimeList.Count;

                if (rightMax > rightEyeBlinkTime * 1.3)
                {
                    rightMax = rightEyeBlinkTime + (rightEyeBlinkTime / 3);
                }
            }

            if (BothEyeTimeList.Count > 0)
            {
                BothMax = BothEyeTimeList.Max();

                foreach (var time in BothEyeTimeList)
                {
                    BothEyeBlinkTime += time;
                }
                BothEyeBlinkTime /= BothEyeTimeList.Count;

                if (BothMax > BothEyeBlinkTime * 1.3)
                {
                    BothMax = BothEyeBlinkTime + (BothEyeBlinkTime / 3);
                }
            }
            
            Console.WriteLine("L{0}  R{1}  B{2}", leftEyeTimeList.Count, rightEyeTimeList.Count, BothEyeTimeList.Count);

            leftEyeTimeList.Clear();
            rightEyeTimeList.Clear();
            BothEyeTimeList.Clear();
        }

    }
}
