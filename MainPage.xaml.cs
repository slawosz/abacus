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
using System.Windows.Threading;

namespace PhoneApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        Abacus abacus;
        public static bool shaked = false;
        DispatcherTimer timer = new DispatcherTimer();
        int level = 40;
        int ticks = 0;

        public MainPage()
        {
            InitializeComponent();
            abacus = new Abacus(9);
            ShakeGesturesHelper.Instance.ShakeGesture += new EventHandler<ShakeGestureEventArgs>(Instance_ShakeGesture);

            ShakeGesturesHelper.Instance.MinimumRequiredMovesForShake = 3;
            ShakeGesturesHelper.Instance.Active = true;
            Container.Children.Add(abacus.GetContainer());
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500); //.FromSeconds(2);
            timer.Tick += new EventHandler(TimerTick);

            ProgressBar.Maximum = 100;
            ProgressBar.Value = 0;
        }

        void StartGame()
        {
            timer.Start();
        }

        void StopGame()
        {
            timer.Stop();
            ProgressBar.Value = 0;
            
        }

        void TimerTick(object sender, EventArgs e)
        {
            ticks++;
            ProgressBar.Value = ticks * 2;
            System.Diagnostics.Debug.WriteLine("timer");
            if (ticks == level)
            {
                StopGame();
            }
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

        private void Button_Tap(object sender, GestureEventArgs e)
        {

            StartGame();
        }

    }
}