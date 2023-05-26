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
    public partial class fHomThu : Form
    {
        CongDan cd;
        public fHomThu() { }
        public fHomThu(CongDan congdan)
        {
            InitializeComponent();
            cd = congdan;
        }
        private void fHomThu_Load(object sender, EventArgs e)
        {
            dtgvHomThu.DataSource = MailDAO.Instance.LayDanhSachNhan(cd.Macd);
            tbNguoiGui.Text = cd.Macd.ToString();
            cbNguoiNhan.DataSource = MailDAO.Instance.LayDanhSachNguoiNhan();
            cbNguoiNhan.DisplayMember = "TenTK";
            cbNguoiNhan.Text = "";
        }
        void ResetThongTinRong()
        {
            tbMaMail.Text = "";
            tbTieuDe.Text = "";
            dtpkNgay.Value = DateTime.Today;
            tbNguoiGui.Text = cd.Macd.ToString();
            cbNguoiNhan.Text = "";
            rtbNoiDung.Text = "";
        }
        bool KiemTraThongTinRong(Mail mail)
        {
            if (mail.Tieude == "" || mail.Ngay == "")
                return true;
            else
                return false;
        }
        private void rbThuGui_CheckedChanged(object sender, EventArgs e)
        {
            dtgvHomThu.DataSource = MailDAO.Instance.LayDanhSachGui(cd.Macd);
        }
        private void rbThuNhan_CheckedChanged(object sender, EventArgs e)
        {
            dtgvHomThu.DataSource = MailDAO.Instance.LayDanhSachNhan(cd.Macd);
        }
        void ReloadDanhSach()
        {
            if (rbThuGui.Checked == true)
            {
                dtgvHomThu.DataSource = MailDAO.Instance.LayDanhSachGui(cd.Macd);
            }
            else
            {
                dtgvHomThu.DataSource = MailDAO.Instance.LayDanhSachNhan(cd.Macd);
            }
        }
        private void btGui_Click(object sender, EventArgs e)
        {
            try
            {
                int nguoigui = int.Parse(tbNguoiGui.Text);
                int nguoinhan = int.Parse(cbNguoiNhan.Text);
                Mail mail = new Mail(tbTieuDe.Text, dtpkNgay.Text, nguoigui, nguoinhan, rtbNoiDung.Text);
                if (KiemTraThongTinRong(mail) == true)
                    MessageBox.Show("Thông tin không hợp lệ!");
                else
                {
                    if (MailDAO.Instance.Them(mail))
                    {
                        MessageBox.Show("Gửi thành công");
                        ReloadDanhSach();
                        ResetThongTinRong();
                    }
                    else
                    {
                        MessageBox.Show("Gửi thất bại");
                    }

                }    
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm thất bại\n" + ex);
            }
        }
        private void dtgvHomThu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dtgvHomThu.CurrentRow.Selected = true;
            tbMaMail.Text = dtgvHomThu.SelectedRows[0].Cells[0].Value.ToString();
            tbTieuDe.Text = dtgvHomThu.SelectedRows[0].Cells[1].Value.ToString();
            dtpkNgay.Text = dtgvHomThu.SelectedRows[0].Cells[2].Value.ToString();
            tbNguoiGui.Text = dtgvHomThu.SelectedRows[0].Cells[3].Value.ToString();
            cbNguoiNhan.Text = dtgvHomThu.SelectedRows[0].Cells[4].Value.ToString();
            rtbNoiDung.Text = dtgvHomThu.SelectedRows[0].Cells[5].Value.ToString();
        }
        private void btDatLai_Click(object sender, EventArgs e)
        {
            ResetThongTinRong();
        }
        private void btXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int nguoigui = int.Parse(tbNguoiGui.Text);
                int nguoinhan = int.Parse(cbNguoiNhan.Text);
                Mail mail = new Mail(Convert.ToInt32(tbMaMail.Text), tbTieuDe.Text, dtpkNgay.Text, nguoigui, nguoinhan, rtbNoiDung.Text);
                if (MailDAO.Instance.Xoa(mail))
                {
                    MessageBox.Show("Xóa thành công");
                    ReloadDanhSach();
                    ResetThongTinRong();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xóa thất bại\n" + ex);
            }
        }
        private void tbNguoiGui_TextChanged(object sender, EventArgs e)
        {
            int nguoigui = int.Parse(tbNguoiGui.Text);
            if (nguoigui == cd.Macd)
            {
                btGui.Enabled = true;
                btXoa.Enabled = true;
                btSua.Enabled = true;
            }  
            else
            {
                btGui.Enabled = false;
                btXoa.Enabled = false;
                btSua.Enabled = false;
            }
        }
        private void btXem_Click(object sender, EventArgs e)
        {
            ReloadDanhSach();
        }
        private void btSua_Click(object sender, EventArgs e)
        {
            try
            {
                int nguoigui = int.Parse(tbNguoiGui.Text);
                int nguoinhan = int.Parse(cbNguoiNhan.Text);
                Mail mail = new Mail(Convert.ToInt32(tbMaMail.Text), tbTieuDe.Text, dtpkNgay.Text, nguoigui, nguoinhan, rtbNoiDung.Text);
                if (KiemTraThongTinRong(mail) == true)
                    MessageBox.Show("Thông tin không hợp lệ!");
                else
                {
                    if (MailDAO.Instance.Sua(mail))
                    {
                        MessageBox.Show("Sửa thành công");
                        ReloadDanhSach();
                        ResetThongTinRong();
                    }
                    else{
                        MessageBox.Show("Sửa thất bại");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sửa thất bại\n" + ex);
            }
        }
        private void tbMaMail_TextChanged(object sender, EventArgs e)
        {
            if (tbMaMail.Text == "")
            {
                cbNguoiNhan.Enabled = true;
                btXoa.Enabled = false;
                btSua.Enabled = false;
            }
            else
            {
                cbNguoiNhan.Enabled = false;
                btXoa.Enabled = true;
                btSua.Enabled = true;
            }
        }
        private void btTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string find = tbTimKiem.Text;
                if (rbThuGui.Checked == true)
                {
                    dtgvHomThu.DataSource = MailDAO.Instance.TimKiemDanhSachGui(cd.Macd, find);
                }
                else
                {
                    dtgvHomThu.DataSource = MailDAO.Instance.TimKiemDanhSachNhan(cd.Macd, find);
                }
            }
            catch
            {
                MessageBox.Show("Không hợp lệ");
            }
        }
    }
}