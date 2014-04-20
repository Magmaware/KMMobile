using System.Collections.Generic;

namespace KMMobile
{
    /// <summary>
    /// A base object with vertices
    /// </summary>
    /// <typeparam name="T">The vertex type</typeparam>
    public class BaseObjectWithVertices<T> : BaseObject<T>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BaseObjectWithVertices()
        {
            Vertices = new List<T>();
        }
        /// <summary>
        /// The vertices of this object
        /// </summary>
        public List<T> Vertices { get; protected set; }
        /// <summary>
        /// Adds a vertex
        /// </summary>
        /// <param name="p">The vertex to add</param>
        public virtual void AddVertex(T p)
        {
            Vertices.Add(p);
        }
    }
}
