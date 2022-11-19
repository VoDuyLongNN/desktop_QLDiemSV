using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;

namespace BUS
{
    public static class BUS_DiemTBNam
    {
        public static List<DTO_DiemTBNam> getDTBNam()
        {
            return DAO.DAO_DiemTBNam.getDTBNam();
        }

        public static List<DTO_DiemTBNam> timDTBNam(int maSV)
        {
            return DAO.DAO_DiemTBNam.timDTBNam(maSV);
        }
    }
}
