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
    public partial class fHoKhau : Form
    {
        CongDan cd;
        TaiKhoan tk;
        public fHoKhau(CongDan user, TaiKhoan tk)
        {
            InitializeComponent();
            cd = user;
            this.tk = tk;
        }
        private void Fill(DataRow dtCD)
        {
            txtMaCD.Text = dtCD["MaCD"].ToString();
            txtHoTen.Text = dtCD["HoTen"].ToString();
            txtNgaySinh.Text = dtCD["NgaySinh"].ToString();
            txtNoiSinh.Text = dtCD["NoiSinh"].ToString();
            txtGioiTinh.Text = dtCD["GioiTinh"].ToString();
            txtNgheNghiep.Text = dtCD["NgheNghiep"].ToString();
            txtDanToc.Text = dtCD["DanToc"].ToString();
            txtTonGiao.Text = dtCD["TonGiao"].ToString();
            txtHonNhan.Text = dtCD["MaHN"].ToString();
            txtTinhTrang.Text = dtCD["TinhTrang"].ToString();
            txtQuanHe.Text = dtCD["QuanHeVoiChuHo"].ToString();
        }
        private void btChiTiet_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnThongTinHoKhau.Enabled)
                    throw new Exception();
                pnChiTietHoKhau.Enabled = true;
                LayDanhSachChiTietHoKhau();
                if (rdoQuanLy.Checked)
                    pnChinhSuaThongTinThanhVien.Enabled = true;
                else
                    pnChinhSuaThongTinThanhVien.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Hiện đang trong chế độ đăng ký hộ khẩu");
            }
        }
        public void HoKhauLoad()
        {
            int macd = cd.Macd;
            int MaHo = HoKhauDAO.Instance.LayMaHo(macd);
            List<Control> ltext = new List<Control>();
            ltext.Add(txtMaHo);
            ltext.Add(txtChuHo);
            ltext.Add(txtTinhThanh);
            ltext.Add(txtQuanHuyen);
            ltext.Add(txtPhuongXa);
            HoKhauDAO.Instance.Fill(MaHo, ltext);
            pnChucNang.Enabled = false;
            pnThongTinHoKhau.Enabled = false;
        }
        private void fHoKhau_Load(object sender, EventArgs e)
        {
            HoKhauLoad();
            pnThongTin.Enabled = false ;
        }
        public void LoadCongDan()
        {
            try
            {
                int index = dtgvChiTietHoKhau.CurrentRow.Index;
                DataTable CurrentRow = (DataTable)dtgvChiTietHoKhau.DataSource;
                if (CurrentRow != null)
                {
                    int maCd = (int)CurrentRow.Rows[index]["MaCD"];
                    DataTable dt = HoKhauDAO.Instance.LayThongTinThanhVien(maCd);
                    DataRow dtCD = dt.Rows[0];
                    if (dtCD == null)
                        throw new Exception();
                    Fill(dtCD);
                }
                else
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("Bị lỗi");
            }
        }
        private void dtgvChiTietHoKhau_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadCongDan();
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                int MaHo = int.Parse(txtTimKiem.Text);
                DataTable dt = HoKhauDAO.Instance.GetHoKhauByID(MaHo);
                if (dt != null)
                    dtgvDanhSachHoKhau.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("Không được để trống");
            }
        }
        private void dtgvDanhSachHoKhau_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dtgvDanhSachHoKhau.CurrentRow.Index;
            DataGridViewRow dt = dtgvDanhSachHoKhau.Rows[index];
            txtMaHo.Text = dt.Cells[0].Value.ToString();
            txtChuHo.Text = dt.Cells[1].Value.ToString();
            txtTinhThanh.Text = dt.Cells[2].Value.ToString();
            txtQuanHuyen.Text = dt.Cells[3].Value.ToString();
            txtPhuongXa.Text = dt.Cells[4].Value.ToString();
        }
        private void btnQuayVe_Click(object sender, EventArgs e)
        {
            HoKhauLoad();
            pnThongTinHoKhau.Enabled = false;
            rdoCongDan.Checked = true;
        }
        private void btnXem_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = HoKhauDAO.Instance.GetHoKhau();
                dtgvDanhSachHoKhau.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("ERROR");
            }
        }
        public void BatChinhSua()
        {
            btnChinhSua.BackColor = Color.Green;
            pnCapNhatHoKhau.Enabled=true;
        }
        public void TatChinhSua()
        {
            btnChinhSua.BackColor = Color.Red;
            pnCapNhatHoKhau.Enabled = false;
        }
        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            pnThongTin.Enabled = !pnThongTin.Enabled;
            if (pnThongTin.Enabled)
            {
                BatChinhSua();
            }
            else
            {
                TatChinhSua();
            }
        }
        private void btnTaoHoKhau_Click(object sender, EventArgs e)
        {
            pnThongTinHoKhau.Enabled = true;
        }
        private void rdoQuanLy_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoQuanLy.Checked)
            {
                if (tk.Phanquyen == 1)
                {
                    pnChucNang.Enabled = true;
                }
                else
                {
                    rdoCongDan.Checked = true;
                    pnChucNang.Enabled = false;
                    MessageBox.Show("Bạn không có quyền này");
                }
            }
        }
        private void btnGui_Click(object sender, EventArgs e)
        {
            try
            {
                if (!pnThongTinHoKhau.Enabled)
                {
                    MessageBox.Show("Không trong chế độ đăng ký hộ khẩu");
                    return;
                }
                int MaCD = cd.Macd;
                int maHo = HoKhauDAO.Instance.LayMaHo(MaCD);
                if (maHo > 0)
                {
                    MessageBox.Show("Công dân đã có hộ khẩu");
                    return;
                }
                CongDan chuHo = CongDanDAO.Instance.GetCongDanByMaCD(MaCD);
                string TinhThanh = txtTinhThanh.Text;
                string PhuongXa = txtPhuongXa.Text;
                string QuanHuyen = txtQuanHuyen.Text;
                HoKhau hoKhauMoi = new HoKhau(0, MaCD, TinhThanh, QuanHuyen, PhuongXa, 0, DateTime.Now.ToString());
                if (HoKhauDAO.Instance.AddToHoKhau(hoKhauMoi))
                    MessageBox.Show("Tạo hộ khẩu thành công");
                else
                    MessageBox.Show("Tạo hộ khẩu thất bại");
            }
            catch
            {
                MessageBox.Show("Không được để trống");
            }
        }
        private void btnLoadLaiCongDan_Click(object sender, EventArgs e)
        {
            LoadCongDan();
        }
        private void rdoCongDan_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCongDan.Checked)
            {
                TatChinhSua();
                pnChinhSuaThongTinThanhVien.Enabled = false;
                pnThongTin.Enabled = false;
                pnChucNang.Enabled = false;
            }
            else
            {
                pnChinhSuaThongTinThanhVien.Enabled = true;
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                int MaCD = int.Parse(txtMaCD.Text);
                int MaHo = int.Parse(txtMaHo.Text);
                if(txtQuanHe.Text == "")
                {
                    MessageBox.Show("Quan hệ không được để trống");
                    return;
                }
                if (!HoKhauDAO.Instance.AddToChiTietHoKhau(MaCD, MaHo, txtQuanHe.Text))
                    throw new Exception();
                MessageBox.Show("Thêm thành công");
                LayDanhSachChiTietHoKhau();
            }
            catch
            {
                MessageBox.Show("Công dân đã có hộ khẩu hoặc sai mã công dân");
            }
        }
        private void btnDien_Click(object sender, EventArgs e)
        {
            try
            {
                int Macd = int.Parse(txtMaCD.Text);
                CongDan cdChild = CongDanDAO.Instance.GetCongDanByMaCD(Macd);
                KhaiSinh khaiSinh = KhaiSinhDAO.Instance.GetKhaiSinhByID(Macd);
                txtHoTen.Text = cdChild.Hoten;
                txtNgaySinh.Text = khaiSinh.NgaySinh;
                txtNoiSinh.Text = khaiSinh.NoiSinh;
                txtGioiTinh.Text = cdChild.Gioitinh;
                txtNgheNghiep.Text = cdChild.Nghenghiep;
                txtDanToc.Text = cdChild.Dantoc;
                txtTonGiao.Text = cdChild.Tongiao;
                txtHonNhan.Text = cdChild.Mahonnhan.ToString();
                txtTinhTrang.Text = cdChild.Tinhtrang;
            }
            catch
            {
                MessageBox.Show("Mã công dân bị sai");
            }
        }
        public void LayDanhSachChiTietHoKhau()
        {
            int macd = int.Parse(txtChuHo.Text);
            int MaHo = HoKhauDAO.Instance.LayMaHo(macd);
            DataTable dt = HoKhauDAO.Instance.LayDanhSach(MaHo, "Detail_Households");
            dtgvChiTietHoKhau.DataSource = dt;
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int macd = int.Parse(txtMaCD.Text);
                if (IsExist(macd) && HoKhauDAO.Instance.Delete(macd))
                {
                    MessageBox.Show("Xóa thành công");
                    LayDanhSachChiTietHoKhau();
                }
                else
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("Xóa thất bại");
            }
            
        }
        public bool IsExist(int macd)
        {
            try
            {
                foreach (DataGridViewRow row in dtgvChiTietHoKhau.Rows)
                {
                    int Ma = (int)row.Cells["MaCD"].Value;
                    string quanHe = row.Cells["QuanHeVoiChuHo"].Value.ToString();
                    if (Ma == macd && quanHe != "Chủ hộ")
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
