using QuanLyCongDanThanhPho.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCongDanThanhPho.DAO
{
    internal class AccountsDAO
    {
        private readonly string ACCOUNT = "Accounts";
        private readonly string MACD = "MaCD";
        private readonly string MATKHAU = "matkhau";
        private static AccountsDAO instance;
        public static AccountsDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new AccountsDAO();
                return instance;
            }
        }
        public int GetQuyenHan(int Macd)
        {
            try
            {
                string SQL = string.Format($"SELECT * FROM Accounts ac WHERE ac.macd = {Macd}");
                DataTable dt = DBConnection.Instance.LayDanhSach(SQL);
                int quyenhan = 0;
                quyenhan = (int)dt.Rows[0]["phanquyen"];
                return quyenhan;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public TaiKhoan GetAccount(int macd, string matkhau)
        {
            try
            {
                string strGetAccountSQL = string.Format($"SELECT * FROM {ACCOUNT} WHERE MACD = N'{macd}' AND matkhau = N'{matkhau}'");
                DataTable result = DBConnection.Instance.LayDanhSach(strGetAccountSQL);
                if (result != null)
                {
                    foreach (DataRow dr in result.Rows)
                    {
                        return new TaiKhoan(dr);
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public CongDan AuthenticateAccount(int macd, string matkhau)
        {
            try
            {
                string strGetCitizenSQL = string.Format($"SELECT * FROM {ACCOUNT},Citizens  WHERE {ACCOUNT}.{MACD} = Citizens.{MACD} AND Citizens.{MACD} = {macd} AND {MATKHAU} = {matkhau}");
                DataTable result = DBConnection.Instance.LayDanhSach(strGetCitizenSQL);
                if (result != null)
                {
                    foreach (DataRow dr in result.Rows)
                    {
                        return new CongDan(dr);
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public bool DoiMatKhau(int macd, string matkhaumoi)
        {
            try
            {
                string strQuery = string.Format($"UPDATE {ACCOUNT} SET {MATKHAU} = N'{matkhaumoi}' WHERE MaCD = {macd}");
                if (DBConnection.Instance.Execute(strQuery))
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
