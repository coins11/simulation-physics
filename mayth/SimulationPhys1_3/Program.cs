using System;
using System.Collections.Generic;
using System.Linq;
using SimulationPhysLib;

namespace SimulationPhys1_3
{
    class Program
    {
        static readonly int points = 200;
        static readonly int seed = 563;

        static void Main(string[] args)
        {
            using (var writer = new System.IO.StreamWriter(System.IO.File.Open("simphys1_3-data.csv", System.IO.FileMode.Create)))
                foreach (var p in Utilities.GetRandomPointEnumerator(seed, -1, 1).Where(p => Math.Pow(p.X, 2) + Math.Pow(p.Y, 2) <= 1.0).Take(points))
                    writer.WriteLine("{0},{1}", p.X, p.Y);
        }
    }
}
