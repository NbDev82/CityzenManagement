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
    public partial class fHoKhau : System.Windows.Forms.Form
    {
        fNguoiDung nguoidung = new fNguoiDung();
        public fHoKhau(fNguoiDung user)
        {
            InitializeComponent();
            nguoidung = user;
        }

        private void btChiTiet_Click(object sender, EventArgs e)
        {
            nguoidung.OpenChildForm(new fChiTietHoKhau(nguoidung));
        }

        private void fHoKhau_Load(object sender, EventArgs e)
        {

        }
    }
}
