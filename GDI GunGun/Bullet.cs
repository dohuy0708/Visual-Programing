using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDI_GunGun
{
    internal class Bullet
    {
        public double x { get; set; }  
        public double y { get; set; }
        public int speed { get; set; }
        public int t { get; set; }
       public Bullet(double x, double y)
        {
            this.x=x;
            this.y=y;
            this.speed=9;
        }
    }
}
