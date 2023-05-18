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
    public partial class fKhaiSinhKhaiTu : Form
    {
        CongDan cd;
        TaiKhoan tk;
        public fKhaiSinhKhaiTu(CongDan cd, TaiKhoan tk)
        {
            InitializeComponent();
            this.cd = cd;
            this.tk = tk;
        }
        public void ResetForm()
        {
            dtgvKhaiSinhKhaiTu.DataSource = null;
            WireEmptyTextBox(this);
        }
        public void WireEmptyTextBox(Control controls)
        {
            foreach (Control control in controls.Controls)
            {
                if (control is Panel Panel)
                {
                    WireEmptyTextBox(Panel);
                }
                if(control is TableLayoutPanel TLP)
                {
                    WireEmptyTextBox(TLP);
                }
                if (control is TextBox textBox)
                {
                    textBox.Text = "";
                }
                else if (control is ComboBox comboBox)
                {
                    comboBox.Text = "";
                }
                else if (control is DateTimePicker dateTimePicker)
                {
                    dateTimePicker.Value = DateTime.Now;
                }
            }
        }
        public void Fill(DataRow dt)
        {
            try
            {
                txtHoTen.Text = dt["HoTen"].ToString();
                cbxGioiTinh.Text = dt["GioiTinh"].ToString();
                cbxDanToc.Text = dt["DanToc"].ToString();
                cbxTinhTrang.Text = dt["TinhTrang"].ToString();
                if (rdoKhaiSinh.Checked)
                {
                    dtpkNgaySinh.Text = dt["NgaySinh"].ToString();
                    cbxNoiSinh.Text = dt["NoiSinh"].ToString();
                    txtMaCCCDCha.Text = dt["MaCD_Cha"].ToString();
                    txtMaCCCDMe.Text = dt["MaCD_Me"].ToString();
                    dtpkNgayMat.Text = "";
                    txtLyDo.Text = "";
                }
                else if (rdoKhaiTu.Checked)
                {
                    dtpkNgayMat.Text = dt["NgayTu"].ToString();
                    txtLyDo.Text = dt["NguyenNhan"].ToString();
                    dtpkNgaySinh.Text = null;
                    cbxNoiSinh.Text = "";
                    txtMaCCCDCha.Text = "";
                    txtMaCCCDMe.Text = "";
                }
            }
            catch { }
        }
        private void rbQuanLy_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoQuanLy.Checked)
            {
                if (tk.Phanquyen == 1)
                {
                    pnCongDan.Enabled = false;
                    pnQuanLy.Enabled = true;
                    pnTimKiem.Enabled = true;
                    
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền này");
                    rdoCongDan.Checked = true;
                }
            }
        }
        private void rdoCongDan_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCongDan.Checked)
            {
                pnCongDan.Enabled = true;
                pnQuanLy.Enabled = false;
                pnTimKiem.Enabled = false;
                ResetForm();
            }
        }
        private void txtMaCD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    if (txtMaCD.Text == "")
                        throw new Exception();
                    int macd = int.Parse(txtMaCD.Text);
                    DataTable dt = KhaiSinhDAO.Instance.GetDataTable(macd);
                    if(dt == null)
                        throw new Exception();
                    Fill(dt.Rows[0]);
                }
                catch
                {
                    MessageBox.Show("Mã công dân không hợp lệ hoặc không đúng");
                }
            }
        }
        private void rdoKhaiSinh_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoKhaiSinh.Checked)
            {
                txtMaCD.Enabled = false;
                pnShowKhaiTu.Visible = false;
                pnThongTin.Enabled = true;
                pnShowKhaiSinh.Visible = true;
            }
            else
            {
                txtMaCD.Enabled = true;
                pnShowKhaiTu.Visible = true;
                pnThongTin.Enabled = false;
                pnShowKhaiSinh.Visible = false;
            }
        }
        private void btnDatLai_Click(object sender, EventArgs e)
        {
            WireEmptyTextBox(this);
        }
        private void btnKhai_Click(object sender, EventArgs e)
        {
            try
            {
                int macd = cd.Macd;
                string CheDo = rdoKhaiSinh.Checked ? "KhaiSinh" : "KhaiTu";
                if (CheDo == "KhaiSinh")
                {
                    int MaCDCha = CCCDDAO.Instance.GetMaCDByMaCCCD(txtMaCCCDCha.Text);
                    int MaCDMe = CCCDDAO.Instance.GetMaCDByMaCCCD(txtMaCCCDMe.Text);
                    if (macd != MaCDCha && macd != MaCDMe)
                    {
                        MessageBox.Show("Người khai phải là cha hoặc mẹ của người được khai sinh");
                        return;
                    }
                    string HoTen = txtHoTen.Text;
                    string NgaySinh = dtpkNgaySinh.Value.ToString();
                    string NoiSinh = cbxNoiSinh.Text;
                    string GioiTinh = cbxGioiTinh.Text;
                    string DanToc = cbxDanToc.Text;
                    string NgayKhai = DateTime.Now.ToString();
                    KhaiSinh ks = new KhaiSinh(NgaySinh,NoiSinh, MaCDCha, MaCDMe, NgayKhai);
                    CongDan cd = new CongDan(HoTen, GioiTinh, DanToc);
                    if(KhaiSinhDAO.Instance.CreateObject(ks, cd))
                    {
                        MessageBox.Show("Khai thành công");
                    }
                    else
                    {
                        MessageBox.Show("Khai thất bại");
                    }
                }
                else if (CheDo == "KhaiTu")
                {
                    int Macd = int.Parse(txtMaCD.Text);
                    int NguoiKhai = cd.Macd;
                    string NguyenNhan = txtLyDo.Text;
                    string NgayMat = dtpkNgayMat.Value.ToString();
                    string NgayKhai = DateTime.Now.ToString();
                    KhaiTu kt = new KhaiTu(Macd, NguoiKhai, NguyenNhan, NgayMat, NgayKhai, "");
                    if (KhaiTuDAO.Instance.Add(kt))
                        MessageBox.Show("Thành công");
                    else
                        throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Người khai phải là chủ hộ trong cùng hộ khẩu");
            }
        }
        private void btnTuChoi_Click(object sender, EventArgs e)
        {
            int index = dtgvKhaiSinhKhaiTu.CurrentRow.Index;
            if (checkIndex(index))
            {
                string strMaCD = dtgvKhaiSinhKhaiTu.Rows[index].Cells["MaCD"].Value.ToString();
                int Macd = int.Parse(strMaCD);
                int state = dtgvKhaiSinhKhaiTu.Rows[index].Cells["NgayDuyet"].Value.ToString() == "NONE" ? 1 : 0;
                if(state == 1)
                {
                    if (rdoKhaiSinh.Checked)
                    {
                        if (!KhaiSinhDAO.Instance.Delete(Macd))
                            MessageBox.Show("Thất bại");
                        else
                        {
                            MessageBox.Show("Duyệt thành công");
                            dtgvKhaiSinhKhaiTu.DataSource = KhaiSinhDAO.Instance.GetDataTable();
                        }
                    }
                    else if (rdoKhaiTu.Checked)
                    {
                        if (!KhaiTuDAO.Instance.Delete(Macd))
                            MessageBox.Show("Thất bại");
                        else
                        {
                            MessageBox.Show("Xóa thành công");
                            dtgvKhaiSinhKhaiTu.DataSource = KhaiTuDAO.Instance.GetDataTable();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không thuộc trường khai sinh hoặc khai tử");
                    }
                }
                else
                {
                    MessageBox.Show("Đơn đã duyệt rồi");
                }
            }
            else
            {
                MessageBox.Show("Chọn 1 đơn để thao tác");
            }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                int Macd = int.Parse(tbTimKiem.Text);
                if (rdoKhaiSinh.Checked)
                {
                    dtgvKhaiSinhKhaiTu.DataSource = KhaiSinhDAO.Instance.GetDataTable(Macd);
                }
                else if (rdoKhaiTu.Checked)
                {
                    dtgvKhaiSinhKhaiTu.DataSource = KhaiTuDAO.Instance.GetDataTable(Macd);
                }
                else
                {
                    MessageBox.Show("Cần chọn khai sinh hoặc khai tử");
                }
            }
            catch
            {
                MessageBox.Show("Không được để trống");
            }
        }
        public bool checkIndex(int index)
        {
            return index >= 0;
        }
        private void btnDuyet_Click(object sender, EventArgs e)
        {
            int index = dtgvKhaiSinhKhaiTu.CurrentRow.Index;
            if (checkIndex(index))
            {
                string NgayDuyet = dtgvKhaiSinhKhaiTu.Rows[index].Cells["NgayDuyet"].Value.ToString();
                if (NgayDuyet != "NONE")
                {
                    MessageBox.Show("Đơn đã duyệt rồi");
                    return;
                }
                NgayDuyet = DateTime.Now.ToString();
                string strMaCD = dtgvKhaiSinhKhaiTu.Rows[index].Cells["MaCD"].Value.ToString();
                int Macd = int.Parse(strMaCD);
                if (rdoKhaiSinh.Checked)
                {
                    if (!KhaiSinhDAO.Instance.UpdateNgayDuyet(Macd, NgayDuyet))
                        MessageBox.Show("Thất bại");
                    else
                    {
                        MessageBox.Show("Duyệt thành công");
                        dtgvKhaiSinhKhaiTu.DataSource = KhaiSinhDAO.Instance.GetDataTable();
                    }    
                }
                else if (rdoKhaiTu.Checked)
                {
                    if (!KhaiTuDAO.Instance.UpdateNgayDuyet(Macd, NgayDuyet))
                        MessageBox.Show("Thất bại");
                    else
                    {
                        MessageBox.Show("Duyệt thành công");
                        dtgvKhaiSinhKhaiTu.DataSource = KhaiTuDAO.Instance.GetDataTable();
                    }
                }
                else
                {
                    MessageBox.Show("Không thuộc trường khai sinh hoặc khai tử");
                }
            }
            else
            {
                MessageBox.Show("Chọn 1 đơn để thao tác");
            }
        }
        private void dtgvKhaiSinhKhaiTu_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = dtgvKhaiSinhKhaiTu.CurrentRow.Index;
                if (checkIndex(index))
                {
                    if (dtgvKhaiSinhKhaiTu.Rows[index].Cells["NgayDuyet"].ToString() == "NONE")
                    {
                        MessageBox.Show("Đơn chưa duyệt rồi");
                        return;
                    }
                    string strMaCD = dtgvKhaiSinhKhaiTu.Rows[index].Cells["MaCD"].ToString();
                    DataTable dt = dtgvKhaiSinhKhaiTu.DataSource as DataTable;
                    DataRow dtRow = dt.Rows[index];
                    txtMaCD.Text = dtRow["MaCD"].ToString();
                    Fill(dtRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnXem_Click(object sender, EventArgs e)
        {
            if (rdoKhaiSinh.Checked)
                dtgvKhaiSinhKhaiTu.DataSource = KhaiSinhDAO.Instance.GetDataTable();
            else if (rdoKhaiTu.Checked)
                dtgvKhaiSinhKhaiTu.DataSource = KhaiTuDAO.Instance.GetDataTable();
            else
                MessageBox.Show("Không hợp lệ");
        }
        private void txtMaCDCha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    string macccd = txtMaCCCDCha.Text != "" ? txtMaCCCDCha.Text : txtMaCCCDMe.Text;
                    int macd = CCCDDAO.Instance.GetMaCDByMaCCCD(macccd);
                    HonNhan DataOfMariage = HonNhanDAO.Instance.ReadByID(macd);
                    string MaCCCDCha = CCCDDAO.Instance.GetMaCCCDByMaCD(DataOfMariage.Macdchong);
                    string HoTenCha = CongDanDAO.Instance.GetCongDanByMaCD(DataOfMariage.Macdchong).Hoten;
                    string MaCCCDMe = CCCDDAO.Instance.GetMaCCCDByMaCD(DataOfMariage.Macdvo);
                    string HoTenMe = CongDanDAO.Instance.GetCongDanByMaCD(DataOfMariage.Macdvo).Hoten;
                    txtHoTenCha.Text = HoTenCha;
                    txtHoTenMe.Text = HoTenMe;
                    txtMaCCCDCha.Text = MaCCCDCha;
                    txtMaCCCDMe.Text = MaCCCDMe;
                }
                catch
                {
                    MessageBox.Show("Mã căn cước công dân không đúng hoặc công dân chưa kết hôn");
                }
            }
        }
    }
}
