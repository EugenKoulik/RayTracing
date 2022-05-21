namespace RayTracing
{
    public struct HitRecord
    {
        public Vector3d p;

        public Vector3d normal;

        public Material matPtr;

        public double t;

        bool frontFace;

        public void SetFaceNormal(ref Ray ray, ref Vector3d outwardNormal)
        {
            frontFace = ray.Dir.Dot(outwardNormal) < 0;

            normal = frontFace ? outwardNormal : -outwardNormal;
        }
    }
}