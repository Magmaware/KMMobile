using System.Collections.Generic;
using WaveEngine.Common.Math;

namespace KMMobile
{
    /// <summary>
    /// Skinned mesh, which may or may not include animation
    /// </summary>
    public class SkinnedMesh : BaseObjectWithVertices<Vector3>, IMesh
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SkinnedMesh()
        {
            Bones = new List<Bone>();
            Surfaces = new List<Surface>();
            FramesCount = 1;
        }
        /// <summary>
        /// The mesh type
        /// </summary>
        public MeshType MeshType
        {
            get
            {
                return MeshType.SkinnedMesh;
            }
        }
        /// <summary>
        /// This is the magic sequence for a skinned mesh
        /// </summary>
        public static int Magic =
        ('m') | ('s' << 8) | ('0' << 16) | ('8' << 24);
        /// <summary>
        /// The bones
        /// </summary>
        public List<Bone> Bones { get; private set; }
        /// <summary>
        /// The surfaces comprising the mesh
        /// </summary>
        public List<Surface> Surfaces { get; private set; }
        /// <summary>
        /// The number of frames in bone-based animation
        /// </summary>
        public int FramesCount { get; private set; }

        /// <summary>
        /// Evaluate bounds
        /// </summary>
        public void EvaluateBounds()
        {
            BoundsEvaluator.EvaluateBounds(this);
        }
    }
}
