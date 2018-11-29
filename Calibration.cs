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
        public long leftEyeBlinkTime2, rightEyeBlinkTime2, BothEyeBlinkTime2, leftMax2, rightMax2, BothMax2;
        public long forcedLeftBlinkTime, forcedRightBlinkTime, forcedBothBlinkTime;
        public bool isCalibrating, isSecondTime, isCalibrationDone;
        public int time;
        public List<long> leftEyeTimeList, rightEyeTimeList, BothEyeTimeList;
        public List<long> leftEyeTimeList2, rightEyeTimeList2, BothEyeTimeList2;
        public Calibration()
        {
            leftEyeBlinkTime = 4500000;
            rightEyeBlinkTime = 4500000;
            BothEyeBlinkTime = 2000000;
            time = 20;
            leftEyeTimeList = new List<long>();
            rightEyeTimeList = new List<long>();
            BothEyeTimeList = new List<long>();
            leftEyeTimeList2 = new List<long>();
            rightEyeTimeList2 = new List<long>();
            BothEyeTimeList2 = new List<long>();
            isCalibrating = false;
            isSecondTime = false;
            isCalibrationDone = false;
            setBlinkTimeFromMemory();
        }
        public void toggleCalinbrating()
        {
            isCalibrating = !isCalibrating;
        }
        public void setBlinkTimeFromMemory()
        {
            rightEyeBlinkTime = (long)Properties.Settings.Default["rEyeBlinkMax"];
            leftEyeBlinkTime = (long)Properties.Settings.Default["lEyeBlinkMax"];
            BothEyeBlinkTime = (long)Properties.Settings.Default["bEyeBlinkMax"];
        }
        public void saveBlinkTimeToMemory()
        {
            Properties.Settings.Default["rEyeBlinkMax"] = rightEyeBlinkTime;
            Properties.Settings.Default["lEyeBlinkMax"] = leftEyeBlinkTime;
            Properties.Settings.Default["bEyeBlinkMax"] = BothEyeBlinkTime;
            Properties.Settings.Default.Save();
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




            if (leftEyeTimeList2.Count > 0)
            {
                leftMax2 = leftEyeTimeList2.Min();

                foreach (var time in leftEyeTimeList2)
                {
                    leftEyeBlinkTime2 += time;
                }
                leftEyeBlinkTime2 /= leftEyeTimeList2.Count;

                if (leftMax2 < leftEyeBlinkTime2 * 0.66)
                {
                    leftMax2 = leftEyeBlinkTime2 / 3 * 2;
                }
            }

            if (rightEyeTimeList2.Count > 0)
            {
                rightMax2 = rightEyeTimeList2.Min();

                foreach (var time in rightEyeTimeList2)
                {
                    rightEyeBlinkTime2 += time;
                }
                rightEyeBlinkTime2 /= rightEyeTimeList2.Count;

                if (rightMax2 > rightEyeBlinkTime2 * 0.66)
                {
                    rightMax2 = rightEyeBlinkTime2 /3 * 2;
                }
            }

            if (BothEyeTimeList2.Count > 0)
            {
                BothMax2 = BothEyeTimeList2.Min();

                foreach (var time in BothEyeTimeList2)
                {
                    BothEyeBlinkTime2 += time;
                }
                BothEyeBlinkTime2 /= BothEyeTimeList2.Count;

                if (BothMax2 > BothEyeBlinkTime2 * 0.66)
                {
                    BothMax2 = BothEyeBlinkTime2 / 3 * 2;
                }
            }

            leftEyeBlinkTime = leftMax * 2;
            rightEyeBlinkTime = rightMax * 2;
            BothEyeBlinkTime = (BothMax + BothMax2) / 2;


            Console.WriteLine("L{0}  R{1}  B{2}", leftEyeTimeList.Count, rightEyeTimeList.Count, BothEyeTimeList.Count);

            leftEyeTimeList.Clear();
            rightEyeTimeList.Clear();
            BothEyeTimeList.Clear();
        }

    }
}
