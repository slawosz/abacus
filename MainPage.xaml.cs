using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace PhoneApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            for (int i = 0; i < 6; i++)
            {
                Rod rod = new Rod(i + 1);
                Container.Children.Add(rod.GetRod());
            }
        }

        public void PhoneApplicationPage_IsEnabledChanged()
        {
        }

        private void LayoutRoot_KeyUp(object sender, KeyEventArgs e)
        {
        }

    }
}