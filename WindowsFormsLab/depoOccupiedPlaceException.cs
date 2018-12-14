using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsLab
{
   public class depoOccupiedPlaceException : Exception
    {
        public depoOccupiedPlaceException(int i) : base("На месте " + i + " уже стоит вагон")
    { }
    }
}
