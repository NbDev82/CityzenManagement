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
    public partial class fThue : Form
    {
        CongDan cd;
        KhaiSinh ks;
        TaiKhoan tk;
        public fThue(CongDan cd,KhaiSinh ks, TaiKhoan tk)
        {
            InitializeComponent();
            this.cd = cd;
            this.ks = ks;
            this.tk = tk;
        }
        private void fThue_Load(object sender, EventArgs e)
        {
            DataRow personalData = TamTruTamVangDAO.Instance.GetPersonalData(cd.Macd);
            txtTen.Text = personalData["Họ tên"].ToString(); 
            txtDiaChi.Text = personalData["Xã"].ToString() + ", " + personalData["Huyện"].ToString() + ", " + personalData["Tỉnh"].ToString();
            txtCCCD.Text = personalData["Mã CCCD"].ToString();
        }
        private void btnTinhThue_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch
            {
                MessageBox.Show("Không hợp lệ");
            }
        }
    }
}
