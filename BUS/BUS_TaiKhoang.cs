using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace BUS
{
    public class BUS_TaiKhoang
    {
        public static bool kiemTraDN(string tk, string mk)
        {
            return DAO.DAO_TaiKhoang.kiemTraDN(tk, mk);
        }
    }
}
