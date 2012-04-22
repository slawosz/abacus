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
        public static readonly int BEAD_SIZE = 50;
        public static virtual int BEADS_COUNT { get { return 4; } }
        public static readonly int FREE_SPACE = 70;
        public static readonly int ROD_WIDTH = BEAD_SIZE * BEADS_COUNT + FREE_SPACE;
        public static readonly int DISTINCT_SPACE = (int)((Double)FREE_SPACE * 0.75);
        public int value = 0;
        private Dictionary<int, int> beadsValues = new Dictionary<int,int>();
      
        Canvas rod = new Canvas();
        Bead[] beads = new Bead[BEADS_COUNT];

        public Rod()
        {
            BuildBeads();
            LinkBeads();
            AddBeadsToRod();
            PrepareBeadsValuesDictionary();
            SetColor();
            Canvas.SetLeft(rod, 0);
            rod.Width = ROD_WIDTH;
            rod.Height = BEAD_SIZE;
        }

        public Canvas GetRod()
        {
            return this.rod;
        }

        private void BuildBeads()
        {
            for (int i = 0; i < BEADS_COUNT; i++)
            {
                beads[i] = new Bead(this, BEAD_SIZE, BEAD_SIZE * i);
            }
        }

        private void LinkBeads()
        {
            for (int i = 1; i < BEADS_COUNT; i++)
            {
                beads[i - 1].SetRightBead(beads[i]);   
            }
        }

        private void AddBeadsToRod()
        {
            for (int i = 0; i < BEADS_COUNT; i++)
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
            int actualValue = BEADS_COUNT;
            for (int i = 0; i < BEADS_COUNT; i++)
            {
                beadsValues.Add(i, actualValue);
                actualValue--;
            }
        }

        public void SetRodValue() 
        {
            value = 0;
            for (int i = 0; i < BEADS_COUNT - 1; i++)
            {
                if (i == 0 && Canvas.GetLeft(beads[i].GetBead()) > DISTINCT_SPACE)
                {
                    beadsValues.TryGetValue(i, out value);
                }
                else
                {
                    if ((Canvas.GetLeft(beads[i + 1].GetBead()) - (Canvas.GetLeft(beads[i].GetBead()) + BEAD_SIZE)) > DISTINCT_SPACE)
                    {
                        beadsValues.TryGetValue(i + 1, out value);
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine(value.ToString());
            System.Diagnostics.Debug.WriteLine(beadsValues.ToString());
        }

    }
}
