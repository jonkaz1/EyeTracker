﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tobii.Interaction;
using System.Threading;


namespace EyeTracker
{
    class Mouse
    {
        //start of import for gaze postion
        public int posX, posY, cursorX, cursorY;
        public bool isCursorActive, isMoveSlowly;
        static Host host = new Host(); 
        GazePointDataStream gazePointDataStream = host.Streams.CreateGazePointDataStream(); //changed from var to GazePointDataStream
        //end of import for gaze postion

        //start of imports for mouse clicks
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        public const int MOUSEEVENTF_RIGHTUP = 0x10;
        //end of imports for mouse clicks

        public Mouse()
        {
            posX = 0;
            posY = 0;
            cursorX = 0;
            cursorY = 0;
            isCursorActive = false;
            isMoveSlowly = false;

            gazePointDataStream.GazePoint((gazePointX, gazePointY, _) => { posX = (int)gazePointX; posY = (int)gazePointY; });
            //Cursor.Position = new Point(posX, posY);
        }
        public void ToggleCursorGaze()
        {
            if (isCursorActive)
            {
                isCursorActive = false;
            }
            else
            {
                isCursorActive = true;
            }
        }
        public void SetCursorPosition()
        {
            if (isCursorActive)
            {
                if (isMoveSlowly)
                {
                    Cursor.Position = new Point(cursorX, cursorY);
                }
                else
                {
                    Cursor.Position = new Point(posX, posY);
                }
            }
        }

        public void saveCursorPosition()
        {
            cursorX = posX;
            cursorY = posY;
            isMoveSlowly = true;
        }
        public void moveCursorSlowly()
        {
            if (posX > cursorX)
            {
                cursorX += 2;
                if (posY > cursorY)
                {
                    cursorY += 2;
                }
                else
                {
                    cursorY -= 2;
                }
            }
            else if (posX < cursorX)
            {
                cursorX -= 2;
                if (posY > cursorY)
                {
                    cursorY += 2;
                }
                else
                {
                    cursorY -= 2;
                }
            }
        }

        public void LeftClick(int PositionX, int PositionY)
        {
            SetCursorPos(PositionX, PositionY);
            mouse_event(MOUSEEVENTF_LEFTDOWN, PositionX, PositionY, 0, 0);
            System.Threading.Thread.Sleep(50);
            mouse_event(MOUSEEVENTF_LEFTUP, PositionX, PositionY, 0, 0);
        }
        public void RightClick(int PositionX, int PositionY)
        {
            SetCursorPos(PositionX, PositionY);
            mouse_event(MOUSEEVENTF_RIGHTDOWN, PositionX, PositionY, 0, 0);
            System.Threading.Thread.Sleep(50);
            mouse_event(MOUSEEVENTF_RIGHTUP, PositionX, PositionY, 0, 0);
        }
    }
}