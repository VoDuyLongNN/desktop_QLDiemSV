using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_DiemTBHocKi
    {
        public int maSV { get; set; }
        public string maHK { get; set; }
        public float diemTB { get; set; }

        public DTO_DiemTBHocKi(int masv, string mahk, float dtb)
        {
            this.maSV = masv;
            this.maHK = mahk;
            this.diemTB = dtb;
        }
    }
}
