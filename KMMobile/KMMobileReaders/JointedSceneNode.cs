namespace KMMobile
{
    /// <summary>
    /// A node which is jointed relative to the parent
    /// </summary>
    public class JointedSceneNode : MeshSceneNode
    {
        /// <summary>
        /// Yields the type of this node
        /// </summary>
        public override SceneNodeType Type
        {
            get
            {
                return SceneNodeType.JointedSceneNode;
            }
        }

        /// <summary>
        /// The hinge between this node and its parent
        /// </summary>
        public Hinge Joint { get; set; }
    }
}
