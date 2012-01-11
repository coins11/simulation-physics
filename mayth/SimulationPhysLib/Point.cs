using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulationPhysLib
{
    /// <summary>
    /// Represents a pair of x-coordinate and y-coordinate that defines a point in a two-dimensional plane.
    /// </summary>
    public struct Point
    {
        static readonly Point empty;

        double x;
        /// <summary>
        /// Gets the x-coordinate of this Point.
        /// </summary>
        public double X
        {
            get { return x; }
        }

        double y;
        /// <summary>
        /// Gets the y-coordinate of this Point.
        /// </summary>
        public double Y
        {
            get { return y; }
        }

        /// <summary>
        /// Gets Point that has X and Y values set to 0.
        /// </summary>
        public Point Empty
        {
            get { return empty; }
        }

        static Point()
        {
            empty = new Point(0, 0);
        }

        /// <summary>
        /// Initializes the Point with the specified coordinates.
        /// </summary>
        /// <param name="x">x-coordinate</param>
        /// <param name="y">y-coordinate</param>
        public Point(double x, double y)
            : this()
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format("X={0},Y={1}", this.X, this.Y);
        }

        #region Operator Overloads
        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }
        #endregion
    }
}
