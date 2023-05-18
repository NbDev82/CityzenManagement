using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCongDanThanhPho.DTO
{
    internal class TamTru
    {
        public int ID { get; set; }
        public int MaCD { get; set; }
        public string MaCCCD { get; set; }
        public string Tinh { get; set; }
        public string Huyen { get; set; }
        public string Xa { get; set; }
        public string LyDo { get; set; }
        public string thoi_gian_bat_dau { get; set; }
        public string TrangThai { get; set; }
        public TamTru() { }
        public TamTru(int MaCD, string MaCCCD, string Tinh, string Huyen, string Xa, string LyDo, string thoi_gian_bat_dau)
        {
            this.MaCD = MaCD;
            this.MaCCCD = MaCCCD;
            this.Tinh = Tinh;
            this.Huyen = Huyen;
            this.Xa = Xa;
            this.LyDo = LyDo;
            this.thoi_gian_bat_dau = thoi_gian_bat_dau;
        }
        public TamTru(DataRow dt)
        {
            this.ID = (int)dt["ID"];
            this.MaCD = (int)dt["Mã CD"];
            this.MaCCCD = dt["Mã CCCD"].ToString();
            this.Tinh = dt["Tỉnh"].ToString();
            this.Huyen = dt["Huyện"].ToString();
            this.Xa = dt["Xã"].ToString();
            this.LyDo = dt["Lý do"].ToString();
            this.thoi_gian_bat_dau = dt["Thời gian bắt đầu"].ToString();
            this.TrangThai = dt["Trạng thái"].ToString();
        }
    }
}
