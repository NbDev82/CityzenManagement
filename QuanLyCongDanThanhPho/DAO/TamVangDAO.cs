using QuanLyCongDanThanhPho.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCongDanThanhPho.DAO
{
    internal class TamVangDAO
    {
        private readonly string Temporarily_Absent = "Temporarily_Absent";
        private readonly string ID = "ID";
        private readonly string MaCD = "MaCD";
        private readonly string MaCCCD = "MaCCCD";
        private readonly string Tinh = "Tinh";
        private readonly string Huyen = "Huyen";
        private readonly string Xa = "Xa";
        private readonly string LyDo = "LyDo";
        private readonly string thoi_gian_bat_dau = "thoi_gian_bat_dau";
        private readonly string thoi_gian_ket_thuc = "thoi_gian_ket_thuc";
        private readonly string TrangThai = "TrangThai";
        private static TamVangDAO instance;
        public static TamVangDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new TamVangDAO();
                return instance;
            }
        }
        public DataTable GetListExpiredPermission()
        {
            try
            {
                string SQL = string.Format($"SELECT ID AS N'ID', MaCD AS N'Mã CD', MaCCCD AS N'Mã CCCD', Tinh AS N'Tỉnh', Huyen AS N'Huyện', Xa AS N'Xã' , LyDo AS N'Lý do', {thoi_gian_bat_dau} AS N'Thời gian bắt đầu',{thoi_gian_ket_thuc} AS N'Thời gian kết thúc', TrangThai AS N'Trạng thái' " +
                                           $"FROM {Temporarily_Absent} " +
                                           $"WHERE {thoi_gian_ket_thuc} < CAST('{DateTime.Now.Date}' AS Date) AND {TrangThai} = N'Chưa duyệt'");
                DataTable dt = DBConnection.Instance.LayDanhSach(SQL);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        public DataTable GetListExpired()
        {
            try
            {
                string SQL = string.Format($"SELECT ID AS N'ID', MaCD AS N'Mã CD', MaCCCD AS N'Mã CCCD', Tinh AS N'Tỉnh', Huyen AS N'Huyện', Xa AS N'Xã' , LyDo AS N'Lý do', {thoi_gian_bat_dau} AS N'Thời gian bắt đầu',{thoi_gian_ket_thuc} AS N'Thời gian kết thúc', TrangThai AS N'Trạng thái' " +
                                           $"FROM {Temporarily_Absent} " +
                                           $"WHERE {thoi_gian_ket_thuc} < CAST('{DateTime.Now.Date}' AS Date) AND {TrangThai} = N'Đã duyệt'");
                DataTable dt = DBConnection.Instance.LayDanhSach(SQL);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        public bool Delete(TamVang tv)
        {
            try
            {
                if (tv == null)
                    return false;
                string strSQL = string.Format($"DELETE FROM {Temporarily_Absent} " +
                                              $"WHERE {Temporarily_Absent}.{ID} = {tv.ID}");
                return DBConnection.Instance.Execute(strSQL);
            }
            catch
            {
                return false;
            }
        }
        public bool SetAccess(TamVang tv)
        {
            try
            {
                if (tv == null)
                    return false;
                string strSQL = string.Format($"UPDATE  {Temporarily_Absent} " +
                                              $"SET {TrangThai} = N'Đã duyệt' " +
                                              $"WHERE {ID} = {tv.ID}");
                return DBConnection.Instance.Execute(strSQL);
            }
            catch
            {
                return false;
            }
        }
        public bool Add(TamVang tv)
        {
            try
            {
                if (tv == null)
                    return false;
                string strSQL = string.Format($"INSERT INTO {Temporarily_Absent}({MaCD},{MaCCCD},{Tinh},{Huyen},{Xa},{LyDo},{thoi_gian_bat_dau},{thoi_gian_ket_thuc}) " +
                                              $"VALUES ({tv.MaCD},N'{tv.MaCCCD}',N'{tv.Tinh}',N'{tv.Huyen}',N'{tv.Xa}',N'{tv.LyDo}',N'{tv.thoi_gian_bat_dau}',N'{tv.thoi_gian_ket_thuc}')");
                return DBConnection.Instance.Execute(strSQL);
            }
            catch
            {
                return false;
            }
        }
        public DataTable GetPartDataByMaCD(int Macd)
        {
            string strSQL = string.Format($"SELECT ID AS N'ID', MaCD AS N'Mã CD', MaCCCD AS N'Mã CCCD', Tinh AS N'Tỉnh', Huyen AS N'Huyện', Xa AS N'Xã' , LyDo AS N'Lý do', {thoi_gian_bat_dau} AS N'Thời gian bắt đầu',{thoi_gian_ket_thuc} AS N'Thời gian kết thúc', TrangThai AS N'Trạng thái' " +
                                          $"FROM Temporarily_Absent " +
                                          $"WHERE Temporarily_Absent.MaCD = {Macd}");
            DataTable dt = DBConnection.Instance.LayDanhSach(strSQL);
            return dt;
        }
        public DataTable GetEntireData()
        {
            string strSQL = string.Format($"SELECT ID AS N'ID', MaCD AS N'Mã CD', MaCCCD AS N'Mã CCCD', Tinh AS N'Tỉnh', Huyen AS N'Huyện', Xa AS N'Xã' , LyDo AS N'Lý do', {thoi_gian_bat_dau} AS N'Thời gian bắt đầu',{thoi_gian_ket_thuc} AS N'Thời gian kết thúc', TrangThai AS N'Trạng thái' " +
                                          $"FROM Temporarily_Absent");
            DataTable dt = DBConnection.Instance.LayDanhSach(strSQL);
            return dt;
        }
    }
}
