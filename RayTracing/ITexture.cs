using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    internal interface ITexture
    {
        public Vector3d Color(double u, double v, ref Vector3d p);
    }

}
