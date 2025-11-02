using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_BLL
{
    public class TinhToanBLL
    {
        public decimal RoundToThousand(decimal value)
        {
            return Math.Round(value / 1000, 0) * 1000;
        }
    }
}
