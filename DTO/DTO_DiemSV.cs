using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_DiemSV
    {
        public string maHK { get; set; }
        public int maSV { get; set; }
        public string maBM { get; set; }
        public string maGV { get; set; }
        public float diemCC { get; set; }
        public float diemGK { get; set; }
        public float diemCK { get; set; }

        public float diemTBM { get; set; }

        public DTO_DiemSV(int masv, string mahk, string mabm, string magv, float dcc, float dgk, float dck, float dtb)
        {
            this.maSV = masv;
            this.maHK = mahk;
            this.maBM = mabm;
            this.maGV = magv;
            this.diemCC = dcc;
            this.diemGK = dgk;
            this.diemCK = dck;
            this.diemTBM = dtb;
        }
    }
}
