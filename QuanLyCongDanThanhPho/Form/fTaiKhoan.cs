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
    public partial class fTaiKhoan : Form
    {
        CongDan cd;
        public fTaiKhoan(CongDan cd)
        {
            InitializeComponent();
            this.cd = cd;
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show(null,"Bạn muốn đổi mật khẩu?","",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    string mkmoi = tbMatKhauMoi.Text;
                    if (mkmoi.Length >= 5)
                    {
                        CongDanDAO.Instance.DoiMatKhau(cd, mkmoi);
                        tbMatKhau.Text = "";
                        tbMatKhauMoi.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu không hợp lệ!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đổi mật khẩu thất bại\n" + ex);
                }
            }
        }

        private void cbHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (cbHienMatKhau.Checked)
            {
                tbMatKhau.UseSystemPasswordChar = false;
                tbMatKhauMoi.UseSystemPasswordChar = false;
            }
            else
            {
                tbMatKhau.UseSystemPasswordChar = true;
                tbMatKhauMoi.UseSystemPasswordChar = true;
            }
        }

        private void fTaiKhoan_Load(object sender, EventArgs e)
        {
            cbLoaiTaiKhoan.Text = cd.Loaitk;
        }
    }
}
