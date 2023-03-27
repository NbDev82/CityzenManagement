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
        CongDan cd = new CongDan();

        public fHomThu() { }

        public fHomThu(CongDan congdan)
        {
            InitializeComponent();
            cd = congdan;
        }

        private void fHomThu_Load(object sender, EventArgs e)
        {
            dtgvHomThu.DataSource = MailDAO.Instance.LayDanhSachNhan(cd.Tentk);
            tbNguoiGui.Text = cd.Tentk;
            cbNguoiNhan.DataSource = MailDAO.Instance.LayDanhSachNguoiNhan();
            cbNguoiNhan.DisplayMember = "TenTK";
            cbNguoiNhan.Text = "";
        }

        void ResetThongTinRong()
        {
            tbMaMail.Text = "";
            tbTieuDe.Text = "";
            dtpkNgay.Value = DateTime.Today;
            tbNguoiGui.Text = cd.Tentk;
            cbNguoiNhan.Text = "";
            rtbNoiDung.Text = "";
        }

        bool KiemTraThongTinRong(Mail mail)
        {
            if (mail.Tieude == "" || mail.Ngay == "" || mail.Nguoigui == "" || mail.Nguoinhan == "")
                return true;
            else
                return false;
        }

        private void rbThuGui_CheckedChanged(object sender, EventArgs e)
        {
            dtgvHomThu.DataSource = MailDAO.Instance.LayDanhSachGui(cd.Tentk);
        }

        private void rbThuNhan_CheckedChanged(object sender, EventArgs e)
        {
            dtgvHomThu.DataSource = MailDAO.Instance.LayDanhSachNhan(cd.Tentk);
        }

        void ReloadDanhSach()
        {
            if (rbThuGui.Checked == true)
            {
                dtgvHomThu.DataSource = MailDAO.Instance.LayDanhSachGui(cd.Tentk);
            }
            else
            {
                dtgvHomThu.DataSource = MailDAO.Instance.LayDanhSachNhan(cd.Tentk);
            }
        }

        private void btGui_Click(object sender, EventArgs e)
        {
            try
            {
                Mail mail = new Mail(tbTieuDe.Text, dtpkNgay.Text, tbNguoiGui.Text, cbNguoiNhan.Text, rtbNoiDung.Text);
                if (KiemTraThongTinRong(mail) == true)
                    MessageBox.Show("Thông tin không hợp lệ!");
                else
                {
                    MailDAO.Instance.Them(mail);
                    ReloadDanhSach();
                    ResetThongTinRong();
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
                Mail mail = new Mail(Convert.ToInt32(tbMaMail.Text), tbTieuDe.Text, dtpkNgay.Text, tbNguoiGui.Text, cbNguoiNhan.Text, rtbNoiDung.Text);
                MailDAO.Instance.Xoa(mail);
                ReloadDanhSach();
                ResetThongTinRong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xóa thất bại\n" + ex);
            }
        }

        private void tbNguoiGui_TextChanged(object sender, EventArgs e)
        {
            if (tbNguoiGui.Text == cd.Tentk)
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
                Mail mail = new Mail(Convert.ToInt32(tbMaMail.Text), tbTieuDe.Text, dtpkNgay.Text, tbNguoiGui.Text, cbNguoiNhan.Text, rtbNoiDung.Text);
                if (KiemTraThongTinRong(mail) == true)
                    MessageBox.Show("Thông tin không hợp lệ!");
                else
                {
                    MailDAO.Instance.Sua(mail);
                    ReloadDanhSach();
                    ResetThongTinRong();
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
            string find = tbTimKiem.Text;
            if (rbThuGui.Checked == true)
            {
                dtgvHomThu.DataSource = MailDAO.Instance.TimKiemDanhSachGui(cd.Tentk ,find);
            }
            else
            {
                dtgvHomThu.DataSource = MailDAO.Instance.TimKiemDanhSachNhan(cd.Tentk, find);
            }
        }
    }
}