using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Kursach_demo1
{
    internal class Bullet : Entity
    {
        //delta x, delta y
        float enx, eny; //const for rotating bullets
        float dx, dy;

        //Bool
        bool inv = false;
        bool rotate = false;
        
        //speeds
        float speedx, speedy;
        float r = 0;

        bool from_enemy = true;

        

        /// <summary>
        /// Конструктор для не вращающихся пуль
        /// </summary>
        /// <param name="c">Colour</param>
        /// <param name="w">Hitbox's width</param>
        /// <param name="sx">Speed in x</param>
        /// <param name="sy">Speed in y</param>
        /// <param name="dx">Delta x from Spawnpoint</param>
        /// <param name="dy">Delta y from Spawnpoint</param>
        public Bullet(Color c, int w, float sx, float sy, float dx, float dy) : base(c, w)
        {
            HitBox = new Ellipse { Fill = new SolidColorBrush(c), Width = w, Height = w,
                Stroke = new SolidColorBrush(Color.FromRgb(0,0,0)), StrokeThickness = 3};
            speedx = sx;
            speedy = sy;
            this.dx = dx;
            this.dy = dy;
            
        }
        public Bullet(Color c, int w, float sx, float sy, float dx, float dy, float r, float ex, float ey) : base(c, w)
        {
            HitBox = new Ellipse { Fill = new SolidColorBrush(c), Width = w, Height = w,
                Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)), StrokeThickness = 3 };
            speedx = sx;
            speedy = sy;
            this.dx = dx;
            this.dy = dy;
            this.r = r;
            enx = ex;
            eny = ey;
            rotate = (r != 0)? true : false;
            
        }
        

        public void Inv()
        {
            inv = true;
        }
        public bool IsInv()
        {
            return inv;
        }

        public void From_Player()
        {
            from_enemy = false;
        }
        public void Spawn(Canvas c, Entity p)  
        { 
            Canvas.SetLeft(HitBox, p.GetX() + dx - HitBox.Width/2);
            Canvas.SetTop(HitBox, p.GetY() + dy - HitBox.Height/2);
            c.Children.Add(HitBox);
        }
        public void Delete(Canvas canvas, List<Bullet> bulls, int i)
        {
            canvas.Children.Remove(HitBox);
            bulls.RemoveAt(i);
            
        }

        
        public void Move(Canvas can, List<Bullet> bulls, int i)
        {
            if (!rotate)
            {
                if (Canvas.GetTop(HitBox) < 0 - HitBox.Height 
                    || Canvas.GetLeft(HitBox) < 0 - HitBox.Width 
                    || Canvas.GetTop(HitBox) > can.Height 
                    || Canvas.GetLeft(HitBox) > can.Width)
                {
                    Delete(can, bulls, i);
                    return;
                }
                Canvas.SetTop(HitBox, Canvas.GetTop(HitBox) + speedy);
                Canvas.SetLeft(HitBox, Canvas.GetLeft(HitBox) + speedx);
                
            }
            else //cos(f) = sin(f+p/2)
            {
                if (Canvas.GetTop(HitBox) < can.Height / 2 - 
                    Math.Sqrt(Math.Pow(can.Height / 2, 2) + Math.Pow(can.Width / 2, 2)) 
                    || Canvas.GetTop(HitBox) > can.Height / 2 + 
                    Math.Sqrt(Math.Pow(can.Height / 2, 2) + Math.Pow(can.Width / 2, 2))
                    || Canvas.GetLeft(HitBox) < can.Width / 2 - 
                    Math.Sqrt(Math.Pow(can.Height / 2, 2) + Math.Pow(can.Width / 2, 2)) 
                    || Canvas.GetLeft(HitBox) > can.Width / 2 + 
                    Math.Sqrt(Math.Pow(can.Height / 2, 2) + Math.Pow(can.Width / 2, 2)) 
                    || IsInv())
                {
                    Delete(can, bulls, i);
                    return;
                }
                double speedy1 = (GetY() - eny) / Math.Sqrt(Math.Pow(GetY() - eny, 2) + 
                    Math.Pow(GetX() - enx, 2));
                double speedx1 = (GetX() - enx) / Math.Sqrt(Math.Pow(GetY() - eny, 2) + 
                    Math.Pow(GetX() - enx, 2));
                Canvas.SetTop(HitBox, Canvas.GetTop(HitBox) + Math.Sqrt(Math.Pow(speedx, 2) + 
                    Math.Pow(speedy, 2)) * speedy1 - r * speedx1);
                Canvas.SetLeft(HitBox, Canvas.GetLeft(HitBox) + Math.Sqrt(Math.Pow(speedx, 2) + 
                    Math.Pow(speedy, 2)) * speedx1 + r * speedy1);
                ChangeVisibillity(can);
                
            }
            if (Canvas.GetTop(HitBox) < can.Height / 2 - 
                Math.Sqrt(Math.Pow(can.Height / 2, 2) + Math.Pow(can.Width / 2, 2)) 
                || Canvas.GetTop(HitBox) > can.Height / 2 + 
                Math.Sqrt(Math.Pow(can.Height / 2, 2) + Math.Pow(can.Width / 2, 2)) 
                || Canvas.GetLeft(HitBox) < can.Width / 2 - 
                Math.Sqrt(Math.Pow(can.Height / 2, 2) + Math.Pow(can.Width / 2, 2)) 
                || Canvas.GetLeft(HitBox) > can.Width / 2 + 
                Math.Sqrt(Math.Pow(can.Height / 2, 2) + Math.Pow(can.Width / 2, 2)) 
                || IsInv())
                Delete(can, bulls, i);
        }
        public bool DidHit(Entity entity)
        {
            if (entity is Player && from_enemy)
                return (Math.Sqrt(Math.Pow(GetX() - entity.GetX(), 2) +
                    Math.Pow(GetY() - entity.GetY(), 2)) < HitBox.Width / 2*0.8);
            else if (entity is Enemy && !from_enemy)
                return (Math.Sqrt(Math.Pow(GetX() - entity.GetX(), 2) +
                    Math.Pow(GetY() - entity.GetY(), 2)) < entity.GetHitbox().Width / 2); 
            return false;
        }

        public void ChangeVisibillity(Canvas can)
        {
            if (Canvas.GetTop(HitBox) < 0 - HitBox.Height
                || Canvas.GetLeft(HitBox) < 0 - HitBox.Width 
                || Canvas.GetTop(HitBox) > can.Height
                || Canvas.GetLeft(HitBox) > can.Width)
                HitBox.Visibility = Visibility.Hidden;
            else if (Canvas.GetTop(HitBox) > 0 && Canvas.GetLeft(HitBox)
                > 0 && Canvas.GetTop(HitBox) < can.Height && Canvas.GetLeft(HitBox)
                < can.Width)
                HitBox.Visibility = Visibility.Visible;
        }

       
    }
}
