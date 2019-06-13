using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentPlatform_110kV
{
    
    public class LevelGenerationViewModel : NotificationObject
    {
        public LevelGenerationViewModel(ExternalCommandData commandData)
        {
            doc = commandData.Application.ActiveUIDocument.Document;
            filterUtils = new FilterUtils(commandData);
            datalevel = new DataLevel(commandData);

            GetAturalLevels();

            MockLevels = GetMockLevels();

            SelectionChangedCommand = new DelegateCommand(new Action(SelectionChangedCommandExcute));

            AddLevelCommand = new DelegateCommand(new Action(AddLevelCommandExcute));
            DeleteLevelCommand = new DelegateCommand(new Action(DeleteLevelCommandExcute));
            UpdateCommand = new DelegateCommand(new Action(UpdateCommandExcute));
        }

        private Document doc;
        private DataLevel datalevel;
        private FilterUtils filterUtils;
        private List<Level> acturalLevels=new List<Level>();



        private bool? dialogResult;
        /// <summary>
        /// 控制窗口的关闭
        /// </summary>
        public bool? DialogResult
        {
            get { return dialogResult; }
            set
            {
                dialogResult = value;
                this.RaisePropertyChanged("DialogResult");
            }
        }

        /// <summary>
        /// 获取实际的标高信息
        /// </summary>
        private void GetAturalLevels()
        {
            acturalLevels = datalevel.AllLevels;            
        }       

        private ObservableCollection<MockLevel> mockLevels;
        /// <summary>
        /// 在MVVM模式中，集合（尤其是可能在View中有变化的集合）建议使用ObservableCollection
        /// </summary>
        public ObservableCollection<MockLevel> MockLevels
        {
            get { return mockLevels; }
            set
            {
                mockLevels = value;
                this.RaisePropertyChanged("MockLevels");
            }
        }

        /// <summary>
        /// 根据doc中的实际标高，自动获得对应的mocklevel
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<MockLevel> GetMockLevels()
        {
            ObservableCollection<MockLevel> mockLevels = 
                new ObservableCollection<MockLevel>();
            foreach (var level in acturalLevels)
            {
                MockLevel mockLevel = new MockLevel()
                { Name=level.Name,Elevation=level.Elevation};
                if (level.Name.Contains("结构"))
                {
                    mockLevel.IsStructural = true;
                }
                mockLevels.Add(mockLevel);
            }
            return mockLevels;
        }

        /// <summary>
        /// 绑定了“站型选择”的SelectionChanged事件
        /// </summary>
        public DelegateCommand SelectionChangedCommand { get; set; }

        private void SelectionChangedCommandExcute()
        {
            if (StationType.Equals("110kV_A2-7"))
            {
                //设置标高列表的值（先清空）
                MockLevels.Clear();
                MockLevel L1 = new MockLevel()
                { Name = "-3.300米层", Elevation = -3300 / 304.8, IsStructural = true };
                MockLevel L2 = new MockLevel()
                { Name = "-3.200米层", Elevation = -3200 / 304.8, IsStructural = false };    
                MockLevel L4 = new MockLevel()
                { Name = "-1.050米层", Elevation = -1050 / 304.8, IsStructural = false };
                MockLevel L5 = new MockLevel()
                { Name = "-0.050米层", Elevation = -50 / 304.8, IsStructural = true };
                MockLevel L6 = new MockLevel()
                { Name = "0.000米层", Elevation = 0, IsStructural = false };
                MockLevel L7 = new MockLevel()
                { Name = "4.750米层", Elevation = 4750 / 304.8, IsStructural = true };
                MockLevel L8 = new MockLevel()
                { Name = "4.800米层", Elevation = 4800 / 304.8, IsStructural = false };
                MockLevel L9 = new MockLevel()
                { Name = "9.600米层", Elevation = 9600 / 304.8, IsStructural = true };
                MockLevel L10 = new MockLevel()
                { Name = "10.600米层", Elevation = 10600 / 304.8, IsStructural = true };
                MockLevels.Add(L1);
                MockLevels.Add(L2);
                MockLevels.Add(L4);
                MockLevels.Add(L5);
                MockLevels.Add(L6);
                MockLevels.Add(L7);
                MockLevels.Add(L8);
                MockLevels.Add(L9);
                MockLevels.Add(L10);
            }
        }

        private List<string> stationTypes = new List<string>()
            { "110kV_A2-7" };

        public List<string> StationTypes
        {
            get { return stationTypes; }
            set
            {
                stationTypes = value;
                this.RaisePropertyChanged("StationTypes");
            }
        }        

        private string stationType;

        public string StationType
        {
            get { return stationType; }
            set
            {
                stationType = value;
                this.RaisePropertyChanged("StationType");
            }
        }

        /// <summary>
        /// 绑定了“添加”按钮
        /// </summary>
        public DelegateCommand AddLevelCommand { get; set; }

        private void AddLevelCommandExcute()
        {
            MockLevel addedLevel;
            string name;
            if (MockLevels.Count==0)
            {
                addedLevel = new MockLevel();
                addedLevel.Elevation =  3000 / 304.8;
                name = (addedLevel.Elevation * 304.8 / 1000).ToString("#0.000");
                addedLevel.Name = name + "米层";
                MockLevels.Add(addedLevel);
            }
            else if (MockLevels.Count> 0)
            {
                MockLevel lastLevel = MockLevels.Last();
                addedLevel = new MockLevel();
                addedLevel.Elevation = lastLevel.Elevation + 3000 / 304.8;
                name = (addedLevel.Elevation * 304.8 / 1000).ToString("#0.000");
                addedLevel.Name = name + "米层";
                MockLevels.Add(addedLevel);
            }            
        }

        /// <summary>
        /// 绑定了“删除”按钮
        /// </summary>
        public DelegateCommand DeleteLevelCommand { get; set; }

        private void DeleteLevelCommandExcute()
        {
            if (MockLevels.Count>0)
            {
                if (SelectedItem == null)
                {
                    int index = MockLevels.Count;
                    MockLevels.RemoveAt(index - 1);
                }
                else
                {
                    MockLevels.Remove(SelectedItem);
                }
            }            
        }

        /// <summary>
        /// 绑定了“开始创建”按钮
        /// </summary>
        public DelegateCommand UpdateCommand { get; set; }

        private void UpdateCommandExcute()
        {
            using (Transaction ts = new Transaction(doc, "更新标高"))
            {
                ts.Start();               
                UpdateLevels();
                if (!TurnOnOrOffWin())
                {
                    ts.RollBack();
                }
                else
                {
                    ts.Commit();
                }
            }
            TurnOnOrOffWin();
        }

        /// <summary>
        /// 只要出现一个提示框，就不关闭
        /// </summary>
        private bool TurnOnOrOffWin()
        {
            bool flag = false;
            if (true)
            {
                this.DialogResult = true;
                flag = true;
            }
            return flag;
        }

        private void UpdateLevels()
        {
            ViewFamilyType viewType = filterUtils.SelectViewFamilyType(ViewFamily.FloorPlan);
            //首先全部删除(00层不能删。在REVIT中，必须至少有一个标高)
            foreach (var level in acturalLevels)
            {
                if (level.Elevation!=0)
                {
                    doc.Delete(level.Id);
                }
                else
                {
                    level.Name= "0.000米层（建筑）";
                }
            }
            //然后根据MockLevels，全部重新生成
            foreach (var mockLevel in MockLevels)
            {
                if (mockLevel.Elevation != 0)
                {
                    Level level = Level.Create(doc, mockLevel.Elevation);
                    string name = (level.Elevation * 304.8 / 1000).ToString("#0.000");
                    if (mockLevel.IsStructural)
                    {
                        level.Name = name + "米层（结构）";
                    }
                    else
                    {
                        level.Name = name + "米层（建筑）";
                    }
                    ViewPlan floorview = ViewPlan.Create(doc, viewType.Id, level.Id);
                }                          
            }
        }

        private MockLevel selectedItem;
        /// <summary>
        /// 对应View界面中用户选择项
        /// </summary>
        public MockLevel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                this.RaisePropertyChanged("SelectedItem");
            }
        }      
    }
}
