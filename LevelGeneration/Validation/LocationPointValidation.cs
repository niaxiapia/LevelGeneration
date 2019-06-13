using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IntelligentPlatform_110kV
{
    public class LocationPointValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {           
            if (((string)value).Contains(","))
            {
                string[] arr = ((string)value).Split(',');
                double x, y;
                bool bx = Double.TryParse(arr[0], out x);
                bool by = Double.TryParse(arr[1], out y);
                if (bx && by)
                {
                    return new ValidationResult(true, null);
                }
                else
                {
                    return new ValidationResult(false, "输入格式不正确，请重新输入！");
                }
            }
            else
            {
                return new ValidationResult(false, "输入格式不正确，请重新输入！");
            }            
        }
    }
}
