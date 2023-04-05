using QuanLyCongDanThanhPho.DAO;
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
    public partial class fHoKhau : System.Windows.Forms.Form
    {
        fNguoiDung nguoidung = new fNguoiDung();
        CongDan cd;
        public fHoKhau(CongDan user)
        {
            InitializeComponent();
            cd = user;
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
            txtHonNhan.Text = dtCD["HonNhan"].ToString();
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
            string macd = cd.Macd;
            int MaHo = HoKhauDAO.Instance.LayMaHo(macd);
            List<Control> ltext = new List<Control>();
            ltext.Add(txtMaHo);
            ltext.Add(txtChuHo);
            ltext.Add(txtTinhThanh);
            ltext.Add(txtQuanHuyen);
            ltext.Add(txtPhuongXa);
            if (HoKhauDAO.Instance.Fill(MaHo, ltext))
            {
                if (cd.Loaitk == "Công Dân")
                {
                    pnChucNang.Enabled = false;
                    pnThongTinHoKhau.Enabled = false;
                }
            }
            else
            {
                pnThongTinHoKhau.Enabled = true;
            }  
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
                    string maCd = (string)CurrentRow.Rows[index]["MaCD"];
                    
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
                DataTable dt = HoKhauDAO.Instance.LayDanhSachHoKhau(MaHo);
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
        }
        private void btnXem_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = HoKhauDAO.Instance.LayDanhSachHoKhau();
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
            if(cd.Loaitk == "Quản lý")
            {
                if (rdoQuanLy.Checked)
                {
                    pnChucNang.Enabled = true;
                }
                else
                {
                    pnChucNang.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Bạn không có quyền này");
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
                string MaCD = txtChuHo.Text;
                int maHo = HoKhauDAO.Instance.LayMaHo(MaCD);
                if (maHo > 0)
                {
                    MessageBox.Show("Công dân đã có hộ khẩu");
                    return;
                }
                CongDan chuHo = CongDanDAO.Instance.LayCongDanBangID(MaCD);
                string TinhThanh = txtTinhThanh.Text;
                string PhuongXa = txtPhuongXa.Text;
                string QuanHuyen = txtQuanHuyen.Text;

                HoKhau hoKhauMoi = new HoKhau("", MaCD, TinhThanh, QuanHuyen, PhuongXa, 0, DateTime.Now.ToString());
                HoKhauDAO.Instance.Add(hoKhauMoi);
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
                if (!HoKhauDAO.Instance.Add(txtMaCD.Text, txtMaHo.Text, txtQuanHe.Text))
                    throw new Exception();
                MessageBox.Show("Thêm thành công");
                LayDanhSachChiTietHoKhau();
            }
            catch
            {
                MessageBox.Show("Công dân đã tòn tại trong hộ khẩu hoặc sai mã công dân");
            }
        }
        private void btnDien_Click(object sender, EventArgs e)
        {
            try
            {
                string Macd = txtMaCD.Text;
                CongDan cdChild = CongDanDAO.Instance.LayCongDanBangID(txtMaCD.Text);
                txtHoTen.Text = cd.Hoten;
                txtNgaySinh.Text = cd.Ngaysinh;
                txtNoiSinh.Text = cd.Noisinh;
                txtGioiTinh.Text = cd.Gioitinh;
                txtNgheNghiep.Text = cd.Nghenghiep;
                txtDanToc.Text = cd.Dantoc;
                txtTonGiao.Text = cd.Tongiao;
                txtHonNhan.Text = cd.Honnhan;
                txtTinhTrang.Text = cd.Tinhtrang;
            }
            catch
            {
                MessageBox.Show("Mã công dân bị sai");
            }
        }
        public void LayDanhSachChiTietHoKhau()
        {
            int MaHo = int.Parse(txtMaHo.Text);
            DataTable dt = HoKhauDAO.Instance.LayDanhSach(MaHo, "ChiTietHoKhau");
            dtgvChiTietHoKhau.DataSource = dt;
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string macd = txtMaCD.Text;
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
        public bool IsExist(string macd)
        {
            try
            {
                foreach (DataGridViewRow row in dtgvChiTietHoKhau.Rows)
                {
                    string Ma = row.Cells["MaCD"].Value.ToString();
                    string quanHe = row.Cells["QuanHeVoiChuHo"].Value.ToString();
                    if (Ma == macd && quanHe != "Là chủ hộ")
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
