using QuanLyCongDanThanhPho.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCongDanThanhPho.DAO
{
    public class CCCDDAO
    {

        private readonly string CCCD = "[Certificates]";
        private readonly string MACD = "MaCD";
        private readonly string MACCCD = "MaCCCD";
        private readonly string HOTEN = "HOTEN";
        private readonly string NGAYSINH = "NGAYSINH";
        private readonly string GIOITINH = "GIOITINH";
        private readonly string QUOCTICH = "QUOCTICH";
        private readonly string QUEQUAN = "QUEQUAN";
        private readonly string NOITHUONGTRU = "NOITHUONGTRU";
        private readonly string HANSUDUNG = "HANSUDUNG";
        private readonly string DACDIEMNHANDANG = "DACDIEMNHANDANG";
        private readonly string AVATAR = "AVATAR";
        private readonly string CONGDAN = "[Citizens]";
        private readonly string KHAISINH = "[Births]";
        private readonly string AVATARS = "AVATARS";
        private static CCCDDAO instance;
        public static CCCDDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new CCCDDAO();
                return instance;
            }
        }
        public string GenerateMaCCCD()
        {
            Random random = new Random();
            int code = random.Next(100000, 999999);
            string MaCCCD = "CD63" + code.ToString();
            string strSQLCheckValidMaCCCD = string.Format($"EXISTS (SELECT * FROM Certificates WHERE MaCCCD = N'{MaCCCD}')");
            while (DBConnection.Instance.CheckCDDuplicate(strSQLCheckValidMaCCCD))
            {
                MaCCCD = "CD63" + code.ToString();
            }
            return MaCCCD;
        }
        public string GetMaCCCDByMaCD(int Macd)
        {
            string strSQL = string.Format($"SELECT {MACCCD} AS N'Mã CCCD' " +
                                          $"FROM {CCCD} " +
                                          $"WHERE {CCCD}.{MACD} = {Macd}");
            DataTable dataTable = DBConnection.Instance.LayDanhSach(strSQL);
            string MaCD = dataTable.Rows[0]["Mã CCCD"].ToString();
            return MaCD;
        }
        public int GetMaCDByMaCCCD(string macccd)
        {
            string strSQL = string.Format($"SELECT {MACD} AS N'Mã CD' " +
                                          $"FROM {CCCD} " +
                                          $"WHERE {CCCD}.{MACCCD} = N'{macccd}'");
            DataTable dataTable = DBConnection.Instance.LayDanhSach(strSQL);
            int MaCD = (int)dataTable.Rows[0]["Mã CD"];
            return MaCD;
        }
        public DataTable GetCCCD(int MaCD)
        {
            string strSQL = string.Format($"SELECT {CONGDAN}.{MACD} AS N'Mã CD', {MACCCD} AS N'Số CCCD',{HOTEN} AS N'Họ và tên',{NGAYSINH} AS N'Ngày sinh',{GIOITINH} AS N'Giới tính',{QUOCTICH} AS N'Quốc tịch',{QUEQUAN} AS N'Quê quán',{NOITHUONGTRU} AS N'Nơi thường trú',{HANSUDUNG} AS N'Hạn',{DACDIEMNHANDANG} AS N'Đặc điểm nhận dạng', {AVATAR} AS N'Hình' " +
                                          $"FROM {CCCD}, {CONGDAN}, {KHAISINH}, {AVATARS} " +
                                          $"WHERE {CCCD}.{MACD} = {CONGDAN}.{MACD} AND {CONGDAN}.{MACD} = {KHAISINH}.{MACD} AND {CONGDAN}.{MACD} = {AVATARS}.{MACD} AND {CCCD}.{MACD} = {MaCD}");
            DataTable dataCCCD = DBConnection.Instance.LayDanhSach(strSQL);
            if(dataCCCD == null)
                return null;
            return dataCCCD;
        }
        public DataTable GetListCCCD()
        {
            string strSQL = string.Format($"SELECT {CONGDAN}.{MACD} AS N'Mã CD', {MACCCD} AS N'Số CCCD',{HOTEN} AS N'Họ và tên',{NGAYSINH} AS N'Ngày sinh',{GIOITINH} AS N'Giới tính',{QUOCTICH} AS N'Quốc tịch',{QUEQUAN} AS N'Quê quán',{NOITHUONGTRU} AS N'Nơi thường trú',{HANSUDUNG} AS N'Hạn',{DACDIEMNHANDANG} AS N'Đặc điểm nhận dạng', {AVATAR} AS N'Hình' " +
                                          $"FROM {CCCD}, {CONGDAN}, {KHAISINH}, {AVATARS} " +
                                          $"WHERE {CCCD}.{MACD} = {CONGDAN}.{MACD} AND {CONGDAN}.{MACD} = {KHAISINH}.{MACD} AND {CONGDAN}.{MACD} = {AVATARS}.{MACD}");
            DataTable dataCCCD = DBConnection.Instance.LayDanhSach(strSQL);
            return dataCCCD;
        }
        public byte[] ConvertImageToByteArray(Image img)
        {
            byte[] arr;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms,ImageFormat.Jpeg);
                arr = ms.ToArray();
            }
            return arr;
        }
        public bool Add(CCCD cccd)
        {
            if(cccd == null)
                return false;
            string strSQLAddCCCD = string.Format($"INSERT INTO {CCCD}({MACD},{MACCCD},{QUOCTICH},{QUEQUAN},{NOITHUONGTRU},{HANSUDUNG},{DACDIEMNHANDANG} ) " +
                                          $"VALUES ({cccd.MaCD},N'{cccd.MaCCCD}',N'{cccd.QuocTich}',N'{cccd.QueQuan}',N'{cccd.NoiThuongTru}',N'{cccd.HanSuDung}',N'{cccd.DacDiemNhanDang}' )");
            string strSQLAddAvatar = string.Format($"INSERT INTO Avatars ({MACD}, Avatar) VALUES ({cccd.MaCD}, @photo) ");
            byte[] image = ConvertImageToByteArray(cccd.Avatar);
            bool flag = DBConnection.Instance.Execute(strSQLAddCCCD);
            DBConnection.Instance.AddImageToDatabase(strSQLAddAvatar, image);
            return flag;
        }
    }
}
