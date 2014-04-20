namespace KMMobile.X3D
{
    /// <summary>
    /// Holds compressed or uncompressed image data
    /// </summary>
    public class X3DMobileImage
    {
        /// <summary>
        /// The binary image data
        /// </summary>
        public byte[] ImageData { get; private set; }
        /// <summary>
        /// Whether this is an .xdz (i.e. compressed) image
        /// </summary>
        public bool IsXdz { get; private set; }
        /// <summary>
        /// Whether the image is flipped
        /// </summary>
        public bool Flip { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">The image data</param>
        /// <param name="isXdz">Whether this is an .xdz (i.e. compressed) image</param>
        /// <param name="flip">Whether the image is flipped</param>
        public X3DMobileImage(byte[] data, bool isXdz, bool flip)
        {
            this.ImageData = data;
            this.IsXdz = isXdz;
            this.Flip = flip;
        }

        /// <summary>
        /// Entry point for the creation of all images
        /// </summary>
        /// <param name="data">The image data</param>
        /// <param name="isXdz">Whether this is an .xdz (i.e. compressed) image</param>
        /// <param name="flip">Whether the image is flipped</param>
        /// <returns>The image</returns>
        public static X3DMobileImage ImageFromBinary(byte[] data, bool isXdz, bool flip)
        {
            return new X3DMobileImage(data, isXdz, flip);
        }
    }
}
