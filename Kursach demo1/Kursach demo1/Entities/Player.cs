using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Kursach_demo1
{
    internal class Player : Entity
    {
        
        public int hp, maxhp = 9;
        
        float speed = 10.0F;
        bool isshooting = false, movesup = false,
            movesdown = false, movesleft = false, movesright = false;
        int shoot_delay = 0;
        public int bombs;

        public int damage = 18;
        public Player(Color color, double width, float kf) : base(color, width)
        {
            maxhp = (int)(maxhp/kf);
            hp = maxhp;
            bombs = (int)(1 * kf);
        }
        public void Do(Canvas can, List<Bullet> bulls)
        {
            Move(can);
            if (isshooting)
                Shoot(can, bulls);
        }
        public void ReSpawn(int x, int y, Canvas c)
        {
            c.Children.RemoveAt(c.Children.IndexOf(HitBox));
            Canvas.SetLeft(HitBox, x - HitBox.Width / 2);
            Canvas.SetTop(HitBox, y - HitBox.Height / 2);
            c.Children.Add(HitBox);
        }
        public void Shift()
        {
            speed = speed/2;
        }
        public void StopShift()
        {
            speed = speed*2;
        }

        public void Shoot(Canvas canvas, List<Bullet> bulls)
        {
            if (shoot_delay == 0)
            {
                isshooting = true;

                int dx = 0;
                int dy = -5;
                for (int i = 0; i < 5; i++)
                {
                    Bullet bul = new Bullet(Color.FromRgb(1, 1, 255), 10, 0, -10, dx, dy);
                    bul.From_Player();
                    bulls.Add(bul);
                    bul.Spawn(canvas, this);
                    if (dx <= 0) {
                        dx = -1 * dx + 15; dy += 4;
                    }
                    else
                    {
                        dx *= -1;
                    }
                }
                shoot_delay = 5; //Здесь задаем задержку
            }
            shoot_delay -= 1;
        }
      
        public void Move(Canvas can)
        {
            if (movesup && Canvas.GetTop(HitBox) > 0)
                Canvas.SetTop(HitBox, Canvas.GetTop(HitBox) - speed);
            if (movesdown && Canvas.GetTop(HitBox) + (HitBox.Height) < can.ActualHeight)
                Canvas.SetTop(HitBox, Canvas.GetTop(HitBox) + speed);
            if (movesleft && Canvas.GetLeft(HitBox) > 0)
                Canvas.SetLeft(HitBox, Canvas.GetLeft(HitBox) - speed);
            if (movesright && Canvas.GetLeft(HitBox) + HitBox.Width < can.ActualWidth)
                Canvas.SetLeft(HitBox, Canvas.GetLeft(HitBox) + speed);
        }

        public void MoveDU()
        {
            movesup = true;
        }
        public void MoveDD()
        {
            movesdown = true;
        }
        public void MoveDL()
        {
            movesleft = true;
        }
        public void MoveDR()
        {
            movesright = true;
        }
        public void StopShoot()
        {
            isshooting = false;
            shoot_delay = 0;
        }

        

        public void SMoveDU()
        {
            movesup = false;
        }
        public void SMoveDD()
        {
            movesdown = false;
        }
        public void SMoveDL()
        {
            movesleft = false;
        }
        public void SMoveDR()
        {
            movesright = false;
        }
    }
}
