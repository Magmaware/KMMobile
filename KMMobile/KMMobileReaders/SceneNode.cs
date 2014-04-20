using System.Collections.Generic;
using WaveEngine.Common.Math;

namespace KMMobile
{
    public abstract class SceneNode
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SceneNode()
        {
            Transform = Matrix.Identity;
            Children = new List<SceneNode>();
            SelectionType = SceneNodeSelectionType.None;
            Scale = 1.0f;
        }
        /// <summary>
        /// The name of the node
        /// </summary>
        public string Name;
        /// <summary>
        /// whether the node is the root of a seperate object 
        /// </summary>
        public bool Group;
        /// <summary>
        /// The transform relative to the parent node
        /// </summary>
        public Matrix Transform { get; set; }
        /// <summary>
        /// Scaling applied to this node
        /// </summary>
        public float Scale { get; set; }
        /// <summary>
        /// The child nodes of this node
        /// </summary>
        public List<SceneNode> Children;
        /// <summary>
        /// Yields the type of this node
        /// </summary>
        public abstract SceneNodeType Type { get; }
        /// <summary>
        /// The type of selection that applies to this node
        /// </summary>
        public SceneNodeSelectionType SelectionType { get; set; }
        /// <summary>
        /// Yields all children
        /// </summary>
        public IEnumerable<SceneNode> AllChildren
        {
            get
            {
                yield return this;
                foreach (var child in Children)
                {
                    foreach (var c in child.AllChildren)
                        yield return c;
                }
            }
        }
    }
}
