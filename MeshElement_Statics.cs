using System;
using System.Collections.Generic;
using OpenTK;

namespace OpenTK.Shapes
{
    partial class MeshElement
    {
        private static readonly double ConverterFactor = Math.PI / 180;

        public static MeshElement[] Sphere(double radius)
        {
            var res = new List<MeshElement>();

            var delta = 10;
            float s = 36;
            float t = 0;
            var sFactor = delta / 10f / 36f;
            var tFactor = delta / 10f / 18f;

            for (var phi = 0; phi <= 180 - delta; phi += delta)
            {
                for (var theta = 0; theta <= 360 - delta; theta += delta)
                {
                    var _1 = GetCartesianOf(radius, phi, theta);
                    var _1Tex = new Vector2(s * sFactor, t * tFactor);

                    var _2 = GetCartesianOf(radius, phi + delta, theta);
                    var _2Tex = new Vector2(s * sFactor, (t + 1) * tFactor);

                    var _3 = GetCartesianOf(radius, phi + delta, theta + delta);
                    var _3Tex = new Vector2((s - 1) * sFactor, (t + 1) * tFactor);

                    var _4 = GetCartesianOf(radius, phi, theta + delta);
                    var _4Tex = new Vector2((s - 1) * sFactor, t * tFactor);

                    Vector3[] vertices = { _1, _2, _3, _4 };
                    Vector2[] texcoord = { _1Tex, _2Tex, _3Tex, _4Tex };
                    res.Add(new MeshElement(vertices, texcoord));
                    s--;
                }
                t++;
            }

            return res.ToArray();
        }

        private static Vector3 GetCartesianOf(double radius, int theta, int phi)
        {
            var th = theta * ConverterFactor;
            var ph = phi * ConverterFactor;
            var x = Convert.ToSingle(radius * Math.Sin(th) * Math.Cos(ph));
            var z = Convert.ToSingle(radius * Math.Sin(th) * Math.Sin(ph));
            var y = Convert.ToSingle(radius * Math.Cos(th));
            return new Vector3(x, y, z);
        }
    }
}
