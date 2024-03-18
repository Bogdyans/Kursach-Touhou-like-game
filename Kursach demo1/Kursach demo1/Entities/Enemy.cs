using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Kursach_demo1
{
    internal class Enemy : Entity
    {
        List<Attack> attacks = new List<Attack>();
        
        public int hp, maxhp = 5000;

        int delay = 0;
        public Enemy(Color color, double width, int kf) : base(color, width)
        {
            HitBox =  new Ellipse
            {
                Width = width,
                Height = width,
                Fill = new SolidColorBrush(color),
                StrokeThickness = 2,
                Stroke = Brushes.Black
            };
            maxhp *= kf;
            hp = maxhp;
        }
        public void ChngStrategy(Canvas field, int pattern_number)
        {
            if (pattern_number == 6)
                SetDelay(200);
            else
                SetDelay(50);
            attacks.Clear();
            hp = maxhp;
            switch (pattern_number)
            {
                case 1:
                    attacks.Add(new BulletCircle
                        (Color.FromArgb(56, 255, 0, 0), 15, 32, 8, 0, 200, 0, this));
                    attacks.Add(new BulletCircle
                        (Color.FromArgb(56, 255, 0, 0), 15, 80, 3, 0,  200, 0, this));
                    attacks.Add(new BulletCircle
                        (Color.FromArgb(56, 255, 0, 0), 15, 32, 1.5F, -1, 200, 0, this));
                    attacks.Add(new BulletCircle
                        (Color.FromArgb(56, 255, 0, 0), 15, 32, 3, 1, 200, 0, this));
                    attacks.Add(new BulletCircle
                        (Color.FromArgb(56, 255, 0, 0), 15, 64, 6, -1, 200, 0, this));
                    attacks.Add(new BulletCircle
                        (Color.FromArgb(56, 255, 0, 0), 15, 100, 1, 0, 200, 0, this));
                    break;
                case 2:
                    attacks.Add(new BulletCircle
                        (Color.FromRgb(255, 126, 0), 20, 10, (float)Math.Pow(3, 1.0 / 4.0), 0, 37,
                        -(float)Math.PI / 12, this));
                    attacks.Add(new BulletCircle
                        (Color.FromRgb(0, 0, 255), 10, 20, (float)Math.Pow(3, 1.0 / 2.0), 0, 30, (float)Math.PI / 12, this));
                    attacks.Add(new BulletCircle
                        (Color.FromRgb(255, 0, 0), 50, 6, 1, -1.5F, 90, -(float)Math.PI / 24, this));
                    attacks.Add(new BulletCircle
                        (Color.FromRgb(255, 255, 255), 20, 56, (float)Math.Pow(3, 1.0 / 3.0), 1, 180, 0, this));
                    break;
                case 3:
                    attacks.Add(new BulletCircle
                        (Color.FromRgb(255, 255, 255), 20, 24, 1, 2.1F, 120, (float)Math.PI/12, this));
                    attacks.Add(new BulletCircle
                        (Color.FromRgb(255, 255, 255), 20, 24, 1, -2.1F, 120, (float)Math.PI / 12, this));
                    attacks.Add(new BulletCircle
                        (Color.FromRgb(255, 127, 0), 25, 8, 2, 0, 30, (float)Math.PI / 32, this));
                    attacks.Add(new BulletCircle
                        (Color.FromRgb(255, 127, 0), 25, 8, 2, 0, 30, -(float)Math.PI / 32, this));
                    attacks.Add(new BulletCircle
                        (Color.FromRgb(255, 0, 0), 10, 16, 3, 0, 15, (float)Math.PI / 32, this));
                    attacks.Add(new BulletCircle
                        (Color.FromRgb(255, 0, 0), 10, 16, 3, 0, 15, -(float)Math.PI / 32, this));
                    break;
                case 4:
                    attacks.Add(new BulletCircle
                        (Color.FromRgb(126, 126, 126), 50, 8, 3, 2, 35, (float)Math.PI / 64, this));
                    attacks.Add(new BulletCircle
                        (Color.FromRgb(126, 126, 126), 50, 8, 3, -2, 35, -(float)Math.PI / 64, this));
                    attacks.Add(new BulletCircle
                        (Color.FromRgb(255, 255, 255), 10, 100, 6, 0, 100, 0, this));
                    attacks.Add(new BulletCircle
                        (Color.FromRgb(255, 255, 255), 10, 50, 6, 0, 50, (float)Math.PI / 100, this));
                    attacks.Add(new BulletCircle
                        (Color.FromRgb(255, 255, 255), 10, 25, 6, 0, 25, (float)Math.PI / 100, this));
                    break;
                case 5:
                    attacks.Add(new BulletCircle
                        (Color.FromArgb(255, 255, 250, 250), 20, 8, 0.8F, 0, 50, (float)Math.PI / 60, this));
                    attacks.Add(new BulletCircle
                        (Color.FromArgb(255, 255, 200, 200), 20, 16, 0.9F, 0, 50, (float)Math.PI / 60, this));
                    attacks.Add(new BulletCircle
                        (Color.FromArgb(255, 255, 150, 150), 20, 32, 1F, 0, 50, (float)Math.PI / 60, this));
                    attacks.Add(new BulletCircle
                        (Color.FromArgb(255, 255, 250, 250), 20, 8, 0.8F, 0, 50, -(float)Math.PI / 60, this));
                    attacks.Add(new BulletCircle
                        (Color.FromArgb(255, 255, 200, 200), 20, 16, 0.9F, 0, 50, -(float)Math.PI / 60, this));
                    attacks.Add(new BulletCircle
                        (Color.FromArgb(255, 255, 100, 100), 20, 64, 1.1F, 0, 50, (float)Math.PI / 60, this));
                    
                    break;
                case 6: // 
                    
                    Canvas.SetTop(HitBox, field.Height / 2 - HitBox.Height/2);
                    attacks.Add(new BulletCircle
                        (Color.FromArgb(255, 150, 100, 100), 20, 40, 6, 0, 5, (float)Math.PI / 120, this));
                    hp /= 5;
                    break;
                case 7: // 
                    
                    Canvas.SetTop(HitBox, field.Height / 4);
                    attacks.Add(new BulletCircle
                        (Color.FromArgb(255, 255, 50, 100), 15, 32, 2, -0.3F, 15, (float)Math.PI / 128, this));
                    attacks.Add(new BulletCircle
                        (Color.FromArgb(255, 100, 50, 255), 15, 32, 2, 0.3F, 15, -(float)Math.PI / 128, this));
                    attacks.Add(new BulletCircle
                        (Color.FromArgb(255, 200, 200, 50), 50, 2, 3, -1.5F, 20, (float)Math.PI / 15, this));
                    attacks.Add(new BulletCircle
                        (Color.FromArgb(255, 200, 200, 50), 50, 2, 3, 1.5F, 20, -(float)Math.PI / 15, this));
                    break;
                case 8: //
                    attacks.Add(new BulletCircle
                        (Color.FromArgb(255, 250, 255, 250), 25, 8, 2, 2, 25, (float)Math.PI / 32, this));
                    attacks.Add(new BulletCircle
                        (Color.FromArgb(255, 150, 255, 150), 25, 16, 2.2F, 0, 25, (float)Math.PI / 32, this));
                    attacks.Add(new BulletCircle
                        (Color.FromArgb(255, 50, 255, 50), 25, 32, 2.5F, 0, 25, (float)Math.PI / 32, this));
                    attacks.Add(new BulletCircle
                        (Color.FromArgb(255, 250, 255, 250), 25, 8, 2, -2, 25, -(float)Math.PI / 16, this));
                    attacks.Add(new BulletCircle
                        (Color.FromArgb(255, 150, 255, 150), 25, 16, 2.2F, 0, 25, -(float)Math.PI / 16, this));
                    attacks.Add(new BulletCircle
                        (Color.FromArgb(255, 50, 255, 50), 25, 32, 2.5F, 0, 25, -(float)Math.PI / 16, this));
                    break;
            }
        }
        public void Do(Canvas can, List<Bullet> bulls)
        {
            if (delay == 0)
            {
                foreach (Attack i in attacks)
                    i.Shoot(bulls, can);
                NotBullet(can, bulls);
                
            }
            else
                delay -= 1;
        }
        public void NotBullet(Canvas can, List<Bullet> bulls)
        {
            Bullet bul = new Bullet(Color.FromArgb(255, 0, 0, 0), 1, 0, 0, 0, 0);
            bul.Inv();
            bulls.Add(bul);
            bul.Spawn(can, this);
        }
        public void SetDelay(int n)
        {
            delay = n;
        }
    }
}
