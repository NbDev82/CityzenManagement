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
    public partial class fChiTietHoKhau : Form
    {
        fNguoiDung nguoidung = new fNguoiDung();
        public fChiTietHoKhau(fNguoiDung user)
        {
            InitializeComponent();
            nguoidung = user;
        }

        private void btQuayLai_Click(object sender, EventArgs e)
        {
            //nguoidung.OpenChildForm(new fHoKhau(nguoidung));
        }

        private void fChiTietHoKhau_Load(object sender, EventArgs e)
        {

        }
    }
}
