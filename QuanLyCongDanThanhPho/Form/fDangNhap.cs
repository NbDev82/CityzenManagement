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
    public partial class fDangNhap : System.Windows.Forms.Form
    {
        public fDangNhap()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                CongDan cd = CongDanDAO.Instance.KiemTraDangNhap(tbxUserName.Text, tbxPassword.Text);
                if (cd != null)
                {
                    fNguoiDung nd = new fNguoiDung(cd);
                    this.Hide();
                    nd.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Đăng nhập thất bại!\n" + ex);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có thật sự muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void cbHienMK_CheckedChanged(object sender, EventArgs e)
        {
            if (cbHienMK.Checked == true)
            {
                tbxPassword.UseSystemPasswordChar = false;
            }
            else
            {
                tbxPassword.UseSystemPasswordChar = true;
            }
        }

        private void fDangNhap_Load(object sender, EventArgs e)
        {

        }
    }
}
