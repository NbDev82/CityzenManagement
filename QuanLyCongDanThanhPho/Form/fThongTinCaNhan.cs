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
    public partial class fThongTinCaNhan : Form
    {
        public fThongTinCaNhan()
        {
            InitializeComponent();
        }

        private void fThongTinCaNhan_Load(object sender, EventArgs e)
        {
            cbNoiSinh.Items.AddRange(Program.tinhthanh.ToArray());
            cbDanToc.Items.AddRange(Program.dantoc.ToArray());
        }

        private void tbDoiMatKhau_Click(object sender, EventArgs e)
        {

        }
    }
}
