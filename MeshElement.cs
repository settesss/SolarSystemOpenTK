using OpenTK;

namespace OpenTK.Shapes
{
    partial class MeshElement
    {
        public MeshElement(Vector3[] vertices, Vector2[] texcoord)
        {
            Vertices = vertices;
            Texcoord = texcoord;
        }

        public Vector3[] Vertices { get; private set; }
        public Vector2[] Texcoord { get; private set; }
    }
}
