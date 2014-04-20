using System.Collections.Generic;
using System.Linq;
using WaveEngine.Common.Math;

namespace KMMobile
{
    /// <summary>
    /// Defines a surface for a mesh
    /// </summary>
    public class Surface : BaseObjectWithVertices<Vector3>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Surface()
        {
            Thickness = 1.0f;
            ThicknessRatio = 0.5f;
            FaceType = FaceType.Other;
            CullMode = CullMode.None;
            Normals = new List<Vector3>();
            Tangents = new List<Vector3>();
            BiTangents = new List<Vector3>();
            BiNormalDirections = new List<int>();
            TexCoords0 = new List<Vector2>();
            TexCoords1 = new List<Vector2>();
            Indices = new List<uint>();
            Matrix = Matrix.Identity;
            Scale = 1.0f;
            Scale2 = 1.0f;
            Visible = true;
        }
        /// <summary>
        /// The type of material
        /// </summary>
        public FaceType FaceType { get; set; }
        /// <summary>
        /// The thickness of the material
        /// </summary>
        public float Thickness { get; set; }
        /// <summary>
        /// The thickness ratio (inside = 0.0 -> outside = 1.0)
        /// </summary>
        public float ThicknessRatio { get; set; }
        /// <summary>
        /// The culling mode (determines whether we need "two-sided" etc.)
        /// </summary>
        public CullMode CullMode { get; set; }

        /// <summary>
        /// Yields the surface type
        /// </summary>
        public virtual SurfaceType SurfaceType
        {
            get
            {
                return SurfaceType.Surface;
            }
        }
        /// <summary>
        /// The transform of this surface relative to the parent
        /// </summary>
        public Matrix Matrix { get; set; }
        /// <summary>
        /// Scaling applied to the surface (for edge thickness)
        /// </summary>
        public float Scale { get; set; }
        /// <summary>
        /// Scaling applied to the surface (on all axes)
        /// </summary>
        public float Scale2 { get; set; }
        /// <summary>
        /// The normals of this surface
        /// </summary>
        public List<Vector3> Normals { get; private set; }
        /// <summary>
        /// The tangents of this surface
        /// </summary>
        public List<Vector3> Tangents { get; private set; }
        /// <summary>
        /// The bitangents of this surface
        /// </summary>
        public List<Vector3> BiTangents { get; private set; }
        /// <summary>
        /// the binormalDirection to the tangent and normal
        /// </summary>
        public List<int> BiNormalDirections { get; private set; }
        /// <summary>
        /// The texture coordinates 0
        /// </summary>
        public List<Vector2> TexCoords0 { get; private set; }
        /// <summary>
        /// The texture coordinates 1
        /// </summary>
        public List<Vector2> TexCoords1 { get; private set; }
        /// <summary>
        /// The indices
        /// </summary>
        public List<uint> Indices { get; private set; }
        /// <summary>
        /// The material attached to this surface (if any)
        /// </summary>
        public Material Material { get; set; }
        /// <summary>
        /// Whether this surface is visible
        /// </summary>
        public bool Visible { get; set; }
        /// <summary>
        /// Evaluates tangent vectors for all vertices
        /// </summary>
        public void EvaluateTangents()
        {
            if (BiNormalDirections.Count > 0)
            {
                //  Don't need to do everything - we already have tangents
            }
            /*
            else if ((Tangent.Count > 0) && (BiTangent.Count > 0))
            {
                //  We have Tangent and BiTangent but we don't have
                //  BiNormalDirection, so calculate it
            }
            */
            else
            {
                //  Need to evaluate BiTangent and BiNormalDirection
                BiTangents.Clear();
                BiNormalDirections.Clear();
                Tangents.Clear();
                var tan1 = new Vector3[Indices.Count];
                var tan2 = new Vector3[Indices.Count];
                var triangleCount = Indices.Count / 3;
                for (var triangle = 0; triangle < triangleCount; triangle++)
                {
                    var i1 = 0 + 3 * triangle;
                    var i2 = 1 + 3 * triangle;
                    var i3 = 2 + 3 * triangle;
                    var _i1 = (int)Indices[i1];
                    var _i2 = (int)Indices[i2];
                    var _i3 = (int)Indices[i3];
                    var v1 = Vertices[_i1];
                    var v2 = Vertices[_i2];
                    var v3 = Vertices[_i3];
                    var w1 = (TexCoords0.Count > _i1) ? TexCoords0[_i1] : Vector2.Zero;
                    var w2 = (TexCoords0.Count > _i2) ? TexCoords0[_i2] : Vector2.Zero;
                    var w3 = (TexCoords0.Count > _i3) ? TexCoords0[_i3] : Vector2.Zero;
                    var x1 = v2.X - v1.X;
                    var x2 = v3.X - v1.X;
                    var y1 = v2.Y - v1.Y;
                    var y2 = v3.Y - v1.Y;
                    var z1 = v2.Z - v1.Z;
                    var z2 = v3.Z - v1.Z;
                    var s1 = w2.X - w1.X;
                    var s2 = w3.X - w1.X;
                    var t1 = w2.Y - w1.Y;
                    var t2 = w3.Y - w1.Y;
                    var r = 1.0f / (s1 * t2 - s2 * t1);
                    var sdir = new Vector3(
                        (t2 * x1 - t1 * x2) * r,
                        (t2 * y1 - t1 * y2) * r,
                        (t2 * z1 - t1 * z2) * r);
                    var tdir = new Vector3(
                        (s1 * x2 - s2 * x1) * r,
                        (s1 * y2 - s2 * y1) * r,
                        (s1 * z2 - s2 * z1) * r);
                    tan1[i1] = tan1[i2] = tan1[i3] = sdir;
                    tan2[i1] = tan2[i2] = tan2[i3] = tdir;
                }
                var tangents = new Vector3[Vertices.Count];
                var binormaldirs = new int[Vertices.Count];
                for (var index = 0; index < Indices.Count; index++)
                {
                    var vertex = (int)Indices[index];
                    var n = Normals[vertex];
                    var t = tan1[index];
                    tangents[vertex] = t - n * Vector3.Dot(n, t);
                    tangents[vertex].Normalize();
                    binormaldirs[vertex] = Vector3.Dot(Vector3.Cross(n, t), tan2[index]) < 0.0f ? -1 : 1;
                }
                BiTangents.AddRange(tangents);
                BiNormalDirections.AddRange(binormaldirs);
            }
        }
    }
}
