using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    internal class Sphere : IHittable
    {
        public Vector3d Center;
        public double Radius;
        public Material Material;

        public Sphere(Vector3d cen, double r, Material m)
        {
            Center = cen;
            Radius = r;
            Material = m;
        }

        public bool Hit(ref Ray r, double tMin, double tMax, ref HitRecord rec)
        {
            var oc = r.Orig - Center;

            var a = r.Dir.LengthSqrd();

            var halfB = oc.Dot(r.Dir);

            var c = oc.LengthSqrd() - Radius * Radius;

            var discriminant = halfB * halfB - a * c;

            if (discriminant < 0) return false;

            var sqrtd = Math.Sqrt(discriminant);

            var root = (-halfB - sqrtd) / a;

            if (root < tMin || root > tMax)
            {
                root = (-halfB + sqrtd) / a;

                if (root < tMin || root > tMax) return false;

            }

            rec.t = root;
            rec.p = r.PointAtParameter(rec.t);
            var outwardNormal = (rec.p = Center) / Radius;

            rec.SetFaceNormal(ref r, ref outwardNormal);
            rec.matPtr = Material;

            return true;
        }
    }
}
