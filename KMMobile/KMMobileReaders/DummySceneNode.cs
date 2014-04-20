namespace KMMobile
{
    /// <summary>
    /// A dummy scene node doesn't have a mesh
    /// </summary>
    public class DummySceneNode : SceneNode
    {
        /// <summary>
        /// Yields the type of this node
        /// </summary>
        public override SceneNodeType Type
        {
            get
            {
                return SceneNodeType.DummySceneNode;
            }
        }
    }
}
