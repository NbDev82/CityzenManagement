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
    public partial class fNguoiDung : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Form CurrentFormChild;
        CongDan congdan = new CongDan();

        public fNguoiDung() { }

        public fNguoiDung(CongDan cd)
        {
            InitializeComponent();
            congdan = cd;
        }

        private void fNguoiDung_Load(object sender, EventArgs e)
        {
            lbBody.Text = "          " + btHomThu.Text.ToUpper();
            pnTitle.BackColor = btHomThu.BackColor;
            OpenChildForm(new fHomThu(congdan));
            tbTenNguoiDung.Text = congdan.Hoten;
            tbLoaiTK.Text = congdan.Loaitk;
        }

        public void OpenChildForm(Form FormChild)
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
            FormChild.Show();
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
            OpenChildForm(new fThongTinCaNhan(congdan));
        }

        private void btHoKhau_Click(object sender, EventArgs e)
        {
            lbBody.Text = "          " + btHoKhau.Text.ToUpper();
            pnTitle.BackColor = btHoKhau.BackColor;
            OpenChildForm(new fHoKhau(congdan));
        }

        private void btHonNhan_Click(object sender, EventArgs e)
        {
            lbBody.Text = "          " + btHonNhan.Text.ToUpper();
            pnTitle.BackColor = btHonNhan.BackColor;
            OpenChildForm(new fHonNhan(congdan));
        }

        private void btKhaiSinhKhaiTu_Click(object sender, EventArgs e)
        {
            lbBody.Text = btKhaiSinhKhaiTu.Text.ToUpper();
            pnTitle.BackColor = btKhaiSinhKhaiTu.BackColor;
            OpenChildForm(new fKhaiSinhKhaiTu());
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            lbBody.Text = "          " + btnTaiKhoan.Text.ToUpper();
            pnTitle.BackColor = btHonNhan.BackColor;
            OpenChildForm(new fTaiKhoan(congdan));
        }
    }
}
