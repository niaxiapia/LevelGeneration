using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace IntelligentPlatform_110kV
{
    public class DataLevel
    {
        public DataLevel(ExternalCommandData commandData)
        {
            doc = commandData.Application.ActiveUIDocument.Document;
            filterUtils = new FilterUtils(commandData);
            
            AllLevels = filterUtils.CollectLevels();
            StructuralLevels = AllLevels.Where(x => x.Name.Contains("结构")).ToList();
        }


        private Document doc;
        private FilterUtils filterUtils;
        public const double Precision = 0.000001;

        private List<Level> structuralLevels = new List<Level>();
        /// <summary>
        /// 带有“结构”字样的标高
        /// </summary>
        public List<Level> StructuralLevels
        {
            get { return structuralLevels; }
            set { structuralLevels = value; }
        }

        private List<Level> allLevels = new List<Level>();
        /// <summary>
        /// 所有标高，并按照层高从低到高排列
        /// </summary>
        public List<Level> AllLevels
        {
            get { return allLevels; }
            set { allLevels = value; }
        }
    }
}
