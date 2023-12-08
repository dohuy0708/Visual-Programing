using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDI_GunGun
{
    internal class Rock
    {
        public double x { get; set; }
        public double y { get;set; }
        public double speed { get; set; }
        public Rock(double x, double y)
        {
            this.x=x;
            this.y=y;
            Random random = new Random();
            this.speed = random.Next(3, 8);
        }

      
    }
}
