using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYNE_Soln.Model.Shape;

namespace LYNE_Soln.Model.Factory
{
    class EmptyFactory : ShapeFactory
    {
        public override Shape.Shape create()
        {
            return new BypassShape(ShapeEnum.Empty, 0, false);
        }
    }
}
