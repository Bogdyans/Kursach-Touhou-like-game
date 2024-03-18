using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Kursach_demo1
{
    internal class BulletCircle : Attack
    {
        
        float rspeed;
        int count;
        float drr = 0;
        public BulletCircle(Color c, int w, int Count,
            float speed, float rspeed, int delay, float DR, Enemy e)
            : base(e, c, w, delay, speed, DR)
        {
            this.rspeed = rspeed;
            count = Count;
        }

        public override void Shoot(List<Bullet> bulls, Canvas canvas)
        {
            if (dl == 0)
            {
                double degree = 2 * Math.PI / count;
                double deg = 0 + drr;
                for (int i = 0; i < count; i++)
                {
                    Bullet bul = new Bullet(c, w,
                        (float)(speed * Math.Cos(deg)), (float)(speed * Math.Sin(deg)),
                        (float)(5 * Math.Cos(deg)), (float)(5*Math.Sin(deg)),
                        (float)rspeed, (float)e.GetX(), (float)e.GetY());
                    bulls.Add(bul);
                    bul.Spawn(canvas, e);
                    deg += degree;
                }
                dl = delay;
                drr += dr;
            }
            else
                dl -= 1;

        }
    }
}
