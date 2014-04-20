namespace KMMobile
{
    /// <summary>
    /// Represents a vertex weight for skinned surfaces
    /// </summary>
    public class VertexWeight
    {
        /// <summary>
        /// The bone to which the vertex is assigned
        /// </summary>
        public ushort Bone { get; set; }
        /// <summary>
        /// The weight
        /// </summary>
        public byte Weight { get; set; }
    }
}
