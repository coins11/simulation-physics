using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimulationPhysLib;

namespace SimulationPhys3_6
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] matrix = new int[5][];
            foreach (var x in Enumerable.Range(0, 5))
                matrix[x] = Enumerable.Repeat(0, 5).ToArray();

            // calc by round
            foreach (var p in Utilities.GetRandomPointEnumerator(563, 0, 4).Take(200))
                matrix[(int)Math.Round(p.X)][(int)Math.Round(p.Y)]++;

            // calc by distance
            //foreach (var p in Utilities.GetRandomPointEnumerator(563, 0, 4).Take(200))
            //{
            //    double min = Math.Pow(p.X, 2) + Math.Pow(p.Y, 2);
            //    var minPoint = Tuple.Create(0, 0);
            //    foreach (var y in Enumerable.Range(0, 5))
            //    {
            //        foreach (var x in Enumerable.Range(0, 5))
            //        {
            //            double dist = Math.Pow(p.X - x, 2) + Math.Pow(p.Y - y, 2);
            //            if (dist < min)
            //            {
            //                min = dist;
            //                minPoint = Tuple.Create(x, y);
            //            }
            //        }
            //    }
            //    matrix[minPoint.Item1][minPoint.Item2]++;
            //}

            foreach (var row in matrix)
            {
                foreach (var v in row)
                    Console.Write(v + "\t");
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
