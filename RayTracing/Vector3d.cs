using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    public class Vector3d
    {
        public static Vector3d Null;

        public double X;
        public double Y;
        public double Z;

        static Vector3d()
        {
            Null = new Vector3d(0, 0, 0);
        }

        public Vector3d(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3d(Vector3d v) : this(v.X, v.Y, v.Z)
        {
        }

        public Vector3d Normalize()
        {
            double t = (double)this.Length();
            return new Vector3d(X / t, Y / t, Z / t);
        }

        static public Vector3d operator +(Vector3d v, Vector3d w)
        {
            return new Vector3d(w.X + v.X, w.Y + v.Y, w.Z + v.Z);
        }

        static public Vector3d operator -(Vector3d v, Vector3d w)
        {
            return new Vector3d(v.X - w.X, v.Y - w.Y, v.Z - w.Z);
        }

        static public Vector3d operator -(Vector3d v)
        {
            return new Vector3d(-v.X, -v.Y, -v.Z);
        }

        static public Vector3d operator *(Vector3d v, Vector3d w)
        {
            return new Vector3d(v.X * w.X, v.Y * w.Y, v.Z * w.Z);
        }

        static public Vector3d operator *(Vector3d v, double f)
        {
            return new Vector3d(v.X * f, v.Y * f, v.Z * f);
        }

        static public Vector3d operator /(Vector3d v, double f)
        {
            return new Vector3d(v.X / f, v.Y / f, v.Z / f);
        }

        static public Vector3d operator +(Vector3d v, double f)
        {
            return new Vector3d(v.X + f, v.Y + f, v.Z + f);
        }

        static public Vector3d operator -(Vector3d v, double f)
        {
            return new Vector3d(v.X - f, v.Y - f, v.Z - f);
        }

        public bool NearZero()
        {
            var s = 1e-8;

            return(Math.Abs(X) < s && Math.Abs(Y) < s && Math.Abs(Z) < s);
        }

        public double Dot(Vector3d w)
        {
            return this.X * w.X + this.Y * w.Y + this.Z * w.Z;
        }

        public double Dot2D(Vector3d w)
        {
            return this.X * w.X + this.Y * w.Y;
        }

        public Vector3d Cross(Vector3d w)
        {
            return new Vector3d(-this.Z * w.Y + this.Y * w.Z,
                               this.Z * w.X - this.X * w.Z,
                              -this.Y * w.X + this.X * w.Y);
        }

        public double Length()
        {
            return Math.Sqrt((double)((X * X) + (Y * Y) + (Z * Z)));
        }

        public double LengthSqrd()
        {
            return (double)((X * X) + (Y * Y) + (Z * Z));
        }

        public static Vector3d UnitVector(Vector3d v)
        {
            return v / v.Length();
        }

        public static Vector3d RandomVector()
        {
            var r = new Random();

            return new Vector3d(r.NextDouble(), r.NextDouble(), r.NextDouble());
        }

        public static Vector3d RandomVector(double r0, double r1)
        {
            return new Vector3d(RndGenerators.RndDoubleInRange(r0, r1), 
                 RndGenerators.RndDoubleInRange(r0, r1), 
                (RndGenerators.RndDoubleInRange(r0, r1)));
        }
    }
}
