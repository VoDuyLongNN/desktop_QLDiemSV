using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class DAO_DiemSV
    {
        public static void updateDTB()
        {
            using(dataQLDSVDataContext db = new dataQLDSVDataContext())
            {
                var query = from dsv in db.DiemSVs
                            select dsv;
                foreach(var row in query)
                {
                    DiemSV sv = db.DiemSVs.Where(p => p.MaSV == row.MaSV && p.MaHK == row.MaHK && p.MaBM == row.MaBM).FirstOrDefault();
                    sv.DiemTBMon = row.DiemCC * 0.1 + row.DiemGK * 0.3 + row.DiemCK * 0.6;

                    db.SubmitChanges();
                }
            }
        }
        public static List<DTO_DiemSV> getDiemSV()
        {
            updateDTB();
            List<DTO_DiemSV> ds = new List<DTO_DiemSV>();
            using(dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                var query = from dsv in dt.DiemSVs
                            orderby dsv.MaSV, dsv.MaHK
                            select dsv;

                foreach(var row in query)
                {
                    int masv = int.Parse(row.MaSV.ToString());
                    string mahk = row.MaHK;
                    string mabm = row.MaBM;
                    string magv = row.MaGV;
                    float dcc = float.Parse(row.DiemCC.ToString());
                    float dgk = float.Parse(row.DiemGK.ToString());
                    float dck = float.Parse(row.DiemCK.ToString());
                    float tb = float.Parse(row.DiemTBMon.ToString());

                    DTO_DiemSV dsv = new DTO_DiemSV(masv, mahk, mabm, magv, dcc, dgk, dck, tb);
                    ds.Add(dsv);
                }    
                return ds;
            }    
        }
        public static List<DTO_DiemSV> timSV(int maSV)
        {
            List<DTO_DiemSV> ds = new List<DTO_DiemSV>();
            using (dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                var query = from dsv in dt.DiemSVs
                            where dsv.MaSV == maSV
                            select dsv;

                foreach (var row in query)
                {
                    string mahk = row.MaHK;
                    int masv = int.Parse(row.MaSV.ToString());
                    string mabm = row.MaBM;
                    string magv = row.MaGV;
                    float dcc = float.Parse(row.DiemCC.ToString());
                    float dgk = float.Parse(row.DiemGK.ToString());
                    float dck = float.Parse(row.DiemCK.ToString());
                    float tb = float.Parse(((dcc * 0.1) + (dgk * 0.3) + (dck * 0.6)).ToString());

                    DTO_DiemSV dsv = new DTO_DiemSV(masv, mahk, mabm, magv, dcc, dgk, dck, tb);
                    ds.Add(dsv);
                }
                return ds;
            }
        }

        public static bool kiemTraMaBM(int maSV, string maHK,string maBM)
        {
            using(dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                int count = dt.DiemSVs.Where(p => p.MaSV == maSV && p.MaHK == maHK && p.MaBM == maBM).Count();
                if (count > 0)
                    return true;
                return false;
            }    
        }

        public static bool kiemTraNhapDiem(float dcc, float dgk, float dck)
        {
            if((dcc < 0 || dcc > 10) || (dgk < 0 || dgk > 10) || (dck < 0 || dck > 10))
                return false;
            return true;
        }

        public static bool themDiemSV(DTO_DiemSV DSV)
        {
            using(dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                DiemSV dsv = new DiemSV();
                dsv.MaHK = DSV.maHK;
                dsv.MaSV = DSV.maSV;
                dsv.MaBM = DSV.maBM;
                dsv.MaGV = DSV.maGV;
                dsv.DiemCC = DSV.diemCC;
                dsv.DiemGK = DSV.diemGK;
                dsv.DiemCK = DSV.diemCK;
                dsv.DiemTBMon = DSV.diemTBM;

                try
                {
                    dt.DiemSVs.InsertOnSubmit(dsv);
                    dt.SubmitChanges();
                    return true;
                }
                catch { return false; }
            }
        }

        public static bool xoaDiemSV(int maSV, string maHK, string maBM)
        {
            using(dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                DiemSV dsv = dt.DiemSVs.Where(p => p.MaSV == maSV && p.MaHK == maHK && p.MaBM == maBM).FirstOrDefault();
                try
                {
                    dt.DiemSVs.DeleteOnSubmit(dsv);
                    dt.SubmitChanges();
                    return true;
                }
                catch { return false;}
            }
        }

        public static bool suaDiemSV(DTO_DiemSV DSV)
        {
            using(dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                DiemSV dsv = dt.DiemSVs.Where(p => p.MaSV == DSV.maSV && p.MaHK == DSV.maHK && p.MaBM == DSV.maBM).FirstOrDefault();
                dsv.MaGV = DSV.maGV;
                dsv.DiemCC = DSV.diemCC;
                dsv.DiemGK = DSV.diemGK;
                dsv.DiemCK = DSV.diemCK;

                try
                {
                    dt.SubmitChanges();
                    return true;
                }
                catch { return false; }
            }
        }

        public static bool kiemTraDTBMon(int maSV, string maHK, float diemTB)

        {
            using(dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                var query = from dsv in dt.DiemSVs
                            where dsv.MaSV == maSV && dsv.MaHK == maHK
                            select dsv.DiemTBMon;

                foreach (var row in query)
                    if (row <= diemTB)
                        return false;
                return true;
            }
        }
    }
}
