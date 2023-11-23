using System;
using OpenTK.Graphics.OpenGL;

namespace OpenTK.Shapes
{
    class OGLShape
    {
        public bool EnableAutoRotate { get; set; }
        public bool Orbiting, Moon;
        public float RotatingSpeed;
        public Vector3 Center;
        public float RotatingRadius;
        public float OrbitingSpeed;
        public float MoonOrbit;
        public float MoonSpeed;

        public Mesh[] MeshPolygons { get => meshPolygons; set => meshPolygons = value; }

        private float rotateAngle;
        private float orbitAngle;
        private float moonAngle;
        private float x1, z1, x2, y2, z2;
        private Mesh[] meshPolygons;

        public virtual void Draw()
        {
            GL.PushMatrix();
            GL.Translate(Center.X, Center.Y, -Center.Z);

            if (EnableAutoRotate)
            {
                GL.Rotate(rotateAngle, Vector3.UnitY);
                rotateAngle = rotateAngle < 360 ? rotateAngle + RotatingSpeed : rotateAngle - 360;

                if (Orbiting)
                {
                    Center.X -= x1;
                    Center.Z -= z1;
                    x1 = RotatingRadius * (float)Math.Cos(orbitAngle * Math.PI / 180);
                    z1 = RotatingRadius * (float)Math.Sin(orbitAngle * Math.PI / 180);
                    Center.X += x1;
                    Center.Z += z1;
                    orbitAngle = orbitAngle < 360 ? orbitAngle + OrbitingSpeed : orbitAngle - 360;

                    if (Moon)
                    {
                        Center.X -= x2;
                        Center.Y -= y2;
                        Center.Z -= z2;
                        x2 = MoonOrbit * (float)Math.Cos(moonAngle * Math.PI / 180);
                        y2 = MoonOrbit * (float)Math.Cos(moonAngle * Math.PI / 180);
                        z2 = MoonOrbit * (float)Math.Sin(moonAngle * Math.PI / 180);
                        Center.X += x2;
                        Center.Y += y2;
                        Center.Z += z2;
                        moonAngle = moonAngle < 360 ? moonAngle + MoonSpeed : moonAngle - 360;
                    }
                }
            }

            ShapeDrawing();
            GL.PopMatrix();
        }

        protected virtual void ShapeDrawing() { }
    }
}
