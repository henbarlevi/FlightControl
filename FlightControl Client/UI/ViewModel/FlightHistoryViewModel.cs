using FlightControl.Contract.Entities;
using FlightControl.Contract.Entities.FHServiceEntities;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.ServerProxies;

namespace UI.ViewModel
{
    public class FlightHistoryViewModel : ViewModelBase
    {
        //proxies:
        public FlightHistoryProxy FHServicePorxy { get; set; }
        //arrivals:
        private ObservableCollection<ArrivalDTO> arrivals; //contain list of all history arrivals to airport
        public ObservableCollection<ArrivalDTO> Arrivals
        {
            get { return arrivals; }
            set
            {
                arrivals = value;
                RaisePropertyChanged(() => Arrivals); //notify porperty changed (for binding)
            }
        }
        //departures:
        private ObservableCollection<DepartureDTO> departures; //contain list of all history arrivals to airport
        public ObservableCollection<DepartureDTO> Departures
        {
            get { return departures; }
            set
            {
                departures = value;
                RaisePropertyChanged(() => Departures); //notify porperty changed (for binding)
            }
        }
        //commands:
        public RelayCommand<object> RefreshHistoryCommand { get; set; }
        public FlightHistoryViewModel()
        {
            //init proxies:
            FHServicePorxy = FlightHistoryProxy.Instance;
            //get info about arrivals and departures from server:
            GetArrivalsFromServer();
            GetDeparturesFromServer();
            RefreshHistoryCommand = new RelayCommand<object>(RefreshHistory);
        }
      
        private void RefreshHistory(object obj)
        {
            GetArrivalsFromServer();
            GetDeparturesFromServer();
        }
        private void GetArrivalsFromServer()
        {
            GetArrivalsResponse response = FHServicePorxy.GetArrivals(new GetArrivalsRequest());
            if (response.IsSuccess)
            {
                Arrivals = new ObservableCollection<ArrivalDTO>(response.Arrivals);
            }
        }
        private void GetDeparturesFromServer()
        {
            GetDeparturesResponse response = FHServicePorxy.GetDepartures(new GetDeparturesRequest());
            if (response.IsSuccess)
            {
                Departures = new ObservableCollection<DepartureDTO>(response.Departures);
            }
        }
    }
}
