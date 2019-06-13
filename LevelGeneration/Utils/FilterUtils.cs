using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;

namespace IntelligentPlatform_110kV
{
    /// <summary>
    /// 过滤器工具类
    /// </summary>
    class FilterUtils
    {
        public FilterUtils(ExternalCommandData commandData)
        {
            doc = commandData.Application.ActiveUIDocument.Document;
        }
        /// <summary>
        /// 链接doc
        /// </summary>
        /// <param name="doc"></param>
        public FilterUtils(Document doc)
        {
            this.doc = doc;
        }

        private Document doc;
        /// <summary>
        /// 根据链接文件名称的关键词，获取对应链接Revit文件
        /// </summary>
        /// <param name="linkInstanceName"></param>
        /// <returns></returns>
        public RevitLinkInstance SellectRevitLinkInstance(string linkInstanceName)
        {
            FilteredElementCollector col = new FilteredElementCollector(doc);
            List<RevitLinkInstance> links = col.OfCategory(BuiltInCategory.OST_RvtLinks).
                OfClass(typeof(RevitLinkInstance)).Cast<RevitLinkInstance>().ToList();
            RevitLinkInstance linkInstance = links.FirstOrDefault(x=>x.Name.Contains(linkInstanceName));
            return linkInstance;
        }

        /// <summary>
        /// 根据链接文件名称的关键词，获取对应链接Revit文件的document
        /// </summary>
        /// <param name="linkInstanceName"></param>
        /// <returns></returns>
        public Document SellectLinkDocument(string linkInstanceName)
        {
            RevitLinkInstance instance = SellectRevitLinkInstance(linkInstanceName);
            return instance.GetLinkDocument();
        }

        public List<FamilySymbol> CollectCabinetFamilySymbols(string familyName)
        {
            List < FamilySymbol > cabinetsFamilySymbols=new List<FamilySymbol>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            cabinetsFamilySymbols = collector.OfCategory(BuiltInCategory.OST_ElectricalEquipment).OfClass(typeof(FamilySymbol)).
                Cast<FamilySymbol>().Where(x => x.Family.Name == familyName).ToList();
            return cabinetsFamilySymbols;
        }

        public GridType SelectGridType(string typeName)
        {
            GridType gridType;
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            gridType=collector.OfClass(typeof(GridType)).
                Cast<GridType>().First(x => x.Name == typeName);
            return gridType;
        }

        /// <summary>
        /// 选择立面
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ViewSection SellectViewSection(string name)
        {
            List<ViewSection> viewSections = CollectViewSections();
            ViewSection viewSection = viewSections.Where(x => x.Name.Contains(name)).ToList()[0];
            return viewSection;
        }

        /// <summary>
        /// 根据视图类型选择一个视图类型：楼层平面、天花板平面、面积平面等
        /// </summary>
        /// <param name="viewFamily"></param>
        /// <returns></returns>
        public ViewFamilyType SelectViewFamilyType(ViewFamily viewFamily)
        {
            ViewFamilyType viewType = null;
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(ViewFamilyType));            
            foreach (ViewFamilyType item in collector)
            {
                if (item.ViewFamily == viewFamily)
                {
                    viewType = item;
                }
            }
            return viewType;
        }
        /// <summary>
        /// 根据族名称，获取对应的族类型
        /// </summary>
        /// <param name="familyName"></param>
        /// <returns></returns>
        public List<FamilySymbol> CollectFamilySymbols(string familyName)
        {
            List<FamilySymbol> familySymbols = new List<FamilySymbol>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            familySymbols = collector.OfClass(typeof(FamilySymbol)).
                Cast<FamilySymbol>().Where(x => x.Family.Name == familyName).ToList();
            return familySymbols;
        }
        /// <summary>
        /// 根据族类型的名称，获取对应的族类型
        /// </summary>
        /// <param name="familySymbolName"></param>
        /// <returns></returns>
        public FamilySymbol SellectFamilySymbol(string familyName,string familySymbolName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            FamilySymbol  familySymbol = collector.OfClass(typeof(FamilySymbol)).
                Cast<FamilySymbol>().
                Where(x=>x.FamilyName.Contains(familyName)).
                First(x => x.Name == familySymbolName);
            return familySymbol;
        }

