using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_SinhVien
    {
        public int maSV { get; set; }
        public string hoTen { get; set; }
        public string gioiTinh { get; set; }
        public string lop { get; set; }
        public string queQuan { get; set; }

        public DTO_SinhVien(int masv, string hoten, string gioitinh, string lop, string quequan)
        {
            this.maSV = masv;
            this.hoTen = hoten;
            this.gioiTinh = gioitinh;
            this.lop = lop;
            this.queQuan = quequan;
        }
    }
}
