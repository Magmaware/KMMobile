using System.Collections.Generic;

namespace KMMobile
{
    /// <summary>
    /// A skinned surface
    /// </summary>
    public class SkinnedSurface : Surface
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SkinnedSurface()
        {
            VertexWeights = new List<VertexWeight[]>();
        }
        /// <summary>
        /// Yields the surface type
        /// </summary>
        public override SurfaceType SurfaceType
        {
            get
            {
                return SurfaceType.SkinnedSurface;
            }
        }
        /// <summary>
        /// The vertex weights
        /// </summary>
        public List<VertexWeight[]> VertexWeights { get; private set; }
    }
}