        /// <summary>
        /// 根据pipingSystemTypeName获取对应的管道系统
        /// </summary>
        /// <param name="pipingSystemTypeName"></param>
        /// <returns></returns>
        public PipingSystemType SelectPipingSystemType(string pipingSystemTypeName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            PipingSystemType pipingSystemType = collector.OfClass(typeof(PipingSystemType)).
                Cast<PipingSystemType>().
                First(x => x.Name == pipingSystemTypeName);
            return pipingSystemType;
        }

        /// <summary>
        /// 根据mechanicalSystemTypeName获取对应的mechanicalSystemType
        /// </summary>
        /// <param name="mechanicalSystemTypeName"></param>
        /// <returns></returns>
        public MechanicalSystemType SelectMechanicalSystemType(string mechanicalSystemTypeName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            MechanicalSystemType mechanicalSystemType = collector.OfClass(typeof(MechanicalSystemType)).
                Cast<MechanicalSystemType>().
                First(x => x.Name == mechanicalSystemTypeName);
            return mechanicalSystemType;
        }

        /// <summary>
        /// 根据ductType的name，获取对应的DuctType
        /// </summary>
        /// <param name="ductTypeName"></param>
        /// <returns></returns>
        public DuctType SelectDuctType(string ductTypeName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            DuctType ductType = collector.OfClass(typeof(DuctType)).
                Cast<DuctType>().
                First(x => x.Name == ductTypeName);
            return ductType;
        }

        /// <summary>
        /// 根据pipeType的name，获取对应的pipeType
        /// </summary>
        /// <param name="pipeTypeName"></param>
        /// <returns></returns>
        public PipeType SelectPipeType(string pipeTypeName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            PipeType pipeType = collector.OfClass(typeof(PipeType)).
                Cast<PipeType>().
                First(x => x.Name == pipeTypeName);
            return pipeType;
        }

        public List<FloorType> CollectFoundationFloorTypes()
        {
            List<FloorType> floorTypes = new List<FloorType>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            floorTypes = collector.OfCategory(BuiltInCategory.OST_StructuralFoundation).
                OfClass(typeof(FloorType)).Cast<FloorType>().ToList();
            return floorTypes;
        }

        public List<Floor> CollectFoundationFloors()
        {
            List<Floor> foundationFloors = new List<Floor>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            foundationFloors = collector.OfCategory(BuiltInCategory.OST_StructuralFoundation).
                OfClass(typeof(Floor)).Cast<Floor>().ToList();
            return foundationFloors;
        }

        public List<FloorType> CollectFloorTypes()
        {
            List<FloorType> floorTypes = new List<FloorType>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            floorTypes = collector.OfCategory(BuiltInCategory.OST_Floors).
                OfClass(typeof(FloorType)).Where(x=>x.Name.Contains("结构")|| x.Name.Contains("建筑")).
                Cast<FloorType>().ToList();
            return floorTypes;
        }

        public List<WallType> CollectWallTypes()
        {
            List<WallType> wallTypes = new List<WallType>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            wallTypes = collector.OfCategory(BuiltInCategory.OST_Walls).
                OfClass(typeof(WallType)).Cast<WallType>().ToList();
            return wallTypes;
        }

        /// <summary>
        /// 获取某一楼层的全部房间
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public List<Room> CollectRooms(Level level)
        {
            List<Room> rooms = new List<Room>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            rooms = collector.OfCategory(BuiltInCategory.OST_Rooms).OfClass(typeof(SpatialElement)).
                Where(x=>x.LevelId==level.Id).Cast<Room>().ToList();
            return rooms;
        }
        /// <summary>
        /// 获取全部房间
        /// </summary>
        /// <returns></returns>
        public List<Room> CollectRooms()
        {
            List<Room> rooms = new List<Room>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            rooms = collector.OfCategory(BuiltInCategory.OST_Rooms).OfClass(typeof(SpatialElement)).
                Cast<Room>().ToList();
            return rooms;
        }


        public List<ModelCurve> CollectModelCurves(string lineCategoryName)
        {
            List<ModelCurve> mcs = new List<ModelCurve>();
            FilteredElementCollector collector=new FilteredElementCollector(doc);
            mcs = collector.OfCategory(BuiltInCategory.OST_Lines).OfClass(typeof (CurveElement)).
                Cast<ModelCurve>().Where(x => x.LineStyle.Name == lineCategoryName).ToList();
            return mcs;
        }

