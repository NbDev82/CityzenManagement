using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCongDanThanhPho
{
    public class HoKhau
    {
        string maho;
        string chuho;
        string tinhthanh;
        string quanhuyen;
        string phuongxa;
        int trangthai;
        string ngaydangky;

        public string Maho { get => maho; set => maho = value; }
        public string Chuho { get => chuho; set => chuho = value; }
        public string Tinhthanh { get => tinhthanh; set => tinhthanh = value; }
        public string Quanhuyen { get => quanhuyen; set => quanhuyen = value; }
        public string Phuongxa { get => phuongxa; set => phuongxa = value; }
        public int Trangthai { get => trangthai; set => trangthai = value; }
        public string Ngaydangky { get => ngaydangky; set => ngaydangky = value; }

        public HoKhau() { }

        public HoKhau(string maho, string chuho, string tinhthanh, string quanhuyen, string phuongxa, int trangthai, string ngaydangky)
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
