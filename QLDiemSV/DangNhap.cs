using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;

namespace QLDiemSV
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            if (txtTenDN.Text != "" && txtMK.Text != "")
            {
                if (BUS.BUS_TaiKhoang.kiemTraDN(txtTenDN.Text, txtMK.Text))
                {
                    QuanLy f = new QuanLy();
                    this.Hide();
                    f.ShowDialog();
                }
                else MessageBox.Show("Tên Đăng Nhập hoặc Mật Khẩu không đúng!");
            }
            else MessageBox.Show("Vui lòng nhập đủ thông tin!");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
