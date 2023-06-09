﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCongDanThanhPho
{
    public class CongDan
    {
        private int macd;
        private string hoten;
        private string gioitinh;
        private string nghenghiep;
        private string dantoc;
        private string tongiao;
        private string tinhtrang;
        private int mahonnhan;
        private int mahokhau;
        public int Macd { get => macd; set => macd = value; }
        public string Hoten { get => hoten; set => hoten = value; }
        public string Nghenghiep { get => nghenghiep; set => nghenghiep = value; }
        public string Dantoc { get => dantoc; set => dantoc = value; }
        public string Tongiao { get => tongiao; set => tongiao = value; }
        public string Tinhtrang { get => tinhtrang; set => tinhtrang = value; }
        public string Gioitinh { get => gioitinh; set => gioitinh = value; }
        public int Mahonnhan { get => mahonnhan; set => mahonnhan = value; }
        public int Mahokhau { get => mahokhau; set => mahokhau = value; }
        public CongDan() { }
        public CongDan(string HoTen, string GioiTinh, string DanToc)
        {
            this.hoten = HoTen;
            this.gioitinh = GioiTinh;
            this.dantoc = DanToc;
        }
        public CongDan(int macd, string hoten, string gioitinh, string nghenghiep, string dantoc, string tongiao, string tinhtrang, int mahonnhan, int mahokhau)
        {
            this.macd = macd;
            this.hoten = hoten;
            this.gioitinh = gioitinh;
            this.nghenghiep = nghenghiep;
            this.dantoc = dantoc;
            this.tongiao = tongiao;
            this.tinhtrang = tinhtrang;
            this.mahonnhan = mahonnhan;
            this.mahokhau =mahokhau;
        }
        public CongDan(DataRow row)
        {
            try
            {
                this.macd = (int)row["MaCD"];
                this.hoten = (string)row["HoTen"];
                this.gioitinh = (string)row["GioiTinh"];
                this.nghenghiep = (string)row["NgheNghiep"];
                this.dantoc = (string)row["DanToc"];
                this.tongiao = (string)row["TonGiao"];
                this.tinhtrang = (string)row["TinhTrang"];
                this.mahonnhan = (int)row["MaHN"];
                this.mahokhau = (int)row["MaHoKhau"];
            }
            catch { }
        }
    }
}
