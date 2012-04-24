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
        Abacus abacus;
        public MainPage()
        {
            InitializeComponent();
            abacus = new Abacus(9);

            Container.Children.Add(abacus.GetContainer());
        }

        public void PhoneApplicationPage_IsEnabledChanged()
        {
        }

        private void LayoutRoot_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void PageTitle_Tap(object sender, GestureEventArgs e)
        {
            PageTitle.Text = abacus.GetValue().ToString();
        }

    }
}