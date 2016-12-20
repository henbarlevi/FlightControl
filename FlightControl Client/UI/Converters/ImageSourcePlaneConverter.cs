using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UI.Converters
{
    /// <summary>
    /// change the image source of a plane by its current stationId
    /// </summary>
    public class ImageSourcePlaneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((int)value)
            {
                case 0:
                case 1:
                case 2:
                    return @"\images\planeArrival.png";
                case 3:
                case 4:
                    return @"\images\planeMoving.png";
                case 5:
                case 6:
                    return @"\images\planePark.png";
                case 8:
                    return @"\images\planeDeparture.png";
                default:
                    return @"\images\planeMoving.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
