using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCongDanThanhPho.DTO
{
    internal class TamVang:TamTru
    {
        public string thoi_gian_ket_thuc { get; set; }
        public TamVang() { }
        public TamVang(int MaCD, string MaCCCD, string Tinh, string Huyen, string Xa, string LyDo, string TgianBdau, string TgianKthuc)
        {
            this.MaCD = MaCD;
            this.MaCCCD = MaCCCD;
            this.Tinh = Tinh;
            this.Huyen = Huyen;
            this.Xa = Xa;
            this.LyDo = LyDo;
            this.thoi_gian_bat_dau = TgianBdau;
            this.thoi_gian_ket_thuc = TgianKthuc;
        }
        public TamVang(DataRow TamVang)
        {
            this.ID = (int)TamVang["ID"];
            this.MaCD = (int)TamVang["Mã CD"];
            this.MaCCCD = TamVang["Mã CCCD"].ToString();
            this.Tinh = TamVang["Tỉnh"].ToString();
            this.Xa = TamVang["Huyện"].ToString();
            this.LyDo = TamVang["Xã"].ToString();
            this.thoi_gian_bat_dau = TamVang["Thời gian bắt đầu"].ToString();
            this.thoi_gian_ket_thuc = TamVang["Thời gian kết thúc"].ToString();
            this.TrangThai = TamVang["Trạng thái"].ToString();
        }
    }
}
