using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCongDanThanhPho.DAO
{
    internal class HonNhanDAO
    {
        private readonly string HONNHAN = "People_Marriage";
        private readonly string MAHN = "MaHN";
        private readonly string MACDCHONG = "MaCDChong";
        private readonly string MACDVO = "MaCDVo";
        private readonly string LOAI = "Loai";
        private readonly string NGAYDANGKY = "NgayDangKy";
        private readonly string XACNHANLAN1 = "XacNhanLan1";
        private readonly string XACNHANLAN2 = "XacnhanLan2";
        private readonly string TRANGTHAI = "TrangThai";
        private static HonNhanDAO instance;
        public static HonNhanDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new HonNhanDAO();
                return instance;
            }
        }
        public int GetMaHN(int macdchong, int macdvo)
        {
            string strSQL = string.Format($"SELECT* " +
                                          $"FROM {HONNHAN} " +
                                          $"WHERE {MACDCHONG} = {macdchong} AND {MACDVO} = {macdvo}");
            DataTable dt = DBConnection.Instance.LayDanhSach(strSQL);
            if (dt == null)
                return -1;
            int mahn = (int)dt.Rows[0]["MaHN"];
            return mahn;
        }
        public void CapNhatTrangThaiHonNhan(HonNhan hn)
        {
            try
            {
                string strSQL = string.Format($"UPDATE {HONNHAN} SET {TRANGTHAI} = N'{hn.Trangthai}' WHERE {MAHN} = {hn.Mahn}");
                int MaCdNam = hn.Macdchong;
                int MaCdNu = hn.Macdvo;
                if (!DBConnection.Instance.Execute(strSQL))
                {
                    throw new Exception();
                }
                CongDanDAO.Instance.CapNhatTrangThaiHonNhan(MaCdNam,hn.Mahn);
                CongDanDAO.Instance.CapNhatTrangThaiHonNhan(MaCdNu, hn.Mahn);
            }
            catch
            {
                MessageBox.Show("Duyệt thất bại");
            }
        }
        public int GetMaHNMax()
        {
            try
            {
                string strSQL = string.Format($"SELECT MAX({MAHN}) AS max FROM {HONNHAN}");
                return DBConnection.Instance.GetMaHNMax(strSQL);
            }
            catch
            {
                return -1;
            }
        }
        public bool isExist(int macdNam, int macdNu, string loai)
        {
            string strSQL = string.Format($"SELECT {MACDCHONG}, {MACDVO} FROM {HONNHAN} WHERE ({MACDCHONG} = {macdNam} OR {MACDVO} = {macdNu}) OR ({MACDVO} = {macdNam} OR {MACDCHONG} = {macdNu})");
            DataTable dtHN = DBConnection.Instance.LayDanhSach(strSQL);
            if (dtHN.Rows.Count == 1)
            {
                int GetMaCDNam = (int)dtHN.Rows[0][MACDCHONG];
                int GetMaCDNu = (int)dtHN.Rows[0][MACDVO];
                if(loai == "Ly hôn")
                {
                    if (macdNu == GetMaCDNu && macdNam == GetMaCDNam)
                        return true;
                }
                else
                {
                    if (macdNam == GetMaCDNu || macdNam == GetMaCDNam)
                        return true;
                    if (macdNu == GetMaCDNam || macdNu == GetMaCDNu)
                        return true;
                }
                return false;
            }
            return false;
        }
        public bool CheckXacNhan(int Macd)
        {
            string strSQL = string.Format($"SELECT {XACNHANLAN1},{XACNHANLAN2} " +
                                          $"FROM {HONNHAN} " +
                                          $"WHERE {XACNHANLAN1} = {Macd} OR {XACNHANLAN2} = {Macd}");
            return DBConnection.Instance.CheckXacNhan(strSQL);
        }
        public void TimKiemHonNhan(DataGridView dtgv, int mahn)
        {
            try
            {
                string strSQL = string.Format($"SELECT * " +
                                              $"FROM {HONNHAN} " +
                                              $"WHERE {MAHN} = {mahn}"); 
                DataTable dt = DBConnection.Instance.LayDanhSach(strSQL);
                if(dt.Rows.Count > 0)
                    dtgv.DataSource = dt;
                else
                {
                    dtgv.DataSource =  null;
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Không tìm thấy hồ sơ hôn nhân");
            }
        }
        public void Load(DataGridView dtgv, string strQuery)
        {
            try
            {
                DataTable dtMail = DBConnection.Instance.LayDanhSach(strQuery);
                if (dtMail != null)
                    dtgv.DataSource = dtMail;
                else
                    dtgv.DataSource = null;
            }
            catch
            {
                MessageBox.Show("ERROR");
            }
        }
        public void LoadMailControler(DataGridView dtgv)
        {
            try
            {
                string strQuery = string.Format($"SELECT * " +
                                                $"FROM {HONNHAN} " +
                                                $"WHERE {XACNHANLAN1} IS NOT NULL AND {XACNHANLAN2} IS NOT NULL " +
                                                $"AND {TRANGTHAI} = N'Chưa Duyệt'");
                Load(dtgv, strQuery);
            }
            catch
            {
                MessageBox.Show("ERROR");
            }
        }
        public void LoadFormPersonal(DataGridView dtgv, CongDan cd)
        {
            try
            {
                string strQuery = string.Format($"SELECT * " +
                                                $"FROM {HONNHAN} " +
                                                $"WHERE {MACDCHONG} = {cd.Macd} OR {MACDVO} = {cd.Macd}");
                Load(dtgv, strQuery);
            }
            catch
            {
                MessageBox.Show("ERROR");
            }
        }
        public bool Create(HonNhan hn)
        {
            string strQuery = string.Format($"INSERT INTO {HONNHAN}({MACDCHONG} , {MACDVO}, {LOAI}, {NGAYDANGKY},{TRANGTHAI}) " +
                $"VALUES ( {hn.Macdchong}, {hn.Macdvo},N'{hn.Loai}','{hn.Ngaydangky}',N'{hn.Trangthai}')");
            return DBConnection.Instance.Execute(strQuery);
        }
        public HonNhan ReadByID(int MaCDChong, int MaCDVo)
        {
            string strQuery = string.Format($"SELECT * FROM {HONNHAN} WHERE ({MACDCHONG} = {MaCDChong} AND {MACDVO} = {MaCDVo}) OR ({MACDCHONG} = {MaCDVo} AND {MACDVO} = {MaCDChong})");
            DataTable dataTable = DBConnection.Instance.LayDanhSach(strQuery);
            if(dataTable == null)
                return null;
            HonNhan honNhan = new HonNhan(dataTable.Rows[0]);
            return honNhan;
        }
        public HonNhan ReadByID(int Macd)
        {
            string strQuery = string.Format($"SELECT * FROM {HONNHAN} WHERE {MACDCHONG} = {Macd} OR {MACDVO} = {Macd}");
            DataTable dataTable = DBConnection.Instance.LayDanhSach(strQuery);
            HonNhan honNhan = new HonNhan(dataTable.Rows[0]);
            return honNhan;
        }
        public void Update(HonNhan hn, int xacNhan, string lanXacNhan)
        {
            try
            {
                string strQuery = string.Format(
                    $"UPDATE {HONNHAN} " +
                    $"SET {lanXacNhan} = {xacNhan} " +
                    $"WHERE {MACDCHONG} = {hn.Macdchong} AND {MACDVO} = {hn.Macdvo}");
                if (DBConnection.Instance.Execute(strQuery))
                {
                    MessageBox.Show("Cập nhật thành công");
                }
                else
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("Cập nhật Thất bại");
            }
        }
        public bool Delete(HonNhan hn)
        {
            try
            {
                string strQuery = string.Format($"DELETE FROM {HONNHAN} " +
                                                $"WHERE {MAHN} = {hn.Mahn}");
                return DBConnection.Instance.Execute(strQuery);
            }
            catch
            {
                return false;
            }
        }
        public void SendForm(HonNhan hn)
        {
            if(hn.Loai == "Kết hôn")
            {
                if (Create(hn))
                {
                    MessageBox.Show("Gửi thành công");
                }
                else
                {
                    MessageBox.Show("Gửi thất bại");
                }
            }
            else if (hn.Loai == "Ly hôn")
            {
                if (ConvertMarriageToDivorce(hn))
                {
                    MessageBox.Show("Gửi thành công");
                }
                else
                {
                    MessageBox.Show("Gửi thất bại");
                }
            }
        }
        public bool ConvertDivorceToMarriage(HonNhan hn)
        {
            try
            {
                string strSQL = string.Format($"UPDATE {HONNHAN} SET {LOAI} = N'Kết hôn', {XACNHANLAN1} = {hn.Macdchong}, {XACNHANLAN2} = {hn.Macdvo}, {TRANGTHAI} = N'Đã duyệt' WHERE {HONNHAN}.{MAHN} = {hn.Mahn}");
                if (DBConnection.Instance.Execute(strSQL))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool ConvertMarriageToDivorce(HonNhan hn)
        {
            try
            {
                string strSQL = string.Format($"UPDATE {HONNHAN} SET {LOAI} = N'Ly hôn', {XACNHANLAN1} = NULL, {XACNHANLAN2} = NULL, {TRANGTHAI} = N'{hn.Trangthai}' WHERE {HONNHAN}.{MAHN} = {hn.Mahn}");
                if (DBConnection.Instance.Execute(strSQL))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
