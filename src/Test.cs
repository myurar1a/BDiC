using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDiC
{
    internal class Test
    {
        public void ConfirmationOfArrivalDate()
        {
            TimeRelated tr = new TimeRelated();
            bool a = tr.ConfirmationOfArrivalDate(" 2022 / 02 / 07");
            Console.WriteLine(a);
        }
    }
}
