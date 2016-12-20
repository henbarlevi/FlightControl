using FlightControl.Contract.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.ServerProxies;
using UI.EventArguments;
using FlightControl.Contract.Entities.FCServiceEntities;
using FlightControl.Contract.Entities;
using System.Collections.ObjectModel;

namespace UI.ViewModel
{
    public class FlightControlViewModel : ViewModelBase
    {
        public FlightControlProxy FCServiceProxy { get; set; } //service from server, get info about airport state
        private ObservableCollection<PlaneDTO> airport; //contain list of all current planes (PlaneDTO) in the airport

        public ObservableCollection<PlaneDTO> Airport
        {
            get { return airport; }
            set
            {
                airport = value;
                RaisePropertyChanged(() => Airport); //notify porperty changed (for binding)
            }
        } //contain list of all current planes (PlaneDTO) in the airport
        
        

        private PlaneDTO stationZeroPlane;
        public PlaneDTO StationZeroPlane
        {
            get { return stationZeroPlane; }
            set
            {
                stationZeroPlane = value;
                RaisePropertyChanged(() => StationZeroPlane);
            }
        }

        private PlaneDTO stationOnePlane;
        public PlaneDTO StationOnePlane
        {
            get { return stationOnePlane; }
            set
            {
                stationOnePlane = value;
                RaisePropertyChanged(() => StationOnePlane);
            }
        }

        private PlaneDTO stationTwoPlane;
        public PlaneDTO StationTwoPlane
        {
            get { return stationTwoPlane; }
            set
            {
                stationTwoPlane = value;
                RaisePropertyChanged(() => StationTwoPlane);
            }
        }
        //3
        private PlaneDTO stationThreePlane;
        public PlaneDTO StationThreePlane
        {
            get { return stationThreePlane; }
            set
            {
                stationThreePlane = value;
                RaisePropertyChanged(() => StationThreePlane);
            }
        }
        //4
        private PlaneDTO stationFourPlane;
        public PlaneDTO StationFourPlane
        {
            get { return stationFourPlane; }
            set
            {
                stationFourPlane = value;
                RaisePropertyChanged(() => StationFourPlane);
            }
        }
        //5
        private PlaneDTO stationFivePlane;
        public PlaneDTO StationFivePlane
        {
            get { return stationFivePlane; }
            set
            {
                stationFivePlane = value;
                RaisePropertyChanged(() => StationFivePlane);
            }
        }
        //6
        private PlaneDTO stationSixPlane;
        public PlaneDTO StationSixPlane
        {
            get { return stationSixPlane; }
            set { stationSixPlane = value;
                RaisePropertyChanged(() => StationSixPlane);
            }
        }
        //7
        private PlaneDTO stationSevenPlane;
        public PlaneDTO StationSevenPlane
        {
            get { return stationSevenPlane; }
            set { stationSevenPlane = value;
                RaisePropertyChanged(() => StationSevenPlane);
            }
        }
        //8
        private PlaneDTO stationEightPlane;
        public PlaneDTO StationEightPlane
        {
            get { return stationEightPlane; }
            set
            {
                stationEightPlane = value;
                RaisePropertyChanged(() => StationEightPlane);
            }
        }

        //ctor:
        public FlightControlViewModel()
        {
            //init proxies:
            FCServiceProxy = FlightControlProxy.Instance;
            FCServiceProxy.AirportChanged += FCServiceProxy_OnAirportChanged;
            //send connection request, get in the response the current airport state:
            ConnectResponse response = FCServiceProxy.Connect(new ConnectRequest { });
            Airport = new ObservableCollection<PlaneDTO>(response.Airport.Planes);
            //PlaneDTO[] allPlanes = new PlaneDTO[] { StationZeroPlane,
            //    StationOnePlane,
            //    StationTwoPlane,
            //    stationThreePlane,
            //    StationFourPlane,
            //    StationFivePlane,
            //    StationSixPlane,
            //    StationSevenPlane,
            //    StationEightPlane };
        }

        private void FCServiceProxy_OnAirportChanged(object sender, AirportEventArgs e)
        {
            StationZeroPlane = null;
            StationOnePlane = null;
            StationTwoPlane = null;
            StationThreePlane = null;
            StationFourPlane = null;
            StationFivePlane = null;
            StationSixPlane = null;
            StationSevenPlane = null;
            StationEightPlane = null;
            Airport = new ObservableCollection<PlaneDTO>(e.ChangedAirport.Planes);

            foreach (var plane in Airport)
            {
                switch (plane.StationId)
                {
                    case 0:
                        StationZeroPlane = plane;
                        continue;
                    case 1:
                        StationOnePlane = plane;
                        continue;
                    case 2:
                        StationTwoPlane = plane;
                        continue;
                    case 3:
                        StationThreePlane = plane;
                        continue;
                    case 4:
                        StationFourPlane = plane;
                        continue;
                    case 5:
                        StationFivePlane = plane;
                        continue;
                    case 6:
                        StationSixPlane = plane;
                        continue;
                    case 7:
                        StationSevenPlane = plane;
                        continue;
                    case 8:
                        StationEightPlane = plane;
                        continue;
                    default:
                        continue;
                }
            }
            // throw new NotImplementedException();
        }
    }
}
