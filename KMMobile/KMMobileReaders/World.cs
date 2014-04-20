using KMMobile.X3D;
using System.Collections.Generic;

namespace KMMobile
{
    /// <summary>
    /// A scene
    /// </summary>
    public class World : SceneNode
    {
        /// <summary>
        /// Yields the type of this node
        /// </summary>
        public override SceneNodeType Type
        {
            get
            {
                return SceneNodeType.World;
            }
        }

        /// <summary>
        /// The list of materials
        /// </summary>
        public List<Material> Materials;

        /// <summary>
        /// Constructor
        /// </summary>
        public World()
        {
            Materials = new List<Material>();
        }

        /// <summary>
        /// The thumbnail (if available)
        /// </summary>
        public X3DMobileImage Thumbnail { get; set; }

        /// <summary>
        /// The preview (if available)
        /// </summary>
        public X3DMobileImage Preview { get; set; }

        /// <summary>
        /// Whether this is for a KSN
        /// </summary>
        public bool KSN { get; set; }

        /// <summary>
        /// Whether this world object is made up of smaller grouped items
        /// </summary>
        public bool Grouped { get; set; }
    }
}
