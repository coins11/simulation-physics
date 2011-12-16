using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulationPhys2_5
{
    class Program
    {
        static void Main(string[] args)
        {
            var densities = new[]{0.3, 0.5, 0.7};

            foreach(var density in densities)
            {
                var ca = new LinearCellularAutomaton(184, 20, density);

                Console.WriteLine("Rule No.{0}: {1}, length={2}, density={3}", ca.RuleNumber, ca.RuleString, ca.Length, ca.Density);

                foreach(int t in Enumerable.Range(0, 21))
                {
                    Console.Write("{0}\t", ca.CurrentTime);
                    foreach (var s in ca.CurrentState)
                        Console.Write(s ? "1" : "0");

                    Console.WriteLine();
                    ca.Next();
                }
            }

            Console.ReadLine();
        }
    }
}
