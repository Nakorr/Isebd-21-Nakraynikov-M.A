using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsLab
{
  public  class depoAlreadyHaveException : Exception
    {
        public depoAlreadyHaveException() : base("В депо уже есть такой вагон")
        { }
    }
}
