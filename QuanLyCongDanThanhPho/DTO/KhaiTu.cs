using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCongDanThanhPho.DTO
{
    internal class KhaiTu
    {
        public int Macd { get; set; }
        public int Nguoikhai { get; set; }
        public string Nguyennhan { get; set; }
        public string Ngaytu { get; set; }
        public string Ngaykhai { get; set; }
        public string NgayDuyet { get; set; }
        public KhaiTu() { }
        public KhaiTu(int Macd, int Nguoikhai, string Nguyennhan, string Ngaytu, string Ngaykhai, string NgayDuyet)
        {
            this.Macd = Macd;
            this.Nguoikhai = Nguoikhai;
            this.Nguyennhan = Nguyennhan;
            this.Ngaytu = Ngaytu;
            this.Ngaykhai = Ngaykhai;
            this.NgayDuyet = NgayDuyet;
        }
        public KhaiTu(DataRow dt)
        {
            this.Macd = (int)dt["MaCD"];
            this.Nguoikhai = (int)dt["NguoiKhai"];
            this.Nguyennhan = (string)dt["NguyenNhan"];
            this.Ngaytu = (string)dt["NgayTu"];
            this.Ngaykhai = (string)dt["NgayKhai"];
            this.NgayDuyet = (string)dt["NgayDuyet"];
        }
    }
}
