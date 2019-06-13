using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IntelligentPlatform_110kV
{
    public class TextToCoordinateConver : IValueConverter
    {
        /// <summary>
        /// 从数据源到目标，XYZ转为text
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {          
            string pointStr = null;            
            if (value != null)//初始化时，value确实为空，所以一直崩程序。。。。
            {
                if (value.ToString().Length > 0)
                {
                    XYZ point = (XYZ)value;                        
                    double? x = point.X*304.8;
                    double? y = point.Y*304.8;
                    pointStr = x.ToString() + "," + y.ToString();                        
                }
            }
            else
            {
                pointStr = null;
            }
            return pointStr;
        }

        /// <summary>
        /// 从目标到数据源，text转为XYZ
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            XYZ point;
            if (((string)value).Length > 0)
            {
                if (((string)value).Contains(","))
                {
                    string[] arr = ((string)value).Split(',');
                    double x, y;
                    bool bx = Double.TryParse(arr[0], out x);
                    bool by = Double.TryParse(arr[1], out y);
                    double z = 0;
                    if (bx && by)
                    {
                        point = new XYZ(x, y, z) / 304.8;
                        return point;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }                    
            }
            else
            {
                return null;
            }
        }
    }
}
