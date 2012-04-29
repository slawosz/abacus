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
    public class Row
    {

        private StackPanel rowContainer = new StackPanel();
        
        Rod bottomRod = new Rod(4);
        Rod upperRod  = new Rod(1);
        int multiplicity;

        public Row(int multiplicity)
        {
            rowContainer.Width = 400;
            rowContainer.Orientation = Orientation.Horizontal;
            this.multiplicity = multiplicity;
            Canvas spacer = new Canvas();
            spacer.Width = 20; 
            rowContainer.Children.Add(bottomRod.GetRod());
            rowContainer.Children.Add(spacer);
            rowContainer.Children.Add(upperRod.GetRod());
        }

        public StackPanel GetRowElement() {
            return rowContainer;
        }

        public int GetRowValue() 
        {
            int value = 0;
            if (upperRod.GetRodValue() == 1)
            {
                value += 5;
            }

            return (value + bottomRod.GetRodValue()) * multiplicity;
        }

        public void Reset()
        {
            bottomRod.Reset();
            upperRod.Reset();
        }

    }
}
