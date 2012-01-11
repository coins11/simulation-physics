using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulationPhys4_8
{
    class Program
    {
        const double G = 1.0;
        const double dx = 1.0;
        const double eps = 1.0E-10;

        static void Main(string[] args)
        {
            double[][] phi = new double[4][];
            foreach (var x in Enumerable.Range(0, 4))
                phi[x] = Enumerable.Repeat(0.0, 4).ToArray();

            double beforeDet = 0.0;
            double det = 0.0;
            for (int i = 1; ; i++)
            {
                for (int x = 1; x < phi.Length - 1; x++)
                {
                    for (int y = 1; y < phi[x].Length - 1; y++)
                    {
                        double p1 = phi[x + 1][y] + phi[x - 1][y] + phi[x][y + 1] + phi[x][y - 1];
                        double p2 = G * Rho(x, y) * dx * dx;
                        phi[x][y] = p1 / 4 - p2 / 4;
                    }
                }

                beforeDet = det;
                det = Determinant2(phi);
                double error = Math.Pow(Math.Abs(det - beforeDet), 2);
                Console.WriteLine("{0}: error={1}", i, error);
                if (error < eps)
                    break;
            }

            foreach (var row in phi.Skip(1).Take(2))
            {
                foreach (var v in row.Skip(1).Take(2))
                    Console.Write(v + "\t");
                Console.WriteLine();
            }
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
