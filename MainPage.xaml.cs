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
            Rod rod = new Rod();    
            Rod shortRod = new ShortRod();    
            ContentPanel.Children.Add(rod.GetRod());
            ContentPanel.Children.Add(shortRod.GetRod());
        }

        public void PhoneApplicationPage_IsEnabledChanged()
        {
        }

        private void LayoutRoot_KeyUp(object sender, KeyEventArgs e)
        {
        }

    }
}