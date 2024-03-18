
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Kursach_demo1
{
    abstract class Entity  
    {
        protected Ellipse HitBox { get; set; }
        
        public Entity(Color c, double w)
        {
            HitBox = new Ellipse
            {
                Fill = new SolidColorBrush(c),
                Width = w,
                Height = w,
                Stroke = new SolidColorBrush(Color.FromRgb(0,0,0)),
                StrokeThickness = 2
            };
        }
        
        public double GetX()
        {
            return Canvas.GetLeft(HitBox)+HitBox.Width/2;
        }
        public double GetY()
        {
            return Canvas.GetTop(HitBox)+HitBox.Height/2;

        }
        public void Spawn(int x, int y, Canvas c)
        {
            Canvas.SetLeft(HitBox, x-HitBox.Width/2);
            Canvas.SetTop(HitBox, y-HitBox.Height/2);
            c.Children.Add(HitBox);
        }


        public Ellipse GetHitbox()
        {
            return HitBox;
        }
    }
}
