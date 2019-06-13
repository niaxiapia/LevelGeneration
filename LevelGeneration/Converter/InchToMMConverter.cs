using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IntelligentPlatform_110kV
{
    class InchToMMConverter : IValueConverter
    {
        //源到目标，即英寸到mm
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double mm = 0;
            if ((value.ToString()).Length>0)
            {
                mm = (double)value;
                mm = mm * 304.8;
            }
            return mm;
        }

        //目标到源，即mm到英寸
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double inch=0;
            if ((value.ToString()).Length > 0)
            {
                double.TryParse(value.ToString(),out inch);
               inch = inch / 304.8;
            }
            return inch;
        }
    }
}
