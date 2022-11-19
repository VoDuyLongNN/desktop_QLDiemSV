using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;

namespace BUS
{
    public static class BUS_DiemTBHocKi
    {
        public static List<DTO_DiemTBHocKi> getDiemTBHocKi()
        {
            return DAO.DAO_DiemTBHocKi.getDiemTBHocKi();
        }

        public static List<DTO_DiemTBHocKi> timDTBHK(int maSV)
        {
            return DAO.DAO_DiemTBHocKi.timDTBHK(maSV);
        }

        public static List<DTO_DiemTBHocKi> getDSHocBong(string maHK, float diemTBMonToiDa, float diemTBHKToiDa, int slSV)
        {
            return DAO.DAO_DiemTBHocKi.getDSHocBong(maHK, diemTBMonToiDa, diemTBHKToiDa, slSV);
        }

        public static List<DTO_DiemTBHocKi> lietKeLoaiHB(string maHK, float diemTBMonToiDa, float diemTBHKToiDa, int slSV, float min, float max)
        {
            return DAO.DAO_DiemTBHocKi.lietKeLoaiHB(maHK, diemTBMonToiDa,diemTBHKToiDa,slSV, min, max);
        }

        public static List<DTO_DiemTBHocKi> dsSVThoiHoc(float dtb, int soHK)
        {
            return DAO.DAO_DiemTBHocKi.dsSVThoiHoc(dtb, soHK);
        }
    }
}
