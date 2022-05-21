using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    public abstract class Material
    {
        public abstract bool Scatter(ref Ray rIn, ref HitRecord hitInfo, out Vector3d attenuation, out Ray scatteredRay);

    }

    internal class Metal : Material
    {

        public Vector3d Albedo;
        public double Fuzziness;

        public Metal(Vector3d albedo, double fuzziness)
        {
            Albedo = albedo;
            Fuzziness = fuzziness;
        }

        public override bool Scatter(ref Ray ray, ref HitRecord hitInfo, out Vector3d attenuation, out Ray scatteredRay)
        {

            var reflectedRay = Ray.Reflect(Vector3d.UnitVector(ray.Dir), hitInfo.normal);

            scatteredRay = new Ray(hitInfo.p, reflectedRay + RndGenerators.RandomInSphere() * Fuzziness);

            attenuation = Albedo;

            return scatteredRay.Dir.Dot(hitInfo.normal) > 0;
        }
    }

    internal class Lambertian : Material
    {
        public Vector3d Albedo;

        public Lambertian(Vector3d albedo)
        {
            Albedo = albedo;
        }

        public override bool Scatter(ref Ray ray, ref HitRecord hitInfo, out Vector3d attenuation, out Ray scatteredRay)
        {
            var scatterDirection = hitInfo.normal + Vector3d.RandomVector();

            if (scatterDirection.NearZero())
            {
                scatterDirection = hitInfo.normal;

            }
            scatteredRay = new Ray(hitInfo.p, scatterDirection);

            attenuation = Albedo;

            return true;
        }
    }

    /*internal class Dielectric : Material
    {
        private double refractiveIndex;

        public Dielectric(double refractiveIndex)
        {
            this.refractiveIndex = refractiveIndex;

        }


       
    }*/
}
