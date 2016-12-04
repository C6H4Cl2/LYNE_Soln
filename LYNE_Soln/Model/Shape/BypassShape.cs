using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYNE_Soln.Model.Shape
{
    /// <summary>
    /// Bypass Shapes are the shapes which cannot be the 
    /// start points or ending points.
    /// These type of shapes can be crossed without checking 
    /// if the previous shape type is the same.
    /// For example, polygon.
    /// </summary>
    class BypassShape : Shape
    {
        public BypassShape(ShapeEnum s, int maxTimeToCross, bool isStartPoint) : base(s, maxTimeToCross, isStartPoint)
        {
        }

        public override bool CanBeCrossed(ShapeEnum from)
        {
            return TimeHasBeenCrossed < MaxTimeToCross;
        }
    }
}
