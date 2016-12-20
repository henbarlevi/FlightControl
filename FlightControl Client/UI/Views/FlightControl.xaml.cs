using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.ViewModel;

namespace UI.Views
{
    /// <summary>
    /// Interaction logic for FlightControl.xaml
    /// </summary>
    public partial class FlightControl : UserControl
    {
        public FlightControl()
        {
            InitializeComponent();
            ViewModelLocator viewModelLocator = new ViewModelLocator();
            DataContext = viewModelLocator.FlightControl;
           
        }
    }
}
