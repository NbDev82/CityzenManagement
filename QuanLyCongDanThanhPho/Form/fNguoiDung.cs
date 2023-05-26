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
    public partial class fNguoiDung :Form
    {
        private Form CurrentFormChild;
        CongDan congdan;
        TaiKhoan taikhoan;
        KhaiSinh khaisinh;
        public fNguoiDung(CongDan cd, TaiKhoan tk, KhaiSinh ks)
        {
            InitializeComponent();
            congdan = cd;
            taikhoan = tk;
            khaisinh = ks;
        }
        private void fNguoiDung_Load(object sender, EventArgs e)
        {
            lbBody.Text = "          " + btHomThu.Text.ToUpper();
            pnTitle.BackColor = btHomThu.BackColor;
            OpenChildForm(new fHomThu(congdan));
            tbTenNguoiDung.Text = congdan.Hoten;
            tbLoaiTK.Text = taikhoan.Phanquyen == 1?"Quản lý": "Công dân";
        }
        public void OpenChildForm(Form FormChild)
        {
            try
            {
                if (CurrentFormChild != null)
                    CurrentFormChild.Close();
                CurrentFormChild = FormChild;
                FormChild.TopLevel = false;
                FormChild.FormBorderStyle = FormBorderStyle.None;
                FormChild.Dock = DockStyle.Fill;
                pnBody.Controls.Add(FormChild);
                pnBody.Tag = FormChild;
                FormChild.BringToFront();
                if(FormChild != null)
                    FormChild.Show();
            }
            catch
            {

            }
        }
        private void btHomThu_Click(object sender, EventArgs e)
        {
            lbBody.Text = "          " + btHomThu.Text.ToUpper();
            pnTitle.BackColor = btHomThu.BackColor;
            OpenChildForm(new fHomThu(congdan));
        }
        private void btThongTinCaNhan_Click(object sender, EventArgs e)
        {
            lbBody.Text = btThongTinCaNhan.Text.ToUpper();
            pnTitle.BackColor = btThongTinCaNhan.BackColor;
            OpenChildForm(new fThongTinCaNhan(congdan, khaisinh,taikhoan));
        }
        private void btHoKhau_Click(object sender, EventArgs e)
        {
            lbBody.Text = "          " + btHoKhau.Text.ToUpper();
            pnTitle.BackColor = btHoKhau.BackColor;
            OpenChildForm(new fHoKhau(congdan, taikhoan));
        }
        private void btHonNhan_Click(object sender, EventArgs e)
        {
            lbBody.Text = "          " + btHonNhan.Text.ToUpper();
            pnTitle.BackColor = btHonNhan.BackColor;
            OpenChildForm(new fHonNhan(congdan,taikhoan));
        }
        private void btKhaiSinhKhaiTu_Click(object sender, EventArgs e)
        {
            lbBody.Text = btKhaiSinhKhaiTu.Text.ToUpper();
            pnTitle.BackColor = btKhaiSinhKhaiTu.BackColor;
            OpenChildForm(new fKhaiSinhKhaiTu(congdan,taikhoan));
        }
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            lbBody.Text = "          " + btnTaiKhoan.Text.ToUpper();
            pnTitle.BackColor = btHonNhan.BackColor;
            OpenChildForm(new fTaiKhoan(taikhoan));
        }
        private void btThue_Click(object sender, EventArgs e)
        {
            lbBody.Text = "          " + btThue.Text.ToUpper();
            pnTitle.BackColor = btThue.BackColor;
            OpenChildForm(new fThue(congdan,khaisinh,taikhoan));
        }
        private void btnTamTruTamVang_Click(object sender, EventArgs e)
        {
            lbBody.Text = "          " + btnTamTruTamVang.Text.ToUpper();
            pnTitle.BackColor = btnTamTruTamVang.BackColor;
            OpenChildForm(new fTamTruTamVang(congdan));
        }
        private void btnCCCD_Click(object sender, EventArgs e)
        {
            lbBody.Text = "          " + btnCCCD.Text.ToUpper();
            pnTitle.BackColor = btnCCCD.BackColor;
            OpenChildForm(new fCCCD(congdan, khaisinh, taikhoan));
        }
    }
}
