using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentPlatform_110kV
{

    public class MockLevel : NotificationObject
    {
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                this.RaisePropertyChanged("Name");
            }
        }

        private double elevation;

        public double Elevation
        {
            get { return elevation; }
            set
            {
                elevation = value;
                this.RaisePropertyChanged("Elevation");
            }
        }

        private bool isStructural;

        public bool IsStructural
        {
            get { return isStructural; }
            set
            {
                isStructural = value;
                this.RaisePropertyChanged("IsStructural");
            }
        }
    }
}
