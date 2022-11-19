using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Linq;
using DTO;
using DAO;

namespace QLDiemSV
{
    public partial class QuanLy : Form
    {
        public QuanLy()
        {
            InitializeComponent();
        }

        public void loadSV()
        {
            dataLoadSV.DataSource = BUS.BUS_SinhVien.getDSSV();
            rbNam.Checked = true;
        }    

        private void tabSV_Click(object sender, EventArgs e)
        {
            loadSV();   
        }

        private void QuanLy_Load(object sender, EventArgs e)
        {
            loadSV();
            loadDiemSV();
            loadHocKi(cbQLDSVHocKi);
            loadHocKi(cbHBHocKi);
            loadBoMon();
            txtTB.Enabled = false;
            btnCB_ThongTinSV.Enabled = false;
            btnHB_ThongTinSV.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DangNhap f = new DangNhap();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        //Tab Quản Lý Sinh Viên
        private void dataLoadSV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataLoadSV.Rows.Count > 0)
            {
                int i = dataLoadSV.CurrentCell.RowIndex;

                txtMaSV.Text = dataLoadSV.Rows[i].Cells[0].Value.ToString();
                txtHoTen.Text = dataLoadSV.Rows[i].Cells[1].Value.ToString();
                if (dataLoadSV.Rows[i].Cells[2].Value.ToString() == "True")
                {
                    rbNam.Checked = true;
                    rbNu.Checked = false;
                }
                else
                {
                    rbNam.Checked = false;
                    rbNu.Checked = true;
                }
                txtLop.Text = dataLoadSV.Rows[i].Cells[3].Value.ToString();
                txtQueQuan.Text = dataLoadSV.Rows[i].Cells[4].Value.ToString();
                txtMaSV.Enabled = false;
            }
        }

        private void toolThem_Click_1(object sender, EventArgs e)
        {
            string gt = rbNam.Checked == true ? "True" : "False";
            if (txtMaSV.Text != "" && txtHoTen.Text != "" && txtLop.Text != "" && gt != "" && txtQueQuan.Text != "")
            {
                try
                {
                    if (BUS.BUS_SinhVien.KiemTraMSV(int.Parse(txtMaSV.Text)))
                    {
                        DTO_SinhVien sv = new DTO_SinhVien(int.Parse(txtMaSV.Text), txtHoTen.Text, gt, txtLop.Text, txtQueQuan.Text);
                        if (BUS.BUS_SinhVien.themSV(sv))
                        {
                            MessageBox.Show("Thêm thành công!");
                            loadSV();
                        }
                        else MessageBox.Show("Thêm thất bại!");
                    }
                    else MessageBox.Show("Bị trùng mã sinh viên");
                } catch { MessageBox.Show("Mã sinh viên k hợp lệ!"); }
            }
            else MessageBox.Show("Vui lòng nhập đủ thông tin!");
        }

        private void toolSua_Click(object sender, EventArgs e)
        {
            string gt = rbNam.Checked == true ? "True" : "False";
            if (txtMaSV.Text != "" && txtHoTen.Text != "" && txtLop.Text != "" && gt != "" && txtQueQuan.Text != "")
            {
                DTO_SinhVien SV = new DTO_SinhVien(int.Parse(txtMaSV.Text), txtHoTen.Text, gt, txtLop.Text, txtQueQuan.Text);
                if (BUS.BUS_SinhVien.suaSV(SV))
                {
                    loadSV();
                    MessageBox.Show("Sửa thành công!");
                }
                else MessageBox.Show("Sửa thất bại!");
            }
            else MessageBox.Show("Vui lòng nhập đủ thông tin!");
        }
        
