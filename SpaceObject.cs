using OpenTK.Graphics.OpenGL;

namespace OpenTK.Shapes
{
    class SpaceObject : OGLShape
    {
        public bool IsStar { get; set; }

        public double Radius { get; set; }

        public int TexType { get; set; }

        public bool IsLinearMotion { get; set; }

        public Vector3? LinearMotionDirection { get; set; }

        public float LinearMotionSpeed { get; set; }

        public SpaceObject(Vector3 center,
            double radius,
            bool autoRotate,
            bool orbiting,
            bool moon,
            float rotatingSpeed,
            float rotatingRadius,
            float orbitingSpeed,
            float moonOrbit,
            float moonSpeed,
            int texType,
            bool isStar,
            bool isLinearMotion = false,
            Vector3? linearMotionDirection = null,
            float linearMotionSpeed = 0)
        {
            Center = center;
            Radius = radius;
            MeshPolygons = Mesh.Sphere(Radius);
            EnableAutoRotate = autoRotate;
            Orbiting = orbiting;
            Moon = moon;
            RotatingSpeed = rotatingSpeed;
            RotatingRadius = rotatingRadius;
            OrbitingSpeed = orbitingSpeed;
            MoonOrbit = moonOrbit;
            MoonSpeed = moonSpeed;
            TexType = texType;
            IsStar = isStar;
            IsLinearMotion = isLinearMotion;
            LinearMotionDirection = linearMotionDirection;
            LinearMotionSpeed = linearMotionSpeed;
        }

        protected override void ShapeDrawing()
        {
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            SetupLightingAndMaterial();

            GL.BindTexture(TextureTarget.Texture2D, TexType);

            GL.Begin(PrimitiveType.Quads);

            foreach (var polygon in MeshPolygons)
            {
                for (var j = 0; j < 4; j++)
                {
                    var vertex = polygon.Vertices[j];
                    var normal = vertex.Normalized();

                    GL.Normal3(normal);
                    GL.TexCoord2(polygon.Texcoord[j]);
                    GL.Vertex3(vertex);
                }
            }

            GL.End();
        }

        private void SetupLightingAndMaterial()
        {
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);

            if (IsStar)
            {
                SetupStarLightingAndMaterial();
            }
            else
            {
                if (IsLinearMotion)
                {
                    SetupCometLightingAndMaterial();
                }
                else
                {
                    SetupPlanetLightingAndMaterial();
                }
            }

            float[] lightModelAmbient = { 1f, 1f, 1f, 1000f };
            GL.LightModel(LightModelParameter.LightModelAmbient, lightModelAmbient);
        }

        private void SetupStarLightingAndMaterial()
        {
            float[] lightPosition = { 1f, 1f, 1f, 1f };
            float[] lightColor = { 2.0f, 2.0f, 2.0f, 2.0f };

            GL.Light(LightName.Light0, LightParameter.Position, lightPosition);
            GL.Light(LightName.Light0, LightParameter.Diffuse, lightColor);
            GL.Light(LightName.Light0, LightParameter.Specular, lightColor);

            float[] lightAmbient = { 1.0f, 1.0f, 1.0f, 1.0f };
            GL.Light(LightName.Light0, LightParameter.Ambient, lightAmbient);

            float[] emission = { 1.0f, 1.0f, 1.0f, 1.0f };
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, emission);
        }

        private void SetupPlanetLightingAndMaterial()
        {
            float[] ambientLightSide = { 0.05f, 0.05f, 0.05f, 1f };
            float[] diffuseLightSide = { 0.2f, 0.2f, 0.2f, 1f };
            float[] specularLightSide = { 0.05f, 0.05f, 0.45f, 1f };

            float[] ambientDarkSide = { 0.02f, 0.02f, 0.02f, 1f };
            float[] diffuseDarkSide = { 0.1f, 0.1f, 0.1f, 1f };

            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, ambientLightSide);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, diffuseLightSide);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, specularLightSide);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, 1.0f);

            float[] noEmission = { 0.0f, 0.0f, 0.0f, 1.0f };

            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, ambientDarkSide);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, diffuseDarkSide);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, noEmission);
        }

        private void SetupCometLightingAndMaterial()
        {
            float[] orangeColor = { 0.9f, 0.1f, 0.0f, 1f };

            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, orangeColor);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, orangeColor);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, orangeColor);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, 1.0f);

            float[] emission = { 0.9f, 0.1f, 0.0f, 1.0f };
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, emission);
        }

        public void MoveLinearMotion(float deltaTime)
        {
            if (IsLinearMotion && LinearMotionDirection != null)
            {
                Center += LinearMotionDirection.Value * LinearMotionSpeed * deltaTime;
            }
        }
    }
}
