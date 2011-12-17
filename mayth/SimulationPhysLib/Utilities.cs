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
        public static IEnumerable<Point> GetRandomPointEnumerator(int seed, int min, int max)
        {
            var rand = new Random(seed);

            while (true)
                yield return new Point(rand.NextDouble(min, max), rand.NextDouble(min, max));
        }
    }
}
