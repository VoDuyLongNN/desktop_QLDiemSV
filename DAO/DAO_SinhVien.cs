using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class DAO_SinhVien
    {
        public static List<DTO_SinhVien> getDSSV()
        {
            List<DTO_SinhVien> dssv = new List<DTO_SinhVien>();
            using(dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                var query = from sv in dt.SinhViens
                            select sv;

                foreach(var row in query)
                {
                    DTO_SinhVien sv = new DTO_SinhVien(row.MaSV, row.HoTen, row.GioiTinh.ToString(), row.Lop, row.QueQuan);
                    dssv.Add(sv);
                }
                return dssv;
            }    
        }

        public static bool xoaSV(int maSV)
        {
            using(dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                SinhVien sv = dt.SinhViens.Where(p => p.MaSV == maSV).SingleOrDefault();
                try
                {
                    dt.SinhViens.DeleteOnSubmit(sv);
                    dt.SubmitChanges();
                    return true;
                }
                catch { return false; }
            }
        }

        public static bool kiemTraMSV(int maSV)
        {
            using(dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                int count = dt.SinhViens.Where(p => p.MaSV == maSV).Count();
                if (count > 0)
                    return false;
                return true;
            }    
        }

        public static bool themSV(DTO_SinhVien SV)
        {
            using(dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                SinhVien sv = new SinhVien();
                sv.MaSV = SV.maSV;
                sv.HoTen = SV.hoTen;
                sv.QueQuan = SV.queQuan;
                sv.Lop = SV.lop;
                sv.GioiTinh = bool.Parse(SV.gioiTinh);
                try
                {
                    dt.SinhViens.InsertOnSubmit(sv);
                    dt.SubmitChanges();
                    return true;
                }
                catch { return false;}
            }    

        }

        public static bool suaSV(DTO_SinhVien SV)
        {
            using(dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                SinhVien sv = dt.SinhViens.Where(p => p.MaSV == SV.maSV).SingleOrDefault();
                sv.HoTen = SV.hoTen;
                sv.QueQuan = SV.queQuan;
                sv.Lop = SV.lop;
                sv.GioiTinh = bool.Parse(SV.gioiTinh);

                try
                {
                    dt.SubmitChanges();
                    return true;
                }
                catch { return true;}
            }
        }

        public static List<SinhVien> timSV(string tenSV)
        {
            List<SinhVien> dssv = new List<SinhVien>();
            using(dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                dssv = dt.SinhViens.Where(p => p.HoTen.Contains(tenSV)).ToList();
                return dssv;
            }
        }

        public static List<DTO_SinhVien> getDSSVTheoMaSV(int maSV)
        {
            using(dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                var query = from i in dt.SinhViens
                            where i.MaSV == maSV
                            select i;

                List<DTO_SinhVien> list = new List<DTO_SinhVien>();
                foreach(var row in query)
                    list.Add(new DTO_SinhVien(int.Parse(row.MaSV.ToString()), row.HoTen, row.GioiTinh.ToString(), row.Lop, row.QueQuan));
                return list;
            }
        }
    }
}
