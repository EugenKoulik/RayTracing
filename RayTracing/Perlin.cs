using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    internal class Perlin
    {
        private static int pointCount = 256;
        private Vector3d[] ranvec;

        private int[] permX;
        private int[] permY;
        private int[] permZ;


        public Perlin()
        {
            ranvec = new Vector3d[pointCount];

            for(int i = 0; i < pointCount; i++)
            {
                ranvec[i] = Vector3d.UnitVector(Vector3d.RandomVector(-1.0, 1.0));
            }
        }

        public double Noise(ref Vector3d p)
        {

            var u = p.X - Math.Floor(p.X);
            var v = p.Y - Math.Floor(p.Y);
            var w = p.Z - Math.Floor(p.Z);

            int i = (int)Math.Floor(p.X);
            int j = (int)Math.Floor(p.Y);
            int k = (int)Math.Floor(p.Z);

            Vector3d[,,] c = new Vector3d[2,2,2];

            for (int di = 0; di < 2; di++)
            {
                for(int dj = 0; dj < 2; dj++)
                {
                    for(int dk = 0; dk < 2; dk++)
                    {
                        c[di,dj,dk] = ranvec[permX[(i+di)&255] ^ permY[(j+dj)&255] ^ permZ[(k+dk)&255]];
                    }
                }
            }

            return PerlinInterp(c, u, v, w);

        }

        public double Turb(ref Vector3d p, int depth = 7)
        {
            var accum = 0.0;
            var tempP = p;
            var weight = 1.0;

            for(int i = 0; i < depth; i++)
            {
                accum += weight * Noise(ref tempP);
                weight *= 0.5;
                tempP *= 2;
            }

            return Math.Abs(accum);
        }

        private int[] PerlinGeneratePerm()
        {
            var p = new int[pointCount];

            for(int i = 0; i < pointCount; i++)
            {
                p[i] = i;
            }

            Permute(ref p, pointCount);

            return p;
        }

        private static void Permute(ref int[] p, int n)
        {
            for(int i = n - 1; i >= 0; i--)
            {
                int target = RndGenerators.RndIntInRange(0, i);
                int tmp = p[i];

                p[i] = p[target];
                p[target] = tmp;
            }
        }

        private static double PerlinInterp(Vector3d[,,] c, double u, double v, double w)
        {
            var uu = u * u * (3 - 2 * u);
            var vv = v * v * (3 - 2 * v);
            var ww = w * w * (3 - 2 * w);
            var accum = 0.0;

            for(int i = 0; i < 2; i++)
            {
                for(int j = 0; j < 2; j++)
                {
                    for(int k = 0; k < 2; k++)
                    {

                        var weightV = new  Vector3d(u-i, v-j, w-k);

                        accum += (i * uu + (1 - i) * (1 - uu)) * (j * vv + (1-j) * 
                            (1 - vv)) * (k * ww + (1 - k) * (1 - ww)) * c[i,j,k].Dot(weightV);
                    }
                }
            }

            return accum;
        }

    }
}
