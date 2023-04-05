using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCongDanThanhPho.DAO
{
    internal class HoKhauDAO
    {
        private readonly string HOKHAU = "HoKhau";
        private readonly string CHITIETHOKHAU = "ChiTietHoKhau";
        private readonly string MAHO = "MaHo";
        private readonly string CHUHO = "ChuHo";
        private readonly string TINHTHANH = "TinhThanh";
        private readonly string QUANHUYEN = "QuanHuyen";
        private readonly string PHUONGXA = "PhuongXa";
        private readonly string NGAYDANGKY = "NgayDangKy";
        private readonly string TRANGTHAI = "TrangThai";

        private readonly string MACD = "MaCD";
        private readonly string TINHTRANGCUTRU = "TinhTrangCuTru";
        private readonly string QUANHEVOICHUHO = "QuanHeVoiChuHo";

        private readonly string CONGDAN = "CongDan";
        private static HoKhauDAO instance;
        public static HoKhauDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new HoKhauDAO();
                return instance;
            }
        } 
        public DataTable LayThongTinThanhVien(string macd)
        {
            string strSQL = string.Format($"" +
                $"SELECT * " +
                $"FROM {CONGDAN}, {CHITIETHOKHAU} " +
                $"WHERE {CONGDAN}.{MACD} = {CHITIETHOKHAU}.{MACD} AND {CONGDAN}.{MACD} = '{macd}'");
            DataTable dt = DBConnection.Instance.LayDanhSach(strSQL);
            return dt;
        }
        public int LayMaHo(string Macd)
        {
            try
            {
                string strSQL = string.Format($"SELECT {MAHO} FROM {CHITIETHOKHAU} WHERE {MACD} = '{Macd}'");
                DataTable dt = DBConnection.Instance.LayDanhSach(strSQL);
                int maHo = int.Parse(dt.Rows[0]["MaHo"].ToString());
                return maHo;
            }
            catch
            {
                return -1;
            }
        }
        public bool Fill(int MaHo, List<Control> ltext)
        {
            DataTable HoKhau = LayDanhSach(MaHo,"HoKhau");
            if (HoKhau.Rows.Count > 0)
            {
                int i = 0;
                foreach (Control c in ltext)
                {
                    c.Text = HoKhau.Rows[0][i++].ToString();
                }
                return true;
            }
            return false;
        }
        public DataTable LayDanhSach(int maHo, string Loai)
        {
            string strSQL = string.Format($"SELECT * FROM {Loai} WHERE {MAHO} = {maHo}");
            DataTable HoKhau = DBConnection.Instance.LayDanhSach(strSQL);
            return HoKhau;
        }
        public DataTable LayDanhSachHoKhau(int maHo)
        {
            try
            {
                string strSQL = string.Format($"SELECT * FROM {HOKHAU} WHERE {MAHO} = {maHo}");
                DataTable HoKhau = DBConnection.Instance.LayDanhSach(strSQL);
                if (HoKhau.Rows.Count <= 0)
                    throw new Exception();
                return HoKhau;
            }
            catch
            {
                MessageBox.Show("Không tồn tại hộ khẩu");
            }
            return null;
        }
        public DataTable LayDanhSachHoKhau()
        {
            try
            {
                string strSQL = string.Format($"SELECT * FROM {HOKHAU}");
                DataTable HoKhau = DBConnection.Instance.LayDanhSach(strSQL);
                if (HoKhau.Rows.Count <= 0)
                    throw new Exception();
                return HoKhau;
            }
            catch
            {
                MessageBox.Show("ERROR");
            }
            return null;
        }
        public bool Add(string macd, string MaHo, string quanhe)
        {
            try
            {
                string strSQL = string.Format($"INSERT INTO {CHITIETHOKHAU}({MAHO},{MACD},{TINHTRANGCUTRU},{QUANHEVOICHUHO},{NGAYDANGKY},{TRANGTHAI}) " +
                                              $"VALUES({MaHo},N'{macd}',N'Thường trú',N'{quanhe}',N'{DateTime.Now}',N'Chưa Duyệt')");
                if (DBConnection.Instance.Execute(strSQL))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool Add(HoKhau hk)
        {
            try
            {
                string trangThai = hk.Trangthai == 1 ? "Đã Duyệt" : "Chưa Duyệt";
                string strSQL = string.Format($"INSERT INTO {HOKHAU}({CHUHO},{TINHTHANH},{QUANHUYEN},{PHUONGXA},{NGAYDANGKY},{TRANGTHAI}) " +
                                              $"VALUES('{hk.Chuho}','{hk.Tinhthanh}','{hk.Quanhuyen}','{hk.Phuongxa}','{hk.Ngaydangky}','{trangThai}')");
                if (DBConnection.Instance.Execute(strSQL))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool Update(CongDan cd)
        {

            return true;
        }
        public bool Delete(string macd)
        {
            string strSQL = string.Format($"DELETE FROM {CHITIETHOKHAU} WHERE {MACD} = '{macd}'");
            return DBConnection.Instance.Execute(strSQL);
        }
    }
}
