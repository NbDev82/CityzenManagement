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
    public partial class fHonNhan : Form
    {
        HonNhan hn;
        CongDan cd;
        TaiKhoan tk;
        public readonly string XACNHANLAN1 = "XacNhanLan1";
        public readonly string XACNHANLAN2 = "XacNhanLan2";
        public fHonNhan(CongDan cd, TaiKhoan tk)
        {
            InitializeComponent();
            this.cd = cd;
            this.tk = tk;
        }
        private void fHonNhan_Load(object sender, EventArgs e)
        {
            HonNhanDAO.Instance.LoadFormPersonal(dtgvHonNhan, cd);
        }
        public void FillA(int Macd)
        {
            DataTable dt = CongDanDAO.Instance.LayDanhSach(Macd);
            if(dt != null)
            {
                cbMaCDNam.Text = dt.Rows[0]["MaCD"].ToString();
                tbHoTenNam.Text = dt.Rows[0]["HoTen"].ToString();
                dtpkNgaySinhNam.Text = dt.Rows[0]["NgaySinh"].ToString();
                tbNoiSinhNam.Text = dt.Rows[0]["NoiSinh"].ToString();
                tbGioiTinhNam.Text = dt.Rows[0]["GioiTinh"].ToString();
                tbNgheNghiepNam.Text = dt.Rows[0]["NgheNghiep"].ToString();
                tbDanTocNam.Text = dt.Rows[0]["DanToc"].ToString();
                tbTonGiaoNam.Text = dt.Rows[0]["TonGiao"].ToString();
                tbQuanHeNam.Text = tbGioiTinhNam.Text == "Nam" ? "Chồng" : "Vợ";
                btnDangKy.Enabled = true;
            }
        }
        public void FillB(int Macd)
        {
            DataTable dt = CongDanDAO.Instance.LayDanhSach(Macd);
            if (dt != null)
            {
                cbMaCDNu.Text = dt.Rows[0]["MaCD"].ToString();
                tbHoTenNu.Text = dt.Rows[0]["HoTen"].ToString();
                dtpkNgaySinhNu.Text = dt.Rows[0]["NgaySinh"].ToString();
                tbNoiSinhNu.Text = dt.Rows[0]["NoiSinh"].ToString();
                tbGioiTinhNu.Text = dt.Rows[0]["GioiTinh"].ToString();
                tbNgheNghiepNu.Text = dt.Rows[0]["NgheNghiep"].ToString();
                tbDanTocNu.Text = dt.Rows[0]["DanToc"].ToString();
                tbTonGiaoNu.Text = dt.Rows[0]["TonGiao"].ToString();
                tbQuanHeNu.Text = tbGioiTinhNu.Text == "Nam" ? "Chồng" : "Vợ";
                btnDangKy.Enabled = true;
            }
        }
        public void WireEvents(Control Controls)
        {
            foreach (Control control in Controls.Controls)
            {
                if (control is Panel Panel)
                {
                    WireEvents(Panel);
                }
                if (control is TextBox textBox)
                {
                    textBox.Text = null;
                }
                else if (control is ComboBox comboBox)
                {
                    comboBox.Text = null;
                }
                else if (control is DateTimePicker dateTimePicker)
                {
                    dateTimePicker.Value = DateTime.Now;
                }
            }
        }
        private void btnDatLai_Click(object sender, EventArgs e)
        {
            WireEvents(this);
            hn = null;
            btnHuy.Enabled = false;
            btnXacNhan.Enabled = false;
            cbMaCDNu.Enabled = true;
            cbMaCDNam.Enabled = true;
            cbLoai.Enabled = true;
        }
        public void ExecuteKetHon()
        {
            try
            {
                if (cbMaCDNam.Text == "" || cbMaCDNu.Text == "")
                    throw new Exception();
                int MaCDNam = int.Parse(cbMaCDNam.Text);
                int MaCDNu = int.Parse(cbMaCDNu.Text);
                string loai = cbLoai.Text;
                if (!HonNhanDAO.Instance.isExist(MaCDNam, MaCDNu, loai))
                {
                    ExecuteSendRequest("Kết hôn");
                }
                else
                {
                    MessageBox.Show("Đã tồn tại đơn hôn nhân");
                }
            }
            catch
            {
                MessageBox.Show("Thông tin không hợp lệ!");
            }
        }
        public void ExecuteLyHon()
        {
            try
            {
                if (cbMaCDNam.Text == "" || cbMaCDNu.Text == "")
                    throw new Exception();
                int MaCDNam;
                int MaCDNu;
                if (tbGioiTinhNam.Text == "Nam")
                {
                    MaCDNam = int.Parse(cbMaCDNam.Text);
                    MaCDNu = int.Parse(cbMaCDNu.Text);
                }
                else
                {
                    MaCDNam = int.Parse(cbMaCDNu.Text);
                    MaCDNu = int.Parse(cbMaCDNam.Text);
                }
                string loai = cbLoai.Text;
                if (HonNhanDAO.Instance.isExist(MaCDNam, MaCDNu, loai))
                {
                    HonNhan hn = HonNhanDAO.Instance.ReadByID(MaCDNam, MaCDNu);
                    if (hn == null)
                        throw new Exception();
                    if (hn.Loai != "Ly hôn")
                        ExecuteSendRequest("Ly hôn");
                    else
                        MessageBox.Show("Đơn ly hôn đã tồn tại");
                }
                else
                {
                    MessageBox.Show("2 người không có mối quan hệ hôn nhân với nhau");
                }
            }
            catch
            {
                MessageBox.Show("Thông tin không hợp lệ!");
            }
        }
        public void ExecuteSendRequest(string chedo)
        {
            int maCDChong = int.Parse(cbMaCDNam.Text);
            int maCDVo = int.Parse(cbMaCDNu.Text);
            string ngay = dtpkNgayDangKy.Value.Date.ToString();
            HonNhan honnhan = new HonNhan(-1, maCDChong, maCDVo, cbLoai.Text, ngay, -1, -1, "Chưa Duyệt");
            if (chedo == "Ly hôn")
            {
                honnhan = HonNhanDAO.Instance.ReadByID(maCDChong, maCDVo);
                honnhan = new HonNhan(honnhan.Mahn, maCDChong, maCDVo, cbLoai.Text, ngay, -1, -1, "Chưa Duyệt");
            }
            HonNhanDAO.Instance.SendForm(honnhan);
            HonNhanDAO.Instance.LoadFormPersonal(dtgvHonNhan, cd);
        }
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (cbLoai.Text == "Kết hôn")
            {
                ExecuteKetHon();
            } else if(cbLoai.Text =="Ly hôn")
            {
                ExecuteLyHon();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn loại đăng ký");
            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbMaCDNam.Text == "" || cbMaCDNu.Text == "")
                    throw new Exception();
                if (!HonNhanDAO.Instance.CheckXacNhan(cd.Macd))
                {
                    HonNhanDAO.Instance.Delete(hn);
                    MessageBox.Show("Hủy thành công");
                    HonNhanDAO.Instance.LoadFormPersonal(dtgvHonNhan, cd);
                }
                else
                {
                    MessageBox.Show("Đã xác nhận, không được hủy");
                }
            }
            catch
            {
                MessageBox.Show("Hủy thất bại");
            }
        }
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                if(!HonNhanDAO.Instance.CheckXacNhan(cd.Macd))
                {
                    if (tbXacNhanNam.Text != "" || tbXacNhanNu.Text != "")
                    {
                        HonNhanDAO.Instance.Update(hn, cd.Macd, XACNHANLAN2);
                    }
                    else
                    {
                        HonNhanDAO.Instance.Update(hn, cd.Macd, XACNHANLAN1);
                    }
                    int MacdNam = int.Parse(cbMaCDNam.Text);
                    int MacdNu = int.Parse(cbMaCDNu.Text);
                    hn = HonNhanDAO.Instance.ReadByID(MacdNam, MacdNu);
                    HonNhanDAO.Instance.LoadFormPersonal(dtgvHonNhan, cd);
                    if (cd.Macd == hn.Xacnhanlan1 || cd.Macd == hn.Xacnhanlan2)
                    {
                        tbXacNhanNam.Text = cd.Macd.ToString();
                    }
                    else
                    {
                        tbXacNhanNu.Text = cd.Macd.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Đã xác nhận từ trước");
                }
                
            }
            catch
            {
                MessageBox.Show("EROR");
            }
        }
        private void rdbtnCongDan_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtnCongDan.Checked)
            {
                pnQuanLy.Visible = false;
                pnTimKiem.Enabled = false;
                HonNhanDAO.Instance.LoadFormPersonal(dtgvHonNhan, cd);
            }
        }
        private void rdobtnQuanLy_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdbtnQuanLy.Checked)
                {
                    if (tk.Phanquyen == 1)
                    {
                        pnQuanLy.Visible = true;
                        pnTimKiem.Enabled = true;
                        HonNhanDAO.Instance.LoadMailControler(dtgvHonNhan);
                    }
                    else
                    {
                        MessageBox.Show("Bạn không có quyền này");
                        rdbtnCongDan.Checked = true;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Không hợp lệ");
            }
        }
        private void btnFill_Click(object sender, EventArgs e)
        {
            try
            {
                int MaCDNam = int.Parse(cbMaCDNam.Text);
                FillA(MaCDNam);
            }
            catch
            {
                MessageBox.Show("Thông tin không hợp lệ");
            }
            try
            {
                int MaCDNu = int.Parse(cbMaCDNu.Text);
                FillB(MaCDNu);
            }
            catch
            {
                MessageBox.Show("Thông tin không hợp lệ");
            }
        }
        private void dtgvHonNhan_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = dtgvHonNhan.CurrentRow.Index;
                DataTable dt = (DataTable)dtgvHonNhan.DataSource;
                DataRow CurrentRow = dt.Rows[index];
                if (CurrentRow != null)
                {
                    int maCdChong = (int)CurrentRow["MaCDChong"];
                    int maCdVo = (int)CurrentRow["MaCDVo"];
                    FillA(maCdChong);
                    FillB(maCdVo);
                    hn = HonNhanDAO.Instance.ReadByID(maCdChong, maCdVo);
                    if (maCdChong == hn.Xacnhanlan1 || maCdChong == hn.Xacnhanlan2)
                    {
                        tbXacNhanNam.Text = cd.Macd.ToString();
                    }
                    if (maCdVo == hn.Xacnhanlan1 || maCdVo == hn.Xacnhanlan2)
                    {
                        tbXacNhanNu.Text = cd.Macd.ToString();
                    }
                    cbLoai.Text = hn.Loai;
                    tbCode.Text = hn.Mahn.ToString();
                    tbTrangThai.Text = hn.Trangthai;
                    dtpkNgayDangKy.Text = hn.Ngaydangky;
                    btnXacNhan.Enabled = true;
                    btnHuy.Enabled = true;
                    cbMaCDNam.Enabled = false;
                    cbMaCDNu.Enabled = false;
                }
                else
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn 1 thư để xem nội dung");
            }
        }
        private void btTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                int mahn = int.Parse(tbTimKiem.Text);
                HonNhanDAO.Instance.TimKiemHonNhan(dtgvHonNhan,mahn);
            }
            catch
            {
                MessageBox.Show("Không hợp lệ");
            }
        }
        private void btXem_Click(object sender, EventArgs e)
        {
            HonNhanDAO.Instance.LoadMailControler(dtgvHonNhan);
            //HonNhanDAO.Instance.LoadFormPersonal(dtgvHonNhan,cd);
        }
        private void btnDuyet_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbNoiDung.Text == "")
                {
                    MessageBox.Show("Nội dung không được để trống");
                    return;
                }
                string TieuDe = "Đồng ý kết hôn";
                if (hn.Loai == "Ly hôn")
                {
                    if (HonNhanDAO.Instance.Delete(hn))
                    {
                        MessageBox.Show("Ly hôn thành công");
                        TieuDe = "Đồng ý ly hôn";
                    }
                    else
                        MessageBox.Show("Ly hôn thất bại");
                    return;
                } else if(hn.Loai == "Kết hôn")
                {
                    hn.Trangthai = "Đã Duyệt";
                    HonNhanDAO.Instance.CapNhatTrangThaiHonNhan(hn);
                    HonNhanDAO.Instance.LoadMailControler(dtgvHonNhan);
                    tbTrangThai.Text = hn.Trangthai;
                }
                string ngay = DateTime.Now.Date.ToString();
                int TKNguoiGui = cd.Macd;
                int TKNguoiChong = int.Parse(cbMaCDNam.Text);
                int TKNguoiVo = int.Parse(cbMaCDNu.Text);
                string mess = tbNoiDung.Text;
                Mail mailToiChong = new Mail(TieuDe, ngay, TKNguoiGui, TKNguoiChong, mess);
                Mail mailToiVo = new Mail(TieuDe, ngay, TKNguoiGui, TKNguoiVo, mess);
                MailDAO.Instance.Them(mailToiChong);
                MailDAO.Instance.Them(mailToiVo);
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn một yêu cầu");
            }
        }
        private void btnTuChoi_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbNoiDung.Text == "")
                {
                    throw new Exception();
                }
                if(hn.Loai == "Kết hôn")
                {
                    if (HonNhanDAO.Instance.Delete(hn))
                    {
                        MessageBox.Show("Gửi thành công");
                    }
                    else
                    {
                        MessageBox.Show("Gửi thất bại");
                    }
                } else if (hn.Loai == "Ly hôn")
                {
                    if (HonNhanDAO.Instance.ConvertDivorceToMarriage(hn))
                    {
                        MessageBox.Show("Gửi thành công");
                    }
                    else
                    {
                        MessageBox.Show("Gửi thất bại");
                    }
                }
                HonNhanDAO.Instance.LoadMailControler(dtgvHonNhan);
            }
            catch
            {
                MessageBox.Show("Nội dung không được để trống");
            }
        }
    }
}
