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

namespace PhoneApp1
{
    public class Rod
    {
        public static int BEAD_SIZE = 50;
        public static int BEADS_COUNT = 4;
        public static int FREE_SPACE = 70;
        public static int ROD_WIDTH = BEAD_SIZE * BEADS_COUNT + FREE_SPACE;
        public static int DISTINCT_SPACE = (int)((Double)FREE_SPACE * 0.75);
        public Int16 value = 0;
      
        Canvas rod = new Canvas();
        Bead[] beads = new Bead[BEADS_COUNT];

        public Rod()
        {
            BuildBeads();
            LinkBeads();
            AddBeadsToRod();
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

        public void SetRodValue() 
        {
            value = 0;
            for (int i = 0; i < BEADS_COUNT - 1; i++)
            {
                if ((Canvas.GetLeft(beads[i + 1].GetBead()) - (Canvas.GetLeft(beads[i].GetBead()) + BEAD_SIZE)) > DISTINCT_SPACE)
                {
                    value = (Int16)(i + 1);
                }
            }
            System.Diagnostics.Debug.WriteLine(value.ToString());
        }

    }
}
