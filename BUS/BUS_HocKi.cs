using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;


namespace BUS
{
    public static class BUS_HocKi
    {
        public static List<DTO_HocKi> getHocKi()
        {
            return DAO.DAO_HocKi.getHocKi();
        }

        public static List<string> getMaHK()
        {
            return DAO.DAO_HocKi.getMaHK();
        }

        public static List<string> getTenHK()
        {
            return DAO.DAO_HocKi.getTenHK();
        }
    }
}
