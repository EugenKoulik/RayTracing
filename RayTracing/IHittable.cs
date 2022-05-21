using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    internal interface IHittable
    {
        bool Hit(ref Ray r, double tMin, double tMax, ref HitRecord rec);

    }
}
