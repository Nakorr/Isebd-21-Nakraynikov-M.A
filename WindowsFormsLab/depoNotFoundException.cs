using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsLab
{
   public class depoNotFoundException : Exception
    {
        public depoNotFoundException(int i) : base("Не найден вагон по месту " +i)
        { }
    }
}
