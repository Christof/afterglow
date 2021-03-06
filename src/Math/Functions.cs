namespace Afterglow.Math
{
    /// <summary>
    /// Contains common mathematical functions.
    /// </summary>
    public static class Functions
    {
        /// <summary>
        /// Returns the square root of a specified number.
        /// </summary>
        /// <param name="x">The number.</param>
        /// <returns>The square root of the given number.</returns>
        public static float Sqrt(float x)
        {
            return (float)System.Math.Sqrt(x);
        }

        /// <summary>
        /// Returns base raised to exponent.
        /// </summary>
        /// <param name="baseValue">The base value.</param>
        /// <param name="exponent">The exponent value.</param>
        /// <returns>Base raised to exponent.</returns>
        public static float Pow(float baseValue, float exponent)
        {
            return (float)System.Math.Pow(baseValue, exponent);
        }

        /// <summary>
        /// Returns the sine of the given angle.
        /// </summary>
        /// <param name="angle">An angle, measured in radians.</param>
        /// <returns>
        /// The sine of angle. If angle is equal to NaN, NegativeInfinity, 
        /// or PositiveInfinity, this method returns NaN.
        /// </returns>
        public static float Sin(float angle)
        {
            return (float)System.Math.Sin(angle);
        }

        /// <summary>
        /// Returns the cosine of the given angle.
        /// </summary>
        /// <param name="angle">An angle, measured in radians.</param>
        /// <returns>The cosine of angle.</returns>
        public static float Cos(float angle)
        {
            return (float)System.Math.Cos(angle);
        }

        /// <summary>
        /// Returns the tangent of the given angle.
        /// </summary>
        /// <param name="angle">An angle, measured in radians.</param>
        /// <returns>
        /// The tangent of angle. If angle is equal to NaN, NegativeInfinity, 
        /// or PositiveInfinity, this method returns NaN.
        /// </returns>
        public static float Tan(float angle)
        {
            return (float)System.Math.Tan(angle);
        }

        /// <summary>
        /// Co-Tangens function (cos / sin).
        /// </summary>
        /// <param name="angle">An angle, measured in radians.</param>
        /// <returns>Co-Tangens of the angle.</returns>
        public static float CoTan(float angle)
        {
            return (float)(System.Math.Cos(angle) / System.Math.Sin(angle));
        }

        /// <summary>
        /// Calculates the angle whose sine is the given number.
        /// </summary>
        /// <param name="number">A number representing a sine, where -1 ≤ number ≤ 1.</param>
        /// <returns>
        /// An angle, θ, measured in radians, such that -π/2 ≤ θ ≤π/2 
        /// -or- 
        /// NaN if number &lt; -1 or number &gt; 1.
        /// </returns>
        public static float Asin(float number)
        {
            return (float)System.Math.Asin(number);
        }

        /// <summary>
        /// Calculates the angle whose cosine is the given number.
        /// </summary>
        /// <param name="number">A number representing a cosine, where -1 ≤ number ≤ 1.</param>
        /// <returns>
        /// An angle, θ, measured in radians, such that 0 ≤ θ ≤π 
        /// -or- 
        /// NaN if number &lt; -1 or number &gt; 1.
        /// </returns>
        public static float Acos(float number)
        {
            return (float)System.Math.Acos(number);
        }

        /// <summary>
        /// Calculates the angle whose tangent is the specified number.
        /// </summary>
        /// <param name="number">A number representing a tangent.</param>
        /// <returns>
        /// An angle, θ, measured in radians, such that -π/2 ≤ θ ≤π/2.
        /// -or- 
        /// NaN if number equals NaN, 
        /// -π/2 if number equals NegativeInfinity, 
        /// or π/2 if number equals PositiveInfinity.
        /// </returns>
        public static float Atan(float number)
        {
            return (float)System.Math.Atan(number);
        }

        /// <summary>
        /// Returns the absolute value for the given one.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The absolute value for the given one.</returns>
        public static float Abs(float value)
        {
            return System.Math.Abs(value);
        }
    }
}