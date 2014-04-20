namespace KMMobile
{
    /// <summary>
    /// The base class for Surface, Mesh, Contour etc.
    /// </summary>
    public class BaseObject<T>
    {
        /// <summary>
        /// The name of this object
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Bounding box minimum
        /// </summary>
        public T BoundingBoxMinimum { get; set; }
        /// <summary>
        /// Bounding box maximum
        /// </summary>
        public T BoundingBoxMaximum { get; set; }
        /// <summary>
        /// Bounding sphere centre
        /// </summary>
        public T SphereCentre { get; set; }
        /// <summary>
        /// Bounding sphere radius
        /// </summary>
        public float SphereRadius { get; set; }
    }
}
