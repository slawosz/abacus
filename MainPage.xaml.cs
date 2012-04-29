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
using ShakeGestures;

namespace PhoneApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        Abacus abacus;
        public static bool shaked = false;

        public MainPage()
        {
            InitializeComponent();
            abacus = new Abacus(9);
            ShakeGesturesHelper.Instance.ShakeGesture += new EventHandler<ShakeGestureEventArgs>(Instance_ShakeGesture);

            ShakeGesturesHelper.Instance.MinimumRequiredMovesForShake = 3;
            ShakeGesturesHelper.Instance.Active = true;
            Container.Children.Add(abacus.GetContainer());
        }

        private void Instance_ShakeGesture(object sender, ShakeGestureEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                abacus.Reset();
            });
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