using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_HocKi
    {
        string maHK { get; set; }
        string tenHK { get; set; }

        public DTO_HocKi(string mahk, string tenhk)
        {
            this.maHK = mahk;
            this.tenHK = tenhk;
        }
    }
}
