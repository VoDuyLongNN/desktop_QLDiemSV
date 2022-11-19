using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_GiaoVien
    {
        public string maGV { get; set; }
        public string maBM { get; set; }
        public string hoTen { get; set; }
        public bool gioiTinh { get; set; }
        public string queQuan { get; set; }

        public DTO_GiaoVien(string magv, string mabm, string ht, bool gt, string qq)
        {
            this.maGV = magv;
            this.maBM = mabm;
            this.hoTen = ht;
            this.queQuan = qq;
            this.gioiTinh = gt;
        }
    }
}
