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
using System.Windows.Media.Imaging;

namespace PhoneApp1
{
    public partial class Settings : PhoneApplicationPage
    {

        Button[] rowButtons; 
        Button[] imageButtons;

        public Settings()
        {
            InitializeComponent();
            rowButtons = (new Button[] {Row1, Row2, Row3, Row4, Row5, Row6, Row7, Row8, Row9});
            imageButtons = (new Button[] { Image1, Image2, Image3, Image4 });
            foreach (Button imageButton in imageButtons)
            {
                EventHandler<GestureEventArgs> BeadImageHandler = new EventHandler<GestureEventArgs>(SetBeadImage);
                imageButton.Tap += BeadImageHandler;
                if (Bead.beadImage == ((BitmapImage)((Image)(imageButton.Content)).Source).UriSource.ToString())
                {
                    setAsSelected(imageButton);
                }

            }
            foreach (Button rowButton in rowButtons)
            {
                EventHandler<GestureEventArgs> RowImageHandler = new EventHandler<GestureEventArgs>(SetRowCount);
                rowButton.Tap += RowImageHandler;
                if (Abacus.rowsCountSettings == Int32.Parse(rowButton.Content.ToString()))
                {
                    setAsSelected(rowButton);
                }
            }
        }

        void SetBeadImage(object sender, GestureEventArgs e) 
        {
            resetBorders(imageButtons);
            System.Diagnostics.Debug.WriteLine("bind");
            String source = ((BitmapImage)((Image)(((Button)sender).Content)).Source).UriSource.ToString();
            Bead.beadImage = source;
            System.Diagnostics.Debug.WriteLine(source);
            setAsSelected((Button)sender); 
        }

        void SetRowCount(object sender, GestureEventArgs e)
        {
            resetBorders(rowButtons);
            Int32 x = Int32.Parse(((Button)sender).Content.ToString());
            Abacus.rowsCountSettings = x;
            System.Diagnostics.Debug.WriteLine(x);
            setAsSelected((Button)sender); 
        }
        
        private void GoToMenu(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void resetBorders(Button[] buttons)
        {
            foreach (Button button in buttons)
            {
                button.BorderBrush = new SolidColorBrush(Colors.White);
            }
        }

        private void setAsSelected(Button button)
        {
            button.BorderBrush = new SolidColorBrush(Colors.Red);
        }
    }
}