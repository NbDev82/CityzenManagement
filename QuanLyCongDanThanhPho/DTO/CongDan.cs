using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCongDanThanhPho
{
    public class CongDan
    {
        string macd;
        string hoten;
        string ngaysinh;
        string noisinh;
        string gioitinh;
        string nghenghiep;
        string dantoc;
        string tongiao;
        string tinhtrang;
        string tentk;
        string matkhau;
        string loaitk;
        string nguoikhai;
        string trangthai;
        string ngaykhai;
        string honnhan;

        public string Macd { get => macd; set => macd = value; }
        public string Hoten { get => hoten; set => hoten = value; }
        public string Ngaysinh { get => ngaysinh; set => ngaysinh = value; }
        public string Noisinh { get => noisinh; set => noisinh = value; }
        public string Nghenghiep { get => nghenghiep; set => nghenghiep = value; }
        public string Dantoc { get => dantoc; set => dantoc = value; }
        public string Tongiao { get => tongiao; set => tongiao = value; }
        public string Tinhtrang { get => tinhtrang; set => tinhtrang = value; }
        public string Tentk { get => tentk; set => tentk = value; }
        public string Matkhau { get => matkhau; set => matkhau = value; }
        public string Loaitk { get => loaitk; set => loaitk = value; }
        public string Nguoikhai { get => nguoikhai; set => nguoikhai = value; }
        public string Trangthai { get => trangthai; set => trangthai = value; }
        public string Ngaykhai { get => ngaykhai; set => ngaykhai = value; }
        public string Gioitinh { get => gioitinh; set => gioitinh = value; }
        public string Honnhan { get => honnhan; set => honnhan = value; }

        public CongDan() { }

        public CongDan(string macd, string hoten, string ngaysinh, string noisinh, string gioitinh, string nghenghiep, string dantoc, string tongiao, string honnhan, string tinhtrang, string tentk, string matkhau, string loaitk, string nguoikhai, string trangthai, string ngaykhai)
        {
            this.macd = macd;
            this.hoten = hoten;
            this.ngaysinh = ngaysinh;
            this.noisinh = noisinh;
            this.gioitinh = gioitinh;
            this.nghenghiep = nghenghiep;
            this.dantoc = dantoc;
            this.tongiao = tongiao;
            this.tinhtrang = tinhtrang;
            this.tentk = tentk;
            this.matkhau = matkhau;
            this.loaitk = loaitk;
            this.nguoikhai = nguoikhai;
            this.trangthai = trangthai;
            this.ngaykhai = ngaykhai;
            this.honnhan = honnhan;
        }

        public CongDan(DataRow row)
        {
            this.macd = (string)row["MaCD"];
            this.hoten = (string)row["HoTen"];
            this.ngaysinh = row["NgaySinh"].ToString();
            this.noisinh = (string)row["NoiSinh"];
            this.gioitinh = (string)row["GioiTinh"];
            this.nghenghiep = (string)row["NgheNghiep"];
            this.dantoc = (string)row["DanToc"];
            this.tongiao = (string)row["TonGiao"];
            this.honnhan = (string)row["HonNhan"];
            this.tinhtrang = (string)row["TinhTrang"];
            this.tentk = (string)row["TenTK"];
            this.matkhau = (string)row["MatKhau"]; ;
            this.loaitk = (string)row["LoaiTK"];
            this.nguoikhai = (string)row["NguoiKhai"];
            this.trangthai = (string)row["TrangThai"];
            this.ngaykhai = row["NgayKhai"].ToString();
        }
    }
}
