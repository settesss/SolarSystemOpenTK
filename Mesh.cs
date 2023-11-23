namespace OpenTK.Shapes
{
    partial class Mesh
    {
        public Vector3[] Vertices { get; private set; }

        public Vector2[] Texcoord { get; private set; }
        public Mesh(Vector3[] vertices, Vector2[] texCoord)
        {
            Vertices = vertices;
            Texcoord = texCoord;
        }
    }
}
