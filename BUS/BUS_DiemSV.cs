using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;
namespace BUS
{
    public class BUS_DiemSV
    {
        public static List<DTO_DiemSV> getDSDiemSV()
        {
            return DAO.DAO_DiemSV.getDiemSV();
        }

        public static List<DTO_DiemSV> timSV(int maSV)
        {
            return DAO.DAO_DiemSV.timSV(maSV);
        }

        public static bool themDiemSV(DTO_DiemSV dsv)
        {
            return DAO.DAO_DiemSV.themDiemSV(dsv);
        }

        public static bool kiemTraMaBM(int maSV, string maHK, string maBM)
        {
            return DAO.DAO_DiemSV.kiemTraMaBM(maSV, maHK, maBM);
        }

        public static bool xoaDiemSV(int maSV, string maHK, string maBM)
        {
            return DAO.DAO_DiemSV.xoaDiemSV(maSV, maHK, maBM);
        }

        public static bool suaDiemSV(DTO_DiemSV dsv)
        {
            return DAO.DAO_DiemSV.suaDiemSV(dsv);
        }

        public static bool kiemTraNhapDiem(float dcc, float dgk, float dck)
        {
            return DAO.DAO_DiemSV.kiemTraNhapDiem(dcc, dgk, dck);
        }

        public static bool kiemTraDTBMon(int maSV, string maHK, float diemTB)
        {
            return DAO.DAO_DiemSV.kiemTraDTBMon(maSV, maHK, diemTB);
        }
    }
}
