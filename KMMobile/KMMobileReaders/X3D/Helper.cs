using System;
using WaveEngine.Common.Math;

namespace KMMobile.X3D
{
    public static class Helper
    {
        public static Matrix ToTransform<T>(this X3DMatrix<T> matrix)
        {
            return new Matrix()
            {
                M11 = Convert.ToSingle(matrix.AX),
                M12 = Convert.ToSingle(matrix.AY),
                M13 = Convert.ToSingle(matrix.AZ),
                M14 = Convert.ToSingle(matrix.T.X),

                M21 = Convert.ToSingle(matrix.BX),
                M22 = Convert.ToSingle(matrix.BY),
                M23 = Convert.ToSingle(matrix.BZ),
                M24 = Convert.ToSingle(matrix.T.Y),

                M31 = Convert.ToSingle(matrix.CX),
                M32 = Convert.ToSingle(matrix.CY),
                M33 = Convert.ToSingle(matrix.CZ),
                M34 = Convert.ToSingle(matrix.T.Z),

                M41 = 0.0f,
                M42 = 0.0f,
                M43 = 0.0f,
                M44 = 1.0f
            };
        }
        /// <summary>
        /// Creates a scale transform matrix from a Vector3 scale value
        /// </summary>
        /// <param name="scale">3 dimenisonal scale</param>
        /// <returns>The scale transform matrix</returns>
        public static Matrix Scale(Vector3 scale)
        {
            return new Matrix
            {
                M11 = scale.X,
                M12 = 0,
                M13 = 0,
                M14 = 0,
                M21 = 0,
                M22 = scale.Y,
                M23 = 0,
                M24 = 0,
                M31 = 0,
                M32 = 0,
                M33 = scale.Z,
                M34 = 0,
                M41 = 0,
                M42 = 0,
                M43 = 0,
                M44 = 1
            };
        }

        /// <summary>
        /// Creates a rotation transform about the X axis
        /// </summary>
        /// <param name="angle">rotation angle, degrees</param>
        /// <returns>The rotation transform matrix</returns>
        public static Matrix RotateX(float angle)
        {
            angle *= 0.017453292f;

            return new Matrix
            {
                M11 = 1,
                M12 = 0,
                M13 = 0,
                M14 = 0,
                M21 = 0,
                M22 = (float)Math.Cos(angle),
                M23 = -(float)Math.Sin(angle),
                M24 = 0,
                M31 = 0,
                M32 = (float)Math.Sin(angle),
                M33 = (float)Math.Cos(angle),
                M34 = 0,
                M41 = 0,
                M42 = 0,
                M43 = 0,
                M44 = 1
            };
        }

        /// <summary>
        /// Creates a rotation transform about the Y axis
        /// </summary>
        /// <param name="angle">rotation angle, degrees</param>
        /// <returns>The rotation transform matrix</returns>
        public static Matrix RotateY(float angle)
        {
            angle *= 0.017453292f;

            return new Matrix
            {
                M11 = (float)Math.Cos(angle),
                M12 = 0,
                M13 = (float)Math.Sin(angle),
                M14 = 0,
                M21 = 0,
                M22 = 1,
                M23 = 0,
                M24 = 0,
                M31 = -(float)Math.Sin(angle),
                M32 = 0,
                M33 = (float)Math.Cos(angle),
                M34 = 0,
                M41 = 0,
                M42 = 0,
                M43 = 0,
                M44 = 1
            };
        }

        /// <summary>
        /// Creates a rotation transform about the Z axis
        /// </summary>
        /// <param name="angle">rotation angle, degrees</param>
        /// <returns>The rotation transform matrix</returns>
        public static Matrix RotateZ(float angle)
        {
            angle *= 0.017453292f;

            return new Matrix
            {
                M11 = (float)Math.Cos(angle),
                M12 = -(float)Math.Sin(angle),
                M13 = 0,
                M14 = 0,
                M21 = (float)Math.Sin(angle),
                M22 = (float)Math.Cos(angle),
                M23 = 0,
                M24 = 0,
                M31 = 0,
                M32 = 0,
                M33 = 1,
                M34 = 0,
                M41 = 0,
                M42 = 0,
                M43 = 0,
                M44 = 1
            };
        }

        /// <summary>
        /// Creates a translation transform matrix
        /// </summary>
        /// <param name="scale">translation vector</param>
        /// <returns>The translation transform matrix</returns>
        public static Matrix Translate(Vector3 translation)
        {
            return new Matrix
            {
                M11 = 1,
                M12 = 0,
                M13 = 0,
                M14 = translation.X,
                M21 = 0,
                M22 = 1,
                M23 = 0,
                M24 = translation.Y,
                M31 = 0,
                M32 = 0,
                M33 = 1,
                M34 = translation.Z,
                M41 = 0,
                M42 = 0,
                M43 = 0,
                M44 = 1
            };
        }

        public static string SpacedString(this Matrix transform)
        {
            return
                transform.M11.ToString() + " " +
                transform.M21.ToString() + " " +
                transform.M31.ToString() + " " +
                transform.M41.ToString() + " " +
                transform.M12.ToString() + " " +
                transform.M22.ToString() + " " +
                transform.M32.ToString() + " " +
                transform.M42.ToString() + " " +
                transform.M13.ToString() + " " +
                transform.M23.ToString() + " " +
                transform.M33.ToString() + " " +
                transform.M43.ToString() + " " +
                transform.M14.ToString() + " " +
                transform.M24.ToString() + " " +
                transform.M34.ToString() + " " +
                transform.M44.ToString();
        }
    }
}
