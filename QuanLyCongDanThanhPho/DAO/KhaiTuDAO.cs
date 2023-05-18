using QuanLyCongDanThanhPho.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCongDanThanhPho.DAO
{
    internal class KhaiTuDAO
    {
        private readonly string KHAITU = "Users_Deleted";
        private readonly string MACD = "MaCD";
        private readonly string NGUOIKHAI = "NguoiKhai";
        private readonly string NGUYENNHAN = "NguyenNhan";
        private readonly string NGAYTU = "NgayTu";
        private readonly string NGAYKHAI = "NgayKhai";
        private readonly string NGAYDUYET = "NgayDuyet";
        private static KhaiTuDAO instance;
        public static KhaiTuDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new KhaiTuDAO();
                return instance;
            }
        }
        public DataTable GetDataTable(int Macd)
        {
            try
            {
                string strSQL = string.Format($"SELECT * " +
                                          $"FROM {KHAITU}, Citizens " +
                                          $"WHERE {KHAITU}.{MACD} = Citizens.{MACD} AND {KHAITU}.{MACD} = {Macd}");
                DataTable dt = DBConnection.Instance.LayDanhSach(strSQL);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        public DataTable GetDataTable()
        {
            try
            {
                string strSQL = string.Format($"SELECT * " +
                                          $"FROM {KHAITU}, Citizens " +
                                          $"WHERE {KHAITU}.{MACD} = Citizens.{MACD}");
                DataTable dt = DBConnection.Instance.LayDanhSach(strSQL);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        public bool Add(KhaiTu kt)
        {
            try
            {
                string strSQL = string.Format($"INSERT INTO {KHAITU}({MACD},{NGUOIKHAI},{NGUYENNHAN},{NGAYTU},{NGAYKHAI}) " +
                                          $"VALUES({kt.Macd},{kt.Nguoikhai},N'{kt.Nguyennhan}',N'{kt.Ngaytu}',N'{kt.Ngaykhai}')");
                if (DBConnection.Instance.Execute(strSQL))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateNgayDuyet(int MaCD, string Ngayduyet)
        {
            try
            {
                string strSQL = string.Format($"UPDATE {KHAITU} " +
                                          $"SET {NGAYDUYET} = N'{Ngayduyet}' " +
                                          $"WHERE {MACD} = {MaCD}");
                return DBConnection.Instance.Execute(strSQL);
            }
            catch { return false; }
        }
        public bool Delete(int Macd)
        {
            try
            {
                string strSQL = string.Format($"DELETE FROM {KHAITU} " +
                                                $"WHERE {MACD} = {Macd}");
                return DBConnection.Instance.Execute(strSQL);
            }
            catch
            {
                return false;
            }
        }
    }
}
