using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LYNE_Soln.Model.Shape;

namespace LYNE_Soln.Model
{
    public class Route : IEnumerable<Shape.Shape>
    {
        public Shape.Shape this[int index] { get { return shapes[index]; } } 
        private List<Shape.Shape> shapes;

        public int TriNum { get { return triCurrentNum; } }
        private int triCurrentNum = 0;
        public int RectNum { get { return rectCurrentNum; } }
        private int rectCurrentNum = 0;
        public int DiaNum { get { return diaCurrentNum; } }
        private int diaCurrentNum = 0;
        public int Count { get { return shapes.Count; } }

        public Route()
        {
            shapes = new List<Shape.Shape>();
        }

        public void Add(Shape.Shape s)
        {
            if (s == null) return;
            shapes.Add(s);
            switch (s.ShapeType)
            {
                case ShapeEnum.Triangle:
                    triCurrentNum++;
                    break;
                case ShapeEnum.Rectangle:
                    rectCurrentNum++;
                    break;
                case ShapeEnum.Diamond:
                    diaCurrentNum++;
                    break;
                default:
                    break;
            }
        }

        public void RemoveLast()
        {
            if (Count > 0)
            {
                switch (shapes[Count - 1].ShapeType)
                {
                    case ShapeEnum.Triangle:
                        triCurrentNum--;
                        break;
                    case ShapeEnum.Rectangle:
                        rectCurrentNum--;
                        break;
                    case ShapeEnum.Diamond:
                        diaCurrentNum--;
                        break;
                    default:
                        break;
                }
                shapes.RemoveAt(Count - 1);
            }
        }

        public IEnumerator<Shape.Shape> GetEnumerator()
        {
            return shapes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator(); 
        }
    }
}
