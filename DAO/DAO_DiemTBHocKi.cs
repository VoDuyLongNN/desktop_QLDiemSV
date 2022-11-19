using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public static class DAO_DiemTBHocKi
    {
        private static void themDLVaoBang()
        {
            using(dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                //Xóa toàn bộ dữ liệu trong bảng và submitChanges
                IEnumerable<DiemTBHocKi> data = from item in dt.DiemTBHocKis where item != null select item;
                dt.DiemTBHocKis.DeleteAllOnSubmit(data);
                dt.SubmitChanges();

                //Thêm dữ liệu lại vào bảng và submitChanges
                var query = (from dsv in dt.DiemSVs
                             select new
                             {
                                 maSV = dsv.MaSV,
                                 maHK = dsv.MaHK
                             }).Distinct();

                List<DiemTBHocKi> ds = new List<DiemTBHocKi>();
                foreach(var row in query)
                {
                    DiemTBHocKi dtb = new DiemTBHocKi();
                    dtb.MaSV = row.maSV;
                    dtb.MaHK = row.maHK;
                    dtb.DiemTB = 0;
                    ds.Add(dtb);
                }
                dt.DiemTBHocKis.InsertAllOnSubmit(ds);
                dt.SubmitChanges();
            }
        }

        private static float tinhDiemTB(int maSV, string maHK)
        {
            DAO_DiemSV.updateDTB();
            using (dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                var result = from dsv in dt.DiemSVs
                             where dsv.MaSV == maSV && dsv.MaHK == maHK
                             select dsv;
                float kq = 0;
                int i = 0;
                foreach (var row in result)
                {
                    kq += float.Parse(row.DiemTBMon.ToString());
                    i++;
                }
                DiemTBHocKi dtbhk = dt.DiemTBHocKis.Where(p => p.MaSV == maSV && p.MaHK == maHK).FirstOrDefault();
                dtbhk.DiemTB = kq / i;
                dt.SubmitChanges();

                return kq / i;
            }
        }

        public static List<DTO_DiemTBHocKi> getDiemTBHocKi()
        {
            themDLVaoBang();
            List<DTO_DiemTBHocKi> dsdtb = new List<DTO_DiemTBHocKi>();
            using(dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                var query = (from dtb in dt.DiemTBHocKis
                             select dtb).Distinct();

                foreach(var row in query)
                {
                    float dtb = tinhDiemTB(int.Parse(row.MaSV.ToString()), row.MaHK);
                    DTO_DiemTBHocKi dtbhk = new DTO_DiemTBHocKi(int.Parse(row.MaSV.ToString()), row.MaHK, dtb);
                    dsdtb.Add(dtbhk);
                }
                return dsdtb;
            }
        }

        public static List<DTO_DiemTBHocKi> timDTBHK(int maSV)
        {
            List<DTO_DiemTBHocKi> dsTim = new List<DTO_DiemTBHocKi>();
            using(dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                var query = from dtb in dt.DiemTBHocKis
                            where dtb.MaSV == maSV
                            select dtb;
                foreach (var row in query)
                    dsTim.Add(new DTO_DiemTBHocKi(int.Parse(row.MaSV.ToString()), row.MaHK, float.Parse(row.DiemTB.ToString())));

                return dsTim;
            }
        }

        public static bool kiemTraHocBong(int maSV, string maHK, float diemTBMonToiDa, float diemTBHKToiDa)
        {
            using(dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                var query = from dtbhk in dt.DiemTBHocKis
                            where dtbhk.MaHK == maHK && dtbhk.MaSV == maSV
                            select dtbhk;

                foreach(var row in query)
                {
                    if(DAO_DiemSV.kiemTraDTBMon(maSV, maHK, diemTBMonToiDa))
                        if(row.DiemTB >= diemTBHKToiDa)
                            return true;
                }

                return false;
            }
        }

        public static List<DTO_DiemTBHocKi> getDSHocBong(string maHK, float diemTBMonToiDa, float diemTBHKToiDa, int slSV)
        {
            List<DTO_DiemTBHocKi> ds = new List<DTO_DiemTBHocKi>();
            using(dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                var query = from dtbhk in dt.DiemTBHocKis
                            where dtbhk.MaHK == maHK
                            orderby dtbhk.DiemTB descending
                            select dtbhk;

                int sl = 0;
                foreach (var row in query)
                {
                    if (kiemTraHocBong(int.Parse(row.MaSV.ToString()), maHK, diemTBMonToiDa, diemTBHKToiDa))
                    {
                        ds.Add(new DTO_DiemTBHocKi(int.Parse(row.MaSV.ToString()), row.MaHK, float.Parse(row.DiemTB.ToString())));
                        sl++;
                    }
                    if (sl == slSV)
                        break;
                }

                return ds;
            }
        }

        public static List<DTO_DiemTBHocKi> lietKeLoaiHB(string maHK, float diemTBMonToiDa, float diemTBHKToiDa, int slSV, float min, float max)
        {
            List<DTO_DiemTBHocKi> ds = new List<DTO_DiemTBHocKi>();
            using (dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                var query = from dtbhk in dt.DiemTBHocKis
                            where dtbhk.MaHK == maHK
                            orderby dtbhk.DiemTB descending
                            select dtbhk;

                int sl = 0;
                foreach (var row in query)
                {
                    if (kiemTraHocBong(int.Parse(row.MaSV.ToString()), maHK, diemTBMonToiDa, diemTBHKToiDa))
                    {
                        if(row.DiemTB >= min && row.DiemTB < max)
                        {
                            ds.Add(new DTO_DiemTBHocKi(int.Parse(row.MaSV.ToString()), row.MaHK, float.Parse(row.DiemTB.ToString())));
                            sl++;
                        }
                    }
                    if (sl == slSV)
                        break;
                }

                return ds;
            }
        }

        //Liệt kê sinh viên trong tình trạng thôi học

        public static bool kiemTraSVThoiHoc(int maSV, float dtb, int soHK)
        {
            using (dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                var query = from i in dt.DiemTBHocKis
                            where i.MaSV == maSV && i.DiemTB <= dtb
                            select i;

                if (query.Count() == soHK)
                    return true;
                return false;
            }
        }
        public static List<DTO_DiemTBHocKi> dsSVThoiHoc(float dtb, int soHK)
        {
            using(dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                var query = from i in dt.DiemTBHocKis
                            where i.DiemTB <= dtb
                            select i;
                List<DTO_DiemTBHocKi> ds = new List<DTO_DiemTBHocKi>();
                foreach(var row in query)
                {
                    if(kiemTraSVThoiHoc(int.Parse(row.MaSV.ToString()), dtb, soHK))
                        ds.Add(new DTO_DiemTBHocKi(int.Parse(row.MaSV.ToString()), row.MaHK, float.Parse(row.DiemTB.ToString())));
                }
                return ds;
            }
        }
    }
}
