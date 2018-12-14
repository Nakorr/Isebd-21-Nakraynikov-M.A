using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsLab
{
   public class depoOverflowException : Exception
    {
        public depoOverflowException() : base("В депо нет свободных мест")
        { }
    }
}
