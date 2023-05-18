using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCongDanThanhPho
{
    public class HoKhau
    {
        private int maho;
        private int chuho;
        private string tinhthanh;
        private string quanhuyen;
        private string phuongxa;
        private int trangthai;
        private string ngaydangky;
        public int Maho { get => maho; set => maho = value; }
        public int Chuho { get => chuho; set => chuho = value; }
        public string Tinhthanh { get => tinhthanh; set => tinhthanh = value; }
        public string Quanhuyen { get => quanhuyen; set => quanhuyen = value; }
        public string Phuongxa { get => phuongxa; set => phuongxa = value; }
        public int Trangthai { get => trangthai; set => trangthai = value; }
        public string Ngaydangky { get => ngaydangky; set => ngaydangky = value; }
        public HoKhau() { }
        public HoKhau(int maho, int chuho, string tinhthanh, string quanhuyen, string phuongxa, int trangthai, string ngaydangky)
        {
            this.maho = maho;
            this.chuho = chuho;
            this.tinhthanh = tinhthanh;
            this.quanhuyen = quanhuyen;
            this.phuongxa = phuongxa;
            this.trangthai = trangthai;
            this.ngaydangky = ngaydangky;
        }
    }
}
