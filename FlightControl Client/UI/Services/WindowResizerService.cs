using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Services
{
    /// <summary>
    /// resize window of view depends on the view Name
    /// </summary>
    public class WindowResizerService
    {
       public static void Resize(string viewName)
        {
            switch (viewName)
            {
                case "FlightControl":
                    App.Current.MainWindow.Width = 1000;
                    App.Current.MainWindow.Height = 650;
                    return;
                case "FlightHistory":
                    App.Current.MainWindow.Width = 700;
                    App.Current.MainWindow.Height = 800;
                    return;
                default:
                    return;
            }
        }
    }
}
