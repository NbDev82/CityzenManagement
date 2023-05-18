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
    public partial class fCCCD : Form
    {
        CongDan cd;
        KhaiSinh ks;
        TaiKhoan tk;
        public fCCCD(CongDan cd, KhaiSinh ks, TaiKhoan tk)
        {
            InitializeComponent();
            this.cd = cd;
            this.ks = ks;
            this.tk = tk;
        }
        private void fCCCD_Load(object sender, EventArgs e)
        {
            try
            {
                if (tk.Phanquyen == 1)
                {
                    pnQuanLy.Enabled = true;
                    dtgvDanhSachCCCD.Enabled = true;
                }
                else
                {
                    pnQuanLy.Enabled = false;
                    dtgvDanhSachCCCD.Enabled = false;
                }
                int MaCD = cd.Macd;
                DataTable dataCCCD = CCCDDAO.Instance.GetCCCD(MaCD);
                DataRow Certificate = dataCCCD.Rows[0];
                CCCD cccd = new CCCD(Certificate);
                if (!LoadData(cccd))
                    throw new Exception();
                pnThongTin.Enabled = false;
                pnThongTin_3.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Tải thông tin CCCD thất bại");
                pnCaNhan.Enabled = true;
                pnThongTin.Enabled = true;
                pnThongTin_3.Enabled = true;
            }
        }
        public bool LoadData(CCCD cccd)
        {
            try
            {
                txtSoCCCD.Text = cccd.MaCCCD;
                txtHoVaTen.Text = cccd.HoVaTen;
                txtDacDiemNhanDang.Text = cccd.DacDiemNhanDang;
                txtNoiThuongTru.Text = cccd.NoiThuongTru;
                txtQueQuan.Text = cccd.QueQuan;
                txtQuocTich.Text = cccd.QuocTich;
                dtpkNgaySinh.Text = cccd.NgaySinh;
                dtpkThoiHan.Text = cccd.HanSuDung;
                txtGioiTinh.Text = cccd.GioiTinh;
                picFace.Image = cccd.Avatar;
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                int MaCD = int.Parse(txtTimKiem.Text);
                DataTable dataCCCD = CCCDDAO.Instance.GetCCCD(MaCD);
                if (dataCCCD == null)
                    throw new Exception();
                dtgvDanhSachCCCD.DataSource = dataCCCD;
            }
            catch
            {
                MessageBox.Show("Không tìm thấy CCCD của công dân này");
            }
        }
        private void brnXem_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dataCCCD = CCCDDAO.Instance.GetListCCCD();
                if (dataCCCD == null)
                    throw new Exception();
                dtgvDanhSachCCCD.DataSource = dataCCCD;
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra");
            }
        }
        private void btnTaiHinh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.Title = "Select an Image File";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                picFace.Load(openFileDialog1.FileName);
            }
        }
        public void WireEvents(Control Controls)
        {
            foreach (Control control in Controls.Controls)
            {
                if(control is FlowLayoutPanel flowPanel)
                {
                    WireEvents(flowPanel);
                }
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
                else if (control is PictureBox pictureBox)
                {
                    pictureBox.Image = pictureBox.InitialImage;
                }
            }
        }
        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            WireEvents(this);
        }
        public bool CheckAge()
        {
            DateTime dateTimePickerValue = dtpkNgaySinh.Value;
            DateTime currentDate = DateTime.Now;
            TimeSpan timeSpan = currentDate.Subtract(dateTimePickerValue);
            int years = (int)(Math.Abs(timeSpan.TotalDays) / 365);
            return years >= 16;
        }
        public bool Check()
        {
            if (string.IsNullOrEmpty(txtHoVaTen.Text))
                return false;
            if (!CheckAge())
                return false;
            if (string.IsNullOrEmpty(txtGioiTinh.Text))
                return false;
            if (string.IsNullOrEmpty(txtQuocTich.Text))
                return false;
            if (string.IsNullOrEmpty(txtQueQuan.Text))
                return false;
            if (string.IsNullOrEmpty(txtNoiThuongTru.Text))
                return false;
            if (string.IsNullOrEmpty(txtDacDiemNhanDang.Text))
                return false;
            if (picFace.Image == null)
                return false;
            return true;
        }
        public string DateOfCertificate()
        {
            DateTime currentDate = DateTime.Now;
            currentDate = currentDate.AddYears(10);
            return currentDate.Date.ToString();
        }
        public string GenerateMaCCCD()
        {
            return CCCDDAO.Instance.GenerateMaCCCD();
        }
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            try
            {
                if(Check())
                {
                    int MaCD = cd.Macd;
                    string MaCCCD = GenerateMaCCCD();
                    string HoVaTen = txtHoVaTen.Text;
                    string NgaySinh = dtpkNgaySinh.Value.ToString();
                    string GioiTinh = txtGioiTinh.Text;
                    if (!(GioiTinh == "Nam" || GioiTinh == "Nữ"))
                        throw new Exception();
                    string QuocTich = txtQuocTich.Text;
                    string QueQuan = txtQueQuan.Text;
                    string NoiThuongTru = txtNoiThuongTru.Text;
                    string HanSuDung = DateOfCertificate();
                    string DacDiemNhanDang = txtDacDiemNhanDang.Text;
                    Image Avatar = picFace.Image;
                    CCCD cccd = new CCCD(MaCD, MaCCCD, HoVaTen, NgaySinh, GioiTinh, QuocTich, QueQuan, NoiThuongTru, HanSuDung, DacDiemNhanDang, Avatar);
                    if (CCCDDAO.Instance.Add(cccd))
                        MessageBox.Show("Đăng ký thành công");
                    else
                        MessageBox.Show("Đăng ký thất bại");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Đăng ký thất bại");
            }
        }
        private void dtgvDanhSachCCCD_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = dtgvDanhSachCCCD.CurrentRow.Index;
                DataTable dttb = (DataTable)dtgvDanhSachCCCD.DataSource;
                DataRow dt = dttb.Rows[index];
                CCCD cccd = new CCCD(dt);
                LoadData(cccd);
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra");
            }
        }
    }
}
