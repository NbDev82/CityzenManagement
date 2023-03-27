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
        private readonly string HONNHAN = "HonNhan";
        private readonly string MAHN = "MaHN";
        private readonly string MACDCHONG = "MaCDChong";
        private readonly string MACDVO = "MaCDVo";
        private readonly string LOAI = "Loai";
        private readonly string NGAYDANGKY = "NgayDangKy";
        private readonly string XACNHANLAN1 = "XacNhanLan1";
        private readonly string XACNHANLAN2 = "XacnhanLan2";
        private readonly string TRANGTHAI = "TrangThai";

        private readonly string MAIL = "Mail";
        private readonly string MAMAIL = "MaMail";
        private readonly string NGAY = "Ngay";
        private readonly string TIEUDE = "TieuDe";
        private readonly string NGUOIGUI = "NguoiGui";
        private readonly string NGUOINHAN = "NguoiNhan";
        private readonly string NOIDUNG = "NoiDung";

        private readonly string tieude = "DonDangKyKetHon";

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
        public void CapNhatTrangThaiHonNhan(HonNhan hn)
        {
            try
            {
                string strSQL = string.Format($"UPDATE {HONNHAN} SET {TRANGTHAI} = N'{hn.Trangthai}' WHERE {MAHN} = {hn.Mahn}");
                string MaCdNam = hn.Macdchong;
                string MaCdNu = hn.Macdvo;
                string DaKetHon = "Đã kêt hôn";
                DBConnection.Instance.Execute(strSQL);
                CongDanDAO.Instance.CapNhatTrangThaiHonNhan(MaCdNam,DaKetHon);
                CongDanDAO.Instance.CapNhatTrangThaiHonNhan(MaCdNu, DaKetHon);

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
        public bool isExist(string macd)
        {
            string strSQL = string.Format($"SELECT {MACDCHONG}, {MACDVO} FROM HonNhan WHERE {MACDCHONG} = '{macd}' OR {MACDVO} = '{macd}'");
            DataTable dtHN = DBConnection.Instance.LayDanhSach(strSQL);
            if (dtHN.Rows.Count > 0)
                return true;
            return false;
        }
        public bool CheckXacNhan(string tk)
        {
            string strSQL = string.Format($"SELECT {XACNHANLAN1},{XACNHANLAN2} FROM HonNhan WHERE {XACNHANLAN1} = '{tk}' OR {XACNHANLAN2} = '{tk}'");
            return DBConnection.Instance.CheckXacNhan(strSQL);
        }
        public void TimKiemHonNhan(DataGridView dtgv, CongDan cd, int mahn)
        {
            try
            {
                string strSQL = string.Format($"SELECT * FROM {HONNHAN} WHERE {MAHN} = {mahn} AND ({MACDCHONG} = '{cd.Macd}' OR {MACDVO} = '{cd.Macd}')");
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
                if (dtMail.Rows.Count > 0)
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
                string strQuery = string.Format($"SELECT * FROM {HONNHAN} WHERE {XACNHANLAN1} IS NOT NULL AND {XACNHANLAN2} IS NOT NULL AND {TRANGTHAI} = N'Chưa Duyệt'");
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
                string strQuery = string.Format($"SELECT * FROM {HONNHAN} WHERE {MACDCHONG} = '{cd.Macd}' OR {MACDVO} = '{cd.Macd}'");
                Load(dtgv, strQuery);
            }
            catch
            {
                MessageBox.Show("ERROR");
            }
        }
        public void Create(HonNhan hn)
        {
            string strQuery = string.Format($"INSERT INTO {HONNHAN}({MAHN},{MACDCHONG} , {MACDVO}, {LOAI}, {NGAYDANGKY},{XACNHANLAN1},{XACNHANLAN2},{TRANGTHAI}) " +
                $"VALUES ('{hn.Mahn}', '{hn.Macdchong}', '{hn.Macdvo}','{hn.Loai}','{hn.Ngaydangky}','{hn.Xacnhanlan1}','{hn.Xacnhanlan2}','{hn.Trangthai}')");
            DBConnection.Instance.Execute(strQuery);
        }
        public List<HonNhan> Read()
        {
            List<HonNhan> cds = new List<HonNhan>();
            string strQuery = string.Format($"SELECT * FROM {HONNHAN}");
            DataTable dataTable = DBConnection.Instance.LayDanhSach(strQuery);
            foreach (DataRow item in dataTable.Rows)
            {
                HonNhan hoSo = new HonNhan(item);
                if (hoSo != null)
                    cds.Add(hoSo);
            }
            return cds;
        }
        public HonNhan ReadByID(string MaCDChong, string MaCDVo)
        {
            string strQuery = string.Format($"SELECT * FROM {HONNHAN} WHERE {MACDCHONG} = '{MaCDChong}' AND {MACDVO} = '{MaCDVo}'");

            DataTable dataTable = DBConnection.Instance.LayDanhSach(strQuery);
            HonNhan honNhan = new HonNhan(dataTable.Rows[0]);
            return honNhan;
        }
        public bool Fill(string id, List<Control> lct)
        {
            DataTable congDan = CongDanDAO.Instance.LayDanhSach(id);
            int i = 0;
            if (congDan.Rows.Count > 0)
            {
                foreach (Control c in lct)
                {
                    c.Text = congDan.Rows[0][i++].ToString();
                }
                return true;
            }
            return false;
        }
        public void Update(HonNhan hn, string xacNhan, string lanXacNhan)
        {
            try
            {
                string strQuery = string.Format(
                    $"UPDATE {HONNHAN} " +
                    $"SET {lanXacNhan} = '{xacNhan}' " +
                    $"WHERE {MACDCHONG} = '{hn.Macdchong}' AND {MACDVO} = '{hn.Macdvo}'");
                DBConnection.Instance.Execute(strQuery);
            }
            catch
            {
                MessageBox.Show("Thất bại");
            }
        }
        public void Delete(HonNhan hn)
        {
            try
            {
                string strQuery = string.Format($"DELETE FROM {HONNHAN} WHERE {MAHN} = '{hn.Mahn}'");
                DBConnection.Instance.Execute(strQuery);
            }
            catch
            {
                MessageBox.Show("Thất bại");
            }
        }

        public void SendForm(HonNhan hn)
        {
            //string strQuery = string.Format($"INSERT INTO {MAIL}({TIEUDE},{NGAY},{NGUOIGUI},{NGUOINHAN},{NOIDUNG}) " +
            //    $" VALUES(N'{tieude}',N'{Ngay}',N'{NguoiGui}',N'{NguoiNhan}', N'{NoiDung}')");

            string strQuery = string.Format($"INSERT INTO {HONNHAN}({MACDCHONG},{MACDVO},{LOAI},{NGAYDANGKY},{TRANGTHAI}) " +
                $" VALUES(N'{hn.Macdchong}',N'{hn.Macdvo}',N'{hn.Loai}',N'{hn.Ngaydangky}', N'{hn.Trangthai}')");

            DBConnection.Instance.Execute(strQuery);
        }
    }
}
