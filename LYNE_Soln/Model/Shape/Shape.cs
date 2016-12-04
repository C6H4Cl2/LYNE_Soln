using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYNE_Soln.Model.Shape
{
    /// <summary>
    /// base shape class
    /// </summary>
    abstract public class Shape
    {
        public int col;
        public int row;
        
        public ShapeEnum ShapeType { get { return shapetype; } }
        private ShapeEnum shapetype;

        public int MaxTimeToCross { get { return maxTimeToCross; } }
        private int maxTimeToCross;
        public int TimeHasBeenCrossed { get { return timeHasBeenCrossed; } }
        private int timeHasBeenCrossed;
        public bool IsStartPoint { get { return isStartPoint; } }
        private bool isStartPoint;

        public Shape(ShapeEnum s, int maxTimeToCross, bool isStartPoint)
        {
            this.shapetype = s;
            this.maxTimeToCross = maxTimeToCross;
            this.isStartPoint = isStartPoint;
            timeHasBeenCrossed = 0;
        }

        /// <summary>
        /// return false if the figure cannot be crossed.
        /// </summary>
        abstract public bool CanBeCrossed(ShapeEnum from);

        public void Cross()
        {
            //int result = (col / 2 + row / 2 * ((this.col + 1) / 2) + 1);
            timeHasBeenCrossed++;
            if (timeHasBeenCrossed > MaxTimeToCross || timeHasBeenCrossed < 0)
            {
                Console.WriteLine(col + " " + row + "\n");
            }
        }

        public void UndoCross()
        {
            timeHasBeenCrossed--;
            if (timeHasBeenCrossed > MaxTimeToCross || timeHasBeenCrossed < 0)
            {
                Console.WriteLine(col + " " + row + "\n");
            }
        }

        public string Description
        {
            get
            {
                if (ShapeType == ShapeEnum.Empty)
                {
                    return "Empty";
                }
                else if (ShapeType == ShapeEnum.Polygon)
                {
                    return String.Format("{0} Holes", MaxTimeToCross);
                }
                else
                {
                    return String.Format("{0} {1}", ShapeType.ToString(), IsStartPoint ? "[start ]" : "");
                }
            }
        }

        //abstract public Figure Clone();

        public override bool Equals(object obj)
        {
            Shape f = obj as Shape;
            if (f == null) return false;
            return f.ShapeType == ShapeType &&
                f.col == col &&
                f.row == row &&
                f.MaxTimeToCross == MaxTimeToCross;
        }

        public override int GetHashCode()
        {
            int hash = (ShapeType.ToString() + col + row).GetHashCode();
            return hash;
        }
    }
}
