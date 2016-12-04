using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYNE_Soln.Model.Shape;

namespace LYNE_Soln.Model.Factory
{
    class Polygon4Factory : ShapeFactory
    {
        public override Shape.Shape create()
        {
            return new BypassShape(ShapeEnum.Polygon, 4, false);
        }
    }
}

