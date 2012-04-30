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
using System.Windows.Controls.Primitives;

namespace PhoneApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        Abacus abacus;
        DispatcherTimer timer = new DispatcherTimer();
        int level = 40;
        int ticks = 0;
        int numberToGuess;
        public static EventHandler GameStarted;
        public static EventHandler GameEnded;
        bool gameWon;
        SettingsWindow settingsWindow;
        public int maxNumber = 999;

        public MainPage()
        {
            InitializeComponent();
            abacus = new Abacus(9);
            ShakeGesturesHelper.Instance.ShakeGesture += new EventHandler<ShakeGestureEventArgs>(Instance_ShakeGesture);
            settingsWindow = new SettingsWindow(this);

            ShakeGesturesHelper.Instance.MinimumRequiredMovesForShake = 3;
            ShakeGesturesHelper.Instance.Active = true;
            Container.Children.Add(abacus.GetContainer());
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500); //.FromSeconds(2);
            timer.Tick += new EventHandler(TimerTick);

            ProgressBar.Maximum = 100;
            ProgressBar.Value = 0;
            EventBinder.Bind(ref Bead.BeadMoved, CheckIfNumberGuessed);
            // Bead.BeadMoved += new EventHandler(UpdateScore);
        }

        public void OnGameStarted(EventArgs e)
        {
            if (GameStarted != null)
            {
                GameStarted(this, e);
            }
        }
        
        public void OnGameEnded(EventArgs e)
        {
            if (GameEnded != null)
            {
                GameEnded(this, e);
            }
        }
        void StartGame()
        {
            gameWon = false;
            abacus.Reset();
            ticks = 0;
            timer.Start();
            ProgressBar.Value = 0;
            DrawNumberToGuess();
            OnGameStarted(EventArgs.Empty);
        }

        private void DrawNumberToGuess()
        {
            Random random = new Random();
            numberToGuess = random.Next(0, maxNumber);
            Number.Text = numberToGuess.ToString();
        }

        void StopGame()
        {
            timer.Stop();
            OnGameEnded(EventArgs.Empty);
            if (!gameWon)
            {
                MessageBox.Show("You loose!\n :(");
            }
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

        private void CheckIfNumberGuessed(object sender, EventArgs e)
        {
            if (abacus.GetValue() == numberToGuess)
            {
                MessageBox.Show("You won!");
                gameWon = true;
                StopGame();
            }
        }

        private void Button_Tap(object sender, GestureEventArgs e)
        {
            StartGame();
        }

        private void Show_SettingsWindow(object sender, GestureEventArgs e)
        {
            settingsWindow.Show();
        }
        
        // private void Button_Tap(object sender, GestureEventArgs e)

    }
}