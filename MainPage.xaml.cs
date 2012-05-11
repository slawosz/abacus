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
        DispatcherTimer timer;
        Double ticks = 40;
        Double actualTicks = 0;
        int numberToGuess;
        public static EventHandler GameStarted;
        public static EventHandler GameEnded;
        bool isGameActive = false;
        bool gameWon;
        SettingsWindow settingsWindow;
        public int maxNumber = 999;
        public Double gameDuration = 5;
        public String[] levels = { "Slow", "Moderate", "Fast" };
        public int[] levelsDuration = { 8, 3, 1 };
        public int currentLevelIndex = 1;

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
            // timer.Interval = TimeSpan.FromSeconds(gameDuration/ticks);
            timer.Interval = TimeSpan.FromSeconds(levelsDuration[currentLevelIndex]/ticks);
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
            isGameActive = true;
            abacus.Reset();
            actualTicks = 0;
            timer.Start();
            ProgressBar.Value = 0;
            DrawNumberToGuess();
            OnGameStarted(EventArgs.Empty);
        }

        private void DrawNumberToGuess()
        {
            Random random = new Random();
            numberToGuess = random.Next(0, maxNumber);
            Number.Text = (new NumberDecorator(numberToGuess)).Decorate();
        }

        void StopGame()
        {
            timer.Stop();
            isGameActive = false;
            OnGameEnded(EventArgs.Empty);
            if (!gameWon)
            {
                MessageBox.Show("You loose!\n :(");
            }
        }

        public void ChangeLevel()
        {
            timer.Interval = TimeSpan.FromSeconds(levelsDuration[currentLevelIndex]/ticks);
        }

        void TimerTick(object sender, EventArgs e)
        {
            actualTicks++;
            ProgressBar.Value = actualTicks * 2;
            System.Diagnostics.Debug.WriteLine("timer");
            System.Diagnostics.Debug.WriteLine(actualTicks);
            if (ticks == actualTicks)
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
            if (!this.isGameActive)
            {
                return;
            }
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
            if (!isGameActive)
            {
                settingsWindow.Show();
            }
        }
        
        private void GoToTraining(object sender, GestureEventArgs e)
        {
            if (!isGameActive)
            {
                NavigationService.Navigate(new Uri("/Training.xaml", UriKind.Relative));
            }
        }
        // private void Button_Tap(object sender, GestureEventArgs e)

    }
}