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
    public class MoveRequest
    {
        public Bead sender { get; set; }
        public bool accepted { get; set; }
        public bool processed { get; set; }
        public Double x { get; set; }

        public MoveRequest(Bead sender, Double x)
        {
            this.sender = sender;
            this.x = x;
            this.accepted = false;
            this.processed = false;
        }
           
    }
}
