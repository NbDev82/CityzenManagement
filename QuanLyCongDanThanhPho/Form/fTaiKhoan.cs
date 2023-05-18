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
    public partial class fTaiKhoan : Form
    {
        TaiKhoan tk;
        public fTaiKhoan(TaiKhoan tk)
        {
            InitializeComponent();
            this.tk = tk;
        }
        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show(null,"Bạn muốn đổi mật khẩu?","",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    if (tbMatKhau.Text != tk.Matkhau)
                        throw new Exception();
                    string mkmoi = tbMatKhauMoi.Text;
                    if (mkmoi.Length >= 5)
                    {
                        if(AccountsDAO.Instance.DoiMatKhau(tk.Macd, mkmoi))
                        {
                            MessageBox.Show("Đổi mật khẩu thành công");
                        }
                        else
                        {
                            MessageBox.Show("Đổi mật khẩu thất bại");
                        }
                        tbMatKhau.Text = "";
                        tbMatKhauMoi.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu không hợp lệ!");
                    }
                }
                catch
                {
                    MessageBox.Show("Mật khẩu hoặc tài khoản không đúng");
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
            cbLoaiTaiKhoan.Text = tk.Phanquyen == 1 ? "Quản lý" : "Công dân"; 
        }
    }
}
