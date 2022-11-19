using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public static class DAO_DiemTBNam
    {
        public static void themDLVaoBang()
        {
            using (dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                //Xóa toàn bộ dữ liệu trong bảng và submitChanges
                IEnumerable<DiemTBNam> data = from item in dt.DiemTBNams where item != null select item;
                dt.DiemTBNams.DeleteAllOnSubmit(data);
                dt.SubmitChanges();

                //Thêm dữ liệu lại vào bảng và submitChanges
                var query = (from dtbhk in dt.DiemTBHocKis
                             select dtbhk.MaSV).Distinct();

                List<DiemTBNam> list = new List<DiemTBNam>();
                foreach (var row in query)
                {
                    DiemTBNam d = new DiemTBNam();
                    d.MaSV = int.Parse(row.ToString());
                    d.DiemTB = 0;
                    list.Add(d);
                }
                dt.DiemTBNams.InsertAllOnSubmit(list);
                dt.SubmitChanges();
            }
        }

        private static float tinhDTBNam(int maSV)
        {
            using(dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                var query = from item in dt.DiemTBHocKis
                            where item.MaSV == maSV
                            select item;

                float dtb = 0;
                int i = 0;
                foreach(var row in query)
                {
                    dtb += float.Parse(row.DiemTB.ToString());
                    i++;
                }

                DiemTBNam diemTBNam = dt.DiemTBNams.Where(p => p.MaSV == maSV).FirstOrDefault();
                diemTBNam.DiemTB = dtb / i;
                dt.SubmitChanges();

                return dtb / i;
            }
        }

        public static List<DTO_DiemTBNam> getDTBNam()
        {
            themDLVaoBang();
            List<DTO_DiemTBNam> dsDiem = new List<DTO_DiemTBNam>();
            using (dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                var query = from d in dt.DiemTBNams
                            select d;

                foreach (var row in query)
                    dsDiem.Add(new DTO_DiemTBNam(int.Parse(row.MaSV.ToString()), tinhDTBNam(int.Parse(row.MaSV.ToString()))));
                return dsDiem;
            }
        }

        public static List<DTO_DiemTBNam> timDTBNam(int maSV)
        {
            List<DTO_DiemTBNam> ds = new List<DTO_DiemTBNam>();
            using(dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                var query = from dtbn in dt.DiemTBNams
                            where dtbn.MaSV == maSV
                            select dtbn;

                foreach (var row in query)
                    ds.Add(new DTO_DiemTBNam(int.Parse(row.MaSV.ToString()), float.Parse(row.DiemTB.ToString())));
                return ds;
            }
        }
    }
}
