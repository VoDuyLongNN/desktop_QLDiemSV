using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using DTO;

namespace DAO
{
    public class DAO_TaiKhoang
    {
        public static bool kiemTraDN(string tk, string mk)
        {
            using(dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                int c = dt.TaiKhoangs.Where(p => p.TenDN == tk && p.MatKhau == mk).Count();
                if(c > 0)
                    return true;
                return false;
            }
        }
    }
}
