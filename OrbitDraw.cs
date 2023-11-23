using OpenTK.Graphics.OpenGL;
using System;

namespace OpenTK.Shapes
{
    class OrbitDraw : OGLShape
    {
        public OrbitDraw(Vector3 center, double radius, float rotatingSpeed, float rotatingRadius, float orbitingSpeed, float moonOrbit, float moonSpeed)
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
                position[phi].X = (float)(Radius * Math.Cos(phi * Math.PI / 180));
                position[phi].Z = (float)(Radius * Math.Sin(phi * Math.PI / 180));
                position[phi].Y = 0;
            }
        }

        public double Radius { get; set; }
        public Vector3[] position = new Vector3[361];

        protected override void ShapeDrawing()
        {
            GL.LineWidth(2);
            GL.Color3(0.329412f, 0.3294128f, 0.329412f);
            GL.Begin(PrimitiveType.LineStrip);

            for (var phi = 0; phi < 361; phi++)
            {
                GL.Vertex3(position[phi]);
            }

            GL.End();
        }
    }
}
