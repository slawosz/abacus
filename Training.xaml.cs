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
    public partial class Training : PhoneApplicationPage
    {
        Abacus abacus;
        public static EventHandler Started;
        public static EventHandler Ended;
        public static bool active;

        public Training()
        {
            InitializeComponent();
            abacus = new Abacus(9);
            Container.Children.Add(abacus.GetContainer());

            ShakeGesturesHelper.Instance.MinimumRequiredMovesForShake = 3;
            ShakeGesturesHelper.Instance.Active = true;
            ShakeGesturesHelper.Instance.ShakeGesture += new EventHandler<ShakeGestureEventArgs>(ResetOnShake);

            EventBinder.Bind(ref Bead.BeadMoved, UpdateValue);
        }
        
        public void OnStarted(EventArgs e)
        {
            if (Started != null)
            {
                Training.active = true;
                Started(this, e);
            }
        }
        
        public void OnEnded(EventArgs e)
        {
            if (Ended != null)
            {
                Training.active = false;
                Ended(this, e);
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            NavigationService.RemoveBackEntry();
            OnStarted(EventArgs.Empty);
        }

        private void ResetOnShake(object sender, ShakeGestureEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                abacus.Reset();
                Number.Text = "0";
            });
        }

        private void UpdateValue(object sender, EventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                Number.Text = abacus.GetValue().ToString();
            });
        }

        private void GoToMenu(object sender, GestureEventArgs e)
        {
            OnEnded(EventArgs.Empty);
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

    }
}