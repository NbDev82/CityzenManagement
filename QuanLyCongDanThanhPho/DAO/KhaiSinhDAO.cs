using QuanLyCongDanThanhPho.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCongDanThanhPho.DAO
{
    internal class KhaiSinhDAO
    {
        private readonly string KHAISINH = "Births";
        private readonly string MACD = "MACD";
        private readonly string NGAYSINH = "NGAYSINH";
        private readonly string NOISINH = "NOISINH";
        private readonly string MACDCHA = "MACDCHA";
        private readonly string MACDME = "MACDME";
        private readonly string NGAYKHAI = "NGAYKHAI";
        private readonly string NGAYDUYET = "NGAYDUYET";
        private static KhaiSinhDAO instance;
        public static KhaiSinhDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new KhaiSinhDAO();
                return instance;
            }
        }
        public bool UpdateNgaySinhNoiSinh(KhaiSinh ks)
        {
            try
            {
                string strSQL = string.Format($"UPDATE {KHAISINH} " +
                                          $"SET {NGAYSINH} = N'{ks.NgaySinh}', {NOISINH} = N'{ks.NoiSinh}' " +
                                          $"WHERE {MACD} = {ks.MaCD}");
                return DBConnection.Instance.Execute(strSQL);
            }
            catch { return false; }
        }
        public bool CreateObject(KhaiSinh ks, CongDan cd)
        {
            try
            {
                bool resAddCD = CongDanDAO.Instance.Add(cd);
                if (!resAddCD)
                    return false;
                string strSQLGetIDCongDan = string.Format($"SELECT MAX(MaCD) AS MaCD FROM dbo.Citizens");
                int ID = DBConnection.Instance.GetID(strSQLGetIDCongDan);
                ks.MaCD = ID;
                bool resAddKS = Add(ks);
                if (resAddKS && resAddCD)
                    return true;
                CongDanDAO.Instance.Xoa(ID);
                return false;
            }
            catch
            {
                return false;
            }
        }
        public DataTable GetDataTable(int Macd)
        {
            try
            {
                string strSQL = string.Format($"SELECT * FROM Citizens, Births WHERE Citizens.MaCD = Births.MaCD AND Citizens.MaCD = {Macd}");
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
                string strSQL = string.Format($"SELECT * FROM Citizens, Births WHERE Citizens.MaCD = Births.MaCD");
                DataTable dt = DBConnection.Instance.LayDanhSach(strSQL);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        public KhaiSinh GetKhaiSinhByID(int Macd)
        {
            try
            {
                string strSQL = string.Format($"SELECT * " +
                                              $"FROM Births " +
                                              $"WHERE MaCD = {Macd}");
                DataTable dt = DBConnection.Instance.LayDanhSach(strSQL);
                if (dt == null)
                    return null;
                KhaiSinh ks = new KhaiSinh(dt.Rows[0]);
                return ks;
            }
            catch
            {
                return null;
            }
        }
        public bool Add(KhaiSinh ks)
        {
            try
            {
                if (ks == null)
                    return false;
                string strSQL = string.Format($"INSERT INTO Births(MaCD,NgaySinh,NoiSinh,MaCD_Cha,MaCD_Me,NgayKhai) " +
                                                $"VALUES({ks.MaCD},N'{ks.NgaySinh}',N'{ks.NoiSinh}',{ks.MaCD_Cha},{ks.MaCD_Me},N'{ks.NgayKhai}')");
                return DBConnection.Instance.Execute(strSQL);
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(int Macd)
        {
            try
            {
                string strSQL = string.Format($"DELETE FROM Births " +
                                                $"WHERE MaCD = {Macd}");
                return DBConnection.Instance.Execute(strSQL);
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
                string strSQL = string.Format($"UPDATE {KHAISINH} " +
                                          $"SET {NGAYDUYET} = N'{Ngayduyet}' " +
                                          $"WHERE {MACD} = {MaCD}");
                return DBConnection.Instance.Execute(strSQL);
            }
            catch { return false; }
        }
    }
}
