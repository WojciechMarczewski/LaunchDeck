using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace LaunchDeck.Converters
{
    [ValueConversion(typeof(int), typeof(int))]
    public class TopRightCornerPositionConverter : IValueConverter
    {

        public object Convert(object ?value, Type ?targetType, object ?parameter, CultureInfo ?culture)
        {
            return SystemParameters.PrimaryScreenWidth - Application.Current.MainWindow.Width;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
