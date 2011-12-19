using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulationPhysLib
{
    /// <summary>
    /// A wrapper class for <see cref="EqualityComparer"/> class.
    /// </summary>
    /// <typeparam name="T">A type for comparing.</typeparam>
    public class EqualityComparerWrapper<T> : System.Collections.Generic.EqualityComparer<T>
    {
        private Func<T, T, bool> comparer;

        /// <summary>
        /// Initializes a new instance of <see cref="EqualityComparer"/> class with the specified function.
        /// </summary>
        /// <param name="comparer">A function to use for comparing.</param>
        public EqualityComparerWrapper(Func<T, T, bool> comparer)
            : base()
        {
            this.comparer = comparer;
        }

        /// <summary>
        /// Determines whether the two specified values are equal.
        /// </summary>
        /// <param name="x">A first value to compare.</param>
        /// <param name="y">A second value to compare.</param>
        /// <returns>true if the specified values are equal; otherwise, false.</returns>
        public override bool Equals(T x, T y)
        {
            return comparer(x, y);
        }

        /// <summary>
        /// Returns an unique hash code.
        /// </summary>
        /// <param name="obj">An object to calculate hash.</param>
        /// <returns>A hash code.</returns>
        public override int GetHashCode(T obj)
        {
            return comparer(obj, obj).GetHashCode();
        }
    }
}
