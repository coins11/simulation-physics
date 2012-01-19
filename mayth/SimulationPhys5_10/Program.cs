using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimulationPhysLib;

namespace SimulationPhys5_10
{
    class Program
    {
        const int Seed = 587;
        static readonly Point MeshOrigin = new Point(500, 500);
        const double dx = 1.0;
        const double eps = 1.0E-10;
        const int nm = 100;
        const double G = 1.0;
        const double M = 1.0;
        const double H = 3.5;
        const int ni = 50;
        const double dt = 0.1;
        const double LowerBound = -3.0;
        const double UpperBound = 3.0;
        const int nk = 40;
        const int margin = 5000;

        struct PointVelocityPair
        {
            public Point Point { get; private set; }
            public Point Velocity { get; private set; }

            public PointVelocityPair(Point point, Point velocity)
                : this()
            {
                this.Point = point;
                this.Velocity = velocity;
            }
        }

        static void Main(string[] args)
        {
            // create 500 points (step.1, 2, 3)
            var points = Utilities.GetRandomPointEnumerator(Seed, LowerBound, UpperBound)
                .Where(p => p.X * p.X + p.Y * p.Y <= 5.0)
                .Take(500)
                .Select(p => new PointVelocityPair(p + MeshOrigin, new Point(H * p.X, H * p.Y)));

            // potential: phi initialize (step.4)
            double[][] phi = new double[nm + 1][];
            foreach (var x in Enumerable.Range(0, nm + 1))
                phi[x] = Enumerable.Repeat(0.0, nm + 1).ToArray();

            IEnumerable<PointVelocityPair> after = points;

            // repeat time-step (step.5)
            for (int n = 0; n < nk; n++)
            {
                // calculate rho (step.6)
                int[][] grid = new int[(int)Math.Ceiling(UpperBound) + margin][];
                foreach (var x in Enumerable.Range(0, (int)Math.Ceiling(UpperBound) + margin))
                    grid[x] = Enumerable.Repeat(0, (int)Math.Ceiling(UpperBound) + margin).ToArray();
                foreach (var p in after)
                    grid[(int)Math.Round(p.Point.X)][(int)Math.Round(p.Point.Y)]++;

                // calculate phi (step.7)
                for (int i = 1; i <= ni; i++)
                {
                    // step.6
                    for (int x = 1; x < nm; x++)
                    {
                        for (int y = 1; y < nm; y++)
                        {
                            double p1 = phi[x + 1][y] + phi[x - 1][y] + phi[x][y + 1] + phi[x][y - 1];
                            double p2 = G * Rho(x, y) * dx * dx;
                            phi[x][y] = p1 / 4 - p2 / 4;
                        }
                    }
                }

                // calculate F (step.8)
                Point[][] F = new Point[(int)Math.Ceiling(UpperBound) + margin][];
                foreach (var x in Enumerable.Range(0, (int)Math.Ceiling(UpperBound) + margin))
                    F[x] = Enumerable.Repeat(new Point(), (int)Math.Ceiling(UpperBound) + margin).ToArray();
                for (int x = 1; x < nm; x++)
                    for (int y = 1; y < nm; y++)
                        F[x][y] = new Point(-(phi[x + 1][y] - phi[x][y]) / dx, -(phi[x][y + 1] - phi[x][y]) / dx);

                // move points (step.9)
                after = after.Select(p =>
                    {
                        Point nearestGrid = new Point((int)Math.Round(p.Point.X), (int)Math.Round(p.Point.Y));
                        Point Fp = new Point(M * F[(int)nearestGrid.X][(int)nearestGrid.Y].X, M * F[(int)nearestGrid.X][(int)nearestGrid.Y].Y);
                        var velocity = new { X = p.Velocity.X + (Fp.X / M) * dt, Y = p.Velocity.Y + (Fp.Y / M) * dt };
                        return new PointVelocityPair(
                            p.Point + new Point(velocity.X * dt, velocity.Y * dt),
                            new Point(velocity.X, velocity.Y));
                    }).ToArray();
            }

            // output
            using (var writer = new System.IO.StreamWriter("result-nk" + nk + ".csv"))
                foreach (var p in after)
                    writer.WriteLine("{0},{1}", p.Point.X, p.Point.Y);

            Console.WriteLine("Finished.");
            Console.ReadLine();
        }

        static double Rho(double x, double y)
        {
            return 6 * x - 3 * y;
        }

        static double Determinant2(double[][] matrix)
        {
            return matrix[1][1] * matrix[2][2] - matrix[1][2] * matrix[2][1];
        }
    }
}
