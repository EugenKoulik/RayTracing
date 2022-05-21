using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    internal static class RndGenerators
    {
        private static Random random = new Random();

        public static Vector3d RandomInSphere()
        {
            Vector3d p;

            do
            {
                p = (new Vector3d(random.NextDouble(), random.NextDouble(), random.NextDouble()) * 2) - new Vector3d(1.0, 1.0, 1.0);

            } while (p.Dot(p) >= 1.0);

            return p;
        }

        public static Vector3d EndInDisk()
        {
            Vector3d p;

            do
            {
                p = (new Vector3d(random.NextDouble(), random.NextDouble(), random.NextDouble()) * 2) - new Vector3d(1.0, 1.0, 1.0);

            } while (p.Dot2D(p) >= 1.0);

            return p;
        }

        public static double RndDoubleInRange(double time0, double time1)
        {
            return random.NextDouble() * (time1 - time0) + time0;
        }

        public static int RndIntInRange(int time0, int time1)
        {
            return random.Next(time0, time1);
        }
    }
}
