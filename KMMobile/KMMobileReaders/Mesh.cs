using System.Collections.Generic;
using WaveEngine.Common.Math;

namespace KMMobile
{
    /// <summary>
    /// Defines a mesh
    /// </summary>
    public class Mesh : BaseObject<Vector3>, IMesh
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Mesh()
        {
            Surfaces = new List<Surface>();
        }
        /// <summary>
        /// Clears everything
        /// </summary>
        public void Clear()
        {
            Surfaces.Clear();
        }
        /// <summary>
        /// The mesh type
        /// </summary>
        public MeshType MeshType
        {
            get
            {
                return MeshType.Mesh;
            }
        }
        /// <summary>
        /// This is the magic sequence for a mesh
        /// </summary>
        public static int Magic =
        ('m') | ('i' << 8) | ('0' << 16) | ('8' << 24);
        /// <summary>
        /// The surfaces comprising the mesh
        /// </summary>
        public List<Surface> Surfaces { get; private set; }
        /// <summary>
        /// Evaluate bounds
        /// </summary>
        public void EvaluateBounds()
        {
            BoundsEvaluator.EvaluateBounds(this);
        }
    }
}
