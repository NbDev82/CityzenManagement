using QuanLyCongDanThanhPho.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCongDanThanhPho.DAO
{
    internal class TamTruDAO
    {

        private readonly string Temporarily_Staying = "Temporarily_Staying";
        private readonly string ID = "ID";
        private readonly string MaCD = "MaCD";
        private readonly string MaCCCD = "MaCCCD";
        private readonly string Tinh = "Tinh";
        private readonly string Huyen = "Huyen";
        private readonly string Xa = "Xa";
        private readonly string LyDo = "LyDo";
        private readonly string thoi_gian_bat_dau = "thoi_gian_bat_dau";
        private readonly string TrangThai = "TrangThai";
        private static TamTruDAO instance;
        public static TamTruDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new TamTruDAO();
                return instance;
            }
        }
        public bool Delete(TamTru tt)
        {
            try
            {
                if (tt == null)
                    return false;
                string strSQL = string.Format($"DELETE FROM {Temporarily_Staying} " +
                                              $"WHERE {Temporarily_Staying}.{ID} = {tt.ID}");
                return DBConnection.Instance.Execute(strSQL);
            }
            catch
            {
                return false;
            }
        }
        public bool SetAccess(TamTru tt)
        {
            try
            {
                if (tt == null)
                    return false;
                string strSQL = string.Format($"UPDATE  {Temporarily_Staying} " +
                                              $"SET {TrangThai} = N'Đã duyệt' " +
                                              $"WHERE {ID} = {tt.ID}");
                return DBConnection.Instance.Execute(strSQL);
            }
            catch
            {
                return false;
            }
        }
        public bool Add(TamTru tt)
        {
            try
            {
                if (tt == null)
                    return false;
                string strSQL = string.Format($"INSERT INTO {Temporarily_Staying}({MaCD},{MaCCCD},{Tinh},{Huyen},{Xa},{LyDo},{thoi_gian_bat_dau}) " +
                                              $"VALUES ({tt.MaCD},N'{tt.MaCCCD}',N'{tt.Tinh}',N'{tt.Huyen}',N'{tt.Xa}',N'{tt.LyDo}',N'{tt.thoi_gian_bat_dau}')");
                return DBConnection.Instance.Execute(strSQL);
            }
            catch
            {
                return false;
            }
        }
        public DataTable GetPartDataByMaCD(int Macd)
        {
            string strSQL = string.Format($"SELECT ID AS N'ID', MaCD AS N'Mã CD', MaCCCD AS N'Mã CCCD', Tinh AS N'Tỉnh', Huyen AS N'Huyện', Xa AS N'Xã' , LyDo AS N'Lý do', {thoi_gian_bat_dau} AS N'Thời gian bắt đầu', TrangThai AS N'Trạng thái'" +
                                            $"FROM Temporarily_Staying " +
                                            $"WHERE Temporarily_Staying.MaCD = {Macd}");
            DataTable dt = DBConnection.Instance.LayDanhSach(strSQL);
            return dt;
        }
        public DataTable GetEntireData()
        {
            string strSQL = string.Format($"SELECT ID AS N'ID', MaCD AS N'Mã CD', MaCCCD AS N'Mã CCCD', Tinh AS N'Tỉnh', Huyen AS N'Huyện', Xa AS N'Xã' , LyDo AS N'Lý do', {thoi_gian_bat_dau} AS N'Thời gian bắt đầu', TrangThai AS N'Trạng thái' " +
                                            $"FROM Temporarily_Staying");
            DataTable dt = DBConnection.Instance.LayDanhSach(strSQL);
            return dt;
        }
    }
}
