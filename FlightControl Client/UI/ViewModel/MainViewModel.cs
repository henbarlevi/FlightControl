using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Controls;
using UI.ServerProxies;
using UI.Services;
using System;
using System.ComponentModel;
using FlightControl.Contract.Entities.FCServiceEntities;

namespace UI.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public FlightControlProxy FCServiceProxy { get; set; }
        public MainViewModel()
        {
            //notity about first loaded view content:
            RaisePropertyChanged(() => WindowContent);
            //init Commands:
            SwitchCommand = new RelayCommand<object>(switchContent);
            //init proxies:
            FCServiceProxy = FlightControlProxy.Instance;
            //subscribe to Closing Window event:
            Application.Current.MainWindow.Closing += MainWindow_OnWindowClosing;

        }
        //on closing window - disconnect from server
        private void MainWindow_OnWindowClosing(object sender, CancelEventArgs e)
        {
            FCServiceProxy.Disconnect(new DisconnectRequest { });  
        }

        private void switchContent(object target)//targrt = view name
        {
            if (target.ToString() == "FlightControl")
                windowContent = new Views.FlightControl();
            else
                windowContent = new Views.FlightHistory();

            //change window size(if needed):
            WindowResizerService.Resize(target as string);
             //notify windowContent change:
            RaisePropertyChanged("WindowContent");
        }

        public RelayCommand<object> SwitchCommand { get; set; }

        private UserControl windowContent;
        public UserControl WindowContent { get { return windowContent; } }
    }
}