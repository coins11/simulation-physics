using System;
using System.Collections.Generic;
using System.Linq;

namespace SimulationPhys1_3
{
    struct Point
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public Point(double x, double y)
            : this()
        {
            X = x;
            Y = y;
        }
    }
    
    class Program
    {
        static readonly int points = 200;
        static readonly int seed = 563;

        static double RandomDouble(Random rand, int min, int max)
        {
            return min + (rand.NextDouble() * (max - min));
        }

        static IEnumerable<Point> GetRandomPointEnumerator(int seed, int min, int max)
        {
            var rand = new Random(seed);

            while (true)
                yield return new Point(RandomDouble(rand, -1, 1), RandomDouble(rand, -1, 1));
        }

        static void Main(string[] args)
        {
            using (var writer = new System.IO.StreamWriter(System.IO.File.Open("simphys1_3-data.csv", System.IO.FileMode.Create)))
                foreach (var p in GetRandomPointEnumerator(seed, -1, 1).Where(p => Math.Pow(p.X, 2) + Math.Pow(p.Y, 2) <= 1.0).Take(points))
                    writer.WriteLine("{0},{1}", p.X, p.Y);
        }
    }
}
