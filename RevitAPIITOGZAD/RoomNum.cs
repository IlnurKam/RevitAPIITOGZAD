using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace RevitAPIITOGZAD
{
    public class RoomNum
    {
        private bool _mAllRooms;
        private int _mStartnum;
        private string _mPref = "";
        private IList<Element> _mRooms = (IList<Element>)new List<Element>();
        private string _mSuffix = "";
        private int _mDirnum;
        private double _mStep;
        private double _mPoint;

        public bool AllRooms
        {
            get => this._mAllRooms;
            set => this._mAllRooms = value;
        }

        public int StartNum
        {
            get => this._mStartnum;
            set => this._mStartnum = value;
        }

        public int DirNum
        {
            get => this._mDirnum;
            set => this._mDirnum = value;
        }

        public string Prefix
        {
            get => this._mPref;
            set => this._mPref = value;
        }

        public string Suffix
        {
            get => this._mSuffix;
            set => this._mSuffix = value;
        }

        public IList<Element> Rooms
        {
            get => this._mRooms;
            set => this._mRooms = value;
        }

        public double Step
        {
            get => this._mStep;
            set => this._mStep = value;
        }

        public double Point
        {
            get => this._mPoint;
            set => this._mPoint = value;
        }

        public RoomNum(UIApplication uiApp, Document doc) => this.GetRooms(uiApp, doc);

        public void RenumRooms(Document doc)
        {
            if (this.AllRooms)
                this.GetAllRooms(doc);
            double[] array = new double[((ICollection<Element>)this.Rooms).Count];
            int newSize = 0;
            double num1 = this._mStep;
            double[,] numArray = new double[((ICollection<Element>)this.Rooms).Count, 4];
            for (int index = 0; index < ((ICollection<Element>)this.Rooms).Count; ++index)
            {
                Room room = (Room)this.Rooms[index];
                if (((SpatialElement)room).Area > 0.0)
                {
                    BoundingBoxXYZ boundingBoxXyz = ((Element)room)[doc.ActiveView];
                    double num2 = 0.0;
                    double num3 = 0.0;
                    double num4 = 0.0;
                    double num5 = 0.0;
                    if (this.Point == 0.0)
                    {
                        num2 = Math.Round(boundingBoxXyz.Max.X, 3);
                        num3 = Math.Round(boundingBoxXyz.Max.Y, 3);
                        num4 = Math.Round(boundingBoxXyz.Min.X, 3);
                        num5 = Math.Round(boundingBoxXyz.Min.Y, 3);
                        if (num2 - num4 < num3 - num5)
                        {
                            if (num2 - num4 < num1)
                                num1 = num2 - num4;
                        }
                        else if (num3 - num5 < num1)
                            num1 = num3 - num5;
                    }
                    if (this.Point == 1.0)
                    {
                        num2 = Math.Round((boundingBoxXyz.Max.X + boundingBoxXyz.Min.X) / 2.0, 3);
                        num3 = Math.Round((boundingBoxXyz.Max.Y + boundingBoxXyz.Min.Y) / 2.0, 3);
                        num4 = Math.Round((boundingBoxXyz.Max.X + boundingBoxXyz.Min.X) / 2.0, 3);
                        num5 = Math.Round((boundingBoxXyz.Max.Y + boundingBoxXyz.Min.Y) / 2.0, 3);
                    }
                    if (this.Point == 2.0)
                    {
                        LocationPoint location = ((Element)room).Location as LocationPoint;
                        num2 = Math.Round(location.Point.X, 3);
                        num3 = Math.Round(location.Point.Y, 3);
                        num4 = Math.Round(location.Point.X, 3);
                        num5 = Math.Round(location.Point.Y, 3);
                    }
                    numArray[index, 0] = num2;
                    numArray[index, 1] = num4;
                    numArray[index, 2] = num3;
                    numArray[index, 3] = num5;
                    if ((this.DirNum == 0 || this.DirNum == 6) && !((IEnumerable<double>)array).Contains<double>(-num3))
                    {
                        array[newSize] = -num3;
                        ++newSize;
                    }
                    if ((this.DirNum == 1 || this.DirNum == 3) && !((IEnumerable<double>)array).Contains<double>(num4))
                    {
                        array[newSize] = num4;
                        ++newSize;
                    }
                    if ((this.DirNum == 2 || this.DirNum == 4) && !((IEnumerable<double>)array).Contains<double>(num5))
                    {
                        array[newSize] = num5;
                        ++newSize;
                    }
                    if ((this.DirNum == 5 || this.DirNum == 7) && !((IEnumerable<double>)array).Contains<double>(-num2))
                    {
                        array[newSize] = -num2;
                        ++newSize;
                    }
                }
            }
            Array.Resize<double>(ref array, newSize);
            Array.Sort<double>(array);
            double[] source = array;
            for (int index = 0; index < array.Length; ++index)
            {
                double num6 = array[index];
                double num7 = this._mStep;
                double secondPoint;
                try
                {
                    secondPoint = array[index + 1];
                }
                catch (Exception ex)
                {
                    secondPoint = num6 + 100.0;
                }
                if (this._mStep > num1)
                    num7 = num1 - 0.00328083989501312;
                if (secondPoint < num6 + num7)
                    source = ((IEnumerable<double>)source).Where<double>((Func<double, bool>)(x => secondPoint != x)).ToArray<double>();
            }
            Dictionary<Room, double>[] dictionaryArray = new Dictionary<Room, double>[source.Length];
            for (int index1 = 0; index1 < source.Length; ++index1)
            {
                double num8 = source[index1];
                dictionaryArray[index1] = new Dictionary<Room, double>();
                double num9;
                try
                {
                    num9 = source[index1 + 1];
                }
                catch (Exception ex)
                {
                    num9 = num8 + 100.0;
                }
                for (int index2 = ((ICollection<Element>)this.Rooms).Count - 1; index2 >= 0; --index2)
                {
                    Room room = (Room)this.Rooms[index2];
                    if (((SpatialElement)room).Area > 0.0)
                    {
                        if (this.DirNum == 0)
                        {
                            double num10 = numArray[index2, 1];
                            double num11 = -numArray[index2, 2];
                            if (num11 >= num8 && num11 < num9)
                                dictionaryArray[index1].Add(room, num10);
                        }
                        if (this.DirNum == 1)
                        {
                            double num12 = numArray[index2, 1];
                            double num13 = -numArray[index2, 2];
                            if (num12 >= num8 && num12 < num9)
                                dictionaryArray[index1].Add(room, num13);
                        }
                        if (this.DirNum == 2)
                        {
                            double num14 = numArray[index2, 1];
                            double num15 = numArray[index2, 3];
                            if (num15 >= num8 && num15 < num9)
                                dictionaryArray[index1].Add(room, num14);
                        }
                        if (this.DirNum == 3)
                        {
                            double num16 = numArray[index2, 1];
                            double num17 = numArray[index2, 3];
                            if (num16 >= num8 && num16 < num9)
                                dictionaryArray[index1].Add(room, num17);
                        }
                        if (this.DirNum == 4)
                        {
                            double num18 = -numArray[index2, 0];
                            double num19 = numArray[index2, 3];
                            if (num19 >= num8 && num19 < num9)
                                dictionaryArray[index1].Add(room, num18);
                        }
                        if (this.DirNum == 5)
                        {
                            double num20 = -numArray[index2, 0];
                            double num21 = numArray[index2, 3];
                            if (num20 >= num8 && num20 < num9)
                                dictionaryArray[index1].Add(room, num21);
                        }
                        if (this.DirNum == 6)
                        {
                            double num22 = -numArray[index2, 0];
                            double num23 = -numArray[index2, 2];
                            if (num23 >= num8 && num23 < num9)
                                dictionaryArray[index1].Add(room, num22);
                        }
                        if (this.DirNum == 7)
                        {
                            double num24 = -numArray[index2, 0];
                            double num25 = -numArray[index2, 2];
                            if (num24 >= num8 && num24 < num9)
                                dictionaryArray[index1].Add(room, num25);
                        }
                    }
                }
                dictionaryArray[index1] = ((IEnumerable<KeyValuePair<Room, double>>)((IEnumerable<KeyValuePair<Room, double>>)dictionaryArray[index1]).OrderBy<KeyValuePair<Room, double>, double>((Func<KeyValuePair<Room, double>, double>)(pair => pair.Value))).ToDictionary<KeyValuePair<Room, double>, Room, double>((Func<KeyValuePair<Room, double>, Room>)(pair => pair.Key), (Func<KeyValuePair<Room, double>, double>)(pair => pair.Value));
            }
            int startNum = this.StartNum;
            for (int index = 0; index < dictionaryArray.Length; ++index)
            {
                foreach (KeyValuePair<Room, double> keyValuePair in dictionaryArray[index])
                {
                    ((Element)keyValuePair.Key)[(BuiltInParameter) - 1006901].Set(this.Prefix + (object)startNum + this.Suffix);
                    ++startNum;
                }
            }
            this.StartNum = startNum;
            Settings.Default["starnum"] = (object)this.StartNum;
            Settings.Default.Save();
        }

        private void GetRooms(UIApplication uiApp, Document doc)
        {
            ICollection<ElementId> elementIds = uiApp.ActiveUIDocument.Selection.GetElementIds();
            if (elementIds.Count <= 0)
                return;
            foreach (ElementId elementId in (IEnumerable<ElementId>)elementIds)
            {
                Element element = doc.GetElement(elementId);
                if (element.Category.Name == "Помещения" || element.Category.Name == "Rooms")
                    ((ICollection<Element>)this._mRooms).Add((Element)(element as Room));
            }
        }

        private void GetAllRooms(Document doc)
        {
            FilteredElementCollector elementCollector = new FilteredElementCollector(doc, ((Element)doc.ActiveView).Id);
            elementCollector.OfCategory((BuiltInCategory) - 2000160);
            this._mRooms = elementCollector.ToElements();
            if (((ICollection<Element>)this._mRooms).Count != 0)
                return;
            TaskDialog.Show("Ошибка!!!", "На данном виде отсутствуют помещения");
        }

        public void SelectRooms(UIApplication uiApp, Document doc)
        {
            Autodesk.Revit.UI.Selection.Selection selection = uiApp?.ActiveUIDocument?.Selection;
            RoomPickFilter roomPickFilter = new RoomPickFilter();
            if (selection == null)
                return;
            IList<Reference> referenceList = selection.PickObjects((ObjectType)1, (ISelectionFilter)roomPickFilter, "Выберите помещения для перенумерации");
            if (((ICollection<Reference>)referenceList).Count != 0)
                ((ICollection<Element>)this.Rooms).Clear();
            foreach (Reference reference in (IEnumerable<Reference>)referenceList)
                ((ICollection<Element>)this.Rooms).Add(doc.GetElement(reference.ElementId));
        }
    }
}

