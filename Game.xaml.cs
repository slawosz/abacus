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
    public partial class Game : PhoneApplicationPage
    {
        // Constructor
        Abacus abacus;
        DispatcherTimer timer;
        Double ticks = 6000;
        Double actualTicks = 0;
        int numberToGuess;
        public static EventHandler GameStarted;
        public static EventHandler GameEnded;
        bool isGameActive = false;
        bool gameWon;

        public Game()
        {
            InitializeComponent();
            abacus = new Abacus(9);
            ShakeGesturesHelper.Instance.ShakeGesture += new EventHandler<ShakeGestureEventArgs>(Instance_ShakeGesture);

            ShakeGesturesHelper.Instance.MinimumRequiredMovesForShake = 3;
            ShakeGesturesHelper.Instance.Active = true;
            Container.Children.Add(abacus.GetContainer());
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += new EventHandler(TimerTick);

            ProgressBar.Maximum = 100;
            ProgressBar.Value = 0;
            EventBinder.Bind(ref Bead.BeadMoved, CheckIfNumberGuessed);
            EventBinder.Bind(ref Bead.BeadTaped, TryToStartGame);
            // Bead.BeadMoved += new EventHandler(UpdateScore);
        }

        public void OnGameStarted(EventArgs e)
        {
            if (GameStarted != null)
            {
                GameStarted(this, e);
            }
        }

        private int maxNumber() {
            int multiplicity = 1;
            int max = 0;
            for (int i = 0; i < Abacus.rowsCountSettings; i++)
            {
                multiplicity = multiplicity * 10;
                max += (multiplicity / 10) * 9;
            }
            System.Diagnostics.Debug.WriteLine("max: " + max);
            return max;
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
            System.Diagnostics.Debug.WriteLine("start game");
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
            numberToGuess = random.Next(0, maxNumber());
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

        void TimerTick(object sender, EventArgs e)
        {
            actualTicks++;
            ProgressBar.Value = (Int32)(actualTicks / (ticks/100));
            //System.Diagnostics.Debug.WriteLine("timer");
            //System.Diagnostics.Debug.WriteLine(actualTicks);
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

        private void TryToStartGame(object sender, EventArgs e)
        {
            if (!isGameActive)
            {
                StartGame();
            }
        }

        private void GoToMenu(object sender, GestureEventArgs e)
        {
            if (!isGameActive)
            {
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("navigated from");
            EventBinder.Clear(ref Bead.BeadMoved);
            EventBinder.Clear(ref Bead.BeadTaped);
        }
    }
}