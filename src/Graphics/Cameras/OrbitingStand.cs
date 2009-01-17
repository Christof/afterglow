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
        private const float MAX_DECLINATION = Constants.HALF_PI - float.Epsilon;

        private const float MIN_DECLINATION = -MAX_DECLINATION;

        private float mMaxDeclination = MAX_DECLINATION;

        private float mMinDeclination = MIN_DECLINATION;

        private float mDeclination;

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
                return new Vector3(
                    Functions.Cos(Azimuth) * Functions.Cos(Declination),
                    Functions.Sin(Declination),
                    Functions.Sin(Azimuth) * Functions.Cos(Declination)) * Radius;
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
        public float Declination
        {
            get
            {
                return mDeclination;
            }
            set
            {
                if (mDeclination > mMaxDeclination)
                    mDeclination = mMaxDeclination;
                else if (mDeclination < mMinDeclination)
                    mDeclination = mMinDeclination;
                else
                    mDeclination = value;
            }
        }

        /// <summary>
        /// Gets or sets the max declination wich is limited to pi/2.
        /// </summary>
        /// <value>The max declination.</value>
        public float MaxDeclination
        {
            get
            {
                return mMaxDeclination;
            }
            set
            {
                if (value > MAX_DECLINATION)
                    mMaxDeclination = MAX_DECLINATION;
                else
                    mMaxDeclination = value;
            }
        }

        /// <summary>
        /// Gets or sets the max declination wich is limited to -spi/2.
        /// </summary>
        /// <value>The min declination.</value>
        public float MinDeclination
        {
            get
            {
                return mMinDeclination;
            }
            set
            {
                if (value > MIN_DECLINATION)
                    mMinDeclination = MIN_DECLINATION;
                else
                    mMinDeclination = value;
            }
        }
    }
}