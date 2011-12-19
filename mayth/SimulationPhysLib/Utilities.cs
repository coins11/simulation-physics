using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulationPhysLib
{
    /// <summary>
    /// Provides some utility methods. This class is static class, so you cannot instantiate it.
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Enumerates <see cref="Point"/> structures infinitely.
        /// </summary>
        /// <param name="seed">A seed number to initialize <see cref="Random"/> class.</param>
        /// <param name="min">The inclusive lower bound of the random number returned.</param>
        /// <param name="max">The inclusive upper bound of the random number returned. <paramref name="max"/> must be greater than or equal to <paramref name="min"/>.</param>
        /// <returns>
        /// An enumerator of <see cref="Point"/> structure.
        /// These <see cref="Point"/> structures have double values greater than or equal to <paramref name="min"/> and less than or equal to <paramref name="max"/>. If <paramref name="min"/> equals <paramref name="max"/>, <paramref name="min"/> is returned.
        /// </returns>
        public static IEnumerable<Point> GetRandomPointEnumerator(int seed, int min, int max)
        {
            var rand = new Random(seed);

            while (true)
                yield return new Point(rand.NextDouble(min, max), rand.NextDouble(min, max));
        }
    }
}
