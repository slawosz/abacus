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
using System.Windows.Media.Imaging;

namespace PhoneApp1
{
    public class Bead
    {

        private Canvas bead;
        private Bead leftBead;
        private Bead rightBead;
        private Rod rod;
        public static EventHandler BeadMoved;
        public static EventHandler BeadTaped;
        public static String beadImage = "images/red.jpg";
        private bool locked = true;
        private int index;

        public Bead(Rod rod, int index)
        {
            this.index = index;
            this.rod = rod;
            bead = new Canvas();
            Canvas.SetLeft(bead, rod.beadSize * index);
            bead.Width = bead.Height = rod.beadSize;
            SetColor(rod.beadSize * index);
            BindManipulationEvent();
            EventBinder.Bind(ref Game.GameStarted, UnlockBead);
            EventBinder.Bind(ref Game.GameEnded, LockBead);
            EventBinder.Bind(ref Training.Started, UnlockBead);
            EventBinder.Bind(ref Training.Ended, LockBead);
        }

        protected double RightEdge()
        {
            return Canvas.GetLeft(bead) + bead.Width;
        }

        protected double LeftEdge()
        {
            return Canvas.GetLeft(bead);
        }

        protected double HomePosition()
        {
            return index * bead.Width;
        }

        protected double EdgePosition()
        {
            return HomePosition() + rod.freeSpace;
        }

        public Canvas GetBead() 
        {
            return bead;
        }

        public void SetRightBead(Bead rightBead)
        {
            this.rightBead = rightBead;
            this.rightBead.SetLeftBead(this);
        }

        protected void SetLeftBead(Bead leftBead)
        {
            this.leftBead = leftBead;
        }
       
        private void SetColor(int left)
        {
            Color c = new Color();
            c.R = (byte)((byte)left % (byte)255);
            c.B = (byte)((40 * (byte)left) % (byte)255);
            c.G = 255;
            c.A = 200;
            
            this.bead.Background = new SolidColorBrush(c); 
            ImageBrush brush = new ImageBrush
            {
                   ImageSource = new BitmapImage(new Uri(Bead.beadImage, UriKind.Relative))
                 };
            this.bead.Background = brush;
        }

        private void BindManipulationEvent() {
            bead.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(OnMove);
            bead.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(OnMoveEnd);
            bead.Tap += new EventHandler<GestureEventArgs>(OnBeadTaped);
        }

        private void OnMove(object sender, ManipulationDeltaEventArgs e)
        {
            Double x = e.DeltaManipulation.Translation.X;
            MoveBead(x);
        }

        private void OnMoveEnd(object sender, ManipulationCompletedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("my move");
            // Double delta = (rod.rodWidth - bead.Width - ((Canvas.GetLeft(bead) - (bead.Width * index))));
            Double delta;
            delta = rod.freeSpace - (Canvas.GetLeft(bead) - (bead.Width * index));
            Double x = e.TotalManipulation.Translation.X;
            if (x > 0)
            {
                delta = rod.freeSpace - (LeftEdge() - HomePosition());
            }
            else
            {
                delta = HomePosition() - LeftEdge();
            }
            MoveBead(delta);
            System.Diagnostics.Debug.WriteLine(x);
            System.Diagnostics.Debug.WriteLine(delta);
            ShowPositions();
        }

        private void LockBead(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("locked !");
            locked = true;
        }

        private void UnlockBead(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("unlocked !");
            locked = false;
        }

        private void MoveBead(Double x)
        {
            if (locked)
            {
                //System.Diagnostics.Debug.WriteLine("locked :(");
                return;
            }
            if (x > 0)
            {
                //System.Diagnostics.Debug.WriteLine("lalala :(");
                MoveRight(x);
            }
            else
            {
              MoveLeft(x);
            }

            OnBeadMoved(EventArgs.Empty);
        }

