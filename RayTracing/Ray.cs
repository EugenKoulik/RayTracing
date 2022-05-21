using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    public class Ray
    {
        public static Ray Null;

        public Vector3d Orig;
        public Vector3d Dir;
        public double Tm;      

        static Ray()
        {
            Null = new Ray(Vector3d.Null, Vector3d.Null);
        }

        public Ray(Vector3d origin, Vector3d direction)
        {
            Orig = origin;
            Dir = direction;
            Tm = 0;
        }

        public Ray(Vector3d origin, Vector3d direction, double time)
        {
            Orig = origin;
            Dir = direction;
            Tm = time;
        }

        public Vector3d PointAtParameter(double t)
        {
            return Orig + (Dir * t);
        }

        public static Vector3d Reflect(Vector3d vector, Vector3d normal)
        {
            return vector - ((vector * normal) * 2) * normal;
        }
    }
}
