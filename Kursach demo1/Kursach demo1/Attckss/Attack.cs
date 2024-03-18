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
    abstract class Attack
    {
        protected float dr;
        protected Enemy e;
        protected Color c;
        protected int w;
        protected int delay, dl;
        protected float speed;
        
        public Attack(Enemy e, Color c, int w, int Delay, float Speed, float dr)
        {
            this.e = e;
            this.c = c;
            this.w = w;
            this.delay = Delay;
            this.speed = Speed;
            
            dl = Delay;
            this.dr = dr;
        }
        public abstract void Shoot(List<Bullet> bulls, Canvas canvas);
        
    }
}
