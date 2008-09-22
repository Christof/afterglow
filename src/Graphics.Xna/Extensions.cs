using TheNewEngine.Math.Primitives;
using XnaMatrix = Microsoft.Xna.Framework.Matrix;

namespace TheNewEngine.Graphics.Xna
{
    /// <summary>
    /// This class contains extension methods which help to map engine types to
    /// the corresponding Xna types.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Converts the vector into an <see cref="Microsoft.Xna.Framework.Vector3"/>.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>The xna vector.</returns>
        public static Microsoft.Xna.Framework.Vector3 ToXna(this Vector3 vector)
        {
            return new Microsoft.Xna.Framework.Vector3(vector.X, vector.Y, vector.Z);
        }

        /// <summary>
        /// Converts the matrix into an <see cref="Microsoft.Xna.Framework.Matrix"/>.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>The xna matrix.</returns>
        public static XnaMatrix ToXna(this Matrix matrix)
        {
            return new XnaMatrix(
                matrix.R1C1, matrix.R1C2, matrix.R1C3, matrix.R1C4,
                matrix.R2C1, matrix.R2C2, matrix.R2C3, matrix.R2C4,
                matrix.R3C1, matrix.R3C2, matrix.R3C3, matrix.R3C4,
                matrix.R4C1, matrix.R4C2, matrix.R4C3, matrix.R4C4);
        }

        /// <summary>
        /// Converts the <see cref="XnaMatrix"/> into our matrix.
        /// </summary>
        /// <param name="matrix">The xna matrix.</param>
        /// <returns>The matrix.</returns>
        public static Matrix ToMath(this XnaMatrix matrix)
        {
            return new Matrix(
                matrix.M11, matrix.M12, matrix.M13, matrix.M14,
                matrix.M21, matrix.M22, matrix.M23, matrix.M24,
                matrix.M31, matrix.M32, matrix.M33, matrix.M34,
                matrix.M41, matrix.M42, matrix.M43, matrix.M44);
        }
    }
}