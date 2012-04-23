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

namespace PhoneApp1
{
    public class Bead
    {

        private Canvas bead;
        private Bead leftBead;
        private Bead rightBead;
        private Rod rod;

        public Bead(Rod rod, int index)
        {
            this.rod = rod;
            bead = new Canvas();
            Canvas.SetLeft(bead, rod.beadSize * index);
            bead.Width = bead.Height = rod.beadSize;
            SetColor(rod.beadSize * index);
            BindManipulationEvent();
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
        }

        private void BindManipulationEvent() {
            bead.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(OnMove);
        }

        private void OnMove(object sender, ManipulationDeltaEventArgs e)
        {
            Double x = e.DeltaManipulation.Translation.X;
            MoveBead(x);
        }

        private void MoveBead(Double x)
        {
            if (x > 0)
            {
                MoveRight(x);
            }
            else
            {
              MoveLeft(x);
            }

            rod.SetRodValue();
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
            return rightBead == null || ((Canvas.GetLeft(GetBead()) + rod.beadSize + x) < Canvas.GetLeft(rightBead.GetBead()));
        }

        private bool IsMoveRightPossible(Double x) {
            if (rightBead == null) 
            {
                return (Canvas.GetLeft(GetBead()) + x + rod.beadSize) <= rod.rodWidth;
            }
            else 
            {
                return ((Canvas.GetLeft(GetBead()) + rod.beadSize + x) < Canvas.GetLeft(rightBead.GetBead()));
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
            return leftBead == null || ((Canvas.GetLeft(leftBead.GetBead()) + rod.beadSize) < Canvas.GetLeft(GetBead()) + x);
        }

        private bool IsMoveLeftPossible(Double x) {
            if (leftBead == null) 
            {
                return (Canvas.GetLeft(GetBead()) + x) >= 0;
            }
            else 
            {
                return ((Canvas.GetLeft(leftBead.GetBead()) + rod.beadSize) < Canvas.GetLeft(GetBead()) + x);
            }
        }
       
        public void MoveBy(Double x) 
        {
            Canvas.SetLeft(this.GetBead(), Canvas.GetLeft(this.GetBead()) + x);
        }

    }
}
