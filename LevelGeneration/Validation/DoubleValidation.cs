using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IntelligentPlatform_110kV
{
    class DoubleValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double input;
            if (double.TryParse((string)value, out input))
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "输入格式有误，请重新输入");
            }
        }
    }
}
