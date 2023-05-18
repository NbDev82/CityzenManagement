using QuanLyCongDanThanhPho.DAO;
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
        private readonly string CONGDAN = "Citizens";
        private readonly string MACD = "MaCD";
        private readonly string HOTEN = "HoTen";
        private readonly string GIOITINH = "GioiTinh";
        private readonly string NGHENGHIEP = "NgheNghiep";
        private readonly string DANTOC = "DanToc";
        private readonly string TONGIAO = "TonGiao";
        private readonly string MAHN = "MaHN";
        private readonly string BIRTH = "Births";
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
        public void CapNhatTrangThaiHonNhan(int Macd,int trangthaihonnhan)
        {
            try
            {
                string strQuery = string.Format($"UPDATE {CONGDAN} SET {MAHN} = N'{trangthaihonnhan}' WHERE {MACD} = N'{Macd}'");
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
        public CongDan GetCongDanByMaCD(int id)
        {
            try
            {
                string strQuery = string.Format($"SELECT * FROM {CONGDAN} WHERE {MACD} = {id}");
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
        public DataTable LayDanhSach(int id)
        {
            string strQuery = string.Format($"SELECT * FROM {CONGDAN}, {BIRTH} WHERE {CONGDAN}.{MACD} = {BIRTH}.{MACD} AND {CONGDAN}.{MACD} = {id}");
            return DBConnection.Instance.LayDanhSach(strQuery);
        }
        public DataTable LayDanhSach()
        {
            string strQuery = string.Format($"SELECT MaCD AS [Mã công dân], HoTen AS [Họ tên], GioiTinh AS [Giới tính], NgheNghiep AS [Nghề nghiệp], DanToc AS [Dân tộc], TonGiao AS [Tôn giáo], TinhTrang AS [Tình trạng], MaHN AS [Mã Hôn nhân], MaHoKhau AS [Mã Hộ Khẩu] FROM {CONGDAN}");
            return DBConnection.Instance.LayDanhSach(strQuery);
        }
        public void Xoa(int Macd)
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
            string strSQL = string.Format(
                $"UPDATE {CONGDAN} " +
                $"SET {HOTEN} = N'{cd.Hoten}', {GIOITINH} = N'{cd.Gioitinh}', {NGHENGHIEP} = N'{cd.Nghenghiep}', {DANTOC} = N'{cd.Dantoc}', {TONGIAO} = N'{cd.Tongiao}' " +
                $"WHERE {MACD} = {cd.Macd}");
            return DBConnection.Instance.Execute(strSQL);
        }
        public bool Add(CongDan cd)
        {
            try
            {
                if (cd == null)
                    return false;
                string strSQL = string.Format($"INSERT INTO {CONGDAN}({HOTEN},{GIOITINH},{DANTOC}) " +
                                                $"VALUES(N'{cd.Hoten}',N'{cd.Gioitinh}',N'{cd.Dantoc}')");
                return DBConnection.Instance.Execute(strSQL);
            }
            catch
            {
                return false;
            }
        }
    }
}
