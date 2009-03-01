using System;
using Afterglow.Graphics.GraphicStreams;
using Afterglow.Math;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using SlimDXVector3 = SlimDX.Vector3;
using SlimDXMatrix = SlimDX.Matrix;

namespace Afterglow.Graphics
{
    /// <summary>
    /// This class contains extension methods which help to map engine types to
    /// the corresponding SlimDX types.
    /// </summary>
    public static class SlimDXExtensions
    {
        /// <summary>
        /// Returns the semantic corresponding to the <see cref="GraphicStreamUsage"/>
        /// for the <see cref="InputElement"/>.
        /// </summary>
        /// <param name="graphicStreamUsage">The graphic stream usage.</param>
        /// <returns>Corresponding semantic.</returns>
        public static string ToSemantic(this GraphicStreamUsage graphicStreamUsage)
        {
            switch (graphicStreamUsage)
            {
                case GraphicStreamUsage.Position:
                    return "POSITION";

                case GraphicStreamUsage.Color:
                    return "COLOR";

                case GraphicStreamUsage.Normal:
                    return "NORMAL";

                case GraphicStreamUsage.Tangent:
                    return "TANGENT";

                case GraphicStreamUsage.Binormal:
                    return "BINORMAL";

                case GraphicStreamUsage.TextureCoordinate:
                    return "TEXCOORD";

                default:
                    throw new InvalidOperationException(string.Format(
                        "Unable to convert the given graphic stream usage ({0})", graphicStreamUsage));
            }
        }

        /// <summary>
        /// Returns the <see cref="Format"/> for the <see cref="InputElement"/>
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>The corresponding <see cref="Format"/>.</returns>
        public static Format ToFormat(this GraphicStreamFormat format)
        {
            switch (format)
            {
                case GraphicStreamFormat.Vector2:
                    return Format.R32G32_Float;

                case GraphicStreamFormat.Vector3:
                    return Format.R32G32B32_Float;

                case GraphicStreamFormat.Vector4:
                    return Format.R32G32B32A32_Float;

                case GraphicStreamFormat.Color4:
                    return Format.R32G32B32A32_Float;

                case GraphicStreamFormat.Float:
                    return Format.R32_Float;

                case GraphicStreamFormat.UInt:
                    return Format.R32_UInt;

                case GraphicStreamFormat.Int:
                    return Format.R32_SInt;

                default:
                    throw new InvalidOperationException(string.Format(
                        "Unable to convert the given format ({0})", format));
            }
        }

        /// <summary>
        /// Converts the vector into an <see cref="SlimDXVector3"/>.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>The slimDX vector.</returns>
        public static SlimDXVector3 ToSlimDX(this Vector3 vector)
        {
            return new SlimDXVector3(vector.X, vector.Y, vector.Z);
        }

        /// <summary>
        /// Converts the matrix into an <see cref="SlimDXMatrix"/>.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>The slimDX matrix.</returns>
        public static SlimDXMatrix ToSlimDX(this Matrix matrix)
        {
            return new SlimDXMatrix
            {
                M11 = matrix.R1C1,
                M12 = matrix.R1C2,
                M13 = matrix.R1C3,
                M14 = matrix.R1C4,
                M21 = matrix.R2C1,
                M22 = matrix.R2C2,
                M23 = matrix.R2C3,
                M24 = matrix.R2C4,
                M31 = matrix.R3C1,
                M32 = matrix.R3C2,
                M33 = matrix.R3C3,
                M34 = matrix.R3C4,
                M41 = matrix.R4C1,
                M42 = matrix.R4C2,
                M43 = matrix.R4C3,
                M44 = matrix.R4C4
            };
        }

        /// <summary>
        /// Converts the <see cref="SlimDXMatrix"/> into our matrix.
        /// </summary>
        /// <param name="matrix">The slimDX matrix.</param>
        /// <returns>The matrix.</returns>
        public static Matrix ToMath(this SlimDXMatrix matrix)
        {
            return new Matrix(
                matrix.M11, matrix.M12, matrix.M13, matrix.M14,
                matrix.M21, matrix.M22, matrix.M23, matrix.M24,
                matrix.M31, matrix.M32, matrix.M33, matrix.M34,
                matrix.M41, matrix.M42, matrix.M43, matrix.M44);
        }

        /// <summary>
        /// Disposes the object if it isn't already disposed or null.
        /// </summary>
        /// <param name="comObject">The COM object.</param>
        public static void DisposeIfNotDisposed(this SlimDX.ComObject comObject)
        {
            if (comObject != null && !comObject.Disposed)
            {
                comObject.Dispose();
            }
        }
    }
}