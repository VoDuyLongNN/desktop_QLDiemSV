using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;

namespace BUS
{
    public class BUS_SinhVien
    {
        public static List<DTO_SinhVien> getDSSV()
        {
            return DAO.DAO_SinhVien.getDSSV();
        }

        public static bool XoaSV(int maSV)
        {
            return DAO.DAO_SinhVien.xoaSV(maSV);
        }

        public static bool KiemTraMSV(int msv)
        {
            return DAO.DAO_SinhVien.kiemTraMSV(msv);
        }    

        public static bool themSV(DTO_SinhVien sv)
        {
            return DAO.DAO_SinhVien.themSV(sv);
        }

        public static bool suaSV(DTO_SinhVien sv)
        {
            return DAO.DAO_SinhVien.suaSV(sv);
        }

        public static List<SinhVien> timSV(string tenSV)
        {
            return DAO.DAO_SinhVien.timSV(tenSV);
        }

        public static List<DTO_SinhVien> getDSSVTheoMaSV(int maSV)
        {
            return DAO.DAO_SinhVien.getDSSVTheoMaSV(maSV);
        }
    }
}
