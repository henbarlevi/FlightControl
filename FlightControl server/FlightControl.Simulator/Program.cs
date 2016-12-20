using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightControl.Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Simulator simulator = new Simulator();
            simulator.Run();
            Console.WriteLine("Simulator is running");
            Console.ReadLine();
        }
    }
}
