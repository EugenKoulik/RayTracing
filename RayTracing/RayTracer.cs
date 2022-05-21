using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    internal class RayTracer
    {       
        public RayTracer()
        {

        }

        public static Vector3d GetColor(ref Ray ray, ref HittableList world, int dept)
        {
            var rec = new HitRecord();

            if(dept <= 0)
            {
                return Vector3d.Null;
            }

            if(world.Hit(ref ray, 0.001, 10000, ref rec))
            {
                Ray scattered;
                Vector3d attenuation;

                if (rec.matPtr != null && rec.matPtr.Scatter(ref ray, ref rec, out attenuation, out scattered))

                    return attenuation * GetColor(ref scattered, ref world, dept - 1);

                return Vector3d.Null;
            }

            Vector3d unitDirection = Vector3d.UnitVector(ray.Dir);

            var t = (unitDirection.Y + 1.0) * 0.5;

            return new Vector3d(1.0, 1.0, 1.0) * (1.0 - t) + new Vector3d(0.5, 0.7, 1.0) * t;
        }


        public static HittableList RandomScene()
        {
            HittableList world = new HittableList();

            /*var groupMaterial = new Lambertian(new Vector3d(0.5, 0.5, 0.5));

            world.Add(new Sphere(new Vector3d(0.0, -1000.0, 0.0), 1000.0, groupMaterial));

            var random = new Random();

            for(int a = -11; a < 11; a++)
            {
                for(int b = -11; b < 11; b++)
                {
                    var chooseMat = random.NextDouble();

                    var center = new Vector3d(a + 0.9 * random.NextDouble(), 0.2, b + 0.9 * random.NextDouble());


                    if((center - new Vector3d(4.0, 0.2, 0.0)).Length() > 0.9)
                    {
                        Material sphereMaterial;

                        if(chooseMat < 0.8)
                        {
                            var albedo = Vector3d.RandomVector() * Vector3d.RandomVector();

                            sphereMaterial = new Lambertian(albedo);

                            world.Add(new Sphere(center, 0.2, sphereMaterial));
                        }
                        else //if (chooseMat < 0.95)
                        {

                            var albedo = Vector3d.RandomVector(0.5, 1.0);
                            var fuzz = RndGenerators.RndDoubleInRange(0.0, 0.5);

                            sphereMaterial = new Metal(albedo, fuzz);

                            world.Add(new Sphere(center, 0.2, sphereMaterial));

                        }
                    }
                }
            }*/

            var material1 = new Lambertian(new Vector3d(1.0, 1.0, 1.0));
            world.Add(new Sphere(new Vector3d(100.0, 10.0, 100.0), 100.0, material1));

            //var material2 = new Metal(new Vector3d(0.7, 0.6, 0.5), 0.0);
            //world.Add(new Sphere(new Vector3d(0.0, 0.0, -50.0), 54.0, material2));

            return world;

        }
    }
}
