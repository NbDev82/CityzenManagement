using QuanLyCongDanThanhPho.DAO;
using QuanLyCongDanThanhPho.DTO;
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
        KhaiSinh ks;
        TaiKhoan tk;
        public fThongTinCaNhan(CongDan cd, KhaiSinh ks, TaiKhoan tk)
        {
            InitializeComponent();
            this.cd = cd;
            this.ks = ks;
            this.tk = tk;
        }
        void LoadThongTinCaNhan(CongDan cd, KhaiSinh ks)
        {
            txtMaCD.Text = cd.Macd.ToString();
            txtHoTen.Text = cd.Hoten;
            dtpkNgaySinh.Text = ks.NgaySinh;
            cbxNoiSinh.Text = ks.NoiSinh;
            cbxGioiTinh.Text = cd.Gioitinh;
            txtNgheNghiep.Text = cd.Nghenghiep;
            cbxDanToc.Text = cd.Dantoc;
            txtTonGiao.Text = cd.Tongiao;
            cbxHonNhan.Text = cd.Mahonnhan.ToString();
            cbxTinhTrang.Text = cd.Tinhtrang;
        }
        private void fThongTinCaNhan_Load(object sender, EventArgs e)
        {
            cbxNoiSinh.Items.AddRange(Program.tinhthanh.ToArray());
            cbxDanToc.Items.AddRange(Program.dantoc.ToArray());
            cd = CongDanDAO.Instance.GetCongDanByMaCD(cd.Macd);
            ks = KhaiSinhDAO.Instance.GetKhaiSinhByID(cd.Macd);
            LoadThongTinCaNhan(cd, ks);
            pnThongTinCaNhan.Enabled = false;
            pnQuanLy.Enabled = false;
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
                if(tk.Phanquyen == 1)
                    pnQuanLy.Enabled = true;
                else
                {
                    MessageBox.Show("Bạn không có quyền này");
                    rdoCongDan.Checked = true;
                }
            }
        }
        private void btTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                int Macd = int.Parse(txtTimKiem.Text);
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
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            int macd = int.Parse(txtMaCD.Text);
            string hoten = txtHoTen.Text;
            string gioitinh = cbxGioiTinh.Text;
            string nghenghiep = txtNgheNghiep.Text;
            string dantoc = cbxDanToc.Text;
            string tongiao = txtTonGiao.Text;
            CongDan cd = new CongDan(macd, hoten, gioitinh, nghenghiep, dantoc, tongiao, "", -1, -1);
            KhaiSinh ks = new KhaiSinh(macd, dtpkNgaySinh.Text, cbxNoiSinh.Text, -1, -1, "");
            if (CongDanDAO.Instance.Update(cd) && KhaiSinhDAO.Instance.UpdateNgaySinhNoiSinh(ks))
                MessageBox.Show("Cập nhật thành công");
            else
                MessageBox.Show("Cập nhật thất bại");
            btnConfirm.Enabled = false;
            pnThongTinCaNhan.Enabled = false;
        }
        private void dtgvThongTinCaNhan_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int Index = dtgvThongTinCaNhan.CurrentRow.Index;
            int Macd = (int)dtgvThongTinCaNhan.Rows[Index].Cells["Mã công dân"].Value;
            CongDan cd = CongDanDAO.Instance.GetCongDanByMaCD(Macd);
            KhaiSinh ks = KhaiSinhDAO.Instance.GetKhaiSinhByID(Macd);
            LoadThongTinCaNhan(cd,ks);
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            pnThongTinCaNhan.Enabled = !pnThongTinCaNhan.Enabled;
            if (pnThongTinCaNhan.Enabled)
                btnConfirm.Enabled = true;
            else
                btnConfirm.Enabled = false;
        }
    }
}
