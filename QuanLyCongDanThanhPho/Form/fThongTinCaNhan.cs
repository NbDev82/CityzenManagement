using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCongDanThanhPho
{
    public partial class fThongTinCaNhan : Form
    {
        CongDan cd;
        public fThongTinCaNhan(CongDan cd)
        {
            InitializeComponent();
            this.cd = cd;
        }
        void LoadThongTinCaNhan(CongDan cd)
        {
            txtMaCD.Text = cd.Macd;
            txtHoTen.Text = cd.Hoten;
            dtpkNgaySinh.Text = cd.Ngaysinh;
            cbxNoiSinh.Text = cd.Noisinh;
            cbxGioiTinh.Text = cd.Gioitinh;
            txtNgheNghiep.Text = cd.Nghenghiep;
            cbxDanToc.Text = cd.Dantoc;
            txtTonGiao.Text = cd.Tongiao;
            cbxHonNhan.Text = cd.Honnhan;
            cbxTinhTrang.Text = cd.Tinhtrang;
            cbxLoaiTaiKhoan.Text = cd.Loaitk;
        }
        void QuanLyLoad()
        {
            ResetThongTinRong();
            pnTimKiem.Enabled = true;
            dtgvThongTinCaNhan.Enabled = true;
            btnConfirm.Enabled = true;
            txtHoTen.Enabled = true;
            dtpkNgaySinh.Enabled = true;
            cbxNoiSinh.Enabled = true;
            cbxGioiTinh.Enabled = true;
            txtNgheNghiep.Enabled = true;
            cbxDanToc.Enabled = true;
            txtTonGiao.Enabled = true;
            cbxHonNhan.Enabled = true;
            cbxTinhTrang.Enabled = true;
            cbxLoaiTaiKhoan.Enabled = true;
        }
        void CongDanLoad()
        {
            txtMaCD.Enabled = false;
            txtHoTen.Enabled = false;
            dtpkNgaySinh.Enabled = false;
            cbxNoiSinh.Enabled = false;
            cbxGioiTinh.Enabled = false;
            txtNgheNghiep.Enabled = false;
            cbxDanToc.Enabled = false;
            txtTonGiao.Enabled = false;
            cbxHonNhan.Enabled = false;
            cbxTinhTrang.Enabled = false;
            cbxTinhTrang.Enabled = false;
            cbxLoaiTaiKhoan.Enabled = false;
            LoadThongTinCaNhan(cd);
            pnTimKiem.Enabled = false;
            dtgvThongTinCaNhan.Enabled = false;
            btnConfirm.Enabled = false;
        }
        void ResetThongTinRong()
        {
            txtMaCD.Text = "";
            txtHoTen.Text = "";
            dtpkNgaySinh.Value = DateTime.Today;
            cbxNoiSinh.Text = "";
            txtNgheNghiep.Text = "";
            cbxDanToc.Text = "";
            txtTonGiao.Text = "Không";
            cbxLoaiTaiKhoan.Text = "Công dân";
        }
        private void fThongTinCaNhan_Load(object sender, EventArgs e)
        {
            cbxNoiSinh.Items.AddRange(Program.tinhthanh.ToArray());
            cbxDanToc.Items.AddRange(Program.dantoc.ToArray());
            //CongDanLoad();
            LoadThongTinCaNhan(cd);
            pnThongTinCaNhan.Enabled = false;
            pnQuanLy.Enabled = false;
            //dtgvThongTinCaNhan.DataSource = CongDanDAO.Instance.LayDanhSach();
            if (cd.Loaitk == "Công dân")
            {
                pnPhanQuyen.Enabled = false;
            }  
        }

        private void tbDoiMatKhau_Click(object sender, EventArgs e)
        {

        }

        private void rbCongDan_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCongDan.Checked == true)
            {
                pnQuanLy.Enabled = false;
            }
        }

        private void rbQuanLy_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoQuanLy.Checked == true)
            {
                pnQuanLy.Enabled = true;
            }
        }

        private void btTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string Macd = txtTimKiem.Text;
                DataTable dt = CongDanDAO.Instance.LayDanhSach(Macd);
                dtgvThongTinCaNhan.DataSource= dt;
            }
            catch
            {
                MessageBox.Show("Không được để trống");
            }
        }

        private void btXem_Click(object sender, EventArgs e)
        {
            DataTable dt = CongDanDAO.Instance.LayDanhSach();
            dtgvThongTinCaNhan.DataSource = dt;
        }

        private void cbLoaiTaiKhoan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaCD.Text == "")
                    throw new Exception();
                string Macd = txtMaCD.Text;
                CongDanDAO.Instance.Xoa(Macd);
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn 1 công dân");
            }

        }
        private void btSua_Click(object sender, EventArgs e)
        {
            //if (!CongDanDAO.Instance.isExist(txtMaCD.Text))
            //{
            //    string macd = txtMaCD.Text;
            //    string hoten = txtHoTen.Text;
            //    string ngaysinh = dtpkNgaySinh.Text;
            //    string noisinh = cbxNoiSinh.Text;
            //    string gioitinh = cbxGioiTinh.Text;
            //    string nghenghiep = txtNgheNghiep.Text;
            //    string dantoc = cbxDanToc.Text;
            //    string tongiao = txtTonGiao.Text;
            //    string honnhan = cbxHonNhan.Text;
            //    string tinhtrang = cbxTinhTrang.Text;
            //    string loaitk = cbxLoaiTaiKhoan.Text;

            //    CongDan cd = new CongDan(macd, hoten, ngaysinh, noisinh, gioitinh, nghenghiep, dantoc, tongiao, honnhan, tinhtrang,"","",loaitk,"","","");
            //    if (CongDanDAO.Instance.Update(cd))
            //        MessageBox.Show("Cập nhật thành công");
            //    else
            //        MessageBox.Show("Cập nhật thất bại");
            //}
            //else
            //{
            //    MessageBox.Show("Mã công dân không đúng");
            //}
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            pnThongTinCaNhan.Enabled = !pnThongTinCaNhan.Enabled;
            btnConfirm.Enabled = true;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {

            string macd = txtMaCD.Text;
            string hoten = txtHoTen.Text;
            string ngaysinh = dtpkNgaySinh.Value.Date.ToString();
            string noisinh = cbxNoiSinh.Text;
            string gioitinh = cbxGioiTinh.Text;
            string nghenghiep = txtNgheNghiep.Text;
            string dantoc = cbxDanToc.Text;
            string tongiao = txtTonGiao.Text;
            CongDan cd = new CongDan(macd, hoten, ngaysinh, noisinh, gioitinh, nghenghiep, dantoc, tongiao, "", "", "", "", "", "", "", "");
            if (CongDanDAO.Instance.Update(cd))
                MessageBox.Show("Cập nhật thành công");
            else
                MessageBox.Show("Cập nhật thất bại");
            btnConfirm.Enabled = false;
        }

        private void dtgvThongTinCaNhan_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int Index = dtgvThongTinCaNhan.CurrentRow.Index;
            string Macd = dtgvThongTinCaNhan.Rows[Index].Cells["Mã công dân"].Value.ToString();
            CongDan cd = CongDanDAO.Instance.LayCongDanBangID(Macd);
            LoadThongTinCaNhan(cd);
        }
    }
}
