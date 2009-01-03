using System;
using TheNewEngine.Math;

namespace TheNewEngine.Graphics.Cameras
{
    /// <summary>
    /// Stand which can orbit on an imaginery sphere.
    /// The position is defined by the azimuth and declination angles and the radius.
    /// The target is the at the center of the coordinate system.
    /// </summary>
    public class OrbitingStand : IStand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrbitingStand"/> class.
        /// </summary>
        public OrbitingStand()
        {
            Radius = 10.0f;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrbitingStand"/> class.
        /// </summary>
        /// <param name="radius">The radius.</param>
        /// <param name="azimuth">The azimuth.</param>
        /// <param name="declination">The declination.</param>
        public OrbitingStand(float radius, float azimuth, float declination)
        {
            Radius = radius;
            Azimuth = azimuth;
            Declination = declination;
        }

        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        /// <value>The radius.</value>
        public float Radius { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        public Vector3 Position
        {
            get
            {
//                return new Vector3(
//                    Functions.Sin(Declination) * Functions.Cos(Azimuth),
//                    Functions.Sin(Declination) * Functions.Sin(Azimuth),
//                    Functions.Cos(Declination)) * Radius;
                return new Vector3(
                    Functions.Cos(Azimuth),
                    0,
                    Functions.Sin(Azimuth)) * Radius;
            }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>The direction.</value>
        public Vector3 Direction
        {
            get { return -Position; }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Gets or sets a vector point upwards.
        /// </summary>
        /// <value>The up vector.</value>
        public Vector3 Up
        {
            get { return Vector3.YAxis; }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the view matrix.
        /// </summary>
        /// <value>The view matrix.</value>
        public Matrix ViewMatrix
        {
            get
            {
                var position = Position;
                return Stand.CalculateViewMatrix(position, -position, Vector3.YAxis);
            }
        }

        /// <summary>
        /// Gets or sets the azimuth.
        /// </summary>
        /// <value>The azimuth.</value>
        public float Azimuth { get; set; }

        /// <summary>
        /// Gets or sets the declination.
        /// </summary>
        /// <value>The declination.</value>
        public float Declination { get; set; }
    }
}