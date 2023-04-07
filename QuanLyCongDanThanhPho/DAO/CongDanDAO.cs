using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            try
            {
                string strQuery = string.Format($"UPDATE {CONGDAN} SET {HONNHAN} = N'{trangthaihonnhan}' WHERE {MACD} = N'{Macd}'");
                if (!DBConnection.Instance.Execute(strQuery))
                {
                    throw new Exception();
                }

            }
            catch
            {
                MessageBox.Show("Cập nhật thất bại");
            }
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
        public CongDan LayCongDanBangID(string id)
        {
            try
            {
                string strQuery = string.Format($"SELECT * FROM {CONGDAN} WHERE {MACD} = '{id}'");
                DataTable dt = DBConnection.Instance.LayDanhSach(strQuery);
                CongDan cd = new CongDan(dt.Rows[0]);
                return cd;
            }
            catch
            {
                MessageBox.Show("Công dân không tồn tại");
            }
            return null;
        }
        public DataTable LayDanhSach(string id)
        {
            string strQuery = string.Format($"SELECT MaCD AS [Mã công dân], HoTen AS [Họ tên], NgaySinh AS [Ngày sinh], NoiSinh AS [Nơi sinh], GioiTinh AS [Giới tính], NgheNghiep AS [Nghề nghiệp], DanToc AS [Dân tộc], TonGiao AS [Tôn giáo], TinhTrang AS [Tình trạng], HonNhan AS [Hôn nhân], TenTK AS [Tên tài khoản], MatKhau AS [Mật khẩu], LoaiTK AS [Loại tài khoản] FROM {CONGDAN} WHERE {MACD} = '{id}'");
            return DBConnection.Instance.LayDanhSach(strQuery);
        }
        public DataTable LayDanhSach()
        {
            string sqlStr = string.Format("SELECT MaCD AS [Mã công dân], HoTen AS [Họ tên], NgaySinh AS [Ngày sinh], NoiSinh AS [Nơi sinh], GioiTinh AS [Giới tính], NgheNghiep AS [Nghề nghiệp], DanToc AS [Dân tộc], TonGiao AS [Tôn giáo], TinhTrang AS [Tình trạng], HonNhan AS [Hôn nhân], TenTK AS [Tên tài khoản], MatKhau AS [Mật khẩu], LoaiTK AS [Loại tài khoản] FROM dbo.CongDan");
            return DBConnection.Instance.LayDanhSach(sqlStr);
        }
        public void DoiMatKhau(CongDan cd, string matkhaumoi)
        {
            try
            {
                string strQuery = string.Format($"UPDATE {CONGDAN} SET {MATKHAU} = N'{matkhaumoi}' WHERE MaCD = N'{cd.Macd}'");
                if (!DBConnection.Instance.Execute(strQuery))
                {
                    throw new Exception();
                }
                cd.Matkhau = matkhaumoi;
            }
            catch
            {
                MessageBox.Show("Đổi mật khẩu thât bại");
            }
        }
        public void Xoa(string Macd)
        {
            try
            {
                string strSQL = string.Format($"DELETE FROM {CONGDAN} WHERE {MACD} = '{Macd}'");
                if(!DBConnection.Instance.Execute(strSQL))
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("Xóa thất bại");
            }
        }
        public bool Update(CongDan cd)
        {
            //CongDan cd = new CongDan(macd, hoten, ngaysinh, noisinh, gioitinh, nghenghiep, dantoc, tongiao, honnhan, tinhtrang, "", "", loaitk, "", "", "");
            string strSQL = string.Format(
                $"UPDATE {CONGDAN} " +
                $"SET {HOTEN} = N'{cd.Hoten}', {NGAYSINH} = N'{cd.Ngaysinh}', {NOISINH} = N'{cd.Noisinh}', {GIOITINH} = N'{cd.Gioitinh}', {NGHENGHIEP} = N'{cd.Nghenghiep}', {DANTOC} = N'{cd.Dantoc}', {TONGIAO} = N'{cd.Tongiao}' " +
                $"WHERE {MACD} = N'{cd.Macd}'");
            return DBConnection.Instance.Execute(strSQL);
        }
    }
}
