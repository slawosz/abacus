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
        //Popup popup = new Popup();
        //MainPage page;
        //Button[] levelButtons = new Button[3];
        //Button[] numberButtons = new Button[3];
        //TextBlock seconds;

        //public SettingsWindow(Game page)
        //{
        //    this.page = page;
        //    Border border = new Border();
        //    border.BorderBrush = new SolidColorBrush(Colors.White);
        //    border.BorderThickness = new Thickness(5.0);

        //    StackPanel panel1 = new StackPanel();
        //    panel1.Background = new SolidColorBrush(Colors.Black);

        //    TextBlock textblock1 = new TextBlock();
        //    textblock1.Text = "Select max number:";
        //    textblock1.Margin = new Thickness(5.0);
        //    textblock1.Foreground = new SolidColorBrush(Colors.White);
        //    textblock1.FontSize = 28;
        //    panel1.Children.Add(textblock1);

        //    int[] maxNumber = { 999, 999999, 999999999 };
        //    for (int i = 0; i < 3; i++)
        //    {
        //        Button button = new Button();
        //        button.Content = maxNumber[i].ToString();
        //        button.Margin = new Thickness(5.0);
        //        button.Click += new RoutedEventHandler(ChangeMaxNumber);
        //        if (page.maxNumber == maxNumber[i])
        //        {
        //            button.Background = new SolidColorBrush(Colors.Brown);
        //        }
        //        numberButtons[i] = button;
        //        panel1.Children.Add(button);
        //    }

        //    TextBlock textblock2 = new TextBlock();
        //    textblock2.Text = "Select game time:";
        //    textblock2.Margin = new Thickness(5.0);
        //    textblock2.Foreground = new SolidColorBrush(Colors.White);
        //    textblock2.FontSize = 28;
        //    panel1.Children.Add(textblock2);

        //    for (int i = 0; i < 3; i++)
        //    {
        //        Button button = new Button();
        //        button.Content = page.levels[i];
        //        button.Margin = new Thickness(5.0);
        //        button.Click += new RoutedEventHandler(ChangeLevel);
        //        if (page.currentLevelIndex == i)
        //        {
        //            button.Background = new SolidColorBrush(Colors.Brown);
        //        }
        //        levelButtons[i] = button;
        //        panel1.Children.Add(button);
        //    }
        //    border.Child = panel1;
        //    panel1.Width = 400;
            
        //    Button close = new Button();
        //    close.Content = "Close";
        //    close.Margin = new Thickness(5.0);
        //    close.Click += new RoutedEventHandler(Close);
        //    panel1.Children.Add(close);

        //    popup.Child = border;

        //    popup.VerticalOffset = 25;
        //    popup.HorizontalOffset = 25;
        //    popup.Width = 400;

        //    popup.IsOpen = false;
        //}

        //void ChangeMaxNumber(object sender, RoutedEventArgs e)
        //{
        //    // Close the popup.
        //    Button button = (Button)sender;
        //    page.maxNumber = Int32.Parse(button.Content.ToString());
        //    for (int i = 0; i < 3; i++)
        //    {
        //        numberButtons[i].Background = new SolidColorBrush(Colors.Black);
        //    }
        //    button.Background = new SolidColorBrush(Colors.Brown);
        //}

        //void ChangeLevel(object sender, RoutedEventArgs e)
        //{
        //    page.currentLevelIndex = Array.IndexOf(page.levels, button.Content);
        //    for (int i = 0; i < 3; i++)
        //    {
        //        levelButtons[i].Background = new SolidColorBrush(Colors.Black);
        //    page.ChangeLevel();
        //}


        //void Close(object sender, RoutedEventArgs e)
        //{
        //    popup.IsOpen = false;
        //}

        //public void Show()
        //{
        //    popup.IsOpen = true;
        //}
    }
}
