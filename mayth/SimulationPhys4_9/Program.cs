﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulationPhys4_9
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

            // boundary condition
            phi[1][3] = 22.5;
            phi[2][3] = 36;
            phi[3][1] = -4.5;
            phi[3][2] = 9;

            for (int i = 1; ; i++)
            {
                double maxError = 0.0;

                for (int x = 1; x < phi.Length - 1; x++)
                {
                    for (int y = 1; y < phi[x].Length - 1; y++)
                    {
                        double previous = phi[x][y];

                        double p1 = phi[x + 1][y] + phi[x - 1][y] + phi[x][y + 1] + phi[x][y - 1];
                        double p2 = G * Rho(x, y) * dx * dx;
                        phi[x][y] = p1 / 4 - p2 / 4;

                        double error = Math.Abs(phi[x][y] - previous);
                        if (maxError < error)
                            maxError = error;
                    }
                }

                Console.WriteLine("{0}: error={1}", i, maxError);
                if (maxError < eps)
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
