using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYNE_Soln.Model.Factory
{
    interface IFactory
    {
        Shape.Shape create();
    }
}
