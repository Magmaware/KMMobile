using System.Collections.Generic;
using WaveEngine.Common.Math;

namespace KMMobile
{
    /// <summary>
    /// Defines a bone
    /// </summary>
    public class Bone
    {
        /// <summary>
        /// The bone name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The parent of the bone
        /// </summary>
        public int Parent { get; set; }
        /// <summary>
        /// The bone position animation for each frame (if any)
        /// </summary>
        public Dictionary<int, Vector4> BonePositionAnimation { get; private set; }
        /// <summary>
        /// The bone rotation animation for each frame (if any)
        /// </summary>
        public Dictionary<int, Quaternion> BoneRotationQuaternionAnimation { get; private set; }

        public Bone()
        {
            BonePositionAnimation = new Dictionary<int, Vector4>();
            BoneRotationQuaternionAnimation = new Dictionary<int, Quaternion>();
            //bind pose data
            BonePositionAnimation.Add(0, Vector4.Zero);
            BoneRotationQuaternionAnimation.Add(0, Quaternion.Identity);
            Parent = -1;
        }
    }
}
