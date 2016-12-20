using FlightControl.Contract.Services;
using FlightControl.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FlightControl.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(FlightControlService), new Uri("http://localhost:888")))
            {
                WSDualHttpBinding bindingInstance = new WSDualHttpBinding();
                //bindingInstance.OpenTimeout = new TimeSpan(0, 0, 5);
                //bindingInstance.SendTimeout = new TimeSpan(0, 0, 5);
                host.AddServiceEndpoint(typeof(IFlightControlService), bindingInstance, "");
                host.AddServiceEndpoint(typeof(IFlightHistoryService), bindingInstance, "");
                host.AddServiceEndpoint(typeof(ISimulatorService), bindingInstance, "");

                host.Open();
                Console.WriteLine("server is running...");
                Console.ReadLine();
            }
        }
    }
}
