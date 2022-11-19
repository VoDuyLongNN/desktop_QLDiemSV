using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_DiemTBNam
    {
        public int maSV { get; set; }
        public float diemTB { get; set; }

        public DTO_DiemTBNam(int maSV, float dtb)
        {
            this.maSV = maSV;
            this.diemTB = dtb;
        }
    }
}
