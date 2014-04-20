namespace KMMobile
{
    /// <summary>
    /// The culling mode
    /// </summary>
    public enum CullMode
    {
        /// <summary>
        /// No culling (requires "two sided")
        /// </summary>
        None,
        /// <summary>
        /// Clockwise culling (two sided if incompatible with the renderer)
        /// </summary>
        Clockwise,
        /// <summary>
        /// Anticlockwise culling (two sided if incompatible with the renderer)
        /// </summary>
        Anticlockwise
    }
}
