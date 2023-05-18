using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCongDanThanhPho.DTO
{
    public class KhaiSinh
    {
        public int MaCD { get; set; }
        public string NgaySinh { get; set; }
        public string NoiSinh { get; set; }
        public int MaCD_Cha { get; set; }
        public int MaCD_Me { get; set; }
        public string NgayKhai { get; set; }
        public string NgayDuyet { get; set; }
        public KhaiSinh() { }
        public KhaiSinh(int MaCD, string NgaySinh, string NoiSinh, int MaCD_Cha, int MaCD_Me, string NgayKhai)
        {
            this.MaCD = MaCD;
            this.NgaySinh = NgaySinh;
            this.MaCD_Cha = MaCD_Cha;
            this.MaCD_Me = MaCD_Me;
            this.NgayKhai = NgayKhai;
            this.NoiSinh = NoiSinh;
        }
        public KhaiSinh(string NgaySinh, string NoiSinh, int MaCD_Cha, int MaCD_Me, string NgayKhai)
        {
            this.NgaySinh = NgaySinh;
            this.MaCD_Cha = MaCD_Cha;
            this.MaCD_Me = MaCD_Me;
            this.NgayKhai = NgayKhai;
            this.NoiSinh = NoiSinh;
        }
        public KhaiSinh(DataRow dt)
        {
            this.MaCD = (int)dt["MaCD"];
            this.NgaySinh = dt["NgaySinh"].ToString();
            this.MaCD_Cha = (int)dt["MaCD_Cha"];
            this.MaCD_Me = (int)dt["MaCD_Me"];
            this.NgayKhai = dt["NgayKhai"].ToString();
            this.NgayDuyet = dt["NgayDuyet"].ToString();
            this.NoiSinh = dt["NoiSinh"].ToString();
        }
    }
}
