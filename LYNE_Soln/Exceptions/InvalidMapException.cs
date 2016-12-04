using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYNE_Soln.Exceptions
{
    class InvalidMapException : Exception
    {
        public InvalidMapException(string message) : base(message)
        {
        }
    }
}
