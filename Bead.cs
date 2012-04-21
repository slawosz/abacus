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

        public Bead(Rod rod, int size, int left)
        {
            this.rod = rod;
            bead = new Canvas();
            Canvas.SetLeft(bead, left);
            bead.Width = size;
            bead.Height = size;
            SetColor(left);
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
            c.R = (byte)left;
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
             //   MoveLeft(x);
            }
                
        }

        // ==========================================================================
        // Moving right logic
        // ==========================================================================
        
        protected void MoveRight(Double x)
        {
            if (MoveRightBeadBy(x))
            {
                MoveBeadBy(x);
            }
        }

        private bool MoveRightBeadBy(Double x)
        {
            if ((this.rightBead != null) && RightBeadWillBePushed(x))
            {
                if (rightBead.MoveRightBeadBy(x))
                {
                    rightBead.MoveBeadBy(x);
                    return true;
                }
            }
            if (this.rightBead == null)
            {
                if (EndReached(x))
                {
                    return false;
                }
                else
                {
                    //MoveBeadBy(x);
                    return true;
                }
            }
            else 
            {
                //MoveBeadBy(x);
                if (RightBeadWillBePushed(x))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        // does this method should include checking if rightBead exists?
        private bool RightBeadWillBePushed(Double x)
        {
            return ((Canvas.GetLeft(bead) + Rod.BEAD_SIZE + x) >= Canvas.GetLeft(rightBead.GetBead()));
        }

        private bool EndReached(Double x)
        {
            return ((Canvas.GetLeft(bead) + Rod.BEAD_SIZE + x) >= Rod.ROD_WIDTH);
        }

        // ==========================================================================
        // Moving left logic
        // ==========================================================================
        protected void MoveLeft(Double x)
        {
            if (MoveLeftBeadBy(x))
            {
                MoveBeadBy(x);
            }
            
        }

        private bool MoveLeftBeadBy(Double x)
        {
            if ((this.leftBead != null) && LeftBeadWillBePushed(x))
            {
                return leftBead.MoveLeftBeadBy(x);
            }
            if (this.leftBead != null)
            {
                return true;
            }
            if (this.leftBead == null)
            {
                if (StartReached(x))
                {
                    return false;
                }
                else
                {
                    MoveBeadBy(x);
                    return true;
                }
            }
            return false;
        }

        // does this method should include checking if leftBead exists?
        private bool LeftBeadWillBePushed(Double x)
        {
            return (Canvas.GetLeft(bead) <= (Canvas.GetLeft(leftBead.GetBead()) + Rod.BEAD_SIZE));
        }

        private bool StartReached(Double x)
        {
            return (Canvas.GetLeft(bead) <= 0);
        }

        private void MoveBeadBy(Double x)
        {
            Canvas.SetLeft(bead, Canvas.GetLeft(bead) + x);
        }

    }
}
