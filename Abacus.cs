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
    public class Abacus
    {
        int rowsCount;
        StackPanel container = new StackPanel();
        Row[] rows;

        public Abacus(int rowsCount)
        {
            int multiplicity = 1;
            for (int i = 0; i < rowsCount; i++)
            {
                multiplicity = multiplicity * 10;
            }
            this.rowsCount = rowsCount;
            rows = new Row[rowsCount];
            for (int i = rowsCount - 1; i >= 0; i--)
            {
                multiplicity = multiplicity / 10;
                System.Diagnostics.Debug.WriteLine(multiplicity);
                rows[i] = new Row(multiplicity);
                Canvas spacer = new Canvas();
                spacer.Height = 2;
                container.Children.Add(rows[i].GetRowElement());
                container.Children.Add(spacer);
            }
        }

        public StackPanel GetContainer()
        {
            return container;
        }

        public int GetValue() {
            int value = 0;
            for (int i = 0; i < rowsCount; i++)
            {
                value += rows[i].GetRowValue();
            }
            return value;
        }

        public void Reset()
        {
            for (int i = 0; i < rowsCount; i++)
            {
                rows[i].Reset();
            }
        }


    }
}
