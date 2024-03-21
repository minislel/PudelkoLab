using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PudelkoLibrary
{
    public static class Utility
    {
        public static Pudelko Kompresuj(this Pudelko p)
        {
            double krawedz = Math.Cbrt(p.Objetosc);
            return new Pudelko(krawedz, krawedz, krawedz);
        }
    }
}
