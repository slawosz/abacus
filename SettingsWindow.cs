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
using System.Windows.Controls.Primitives;

namespace PhoneApp1
{
    public class SettingsWindow
    {
        Popup popup = new Popup();
        MainPage page;

        public SettingsWindow(MainPage page)
        {
            this.page = page;
            Border border = new Border();
            border.BorderBrush = new SolidColorBrush(Colors.Black);
            border.BorderThickness = new Thickness(5.0);

            StackPanel panel1 = new StackPanel();
            panel1.Background = new SolidColorBrush(Colors.LightGray);

            int[] maxNumber = { 999, 999999, 999999999 };
            for (int i = 0; i < 3; i++)
            {
                Button button = new Button();
                button.Content = maxNumber[i].ToString();
                button.Margin = new Thickness(5.0);
                button.Click += new RoutedEventHandler(button_Click);
                panel1.Children.Add(button);
            }
            TextBlock textblock1 = new TextBlock();
            textblock1.Text = "The popup control";
            textblock1.Margin = new Thickness(5.0);
            panel1.Children.Add(textblock1);
            border.Child = panel1;

            // Set the Child property of Popup to the border 
            // which contains a stackpanel, textblock and button.
            popup.Child = border;

            // Set where the popup will show up on the screen.
            popup.VerticalOffset = 25;
            popup.HorizontalOffset = 25;

            // Open the popup.
            popup.IsOpen = false;
        }

        void button_Click(object sender, RoutedEventArgs e)
        {
            // Close the popup.
            Button button = (Button)sender;
            page.maxNumber = Int32.Parse(button.Content.ToString());
            popup.IsOpen = false;
        }

        public void Show()
        {
            popup.IsOpen = true;
        }

    }
}
