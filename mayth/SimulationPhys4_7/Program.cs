using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimulationPhysLib;

namespace SimulationPhys4_7
{
    class Program
    {
        const int Seed = 587;
        const double HubbleConstant = 71;
        const double timeStep = 0.1;
        static readonly Point MeshOrigin = new Point(1.0, 1.0);

        static void Main(string[] args)
        {
            var points = Utilities.GetRandomPointEnumerator(Seed, -1, 1)
                .Where(p => p.X * p.X + p.Y * p.Y <= 1.0)
                .Take(200)
                .Select(p => new
                {
                    Point = p + MeshOrigin,
                    Velocity = new Point(HubbleConstant * p.X, HubbleConstant * p.Y)
                });
            var after = points.Map(p => new
                {
                    Point = new Point(p.Point.X + p.Velocity.X * timeStep, p.Point.Y + p.Velocity.Y * timeStep),
                    Velocity = p.Velocity
                });

            using (var writer = new System.IO.StreamWriter("result.csv"))
                foreach (var p in points.Zip(after, (p, s) => Tuple.Create(p, s)))
                    writer.WriteLine("{0},{1},,{2},{3}", p.Item1.Point.X, p.Item1.Point.Y, p.Item2.Point.X, p.Item2.Point.Y);
        }
    }
}
