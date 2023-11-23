using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using OpenTK.Shapes;

namespace OpenTK
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] texturePaths =
            {
                "earth-day.jpg", "sun.jpg", "moon.jpg",
                "mercury.jpg", "venus.jpg", "mars.jpg", "jupiter.jpg",
                "saturn.jpg", "uranus.jpg", "neptune.jpg", "saturn-ring.png"
            };

            var bitmaps = new Bitmap[texturePaths.Length];
            var bitmapDatas = new BitmapData[texturePaths.Length];

            try
            {
                for (var i = 0; i < texturePaths.Length; i++)
                {
                    var texturePath = System.IO.Path.Combine(
                        System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        @"Textures\texture\",
                        texturePaths[i]);

                    bitmaps[i] = new Bitmap(texturePath);
                    var rect = new Rectangle(0, 0, bitmaps[i].Width, bitmaps[i].Height);
                    bitmapDatas[i] = bitmaps[i]
                        .LockBits(rect, ImageLockMode.ReadOnly, bitmaps[i].PixelFormat);
                }

                var view = new Viewport(
                    bitmapDatas[1],
                    bitmapDatas[3],
                    bitmapDatas[4],
                    bitmapDatas[0],
                    bitmapDatas[2],
                    bitmapDatas[5],
                    bitmapDatas[6],
                    bitmapDatas[7],
                    bitmapDatas[10],
                    bitmapDatas[8],
                    bitmapDatas[9]);

                var initialPosition = new Vector3(0.0f, 0.0f, 0.0f);

                var venusOrbit = new OrbitBuilder(
                    initialPosition,
                    6,
                    1,
                    15,
                    1,
                    2,
                    2);
                view.AddShape(venusOrbit);

                var marsOrbit = new OrbitBuilder(
                    initialPosition,
                    11,
                    1,
                    15,
                    1,
                    2,
                    2);
                view.AddShape(marsOrbit);

                var earthOrbit = new OrbitBuilder(
                    initialPosition,
                    15,
                    1,
                    15,
                    1,
                    2,
                    2);
                view.AddShape(earthOrbit);

                var mercuryOrbit = new OrbitBuilder(
                    initialPosition,
                    23,
                    1,
                    20,
                    1,
                    2,
                    2);
                view.AddShape(mercuryOrbit);

                var jupiterOrbit = new OrbitBuilder(
                    initialPosition,
                    30,
                    1,
                    25,
                    1,
                    2,
                    2);
                view.AddShape(jupiterOrbit);

                var saturnOrbit = new OrbitBuilder(
                    initialPosition,
                    35,
                    1,
                    30,
                    1,
                    2,
                    2);
                view.AddShape(saturnOrbit);

                var uranusOrbit = new OrbitBuilder(
                    initialPosition,
                    40,
                    1,
                    30,
                    1,
                    2,
                    2);
                view.AddShape(uranusOrbit);

                var neptuneOrbit = new OrbitBuilder(
                    initialPosition,
                    45,
                    1,
                    30,
                    1,
                    2,
                    2);
                view.AddShape(neptuneOrbit);

                view.Start();
            }
            finally
            {
                for (var i = 0; i < texturePaths.Length; i++)
                {
                    if (bitmaps[i] != null && bitmapDatas[i] != null)
                    {
                        bitmaps[i].UnlockBits(bitmapDatas[i]);
                    }
                }
            }
        }
    }
}
