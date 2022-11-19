using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public static class DAO_HocKi
    {
        public static List<DTO_HocKi> getHocKi()
        {
            List<DTO_HocKi> ds = new List<DTO_HocKi>();
            using(dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                var query = from hk in dt.HocKis
                            select hk;

                foreach(var row in query)
                {
                    DTO_HocKi hk = new DTO_HocKi(row.MaHK, row.TenHK);
                    ds.Add(hk);
                }
            }

            return ds;
        }

        public static List<string> getMaHK()
        {
            List<string> ds = new List<string>();
            using (dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                var query = from hk in dt.HocKis
                            select hk;

                foreach (var row in query)
                {
                    ds.Add(row.MaHK);
                }
            }

            return ds;
        }

        public static List<string> getTenHK()
        {
            List<string> ds = new List<string>();
            using (dataQLDSVDataContext dt = new dataQLDSVDataContext())
            {
                var query = from hk in dt.HocKis
                            select hk;

                foreach (var row in query)
                {
                    ds.Add(row.TenHK);
                }
            }

            return ds;
        }
    }
}