        private void toolXoa_Click_1(object sender, EventArgs e)
        {
            if (txtMaSV.Text != "")
            {
                DialogResult dl = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo);
                if (dl == DialogResult.Yes)
                {
                    if (BUS.BUS_SinhVien.XoaSV(int.Parse(txtMaSV.Text)))
                    {
                        MessageBox.Show("Xóa thành công!");
                        loadSV();
                    }
                    else
                        MessageBox.Show("Xóa thất bại");
                }
            }
            else MessageBox.Show("Chọn sinh viên cần xóa");
        }

        private void toolLamMoi_Click_1(object sender, EventArgs e)
        {
            txtMaSV.Clear();
            txtMaSV.Enabled = true;
            rbNam.Checked = false;
            rbNu.Checked = false;
            txtMaSV.Enabled = true;
            txtHoTen.Clear();
            txtLop.Clear();
            txtQueQuan.Clear();
            loadSV();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (txtTim.Text != "")
                dataLoadSV.DataSource = BUS.BUS_SinhVien.timSV(txtTim.Text);
            else MessageBox.Show("Nhập tên sinh viên cần tìm!");
        }

        //Tab Quản Lý Điểm Sinh Viên
        public void loadHocKi(ComboBox cb)
        {
            DTO.dataQLDSVDataContext dt = new dataQLDSVDataContext();
            var query = from hk in dt.GetTable<HocKi>()
                        select hk;
            cb.DataSource = query;
            cb.DisplayMember = "TenHK";
            cb.ValueMember = "MaHK";
        }

        public void loadBoMon()
        {
            DTO.dataQLDSVDataContext dt = new DTO.dataQLDSVDataContext();
            var query = from bm in dt.GetTable<BoMon>()
                        select bm;

            cbQLDSVBoMon.DataSource = query;
            cbQLDSVBoMon.DisplayMember = "TenBM";
            cbQLDSVBoMon.ValueMember = "MaBM";
        }

        //Đổ dữ liệu lên cbQLDSVGiaoVien khi chọn môn
        private void cbQLDSVBoMon_SelectedValueChanged(object sender, EventArgs e)
        {
            DTO.dataQLDSVDataContext dt = new DTO.dataQLDSVDataContext();
            var query = from gv in dt.GetTable<GiaoVien>()
                        where gv.MaBM == cbQLDSVBoMon.SelectedValue.ToString()
                        select gv;

            cbQLDSVGiaoVien.DataSource = query;
            cbQLDSVGiaoVien.DisplayMember = "HoTen";
            cbQLDSVGiaoVien.ValueMember = "MaGV";
        }

        public void loadDiemSV()
        {
            dataDiemSV.DataSource = BUS.BUS_DiemSV.getDSDiemSV();
        }


        private void tabDiemSV_Click(object sender, EventArgs e)
        {
            loadDiemSV();
            loadHocKi(cbQLDSVHocKi);
            loadBoMon();
            txtTB.Enabled = false;
        }

        private void dataDiemSV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataDiemSV.RowCount > 0)
            {
                int i = dataDiemSV.CurrentCell.RowIndex;
                try
                {
                    cbQLDSVHocKi.SelectedValue = dataDiemSV.Rows[i].Cells[0].Value.ToString();
                    txtQLDMaSV.Text = dataDiemSV.Rows[i].Cells[1].Value.ToString();
                    cbQLDSVBoMon.SelectedValue = dataDiemSV.Rows[i].Cells[2].Value.ToString();
                    txtCC.Text = dataDiemSV.Rows[i].Cells[4].Value.ToString();
                    txtGK.Text = dataDiemSV.Rows[i].Cells[5].Value.ToString();
                    txtCK.Text = dataDiemSV.Rows[i].Cells[6].Value.ToString();
                    txtTB.Text = dataDiemSV.Rows[i].Cells[7].Value.ToString();
                }
                catch { txtQLDMaSV.Text = dataDiemSV.Rows[i].Cells[0].Value.ToString(); }
                txtQLDMaSV.Enabled = false;
            }
        }

        private void btnQLDSVTim_Click(object sender, EventArgs e)
        {
            if (txtQLDSVTim.Text != "")
            {
                try
                {
                    //Tìm thông tin điểm trung bình học kì sinh viên theo mã SV
                    if (txtQLDSVInfoSave.Text == "DTBHK")
                        dataDiemSV.DataSource = BUS.BUS_DiemTBHocKi.timDTBHK(int.Parse(txtQLDSVTim.Text));
                    //Tìm thông tin điểm trung bình năm sinh viên theo mã SV
                    else if (txtQLDSVInfoSave.Text == "DTBN")
                        dataDiemSV.DataSource = BUS.BUS_DiemTBNam.timDTBNam(int.Parse(txtQLDSVTim.Text));
                    //Tìm thông tin điểm sinh viên theo mã SV
                    else
                        dataDiemSV.DataSource = BUS.BUS_DiemSV.timSV(int.Parse(txtQLDSVTim.Text));
                }
                catch { MessageBox.Show("Nhập điểm SV sai định dạng!"); }
            }
            else MessageBox.Show("Nhập mã sinh viên cần tìm!");
        }

        private void toolQLDSVThem_Click(object sender, EventArgs e)
        {
            //Kiểm tra đã nhập đủ thông tin trước khi thêm hay chưa
            if (txtCC.Text != "" && txtGK.Text != "" && txtCK.Text != "" && txtQLDMaSV.Text != "")
            {
                //Kiểm tra kiểm tra mã sinh viên nhập vào là kiếu số
                try
                {
                    //Kiểm tra mã sinh viên có tồn tại không
                    if (BUS.BUS_SinhVien.KiemTraMSV(int.Parse(txtQLDMaSV.Text)) == false)
                    {
                        //Kiểm tra điểm sinh viên nhập vào có phải kiểu số không
                        try
                        {
                            //Kiểm tra điểm nhập vào có nằm trong khoảng 0 - 10 hay không
                            if (BUS.BUS_DiemSV.kiemTraNhapDiem(float.Parse(txtCC.Text), float.Parse(txtGK.Text), float.Parse(txtCK.Text)))
                            {
                                //Kiểm tra xem sinh viên đã thi môn này trong học kì hay chưa
                                if (!BUS.BUS_DiemSV.kiemTraMaBM(int.Parse(txtQLDMaSV.Text), cbQLDSVHocKi.SelectedValue.ToString(), cbQLDSVBoMon.SelectedValue.ToString()))
                                {
                                    DTO_DiemSV dsv = new DTO_DiemSV(int.Parse(txtQLDMaSV.Text), cbQLDSVHocKi.SelectedValue.ToString(),
                                                                    cbQLDSVBoMon.SelectedValue.ToString(), cbQLDSVGiaoVien.SelectedValue.ToString(),
                                                                    float.Parse(txtCC.Text), float.Parse(txtGK.Text), float.Parse(txtCK.Text), 0);
                                    if (BUS.BUS_DiemSV.themDiemSV(dsv))
                                    {
                                        MessageBox.Show("Thêm thành công");
                                        loadDiemSV();
                                    }
                                    else MessageBox.Show("Thêm thất bại");
                                }
                                else MessageBox.Show("Sinh viên " + txtQLDMaSV.Text + " đã thi môn " + cbQLDSVBoMon.Text + " trong học kì " + cbQLDSVHocKi.Text);
                            }
                            else MessageBox.Show("Nhập điểm trong khoảng 0 - 10!");
                        }
                        catch { MessageBox.Show("Nhập điểm kiểu số!"); }
                    }
                    else MessageBox.Show("Mã sinh viên không tồn tại!");
                }
                catch { MessageBox.Show("Nhập mã sinh viên là kiểu số!"); }
            }
            else MessageBox.Show("Vui lòng điền đủ thông tin!");
        }

        private void toolQLDSVXoa_Click(object sender, EventArgs e)
        {
            if (txtQLDMaSV.Text != "")
            {
                DialogResult dialog = MessageBox.Show("Bạn chắc chán muốn xóa?", "Thông báo", MessageBoxButtons.YesNo);
                if(dialog == DialogResult.Yes)
                {
                    if (BUS.BUS_DiemSV.xoaDiemSV(int.Parse(txtQLDMaSV.Text), cbQLDSVHocKi.SelectedValue.ToString(), cbQLDSVBoMon.SelectedValue.ToString()))
                    {
                        MessageBox.Show("Đã xóa!");
                        loadDiemSV();
                    }
                    else MessageBox.Show("Xóa thất bại!");
                }    
            }
            else MessageBox.Show("Chọn sinh viên cần xóa");
        }

        private void toolQLDSVSua_Click(object sender, EventArgs e)
        {
            //Kiểm tra đã nhập đủ thông tin trước khi sửa hay chưa
            if (txtQLDMaSV.Text != "" && txtCC.Text != "" && txtGK.Text != "" && txtCK.Text != "" && cbQLDSVHocKi.Text != "" && cbQLDSVGiaoVien.Text != "" && cbQLDSVBoMon.Text != "")
            {
                //Kiểm tra mã sinh viên có tồn tại không
                if (!BUS.BUS_SinhVien.KiemTraMSV(int.Parse(txtQLDMaSV.Text)))
                {
                    try
                    {
                        //Kiểm tra điểm nhập vào có nằm trong khoảng 0 - 10 hay không
                        if (BUS.BUS_DiemSV.kiemTraNhapDiem(float.Parse(txtCC.Text), float.Parse(txtGK.Text), float.Parse(txtCK.Text)))
                        {

                            DTO_DiemSV DSV = new DTO_DiemSV(int.Parse(txtQLDMaSV.Text), cbQLDSVHocKi.SelectedValue.ToString(), cbQLDSVBoMon.SelectedValue.ToString(),
                                                cbQLDSVGiaoVien.SelectedValue.ToString(), float.Parse(txtCC.Text), float.Parse(txtGK.Text), float.Parse(txtCK.Text), 0);
                            if (BUS.BUS_DiemSV.suaDiemSV(DSV))
                            {
                                MessageBox.Show("Sửa thành công!");
                                loadDiemSV();
                            }
                            else MessageBox.Show("Sửa thất bại!");
                        }
                        else MessageBox.Show("Nhập điểm trong khoảng 0 - 10");
                    }
                    catch { MessageBox.Show("Nhập điểm là kiểu số!"); }
                }
                else MessageBox.Show("Không tồn tại mã sinh viên: " + txtQLDMaSV.Text);
            }
            else MessageBox.Show("Vui lòng nhập đủ thông tin!");
        }

        private void toolQLDSVLamMoi_Click(object sender, EventArgs e)
        {
            txtQLDMaSV.Clear();
            txtCC.Clear();
            txtCK.Clear();
            txtGK.Clear();
            txtTB.Clear();
            txtQLDSVInfoSave.Clear();
            loadDiemSV();
            loadBoMon();
            loadHocKi(cbQLDSVHocKi);
            txtTB.Enabled = false;
            txtQLDMaSV.Enabled = true;
        }

        private void btnQLDSV_DTBHK_Click(object sender, EventArgs e)
        {
            dataDiemSV.DataSource = BUS.BUS_DiemTBHocKi.getDiemTBHocKi();
            txtQLDSVInfoSave.Text = "DTBHK";
        }

        private void btnQLDSV_THKTBN_Click(object sender, EventArgs e)
        {
            dataDiemSV.DataSource = BUS.BUS_DiemTBNam.getDTBNam();
            txtQLDSVInfoSave.Text = "DTBN";
        }

        //Tab Học Bổng
        private void btnHB_Xem_Click_1(object sender, EventArgs e)
        {
            btnHB_ThongTinSV.Enabled = false;
            if (txtHB_SoLuongToiDa.Text != "" && txtHB_DiemThiToiDa.Text != "" && txtHB_DiemTBToiDa.Text != "")
            {
                try
                {
                    List<DTO_DiemTBHocKi> ds = BUS.BUS_DiemTBHocKi.getDSHocBong(cbHBHocKi.SelectedValue.ToString(), float.Parse(txtHB_DiemThiToiDa.Text), float.Parse(txtHB_DiemTBToiDa.Text), int.Parse(txtHB_SoLuongToiDa.Text));
                    dataCN.DataSource = ds;
                    int sl = ds.Count;
                    lHB_SoLuongThuc.Text = sl + "/" + txtHB_SoLuongToiDa.Text;
                }
                catch { MessageBox.Show("Không thực hiện được! Kiểm tra lại thông tin!"); }
            }
            else MessageBox.Show("Nhập đủ thông tin khi tìm kiếm!");
        }




        private void btnHB_XX_Click(object sender, EventArgs e)
        {
            btnHB_ThongTinSV.Enabled = false;
            if (txtHB_SoLuongToiDa.Text != "" && txtHB_DiemThiToiDa.Text != "" && txtHB_DiemTBToiDa.Text != "")
            {
                float dtb = float.Parse(txtHB_DiemTBToiDa.Text);
                try
                {
                    List<DTO_DiemTBHocKi> ds = BUS.BUS_DiemTBHocKi.lietKeLoaiHB(cbHBHocKi.SelectedValue.ToString(), float.Parse(txtHB_DiemThiToiDa.Text), 
                                                                                float.Parse(txtHB_DiemTBToiDa.Text), int.Parse(txtHB_SoLuongToiDa.Text), dtb+2, 11);
                    dataCN.DataSource = ds;
                    int sl = ds.Count();
                    lHB_SoLuongThuc.Text = sl + "/" + txtHB_SoLuongToiDa.Text;
                }
                catch { MessageBox.Show("Không thực hiện được! Kiểm tra lại thông tin!"); }
            }
            else MessageBox.Show("Nhập đủ thông tin khi tìm kiếm!");
        }

        private void btnHB_Gioi_Click(object sender, EventArgs e)
        {
            btnHB_ThongTinSV.Enabled = false;
            if (txtHB_SoLuongToiDa.Text != "" && txtHB_DiemThiToiDa.Text != "" && txtHB_DiemTBToiDa.Text != "")
            {
                float dtb = float.Parse(txtHB_DiemTBToiDa.Text);
                try
                {
                    List<DTO_DiemTBHocKi> ds = BUS.BUS_DiemTBHocKi.lietKeLoaiHB(cbHBHocKi.SelectedValue.ToString(), float.Parse(txtHB_DiemThiToiDa.Text), 
                                                                                float.Parse(txtHB_DiemTBToiDa.Text), int.Parse(txtHB_SoLuongToiDa.Text), dtb+1, dtb+2);
                    dataCN.DataSource = ds;
                    int sl = ds.Count();
                    lHB_SoLuongThuc.Text = sl + "/" + txtHB_SoLuongToiDa.Text;
                }
                catch { MessageBox.Show("Không thực hiện được! Kiểm tra lại thông tin!"); }
            }
            else MessageBox.Show("Nhập đủ thông tin khi tìm kiếm!");
        }

        private void btnHB_Kha_Click(object sender, EventArgs e)
        {
            btnHB_ThongTinSV.Enabled = false;
            if (txtHB_SoLuongToiDa.Text != "" && txtHB_DiemThiToiDa.Text != "" && txtHB_DiemTBToiDa.Text != "")
            {
                float dtb = float.Parse(txtHB_DiemTBToiDa.Text);
                try
                {
                    List<DTO_DiemTBHocKi> ds = BUS.BUS_DiemTBHocKi.lietKeLoaiHB(cbHBHocKi.SelectedValue.ToString(), float.Parse(txtHB_DiemThiToiDa.Text), 
                                                                                float.Parse(txtHB_DiemTBToiDa.Text), int.Parse(txtHB_SoLuongToiDa.Text), dtb, dtb+1);
                    dataCN.DataSource = ds;
                    int sl = ds.Count();
                    lHB_SoLuongThuc.Text = sl + "/" + txtHB_SoLuongToiDa.Text;
                }
                catch { MessageBox.Show("Không thực hiện được! Kiểm tra lại thông tin!"); }
            }
            else MessageBox.Show("Nhập đủ thông tin khi tìm kiếm!");
        }

        private void dataCN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataCN.Rows.Count > 0)
            {
                int i = dataCN.CurrentCell.RowIndex;
                btnHB_ThongTinSV.Enabled = true;
                txtHB_MaSV.Text = dataCN.Rows[i].Cells[0].Value.ToString();
            }
        }

        private void btnHB_ThongTinSV_Click(object sender, EventArgs e)
        {
            List<DTO_SinhVien> list = BUS.BUS_SinhVien.getDSSVTheoMaSV(int.Parse(txtHB_MaSV.Text));
            dataCN.DataSource = list;
        }


        //tab sinh viên thôi học
        private void btnCB_XemSVThoiHoc_Click(object sender, EventArgs e)
        {
            if (txtCB_SoDTB.Text != "" && txtCB_SoHK.Text != "")
            {
                try
                {
                    List<DTO_DiemTBHocKi> list = BUS.BUS_DiemTBHocKi.dsSVThoiHoc(float.Parse(txtCB_SoDTB.Text), int.Parse(txtCB_SoHK.Text));
                    dataCanhBao.DataSource = list;
                }
                catch { MessageBox.Show("Thao tác không thể thực hiện, vui lòng kiểm tra thông tin nhập vào!"); }
            }
            else MessageBox.Show("Nhập đủ thông tin!");
        }

        private void btnCB_XemSVCanhBao_Click(object sender, EventArgs e)
        {
            if (txtCB_SoDTB.Text != "" && txtCB_SoHK.Text != "")
            {
                try
                {
                    List<DTO_DiemTBHocKi> list = BUS.BUS_DiemTBHocKi.dsSVThoiHoc(float.Parse(txtCB_SoDTB.Text), int.Parse(txtCB_SoHK.Text) - 1);
                    dataCanhBao.DataSource = list;
                }
                catch { MessageBox.Show("Thao tác không thể thực hiện, vui lòng kiểm tra thông tin nhập vào!"); }
            }
            else MessageBox.Show("Nhập đủ thông tin!");
        }

        private void btnCB_ThongTinSV_Click(object sender, EventArgs e)
        {
            List<DTO_SinhVien> list = BUS.BUS_SinhVien.getDSSVTheoMaSV(int.Parse(txtCB_MaSV.Text));
            dataCanhBao.DataSource = list;
        }

        private void dataCanhBao_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataCanhBao.RowCount > 0)
            {
                int i = dataCanhBao.CurrentCell.RowIndex;
                btnCB_ThongTinSV.Enabled = true;
                txtCB_MaSV.Text = dataCanhBao.Rows[i].Cells[0].Value.ToString();
            }
        }


    }
}