        private void OnBeadTaped(object sender, GestureEventArgs args)
        {
            if (BeadTaped != null)
            {
                if (!Training.active)
                {
                    System.Diagnostics.Debug.WriteLine("taped!");
                    PhoneApp1.Bead.BeadTaped(this, args);
                }
            }
        }

        public void OnBeadMoved(EventArgs args)
        {
            if (BeadMoved != null)
            {
                PhoneApp1.Bead.BeadMoved(this, args);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("none :(");
            }
        }

        // ==========================================================================
        // Moving right logic
        // ==========================================================================
        
        protected void MoveRight(Double x)
        {
            MoveRequest request = new MoveRequest(this, x);
            this.SendMoveRightRequest(request);
        }

        private void SendMoveRightRequest(MoveRequest request)
        {
            if (CanDecideAboutMoveRight(request.x)) {
                if (IsMoveRightPossible(request.x))
                {
                    request.accepted = true;
                    MoveBy(request.x);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("no move :(");
                }
                request.processed = true;
                if (this != request.sender)
                {
                    leftBead.SendMoveRightRequest(request);
                }
            }
            else 
            {
                if (request.accepted)
                {
                    MoveBy(request.x);
                }
                if (this != request.sender || !request.processed)
                {
                    if (request.processed)
                    {
                        leftBead.SendMoveRightRequest(request);
                    }
                    else
                    {
                        rightBead.SendMoveRightRequest(request);
                    }
                }
            }
        }

        private bool CanDecideAboutMoveRight(Double x) {
            return rightBead == null || ((RightEdge() + x) <= rightBead.LeftEdge());
        }

        private bool IsMoveRightPossible(Double x) {
            if (rightBead == null) 
            {
                return (RightEdge() + x) <= rod.rodWidth;
            }
            else 
            {
                return (RightEdge() + x) <= rightBead.LeftEdge();
            }
        }

        // ==========================================================================
        // Moving left logic
        // ==========================================================================
        
        protected void MoveLeft(Double x)
        {
            MoveRequest request = new MoveRequest(this, x);
            this.SendMoveLeftRequest(request);
        }

        private void SendMoveLeftRequest(MoveRequest request)
        {
            if (CanDecideAboutMoveLeft(request.x)) {
                if (IsMoveLeftPossible(request.x))
                {
                    request.accepted = true;
                    MoveBy(request.x);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("no move left :(");
                }
                request.processed = true;
                if (this != request.sender)
                {
                    rightBead.SendMoveLeftRequest(request);
                }
            }
            else 
            {
                if (request.accepted)
                {
                    MoveBy(request.x);
                }
                if (this != request.sender || !request.processed)
                {
                    if (request.processed)
                    {
                        rightBead.SendMoveLeftRequest(request);
                    }
                    else
                    {
                        leftBead.SendMoveLeftRequest(request);
                    }
                }
            }
        }

        private bool CanDecideAboutMoveLeft(Double x) {
            return leftBead == null || ((Canvas.GetLeft(leftBead.GetBead()) + rod.beadSize) <= Canvas.GetLeft(GetBead()) + x);
        }

        private bool IsMoveLeftPossible(Double x) {
            if (leftBead == null) 
            {
                return (Canvas.GetLeft(GetBead()) + x) >= 0;
            }
            else 
            {
                return ((Canvas.GetLeft(leftBead.GetBead()) + rod.beadSize) <= Canvas.GetLeft(GetBead()) + x);
            }
        }
       
        public void MoveBy(Double x) 
        {
            Canvas.SetLeft(this.GetBead(), Canvas.GetLeft(this.GetBead()) + x);
        }

        private void ShowPositions()
        {
            string str = "";
            for(int i = 0; i < rod.beadsCount; i++)
            {
                str += index+ ": " + rod.beads[i].LeftEdge() + ", " + rod.beads[i].RightEdge() + "; ";
            }
            System.Diagnostics.Debug.WriteLine(str);
        }
    }
}
