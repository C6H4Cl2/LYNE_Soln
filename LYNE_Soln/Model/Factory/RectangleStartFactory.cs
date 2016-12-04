using LYNE_Soln.Model.Shape;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYNE_Soln.Model.Factory
{
    class RectangleStartFactory : ShapeFactory
    {
        public override Shape.Shape create()
        {
            return new RegularShape(ShapeEnum.Rectangle, 1, true);
        }
    }
}
