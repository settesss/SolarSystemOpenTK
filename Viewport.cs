namespace OpenTK
{
    using System.Drawing.Imaging;

    partial class Viewport
    {
        public GameWindow Window { get; }

        public BitmapData texData1;

        public BitmapData texData2;

        public BitmapData texData3;

        public BitmapData texData4;

        public BitmapData texData5;

        public BitmapData texData6;

        public BitmapData texData7;

        public BitmapData texData8;

        public BitmapData texData9;

        public BitmapData texData10;

        public BitmapData texData11;

        public Viewport(
            BitmapData textureData1,
            BitmapData textureData2,
            BitmapData textureData3,
            BitmapData textureData4,
            BitmapData textureData5,
            BitmapData textureData6,
            BitmapData textureData7,
            BitmapData textureData8,
            BitmapData textureData9,
            BitmapData textureData10,
            BitmapData textureData11)
        {
            Window = new GameWindow(1720, 800);
            InitializeObjects();
            SetEvents();
            texData1 = textureData1;
            texData2 = textureData2;
            texData3 = textureData3;
            texData4 = textureData4;
            texData5 = textureData5;
            texData6 = textureData6;
            texData7 = textureData7;
            texData8 = textureData8;
            texData9 = textureData9;
            texData10 = textureData10;
            texData11 = textureData11;

            InitializeStarPositions();
        }

        public Vector3[] StarPosition = new Vector3[1000];

        public void Start()
        {
            Window.Run(60.0);
        }

        public void AddShape(Shapes.OGLShape oGlShape)
        {
            _drawList.Add(oGlShape);
        }

        private void InitializeStarPositions()
        {
            for (var i = 0; i < 1000; i++)
            {
                StarPosition[i].X = 0;
                StarPosition[i].Y = 0;
                StarPosition[i].Z = 0;
            }
        }
    }
}
