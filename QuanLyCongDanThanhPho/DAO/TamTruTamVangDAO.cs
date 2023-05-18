using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCongDanThanhPho.DAO
{
    internal class TamTruTamVangDAO
    {
        private static TamTruTamVangDAO instance;
        public static TamTruTamVangDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new TamTruTamVangDAO();
                return instance;
            }
        }
        public DataRow GetPersonalData(int Macd)
        {
            try
            {
                int MaHo = HoKhauDAO.Instance.LayMaHo(Macd);
                string strSQL = string.Format($"SELECT Citizens.MaCD AS N'Mã CD', HoTen AS N'Họ tên', MaCCCD AS N'Mã CCCD', NgaySinh AS N'Ngày sinh', TinhThanh AS N'Tỉnh', QuanHuyen AS N'Huyện', PhuongXa AS N'Xã' " +
                                              $"FROM Citizens, Certificates, Births, Households " +
                                              $"WHERE Citizens.MaCD = Certificates.MaCD AND Citizens.MaCD = Births.MaCD AND Households.MaHo = {MaHo} AND Citizens.MaCD = {Macd}");
                DataTable dt = DBConnection.Instance.LayDanhSach(strSQL);
                return dt.Rows[0];
            }
            catch
            {
                return null;
            }
        }
    }
}
