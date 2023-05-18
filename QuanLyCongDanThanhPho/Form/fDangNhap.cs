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
    public partial class fDangNhap : Form
    {
        public fDangNhap()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                int account = int.Parse(txtAccount.Text);
                string password = txtPassword.Text;
                CongDan cd = AccountsDAO.Instance.AuthenticateAccount(account, password);
                if (cd != null)
                {
                    TaiKhoan tk = AccountsDAO.Instance.GetAccount(account, password);
                    KhaiSinh ks = KhaiSinhDAO.Instance.GetKhaiSinhByID(account);
                    fNguoiDung nd = new fNguoiDung(cd,tk,ks);
                    this.Hide();
                    nd.ShowDialog();
                    this.Show();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!\n");
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
        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowPassword.Checked == true)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }
    }
}
