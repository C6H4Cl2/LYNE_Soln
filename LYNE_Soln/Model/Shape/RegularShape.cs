using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYNE_Soln.Model.Shape
{
    /// <summary>
    /// Regular shapes are the shapes which can be the 
    /// start points or ending points.
    /// These type of shapes can only be crossed iff the 
    /// previous shape type is the same.
    /// Such as rectangle, triangle and diamond
    /// </summary>
    class RegularShape : Shape
    {
        public RegularShape(ShapeEnum s, int maxTimeToCross, bool isStartPoint) : base(s, maxTimeToCross, isStartPoint)
        {
        }

        public override bool CanBeCrossed(ShapeEnum from)
        {
            // check if the prev shape is equal to this
            if (from != ShapeType) return false;
            else return TimeHasBeenCrossed < MaxTimeToCross;
        }
    }
}
