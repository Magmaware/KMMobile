using KMMobile.ZipLib.Zip;
using System;

namespace KMMobile.X3D
{
    /// <summary>
    /// This class knows how to read the thumbnail from KSN and KOB files
    /// </summary>
    public class X3DThumbnailReader
    {
        /// <summary>
        /// Function to create an image from binary data
        /// </summary>
        private Func<byte[], bool, bool, X3DMobileImage> createImageFromBinary;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="createImageFromBinary">Function to create an image from binary data</param>
        public X3DThumbnailReader(Func<byte[], bool, bool, X3DMobileImage> createImageFromBinary = null)
        {
            this.createImageFromBinary = createImageFromBinary ?? X3DMobileImage.ImageFromBinary;
        }

        /// <summary>
        /// Reads
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="large"></param>
        /// <returns></returns>
        public X3DMobileImage Read(string filename, bool large)
        {
            try
            {
                var zipFile = new ZipFile(filename);
                var reader = new X3DBaseReader(zipFile, createImageFromBinary);
                var thumbnail = large ? reader.Preview : reader.Thumbnail;
                zipFile.Close();
                return thumbnail;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// The extensions this reader deals with
        /// </summary>
        public string[] Extensions
        {
            get { return new [] { "ksn", "kob" }; }
        }
    }
}
