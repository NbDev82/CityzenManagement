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
    public partial class fHonNhan : Form
    {
        HonNhan hn;
        CongDan cd;

        public readonly string XACNHANLAN1 = "XacNhanLan1";
        public readonly string XACNHANLAN2 = "XacNhanLan2";

        public fHonNhan(CongDan cd)
        {
            InitializeComponent();
            this.cd = cd;
        }
        private void fHonNhan_Load(object sender, EventArgs e)
        {
            HonNhanDAO.Instance.LoadFormPersonal(dtgvHonNhan, cd);
        }
        public void FillA(string Macd)
        {
            List<Control> ltext = new List<Control>();
            ltext.Add(cbMaCDNam);
            ltext.Add(tbHoTenNam);
            ltext.Add(dtpkNgaySinhNam);
            ltext.Add(tbNoiSinhNam);
            ltext.Add(tbGioiTinhNam);
            ltext.Add(tbNgheNghiepNam);
            ltext.Add(tbDanTocNam);
            ltext.Add(tbTonGiaoNam);
            if (HonNhanDAO.Instance.Fill(Macd, ltext))
            {
                tbQuanHeNam.Text = tbGioiTinhNam.Text == "Nam" ? "Chồng" : "Vợ";
                btnDangKy.Enabled = true;
            }

        }
        public void FillB(string Macd)
        {
            List<Control> ltext = new List<Control>();
            ltext.Add(cbMaCDNu);
            ltext.Add(tbHoTenNu);
            ltext.Add(dtpkNgaySinhNu);
            ltext.Add(tbNoiSinhNu);
            ltext.Add(tbGioiTinhNu);
            ltext.Add(tbNgheNghiepNu);
            ltext.Add(tbDanTocNu);
            ltext.Add(tbTonGiaoNu);
            if (HonNhanDAO.Instance.Fill(Macd, ltext))
            {
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

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbMaCDNam.Text == "" || cbMaCDNu.Text == "")
                    throw new Exception();
                if (!HonNhanDAO.Instance.isExist(cbMaCDNam.Text) && !HonNhanDAO.Instance.isExist(cbMaCDNu.Text))
                {
                    string maCDChong = tbQuanHeNam.Text == "Chồng" ? cbMaCDNam.Text : cbMaCDNu.Text;
                    string maCDVo = tbQuanHeNu.Text == "Vợ" ? cbMaCDNu.Text : cbMaCDNam.Text;
                    string ngay = dtpkNgayDangKy.Value.Date.ToString();
                    string Admin = CongDanDAO.Instance.GetControler();
                    int mahn = HonNhanDAO.Instance.GetMaHNMax() + 1;
                    HonNhan honNhan = new HonNhan(mahn, maCDChong, maCDVo, cbLoai.Text, ngay, null, null, "Chưa Duyệt");
                    HonNhanDAO.Instance.SendForm(honNhan);

                    HonNhanDAO.Instance.LoadFormPersonal(dtgvHonNhan, cd);
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbMaCDNam.Text == "" || cbMaCDNu.Text == "")
                    throw new Exception();
                if (!HonNhanDAO.Instance.CheckXacNhan(cd.Tentk))
                {
                    HonNhanDAO.Instance.Delete(hn);
                    MessageBox.Show("Hủy thành công");
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
                if(!HonNhanDAO.Instance.CheckXacNhan(cd.Tentk))
                {
                    if (tbXacNhanNam.Text != "" || tbXacNhanNu.Text != "")
                    {
                        HonNhanDAO.Instance.Update(hn, cd.Tentk, XACNHANLAN2);
                    }
                    else
                    {
                        HonNhanDAO.Instance.Update(hn, cd.Tentk, XACNHANLAN1);
                    }
                    hn = HonNhanDAO.Instance.ReadByID(cbMaCDNam.Text, cbMaCDNu.Text);
                    HonNhanDAO.Instance.LoadFormPersonal(dtgvHonNhan, cd);
                    if (cd.Tentk == hn.Xacnhanlan1 || cd.Tentk == hn.Xacnhanlan2)
                    {
                        tbXacNhanNam.Text = cd.Tentk;
                    }
                    else
                    {
                        tbXacNhanNu.Text = cd.Tentk;
                    }
                }
                else
                {
                    MessageBox.Show("Đã xác nhận từ trước");
                }
                
            }
            catch
            {

            }
        }

        private void rdbtnCongDan_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtnCongDan.Checked)
                HonNhanDAO.Instance.LoadFormPersonal(dtgvHonNhan, cd);
        }

        private void rdobtnQuanLy_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtnQuanLy.Checked)
                HonNhanDAO.Instance.LoadMailControler(dtgvHonNhan);
        }

        private void btnFill_Click(object sender, EventArgs e)
        {
            try
            {
                FillA(cbMaCDNam.Text);
                FillB(cbMaCDNu.Text);
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
                DataTable CurrentRow = (DataTable)dtgvHonNhan.DataSource;
                if (CurrentRow != null)
                {
                    string maCdChong = (string)CurrentRow.Rows[index]["MaCDChong"];
                    string maCdVo = (string)CurrentRow.Rows[index]["MaCDVo"];
                    FillA(maCdChong);
                    FillB(maCdVo);
                    string tkChong = (string)CongDanDAO.Instance.LayDanhSach(maCdChong).Rows[0]["TenTK"];
                    string tkVo = (string)CongDanDAO.Instance.LayDanhSach(maCdVo).Rows[0]["TenTK"];
                    hn = HonNhanDAO.Instance.ReadByID(maCdChong, maCdVo);
                    if (tkChong == hn.Xacnhanlan1 || tkChong == hn.Xacnhanlan2)
                    {
                        tbXacNhanNam.Text = cd.Tentk;
                    }
                    if (tkVo == hn.Xacnhanlan1 || tkVo == hn.Xacnhanlan2)
                    {
                        tbXacNhanNu.Text = cd.Tentk;
                    }
                    cbLoai.Text = hn.Loai;
                    tbCode.Text = hn.Mahn.ToString();
                    tbTrangThai.Text = hn.Trangthai;
                    dtpkNgayDangKy.Text = hn.Ngaydangky;
                    btnXacNhan.Enabled = true;
                    btnHuy.Enabled = true;
                    cbMaCDNam.Enabled = false;
                    cbMaCDNu.Enabled = false;
                    cbLoai.Enabled = false;
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
            int mahn = int.Parse(tbTimKiem.Text);
            HonNhanDAO.Instance.TimKiemHonNhan(dtgvHonNhan,cd, mahn);
        }

        private void btXem_Click(object sender, EventArgs e)
        {
            HonNhanDAO.Instance.LoadFormPersonal(dtgvHonNhan,cd);
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
                hn.Trangthai = "Đã Duyệt";
                HonNhanDAO.Instance.CapNhatTrangThaiHonNhan(hn);
                HonNhanDAO.Instance.LoadMailControler(dtgvHonNhan);
                tbTrangThai.Text = hn.Trangthai;
                string TieuDe = "Đồng ý kết hôn";
                string ngay = DateTime.Now.Date.ToString();
                string TKNguoiGui = cd.Tentk;
                string TKNguoiChong = (string)CongDanDAO.Instance.LayDanhSach(cbMaCDNam.Text).Rows[0]["TenTK"];
                string TKNguoiVo = (string)CongDanDAO.Instance.LayDanhSach(cbMaCDNu.Text).Rows[0]["TenTK"];
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
                HonNhanDAO.Instance.Delete(hn);
                HonNhanDAO.Instance.LoadMailControler(dtgvHonNhan);
            }
            catch
            {
                MessageBox.Show("Nội dung không được để trống");
            }
        }
    }
}
