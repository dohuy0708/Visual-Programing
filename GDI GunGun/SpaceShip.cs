using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDI_GunGun
{
    internal class SpaceShip
    {
        public double x { get; set; }
         public double y { get; set; }
        public double h { get; set; }
        public double w { get; set; }
        public SpaceShip(double x, double y, double h, double w)
        {
            this.x=x;
            this.y=y;
            this.h=h;
            this.w=w;
        }
    }
}
