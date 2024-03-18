using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Kursach_demo1
{
    internal class Field
    {
        Canvas field = new Canvas
        {
            Name = "field",
            Height = 544.3,
            Width = 933.12,
            Focusable = true,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top
        };
        Border border = new Border { Width = 933.12, Height = 544.3,
            BorderThickness = new System.Windows.Thickness(3),
            BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0)) };
    
        public void Set(Canvas screen)
        {
            screen.Children.Add(field);
            Canvas.SetTop(field, 86.4);
            Canvas.SetLeft(field, 43.2);
            field.Children.Add(border);
            field.Focus();
        }
        public void SetBorder() 
        {
            field.Children.Add(border);
        }
        public double GetHeight()
        {
            return field.Height;
        }
        public double GetWidth()
        {
            return field.Width;
        }
        public Canvas GetCanvas()
        {
            return field;
        }
    }
}
