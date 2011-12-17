using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulationPhysLib
{
    public static class RandomExtension
    {
        /// <summary>
        /// Returns a random number wihtin a specified range.
        /// </summary>
        /// <param name="rand">An instance of <see cref="Random"/> class to use.</param>
        /// <param name="min">The inclusive lower bound of the random number returned.</param>
        /// <param name="max">The inclusive upper bound of the random number returned. <paramref name="max"/> must be greater than or equal to <paramref name="min"/>.</param>
        /// <returns>A double value greater than or equal to <paramref name="min"/> and less than or equal to <paramref name="max"/>. If <paramref name="min"/> equals <paramref name="max"/>, <paramref name="min"/> is returned.</returns>
        public static double NextDouble(this Random rand, double min, double max)
        {
            System.Diagnostics.Contracts.Contract.Requires<ArgumentException>(min <= max);

            return min + (rand.NextDouble() * (max - min));
        }
    }
}
