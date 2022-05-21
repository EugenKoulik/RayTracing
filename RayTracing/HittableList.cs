using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    internal class HittableList : List<IHittable>
    {
        public bool Hit(ref Ray r, double tMin, double tMax, ref HitRecord hitRecord)
        {
            HitRecord tempRecord = new HitRecord();

            var hitAnything = false;
            var closestSoFar = tMax;

            foreach(var currentHit in this)
            {
                if(currentHit.Hit(ref r, tMin, closestSoFar, ref hitRecord))
                {
                    hitAnything = true;
                    closestSoFar = tempRecord.t;
                    hitRecord = tempRecord;
                }
            }

            return hitAnything;
        }
    }
}
