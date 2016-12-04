using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYNE_Soln.Model.Shape;

namespace LYNE_Soln.Model.Factory
{
    abstract class ShapeFactory : IFactory
    {
        public abstract Shape.Shape create();

        static public Shape.Shape createBy(IFactory f)
        {
            return f.create();
        }
    }
}
