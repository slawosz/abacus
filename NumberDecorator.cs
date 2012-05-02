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
using System.Globalization;

namespace PhoneApp1
{
    public class NumberDecorator
    {
        int number;

        public NumberDecorator(int number)
        {
            this.number = number;
        }

        public String Decorate()
        {
            NumberFormatInfo nfi = new CultureInfo("en-US").NumberFormat;
            return number.ToString("N0", nfi);
        }

    }
}