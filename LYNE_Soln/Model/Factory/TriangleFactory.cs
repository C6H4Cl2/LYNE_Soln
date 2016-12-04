using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYNE_Soln.Model.Shape;

namespace LYNE_Soln.Model.Factory
{
    class TriangleFactory : ShapeFactory
    {
        public override Shape.Shape create()
        {
            return new RegularShape(ShapeEnum.Triangle, 1, false);
        }
    }
}
