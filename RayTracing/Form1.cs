using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Drawing = System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace RayTracing
{
    public partial class Form1 : Form
    {
        double aspectRatio;
        int samplePerPixel = 10;
        int maxDepth = 50;

        Vector3d lookFrom = new Vector3d(1.0, 0.0, 0.0);
        Vector3d lookAt = new Vector3d(0.0, 0.0, 0.0);
        Vector3d vup = new Vector3d(0.0, 1.0, 0.0);

        double distToFocus = 10.0;
        double apertude = 0.1;

        Camera camera;
        HittableList world;
        Random random;

        public Form1()
        {
            InitializeComponent();
            
            aspectRatio = pictureBox1.Width / pictureBox1.Height;

            camera = new Camera(lookFrom, lookAt, vup, 20.0, aspectRatio, apertude, distToFocus);

            world = RayTracer.RandomScene();

            random = new Random();

            pictureBox1.Image = Draw(new Bitmap(pictureBox1.Width, pictureBox1.Height));
        }

        private Bitmap Draw(Bitmap bmp)
        {               
            for (int j = bmp.Height - 1; j >= 0; --j)
            {
                for (int i = 0; i <  bmp.Width; ++i)		
                {
                    var pixelColor = Vector3d.Null;

                    for(int s = 0; s < samplePerPixel; ++s)
                    {
                        var u = (double)(i + random.NextDouble()) / bmp.Height;
                        var v = (double)(j + random.NextDouble()) / bmp.Width;

                        var r = camera.GetRay(u, v);

                        pixelColor += RayTracer.GetColor(ref r, ref world, maxDepth);

                    }

                    bmp.SetPixel(i, j, WriteColor(pixelColor, samplePerPixel));
                }
            }

            return bmp;
        }

        private Color WriteColor(Vector3d pixelColor, int samplePerPixel)
        {
            var r = pixelColor.X;
            var g = pixelColor.Y;
            var b = pixelColor.Z;

            var scale = 1.0 / samplePerPixel;

            r = Math.Sqrt(scale * r);
            g = Math.Sqrt(scale * g);
            b = Math.Sqrt(scale * b);

            var lol1 = 256.0 * Math.Clamp(r, 0.0, 0.999);
            var lol2 = 256.0 * Math.Clamp(g, 0.0, 0.999);
            var lol3 = 256.0 * Math.Clamp(b, 0.0, 0.999);

            return Color.FromArgb((int)lol1, (int)lol2, (int)lol3);

        }

    }
}