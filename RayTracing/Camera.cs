using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    internal class Camera
    {
        private Vector3d origin;
        private Vector3d lowerLeftCorner;
        private Vector3d horizontal;
        private Vector3d vertical;
        private Vector3d u, v, w;

        private double lensRadius;            
        private double time0, time1;

        public Camera() : this(new Vector3d(0.0, 0.0, -1.0), 
            new Vector3d(0.0, 0.0, 0.0), new Vector3d(0.0, 1.0, 0.0), 40.0, 1.0, 0.0, 10.0)
        {

        }

        public Camera(Vector3d lookFrom, Vector3d lookAt, Vector3d vup, double vfov, 
            double aspectRatio, double focusDist, double time0 = 0, double time1 = 0)
        {
            var theta = (Math.PI / 180) * vfov;
            var h = Math.Tan(theta / 2);
            var viewportHeight = 2.0 * h;
            var viewportWidth = aspectRatio * viewportHeight;

            w = Vector3d.UnitVector(lookFrom - lookAt);
            u = Vector3d.UnitVector(vup.Cross(w));
            v = w.Cross(u);

            origin = lookFrom;
            horizontal = u * focusDist * viewportWidth;
            vertical = v * focusDist * viewportHeight;
            lowerLeftCorner = origin - horizontal / 2 - vertical / 2 - w * focusDist;

            lensRadius = aspectRatio / 2;
            this.time0 = time0;
            this.time1 = time1;
            
        }

        public Ray GetRay(double s, double t)
        {
            var rd = RndGenerators.EndInDisk() * lensRadius;
            
            var offset = u * rd.X + v * rd.Y;

            return new Ray(origin + offset, lowerLeftCorner + horizontal * s + vertical * t - offset - origin, 
                RndGenerators.RndDoubleInRange(time0, time1));
        }

    }
}
