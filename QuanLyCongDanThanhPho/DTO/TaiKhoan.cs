using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCongDanThanhPho.DTO
{
    public class TaiKhoan
    {
        public int Macd { get; set; }
        public string Matkhau { get; set; }
        public int Phanquyen { get; set; }
        public TaiKhoan() { }
        public TaiKhoan(DataRow dt)
        {
            this.Macd = (int)dt["MaCD"];
            this.Matkhau = (string)dt["matkhau"];
            this.Phanquyen = (int)dt["phanquyen"];
        }
    }
}
