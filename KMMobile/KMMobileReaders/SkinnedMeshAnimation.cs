using System.Collections.Generic;
using WaveEngine.Common.Math;

namespace KMMobile
{
    /// <summary>
    /// Separate animation for a skinned mesh
    /// </summary>
    public class SkinnedMeshAnimation
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SkinnedMeshAnimation()
        {
            Bones = new List<Bone>();
            Flags = new List<byte>();
            Data = new List<ushort>();
        }
        /// <summary>
        /// This is the magic sequence for a skinned mesh animation
        /// </summary>
        public static int Magic =
        ('a') | ('s' << 8) | ('0' << 16) | ('8' << 24);
        /// <summary>
        /// The bones
        /// </summary>
        public List<Bone> Bones { get; private set; }
        /// <summary>
        /// The animation bounding box minimum
        /// </summary>
        public Vector4 AnimationBoundingBoxMinimum { get; private set; }
        /// <summary>
        /// The animation bounding box size (xyz_max - xyz_min)
        /// </summary>
        public Vector4 AnimationBoundingBoxSize { get; private set; }
        /// <summary>
        /// The flags
        /// </summary>
        public List<byte> Flags { get; private set; }
        /// <summary>
        /// The encoded animation data
        /// </summary>
        public List<ushort> Data { get; private set; }
    }
}
