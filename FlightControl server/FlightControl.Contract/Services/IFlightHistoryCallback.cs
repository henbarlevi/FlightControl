using FlightControl.Contract.Entities.FHServiceEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FlightControl.Contract.Services
{
    /// <summary>
    /// the client will get callback about any departure/arrvial that acquired
    /// </summary>
    [ServiceContract]
    public interface IFlightHistoryCallback
    {
        [OperationContract]
        void OnArrivalAcquired(OnArrivalAcquired request);
    }
}
