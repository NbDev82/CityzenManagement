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
        void LoadThongTinCaNhan()
        {
            tbMaCD.Text = cd.Macd;
            tbHoTen.Text = cd.Hoten;
            dtpkNgaySinh.Text = cd.Ngaysinh;
            cbNoiSinh.Text = cd.Noisinh;
            cbGioiTinh.Text = cd.Gioitinh;
            tbNgheNghiep.Text = cd.Nghenghiep;
            cbDanToc.Text = cd.Dantoc;
            tbTonGiao.Text = cd.Tongiao;
            cbHonNhan.Text = cd.Honnhan;
            cbTinhTrang.Text = cd.Tinhtrang;
            cbLoaiTaiKhoan.Text = cd.Loaitk;
        }
        void QuanLyLoad()
        {
            ResetThongTinRong();
            pnTimKiem.Enabled = true;
            dtgvThongTinCaNhan.Enabled = true;
            btThem.Enabled = true;
            btSua.Enabled = true;
            btXoa.Enabled = true;
            tbHoTen.Enabled = true;
            dtpkNgaySinh.Enabled = true;
            cbNoiSinh.Enabled = true;
            cbGioiTinh.Enabled = true;
            tbNgheNghiep.Enabled = true;
            cbDanToc.Enabled = true;
            tbTonGiao.Enabled = true;
            cbHonNhan.Enabled = true;
            cbTinhTrang.Enabled = true;
            cbLoaiTaiKhoan.Enabled = true;
        }
        void CongDanLoad()
        {
            tbMaCD.Enabled = false;
            tbHoTen.Enabled = false;
            dtpkNgaySinh.Enabled = false;
            cbNoiSinh.Enabled = false;
            cbGioiTinh.Enabled = false;
            tbNgheNghiep.Enabled = false;
            cbDanToc.Enabled = false;
            tbTonGiao.Enabled = false;
            cbHonNhan.Enabled = false;
            cbTinhTrang.Enabled = false;
            cbLoaiTaiKhoan.Enabled = false;
            LoadThongTinCaNhan();
            pnTimKiem.Enabled = false;
            dtgvThongTinCaNhan.Enabled = false;
            btThem.Enabled = false;
            btSua.Enabled = false;
            btXoa.Enabled = false;
        }
        void ResetThongTinRong()
        {
            tbMaCD.Text = "";
            tbHoTen.Text = "";
            dtpkNgaySinh.Value = DateTime.Today;
            cbNoiSinh.Text = "";
            tbNgheNghiep.Text = "";
            cbDanToc.Text = "";
            tbTonGiao.Text = "Không";
            cbLoaiTaiKhoan.Text = "Công dân";
        }
        private void fThongTinCaNhan_Load(object sender, EventArgs e)
        {
            cbNoiSinh.Items.AddRange(Program.tinhthanh.ToArray());
            cbDanToc.Items.AddRange(Program.dantoc.ToArray());
            //CongDanLoad();
            LoadThongTinCaNhan();
            pnQuanLy.Visible = true;
            dtgvThongTinCaNhan.DataSource = CongDanDAO.Instance.LayDanhSach();
            if (cd.Loaitk == "Công dân")
            {
                pnTimKiem.Visible = false;
                dtgvThongTinCaNhan.Visible = false;
                pnChucNang.Visible = false;

                pnThongTinCaNhan.Enabled = false;
                pnQuanLy.Visible = false;
                pnChucNang.Enabled = false;
            }  
        }

        private void tbDoiMatKhau_Click(object sender, EventArgs e)
        {

        }

        private void rbCongDan_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCongDan.Checked == true)
            {
                // CongDanLoad();
                pnQuanLy.Visible = false;
                pnThongTinCaNhan.Enabled = false;
            }
        }

        private void rbQuanLy_CheckedChanged(object sender, EventArgs e)
        {
            if (rbQuanLy.Checked == true)
            {
                pnQuanLy.Visible = true;
                pnThongTinCaNhan.Enabled = true;
                //QuanLyLoad();
            }
        }

        private void btTimKiem_Click(object sender, EventArgs e)
        {
            string Macd = tbTimKiem.Text;
            DataTable dt = CongDanDAO.Instance.LayDanhSach(Macd);
            dtgvThongTinCaNhan.DataSource= dt;
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
                if (tbMaCD.Text == "")
                    throw new Exception();
                string Macd = tbMaCD.Text;
                CongDanDAO.Instance.Xoa(Macd);
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn 1 công dân");
            }

        }
    }
}
