using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADayAtTheRaces
{
    public class Greyhound
    {
        public int dogNumber;
        public int StartingPosition;
        public int RacetrackLength;
        public PictureBox MyPictureBox = null;
        public int Location = 0;
        public Random Randomizer;

        public bool Run()
        {
            int distance;
            distance = Randomizer.Next(1, 4);

            Point myPointer = MyPictureBox.Location;
            myPointer.X += distance;
            MyPictureBox.Location = myPointer;
            if (myPointer.X >= RacetrackLength)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void TakeStartingPosition()
        {
            Point pointer = MyPictureBox.Location;
            pointer.X = StartingPosition;
            MyPictureBox.Left = StartingPosition;
        }
    }
}
