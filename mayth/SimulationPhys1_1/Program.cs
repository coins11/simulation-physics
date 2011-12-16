using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulationPhys1_2
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

        static readonly int tryCount = 10;
        static readonly int points = 100;
        static readonly IList<int> seeds = new System.Collections.ObjectModel.ReadOnlyCollection<int>(
            new List<int>() { 619, 353, 431, 811, 103, 823, 631, 769, 827, 271 });

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

        static bool IsGreater(double x, double y)
        {
            return Math.Pow(x, 2.0) + Math.Pow(y, 2.0) <= 1.0;
        }

        static double GetArea(IEnumerable<Point> pairs)
        {
            return (double)((from p in pairs
                             where IsGreater(p.X, p.Y)
                             select p).Count() * 4) / points;
        }

        static void Main(string[] args)
        {
            var result = from i in Enumerable.Range(0, tryCount)
                         select GetArea(GetRandomPointEnumerator(seeds[i], -1, 1).Take(points));
            double sum = 0.0;
            foreach (var d in result)
                sum += d;
            System.Diagnostics.Debug.WriteLine("sum=" + sum);
            double ave = sum / result.Count();
            System.Diagnostics.Debug.WriteLine("ave=" + ave);

            System.Diagnostics.Debug.WriteLine("points={0}: ave={1}, (pi-ave)/pi={2}", points, result.Average(), (Math.PI - result.Average()) / Math.PI);

            Console.ReadLine();
        }
    }
}