        public List<FamilyInstance> SelectFans(Level level)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<FamilyInstance> fans = new List<FamilyInstance>();
            fans = collector.OfCategory(BuiltInCategory.OST_Windows).OfClass(typeof(FamilyInstance))
                    .Cast<FamilyInstance>().Where(x => x.LevelId == level.Id && x.Name.Contains("轴流风机")).ToList();
            return fans;
        }
        /// <summary>
        /// 根据族名称，获取对应的族实例
        /// </summary>
        /// <param name="familyName"></param>
        /// <returns></returns>
        public List<FamilyInstance> CollectFamilyInstances(string familyName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<FamilyInstance> familyInstances = new List<FamilyInstance>();
            familyInstances = collector.
                OfClass(typeof(FamilyInstance)).Cast<FamilyInstance>().
                Where(x => x.Symbol.FamilyName == familyName).ToList();
            return familyInstances;
        }
        /// <summary>
        /// 获取所有楼板
        /// </summary>
        /// <returns></returns>
        public List<Floor> CollectFloors()
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<Floor> floors = collector.OfCategory(BuiltInCategory.OST_Floors).
                OfClass(typeof(Floor)).Cast<Floor>().ToList();
            return floors;
        }
        /// <summary>
        /// 获取某楼层的所有楼板
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public List<Floor> CollectFloors(Level level)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<Floor> floors = collector.OfCategory(BuiltInCategory.OST_Floors).OfClass(typeof(Floor)).
               Where(x => x.LevelId==level.Id).Cast<Floor>().ToList();
            return floors;
        }

        public FloorType SelectFloorType(string aidFloorTypeName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<FloorType> floorTypes = collector.OfClass(typeof(FloorType)).Cast<FloorType>().ToList();
            return floorTypes.FirstOrDefault(floorType => floorType.Name == aidFloorTypeName);
        }

        /// <summary>
        /// 根据familyName和symbolName获取对应的familysymbol,
        /// </summary>
        /// <param name="familyName"></param>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        public FamilySymbol SelectFamilySymbol(string familyName,string symbolName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            FamilySymbol familySymbol =
                collector.OfClass(typeof(FamilySymbol)).Cast<FamilySymbol>().
                Where(x=>x.FamilyName==familyName).
                First(x => x.Name.Equals(symbolName));
            return familySymbol;
        }

        /// <summary>
        /// 这个方法，到时候看建筑的时候，得删除，因为只靠symbolName获取到的symbol是不准确的。有可能有重名
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        public FamilySymbol SelectFamilySymbol(string symbolName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            FamilySymbol familySymbol =
                collector.OfClass(typeof(FamilySymbol)).Cast<FamilySymbol>().First(x => x.Name.Equals(symbolName));
            return familySymbol;
        }


        public GridType SellectGridType(string typeName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<GridType> gridTypes = collector.OfClass(typeof(GridType)).Cast<GridType>()
                .Where(x => x.Name == typeName).ToList();
            GridType gridType = null;
            if (gridTypes.Count>0)
            {
                gridType = gridTypes.First();
            }
            return gridType;
        }
        /// <summary>
        /// 根据轴网类型，获取轴网
        /// </summary>
        /// <param name="gridType"></param>
        /// <returns></returns>
        public List<Grid> CollectGrids(GridType gridType)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<Grid> grids = collector.OfCategory(BuiltInCategory.OST_Grids).OfClass(typeof(Grid)).
               Where(x=>x.GetTypeId()== gridType.Id).Cast<Grid>().ToList();
            return grids;
        }

        /// <summary>
        /// 获取所有轴网,并按照X、Y值从小到大排列
        /// </summary>
        /// <param name="gridType"></param>
        /// <returns></returns>
        public List<Grid> CollectGrids()
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<Grid> grids = collector.OfCategory(BuiltInCategory.OST_Grids).
                OfClass(typeof(Grid)).Cast<Grid>().
               OrderBy(x => (x.Curve as Line).GetEndPoint(0).X).
               OrderBy(x => (x.Curve as Line).GetEndPoint(0).Y).
               ToList();
            return grids;
        }

        public List<Grid> CollectVerticalGrids()
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<Grid> verticalGrids = collector.OfCategory(BuiltInCategory.OST_Grids).OfClass(typeof(Grid))
                .Cast<Grid>().
                Where(x => (x.Curve as Line).Direction.IsAlmostEqualTo(new XYZ(0, 1, 0))).
                Where(x => x.IsHidden(doc.ActiveView) == false).ToList();
            verticalGrids = verticalGrids.OrderBy(x => (x.Curve as Line).GetEndPoint(0).X).ToList();
            return verticalGrids;
        }

        public List<Grid> CollectHorizontalGrids()
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc, doc.ActiveView.Id);
            List<Grid> horizontalGrids = collector.OfCategory(BuiltInCategory.OST_Grids).OfClass(typeof(Grid))
                .Cast<Grid>().
                Where(x => (x.Curve as Line).Direction.IsAlmostEqualTo(new XYZ(1, 0, 0))).
                Where(x => x.IsHidden(doc.ActiveView) == false).ToList();
            horizontalGrids = horizontalGrids.OrderBy(x => (x.Curve as Line).GetEndPoint(0).Y).ToList();
            return horizontalGrids;
        }

        /// <summary>
        /// 获取所有的楼层,并按照层高从低到高排列
        /// </summary>
        /// <returns></returns>
        public List<Level> CollectLevels()
        {
            FilteredElementCollector levelCollector = new FilteredElementCollector(doc);
            List<Level> levels = levelCollector.OfClass(typeof(Level)).Cast<Level>().ToList();
            levels = levels.OrderBy(x => x.Elevation).ToList();
            return levels;
        }

        public ViewPlan SelectViewPlan(string name)
        {
            List<ViewPlan> viewPlans = CollectViewPlans();
            ViewPlan viewPlan = viewPlans.Where(x => x.Name.Contains(name)).ToList()[0];
            return viewPlan;
        }

        public ViewPlan SelectViewPlan(Level level)
        {
            List<ViewPlan> viewPlans = CollectViewPlans();
            ViewPlan viewPlan = viewPlans.Where(x => x.get_Parameter(BuiltInParameter.PLAN_VIEW_LEVEL).
            AsString()== level.Name).ToList()[0];
            return viewPlan;
        }

        public List<ViewPlan> CollectViewPlans()
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<ViewPlan> viewPlans = collector.OfClass(typeof(ViewPlan)).Cast<ViewPlan>().ToList();
            return viewPlans;
        }


        public List<RoofType> CollectRoofTypes()
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<RoofType> roofTypes = collector.OfClass(typeof(RoofType)).Cast<RoofType>().ToList();
            return roofTypes;
        }

        /// <summary>
        /// 选择立面
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ViewSection SelectViewSection(string name)
        {
            List<ViewSection> viewSections = CollectViewSections();
            ViewSection viewSection = viewSections.Where(x => x.Name.Contains(name)).ToList()[0];
            return viewSection;
        }

        public List<ViewSection> CollectViewSections()
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<ViewSection> viewSections = collector.OfClass(typeof(ViewSection)).Cast<ViewSection>().ToList();
            return viewSections;
        }

        /// <summary>
        /// 如果需要某楼层的所有墙，name参数填""即可，
        /// </summary>
        /// <param name="level"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Wall> CollectWalls(Level level,string name)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<Wall> walls = collector.OfCategory(BuiltInCategory.OST_Walls).OfClass(typeof (Wall)).
                Where(x=>x.Name.Contains(name)).
                Where(x=>x.get_Parameter(BuiltInParameter.WALL_BASE_CONSTRAINT).AsElementId()== level.Id).
                Cast<Wall>().ToList();
            return walls;
        }

        /// <summary>
        /// 获取所有的墙
        /// </summary>
        /// <param name="level"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Wall> CollectWalls()
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<Wall> walls = collector.OfCategory(BuiltInCategory.OST_Walls).
                OfClass(typeof(Wall)).Cast<Wall>().ToList();
            return walls;
        }

        public WallType SelectWallType(string aidWallTypeName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<WallType> wallTypes = collector.OfClass(typeof(WallType)).Cast<WallType>().ToList();
            return wallTypes.FirstOrDefault(wallType => wallType.Name == aidWallTypeName);
        }

        public List<FamilyInstance> CollectGenericModels(string name)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<FamilyInstance> genericModels =
                collector.OfCategory(BuiltInCategory.OST_GenericModel)
                    .OfClass(typeof(FamilyInstance))
                    .Where(x => x.Name.Contains(name))
                    .Cast<FamilyInstance>()
                    .ToList();
            genericModels = genericModels.OrderBy(x => (x.Location as LocationPoint).Point.X).
                OrderBy(x => (x.Location as LocationPoint).Point.Y).ToList();
            return genericModels;
        }

        public List<FamilyInstance> CollectSpecialityEquipments(string name)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<FamilyInstance> specialityEquipments =
                collector.OfCategory(BuiltInCategory.OST_SpecialityEquipment)
                    .OfClass(typeof(FamilyInstance))
                    .Where(x => x.Name.Contains(name))
                    .Cast<FamilyInstance>()
                    .ToList();
            specialityEquipments = specialityEquipments.OrderBy(x => (x.Location as LocationPoint).Point.X).
                OrderBy(x => (x.Location as LocationPoint).Point.Y).ToList();
            return specialityEquipments;
        }

        public FamilySymbol SelectRoomTag(string name)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            FamilySymbol roomTag = collector.OfCategory(BuiltInCategory.OST_RoomTags).
                OfClass(typeof(FamilySymbol)).Where(x => x.Name == name).Cast<FamilySymbol>().ToList()[0];
            return roomTag;
        }

        public List<FamilyInstance> CollectElectricalEquipments(string name)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<FamilyInstance> electricalEquipments =
                collector.OfCategory(BuiltInCategory.OST_ElectricalEquipment)
                    .OfClass(typeof(FamilyInstance))
                    .Where(x => x.Name.Contains(name))
                    .Cast<FamilyInstance>()
                    .ToList();
            electricalEquipments = electricalEquipments.OrderBy(x => (x.Location as LocationPoint).Point.X).
                OrderBy(x => (x.Location as LocationPoint).Point.Y).ToList();
            return electricalEquipments;
        }
        

        public List<FamilyInstance> CollectEEsByFamilyName(string familyName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<FamilyInstance> electricalEquipments =
                collector.OfCategory(BuiltInCategory.OST_ElectricalEquipment)
                    .OfClass(typeof(FamilyInstance))                    
                    .Cast<FamilyInstance>()
                    .Where(x=>x.Symbol.FamilyName.Equals(familyName))
                    .ToList();
            electricalEquipments = electricalEquipments.OrderBy(x => (x.Location as LocationPoint).Point.X).
                OrderBy(x => (x.Location as LocationPoint).Point.Y).ToList();
            return electricalEquipments;
        }

        public List<FamilyInstance> CollectGenericAnnotations(string name)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<FamilyInstance> genericAnnotations =
                collector.OfCategory(BuiltInCategory.OST_GenericAnnotation)
                    .OfClass(typeof(FamilyInstance))
                    .Where(x => x.Name.Contains(name))
                    .Cast<FamilyInstance>()
                    .ToList();
            genericAnnotations = genericAnnotations.OrderBy(x => (x.Location as LocationPoint).Point.X).
                OrderBy(x => (x.Location as LocationPoint).Point.Y).ToList();
            return genericAnnotations;
        }


        public Reference GetReferenceOfFamilyInstance(FamilyInstance instance, SpecialReferenceType ReferenceType)
        {
            Reference indexReference = null;
            int index = (int)ReferenceType;
            Options geomOptions = new Options();
            geomOptions.ComputeReferences = true;
            geomOptions.DetailLevel = ViewDetailLevel.Medium;
            geomOptions.IncludeNonVisibleObjects = true;
            GeometryElement geoElement = instance.get_Geometry(geomOptions);
            foreach (GeometryObject obj in geoElement)
            {
                if (obj is GeometryInstance)
                {
                    GeometryInstance geoInstance = obj as GeometryInstance;
                    if (geoInstance != null)
                    {
                        GeometryElement geoSymbol = geoInstance.GetSymbolGeometry();
                        string sampleStableRef = null;
                        if (geoSymbol != null)
                        {
                            foreach (GeometryObject geomObj in geoSymbol)
                            {
                                if (geomObj is Solid)
                                {
                                    Solid solid = geomObj as Solid;
                                    if (solid.Faces.Size > 0)
                                    {
                                        Face face = solid.Faces.get_Item(0);
                                        sampleStableRef = face.Reference.ConvertToStableRepresentation(doc);
                                        break;
                                    }
                                }
                            }
                        }
                        if (sampleStableRef != null)
                        {
                            string[] refTokens = sampleStableRef.Split(new char[] { ':' });
                            string customStableRef = refTokens[0] + ':' + refTokens[1] + ':' + refTokens[2] + ':' +
                                                     refTokens[3] + ':' + index.ToString();
                            indexReference = Reference.ParseFromStableRepresentation(doc, customStableRef);
                        }
                        break;
                    }
                }
            }
            return indexReference;
        }

        public enum SpecialReferenceType
        {
            Left = 0,
            CenterLR = 1,
            Right = 2,
            Front = 3,
            CenterFB = 4,
            Back = 5,
            Bottom = 6,
            CenterElevation = 7,
            Top = 8
        }

        public FamilySymbol SelecetWindowTag(string tagName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<FamilySymbol> tagSymbols = collector.OfCategory(BuiltInCategory.OST_WindowTags).OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>().Where(x => x.Name == tagName).ToList();
            FamilySymbol windowTagSymbol = tagSymbols[0];
            return windowTagSymbol;
        }

        public List<FamilyInstance> CollectWindows(Level level)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<FamilyInstance> windows =
                collector.OfCategory(BuiltInCategory.OST_Windows).OfClass(typeof(FamilyInstance))
                    .Cast<FamilyInstance>().Where(x =>
                    x.get_Parameter(BuiltInParameter.FAMILY_LEVEL_PARAM).AsElementId() == level.Id
                    && x.Name.StartsWith("C")).ToList();
            return windows;
        }

        public List<FamilyInstance> CollectBlindWindows(Level level)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<FamilyInstance> windows =
                collector.OfCategory(BuiltInCategory.OST_Windows).OfClass(typeof(FamilyInstance))
                    .Cast<FamilyInstance>().Where(x =>
                    x.get_Parameter(BuiltInParameter.FAMILY_LEVEL_PARAM).AsElementId() == level.Id
                    && x.Name.StartsWith("SC")).ToList();
            return windows;
        }

        public FamilySymbol SelecetDoorTag(string tagName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<FamilySymbol> tagSymbols = collector.OfCategory(BuiltInCategory.OST_DoorTags).OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>().Where(x => x.Name == tagName).ToList();
            FamilySymbol doorTagSymbol = tagSymbols[0];
            return doorTagSymbol;
        }
        /// <summary>
        /// 获取某一固定楼层的所有门
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public List<FamilyInstance> CollectDoors(Level level)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<FamilyInstance> doors =
                collector.OfCategory(BuiltInCategory.OST_Doors).OfClass(typeof(FamilyInstance))
                    .Cast<FamilyInstance>().Where(x =>
                    x.get_Parameter(BuiltInParameter.FAMILY_LEVEL_PARAM).AsElementId() == level.Id).ToList();
            return doors;
        }
        /// <summary>
        /// 获取文档中的所有门
        /// </summary>
        /// <returns></returns>
        public List<FamilyInstance> CollectDoors()
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<FamilyInstance> doors =
                collector.OfCategory(BuiltInCategory.OST_Doors).OfClass(typeof(FamilyInstance))
                    .Cast<FamilyInstance>().ToList();
            return doors;
        }
        /// <summary>
        /// 首先获取所有墙，然后排除幕墙、地下墙等类型，
        /// 然后根据剩余墙的LocationCurve，筛选出其中的最值，确定其为外墙的墙类型
        /// </summary>
        /// <returns></returns>
        public WallType SelectArchiOutWallType()
        {
            List<Wall> walls = CollectWalls().
                Where(x => x.Name.Contains("混凝土") == false).
                Where(x => (x as Wall).WallType.Kind == WallKind.Basic).ToList();
            double Xmax= walls.Max(x => (x.Location as LocationCurve).Curve.GetEndPoint(0).X);
            WallType archioutwalltype = walls.
                First(x => (x.Location as LocationCurve).Curve.GetEndPoint(0).X == Xmax).WallType;
            return archioutwalltype;
        }

        public FamilySymbol SelecetAnnotation(string tagName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<FamilySymbol> annotationSymbols = collector.OfCategory(BuiltInCategory.OST_GenericAnnotation).OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>().Where(x => x.Name == tagName).ToList();
            FamilySymbol annotationSymbol = annotationSymbols[0];
            return annotationSymbol;
        }

        public List<FamilyInstance> CollectStructuralFoundation(string name)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<FamilyInstance> structuralFoundations = new List<FamilyInstance>();
            structuralFoundations = collector.OfCategory(BuiltInCategory.OST_StructuralFoundation).OfClass(typeof(FamilyInstance))
                    .Cast<FamilyInstance>().Where(x => x.Name.Equals(name)).ToList();
            return structuralFoundations;
        }
    }
}
