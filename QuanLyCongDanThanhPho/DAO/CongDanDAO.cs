﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCongDanThanhPho
{
    internal class CongDanDAO
    {
        private readonly string CONGDAN = "CongDan";
        private readonly string MACD = "MaCD";
        private readonly string HOTEN = "HoTen";
        private readonly string NGAYSINH = "NgaySinh";
        private readonly string NOISINH = "NoiSinh";
        private readonly string GIOITINH = "GioiTinh";
        private readonly string NGHENGHIEP = "NgheNghiep";
        private readonly string TINHTRANG = "TinhTrang";
        private readonly string HONNHAN = "HonNhan";
        private readonly string DANTOC = "DanToc";
        private readonly string TONGIAO = "TonGiao";
        private readonly string TENTK = "TenTK";
        private readonly string MATKHAU = "MatKhau";
        private readonly string LOAITK = "LoaiTK";
        private readonly string NGUOIKHAI = "NguoiKhai";
        private readonly string NGAYKHAI = "NgayKhai";
        private readonly string TRANGTHAI = "TrangThai";

        private static CongDanDAO instance;
        public static CongDanDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new CongDanDAO();
                return instance;
            }
        }
        public void CapNhatTrangThaiHonNhan(string Macd,string trangthaihonnhan)
        {
            string strQuery = string.Format($"UPDATE {CONGDAN} SET {HONNHAN} = N'{trangthaihonnhan}' WHERE {MACD} = N'{Macd}'");
            DBConnection.Instance.Execute(strQuery);
        }
        public CongDan KiemTraDangNhap(string tentk, string matkhau)
        {
            string query = string.Format("SELECT * FROM dbo.CongDan WHERE TenTK = N'{0}' AND MatKhau = N'{1}'", tentk, matkhau);
            DataTable result = DBConnection.Instance.LayDanhSach(query);

            foreach (DataRow dr in result.Rows)
            {
                return new CongDan(dr);
            }
            return null;
        }
        public string GetControler()
        {
            string strQuery = string.Format($"SELECT {TENTK} FROM {CONGDAN} WHERE {LOAITK} = N'Quản lý'");
            DataTable dataTable = DBConnection.Instance.LayDanhSach(strQuery);
            string tk = dataTable.Rows[0]["TenTK"].ToString();
            return tk;
        }
        public DataTable LayDanhSach(string id)
        {
            string strQuery = string.Format($"SELECT * FROM {CONGDAN} WHERE {MACD} = '{id}'");
            return DBConnection.Instance.LayDanhSach(strQuery);
        }
        public void DoiMatKhau(CongDan cd, string matkhaumoi)
        {
            string sqlStr = string.Format($"UPDATE {CONGDAN} SET {MATKHAU} = N'{matkhaumoi}' WHERE MaCD = N'{cd.Macd}'");
            DBConnection.Instance.Execute(sqlStr);
            cd.Matkhau = matkhaumoi;
        }
    }
}