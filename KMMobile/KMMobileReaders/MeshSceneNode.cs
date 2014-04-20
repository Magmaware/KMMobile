namespace KMMobile
{
    public enum WallType
    {
        /// <summary>
        /// Has no thickness
        /// </summary>
        NoThickness = 0,
        /// <summary>
        /// A single wall (folds around the centre)
        /// </summary>
        SingleWall = 1,
        /// <summary>
        /// A double wall (folds around a given offset)
        /// </summary>
        DoubleWall = 2,
        /// <summary>
        /// A double wall (folds around a given offset)
        /// </summary>
        TripleWall = 3,
        /// <summary>
        /// Xanita (folds around the outside)
        /// </summary>
        SolidBoard = 4,
        /// <summary>
        /// Xanita (folds around the outside)
        /// </summary>
        Structural = 5
    }

    /// <summary>
    /// A mesh scene node
    /// </summary>
    public class MeshSceneNode : SceneNode
    {
        /// <summary>
        /// Yields the type of this node
        /// </summary>
        public override SceneNodeType Type
        {
            get
            {
                return SceneNodeType.MeshSceneNode;
            }
        }

        public WallType wallType { get; set; }

        /// <summary>
        /// The mesh for this node
        /// </summary>
        public IMesh Mesh { get; set; }
    }
}
