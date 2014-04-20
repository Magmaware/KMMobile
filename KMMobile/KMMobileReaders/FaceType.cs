namespace KMMobile
{
    /// <summary>
    /// The face type
    /// </summary>
    public enum FaceType
    {
        /// <summary>
        /// The front surface of a panel (transform does not apply, but thickness
        /// does)
        /// </summary>
        Front,
        /// <summary>
        /// The back surface of a panel (transform does not apply, but thickness
        /// does)
        /// </summary>
        Back,
        /// <summary>
        /// The edge surface of a panel (transform does not apply, but thickness
        /// does)
        /// </summary>
        Edge,
        /// <summary>
        /// Another type of surface (which is potentially transformed,
        /// but for which thickness etc. does not apply)
        /// </summary>
        Other
    }
}
