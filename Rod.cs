using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace PhoneApp1
{
    public class Rod
    {
        public int beadSize;
        public int beadsCount;
        public int freeSpace;
        public int value;
        public int rodWidth;
        public int distinctSpace;
        private Dictionary<int, int> beadsValues = new Dictionary<int,int>();
      
        Canvas rod = new Canvas();
        Bead[] beads;

        public Rod(int beadsCount)
        {
            beadSize = 50;
            this.beadsCount = beadsCount;
            freeSpace = 70;
            value = 0;
            beads = new Bead[beadsCount];
            rodWidth = beadSize * beadsCount + freeSpace;
            distinctSpace = (int)((Double)freeSpace * 0.75);
            BuildBeads();
            LinkBeads();
            AddBeadsToRod();
            PrepareBeadsValuesDictionary();
            SetColor();
            Canvas.SetLeft(rod, 0);
            rod.Width = rodWidth;
            rod.Height = beadSize;
        }

        public Canvas GetRod()
        {
            return this.rod;
        }

        private void BuildBeads()
        {
            for (int i = 0; i < beadsCount; i++)
            {
                beads[i] = new Bead(this, i);
            }
        }

        private void LinkBeads()
        {
            for (int i = 1; i < beadsCount; i++)
            {
                beads[i - 1].SetRightBead(beads[i]);   
            }
        }

        private void AddBeadsToRod()
        {
            for (int i = 0; i < beadsCount; i++)
            {
                rod.Children.Add(beads[i].GetBead());
            }
        }

        private void SetColor()
        {
            Color c = new Color();
            c.R = 255;
            c.B = 0;
            c.G = 0;
            c.A = 255;

            this.rod.Background = new SolidColorBrush(c); 
        }

        private void PrepareBeadsValuesDictionary()
        {
            int actualValue = beadsCount;
            for (int i = 0; i < beadsCount; i++)
            {
                beadsValues.Add(i, actualValue);
                actualValue--;
            }
        }

        public void SetRodValue() 
        {
            value = 0;
            for (int i = 0; i < beadsCount - 1; i++)
            {
                if (i == 0 && Canvas.GetLeft(beads[i].GetBead()) > distinctSpace)
                {
                    beadsValues.TryGetValue(i, out value);
                }
                else
                {
                    if ((Canvas.GetLeft(beads[i + 1].GetBead()) - (Canvas.GetLeft(beads[i].GetBead()) + beadSize)) > distinctSpace)
                    {
                        beadsValues.TryGetValue(i + 1, out value);
                    }
                }
            }
            if (beadsCount == 1 && Canvas.GetLeft(beads[0].GetBead()) > distinctSpace)
            {
                value = 1;
            }
            System.Diagnostics.Debug.WriteLine(value.ToString());
        }

    }
}
