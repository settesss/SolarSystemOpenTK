using OpenTK.Graphics.OpenGL;
using System;

namespace OpenTK.Shapes
{
    class OrbitBuilder : OGLShape
    {
        public double Radius { get; set; }

        public Vector3[] Position = new Vector3[361];

        public OrbitBuilder(
            Vector3 center,
            double radius,
            float rotatingSpeed,
            float rotatingRadius,
            float orbitingSpeed,
            float moonOrbit,
            float moonSpeed)
        {
            Center = center;
            Radius = radius;
            RotatingSpeed = rotatingSpeed;
            RotatingRadius = rotatingRadius;
            OrbitingSpeed = orbitingSpeed;
            MoonOrbit = moonOrbit;
            MoonSpeed = moonSpeed;

            for (var phi = 0; phi < 361; phi++)
            {
                Position[phi].X = (float)(Radius * Math.Cos(phi * Math.PI / 180));
                Position[phi].Z = (float)(Radius * Math.Sin(phi * Math.PI / 180));
                Position[phi].Y = 0;
            }
        }

        protected override void ShapeDrawing()
        {
            GL.LineWidth(2);
            GL.Color3(0.329412f, 0.3294128f, 0.329412f);
            GL.Begin(PrimitiveType.LineStrip);

            for (var phi = 0; phi < 361; phi++)
            {
                GL.Vertex3(Position[phi]);
            }

            GL.End();
        }
    }
}
