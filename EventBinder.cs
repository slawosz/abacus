﻿using System;
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
    public class EventBinder
    {
        public static void Bind(ref EventHandler eventHandler, Action<object, EventArgs> target)
        {
            eventHandler += new EventHandler(target);
        }
    
        public static void Clear(ref EventHandler eventHandler)
        {
            eventHandler = null;
        }

    }
}
