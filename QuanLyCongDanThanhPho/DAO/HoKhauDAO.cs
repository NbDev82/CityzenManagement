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
        private readonly string HOKHAU = "Households";
        private readonly string CHITIETHOKHAU = "Detail_Households";
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
        private readonly string CONGDAN = "Citizens";
        private readonly string KHAISINH = "Births";
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
        public DataTable LayThongTinThanhVien(int macd)
        {
            string strSQL = string.Format($"" +
                $"SELECT * " +
                $"FROM {CONGDAN}, {CHITIETHOKHAU}, {KHAISINH} " +
                $"WHERE {CONGDAN}.{MACD} = {CHITIETHOKHAU}.{MACD} AND {CONGDAN}.{MACD} = {KHAISINH}.{MACD} AND {CONGDAN}.{MACD} = {macd}");
            DataTable dt = DBConnection.Instance.LayDanhSach(strSQL);
            return dt;
        }
        public int LayMaHo(int Macd)
        {
            try
            {
                string strSQL = string.Format($"SELECT {MAHO} FROM {CHITIETHOKHAU} WHERE {MACD} = {Macd}");
                DataTable dt = DBConnection.Instance.LayDanhSach(strSQL);
                int maHo = (int)dt.Rows[0][MAHO];
                return maHo;
            }
            catch
            {
                return -1;
            }
        }
        public bool Fill(int MaHo, List<Control> ltext)
        {
            DataTable HoKhau = LayDanhSach(MaHo,HOKHAU);
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
        public DataTable GetHoKhauByID(int maHo)
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
        public DataTable GetHoKhau()
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
        public bool AddToChiTietHoKhau(int macd, int MaHo, string quanhe)
        {
            try
            {
                string strSQL = string.Format($"INSERT INTO {CHITIETHOKHAU}({MAHO},{MACD},{TINHTRANGCUTRU},{QUANHEVOICHUHO},{NGAYDANGKY},{TRANGTHAI}) " +
                                              $"VALUES({MaHo},{macd},N'Thường trú',N'{quanhe}',N'{DateTime.Now}',N'Chưa Duyệt')");
                if (DBConnection.Instance.Execute(strSQL))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public int LayMaHoTuHoKhau(int Machuho)
        {
            try
            {
                string strSQL = string.Format($"SELECT * FROM {HOKHAU} WHERE ChuHo = {Machuho} ");
                DataTable dt = DBConnection.Instance.LayDanhSach(strSQL);
                int maho = (int)dt.Rows[0]["MaHo"];
                return maho;
            }
            catch
            {
                return -1;
            }
        }
        public bool AddToHoKhau(HoKhau hk)
        {
            try
            {
                string trangThai = hk.Trangthai == 1 ? "Đã Duyệt" : "Chưa Duyệt";
                string strSQL = string.Format($"INSERT INTO {HOKHAU}({CHUHO},{TINHTHANH},{QUANHUYEN},{PHUONGXA},{NGAYDANGKY},{TRANGTHAI}) " +
                                              $"VALUES({hk.Chuho},N'{hk.Tinhthanh}',N'{hk.Quanhuyen}',N'{hk.Phuongxa}',N'{hk.Ngaydangky}',N'{trangThai}')");
                if (DBConnection.Instance.Execute(strSQL))
                {
                    int MaHo = LayMaHoTuHoKhau(hk.Chuho);
                    if (MaHo < 0)
                        return false;
                    string addToDetailHouseholds = string.Format($"INSERT INTO {CHITIETHOKHAU}(MaHo,MaCD,{TINHTRANGCUTRU},{QUANHEVOICHUHO},{NGAYDANGKY},{TRANGTHAI}) " +
                                                                 $"VALUES({MaHo},N'{hk.Chuho}',N'Thường trú',N'Chủ hộ',N'{hk.Ngaydangky}',N'{trangThai}')");
                    DBConnection.Instance.Execute(addToDetailHouseholds);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(int macd)
        {
            string strSQL = string.Format($"DELETE FROM {CHITIETHOKHAU} WHERE {MACD} = {macd} ");
            return DBConnection.Instance.Execute(strSQL);
        }
    }
}
