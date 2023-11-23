using System.Drawing;

namespace OpenTK
{
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Input;
    using OpenTK.Shapes;
    using System;
    using System.Collections.Generic;
    using System.Drawing.Imaging;

    partial class Viewport : Program
    {
        private bool leftMouseDown;

        private bool rightMouseDown;

        private Point lastMousePos;

        private float zoomSpeed = 3.0f;

        static readonly Vector3 InitialPosition = new Vector3(0.0f, 0.0f, 0.0f);

        static readonly Vector3 cometInitialPosition = new Vector3(0.0f, 20.0f, 0.0f);

        static Vector3 linearMotionDirection = new Vector3(-1.5f, -1.5f, 0f);

        static float linearMotionSpeed = 2.0f;

        private SpaceObject _sun = new SpaceObject(InitialPosition, 5, true, false, false, 1, 0, 0, 0, 0, 1, true);
        private SpaceObject _mercury = new SpaceObject(InitialPosition, 0.488f, true, true, false, 1, 6, 1.5f, 0, 0, 2, false);
        private SpaceObject _venus = new SpaceObject(InitialPosition, 1, true, true, false, 2, 11, 1.3f, 0, 0, 3, false);
        private SpaceObject _earth = new SpaceObject(InitialPosition, 1.3f, true, true, false, 3, 15, 1.1f, 0, 0, 4, false);
        private SpaceObject _moon = new SpaceObject(InitialPosition, 0.7f, true, true, true, 1, 15, 1.1f, 1.5f, 2f, 5, false);
        private SpaceObject _mars = new SpaceObject(InitialPosition, 0.88f, true, true, false, 1, 23, 0.9f, 0, 0, 6, false);
        private SpaceObject _marsSatellite = new SpaceObject(InitialPosition, 0.4f, true, true, true, 1, 23, 0.9f, 1f, 2f, 5, false);
        private SpaceObject _jupiter = new SpaceObject(InitialPosition, 2.5f, true, true, false, 1, 30, 0.6f, 0, 0, 7, false);
        private SpaceObject _saturn = new SpaceObject(InitialPosition, 2f, true, true, false, 1, 35, 0.5f, 0, 0, 8, false);
        private SpaceObject _uranus = new SpaceObject(InitialPosition, 1.5f, true, true, false, 1, 40, 0.3f, 0, 0, 9, false);
        private SpaceObject _neptune = new SpaceObject(InitialPosition, 1.3f, true, true, false, 1, 45, 0.2f, 0, 0, 10, false);
        private SpaceObject _comet = new SpaceObject(cometInitialPosition, 0.5f, true, true, false, 1, 50, 0.1f, 0, 0, 11, false, true, linearMotionDirection, linearMotionSpeed);

        private List<OGLShape> _drawList;

        public Key KeyPressed;

        private void InitializeObjects()
        {
            _drawList = new List<OGLShape>
            {
                _sun,
                _mercury,
                _venus,
                _earth,
                _moon,
                _mars,
                _marsSatellite,
                _jupiter,
                _saturn,
                _uranus,
                _neptune,
                _comet
            };
        }

        private void SetEvents()
        {
            Window.RenderFrame += Window_RenderFrame;
            Window.Resize += Form_Resize;
            Window.Load += Form_Load;
            Window.KeyDown += Window_KeyDown;
            Window.MouseWheel += Window_MouseWheel;
            Window.MouseUp += Window_MouseUp;
            Window.MouseDown += Window_MouseDown;
            Window.MouseMove += Window_MouseMove;
        }

        private void MoveModel(Vector3 direction, float speed)
        {
            KeyPressed = Key.Clear;
            GL.MatrixMode(MatrixMode.Projection);
            GL.Translate(direction * speed);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        private void RotateModel(Vector3 axis, float speed)
        {
            KeyPressed = Key.Clear;
            GL.MatrixMode(MatrixMode.Projection);
            GL.Rotate(speed, axis);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);

            var textureCount = 11;
            int[] textureIDs = new int[textureCount];
            BitmapData[] textureDataArray = {
                texData1,
                texData2,
                texData3,
                texData4,
                texData5,
                texData6,
                texData7,
                texData8,
                texData9,
                texData10,
                texData11};

            for (var i = 0; i < textureCount; i++)
            {
                GL.GenTextures(1, out textureIDs[i]);
                GL.BindTexture(TextureTarget.Texture2D, textureIDs[i]);
                GL.TexImage2D(
                    TextureTarget.Texture2D,
                    0,
                    PixelInternalFormat.Rgb,
                    textureDataArray[i].Width,
                    textureDataArray[i].Height,
                    0,
                    Graphics.OpenGL.PixelFormat.Bgr,
                    PixelType.UnsignedByte,
                    textureDataArray[i].Scan0);
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            }
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            if (Window.Height != 0)
            {
                GL.Viewport(0, 0, Window.Width, Window.Height);

                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();

                var fov = 60f * (float)Math.PI / 180.0f;

                var aspectRatio = (float)Window.Width / Window.Height;

                var perspective =
                    Matrix4.CreatePerspectiveFieldOfView(fov, aspectRatio, 0.10f, 200.0f);
                GL.LoadMatrix(ref perspective);

                GL.MatrixMode(MatrixMode.Modelview);

                MoveModel(Vector3.UnitY, 0);
                MoveModel(Vector3.UnitZ, -120);
                RotateModel(Vector3.UnitX, 40);
            }
        }

        private void Window_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            KeyPressed = e.Key;
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                MoveModel(Vector3.UnitY, -zoomSpeed);
                MoveModel(Vector3.UnitZ, -zoomSpeed);
            }
            else
            {
                MoveModel(-Vector3.UnitY, -zoomSpeed);
                MoveModel(-Vector3.UnitZ, -zoomSpeed);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                leftMouseDown = true;
                lastMousePos = new Point(e.X, e.Y);
            }
            else if (e.Button == MouseButton.Right)
            {
                rightMouseDown = true;
                lastMousePos = new Point(e.X, e.Y);
            }
        }

        private void Window_MouseMove(object sender, MouseMoveEventArgs e)
        {
            if (leftMouseDown)
            {
                var deltaX = e.X - lastMousePos.X;
                var deltaY = e.Y - lastMousePos.Y;

                MoveModel(Vector3.UnitX, deltaX * 0.01f);
                MoveModel(Vector3.UnitY, -deltaY * 0.01f);

                lastMousePos = new Point(e.X, e.Y);
            }

            if (rightMouseDown)
            {
                var deltaX = e.X - lastMousePos.X;
                var deltaY = e.Y - lastMousePos.Y;

                RotateModel(Vector3.UnitY, deltaX * 0.5f);
                RotateModel(Vector3.UnitX, deltaY * 0.5f);

                lastMousePos = new Point(e.X, e.Y);
            }
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                leftMouseDown = false;
            }
            else if (e.Button == MouseButton.Right)
            {
                rightMouseDown = false;
            }
        }

        private void Window_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.LoadIdentity();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _comet.MoveLinearMotion((float)e.Time);

            _comet.MoveLinearMotion((float)e.Time);

            foreach (var shape in _drawList)
            {
                shape.Draw();
            }

            Window.SwapBuffers();
        }
    }
}
