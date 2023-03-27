using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCongDanThanhPho
{
    public class HonNhan
    {
        int mahn;
        string macdchong;
        string macdvo;
        string loai;
        string ngaydangky;
        string xacnhanlan1;
        string xacnhanlan2;
        string trangthai;

        public int Mahn { get => mahn; set => mahn = value; }
        public string Macdchong { get => macdchong; set => macdchong = value; }
        public string Macdvo { get => macdvo; set => macdvo = value; }
        public string Loai { get => loai; set => loai = value; }
        public string Ngaydangky { get => ngaydangky; set => ngaydangky = value; }
        public string Xacnhanlan1 { get => xacnhanlan1; set => xacnhanlan1 = value; }
        public string Xacnhanlan2 { get => xacnhanlan2; set => xacnhanlan2 = value; }
        public string Trangthai { get => trangthai; set => trangthai = value; }

        public HonNhan() { }

        public HonNhan(int mahn, string macdchong, string macdvo, string loai, string ngaydangky, string xacnhanlan1, string xacnhanlan2, string trangthai)
        {
            this.mahn = mahn;
            this.macdchong = macdchong;
            this.macdvo = macdvo;
            this.loai = loai;
            this.ngaydangky = ngaydangky;
            this.xacnhanlan1 = xacnhanlan1;
            this.xacnhanlan2 = xacnhanlan2;
            this.trangthai = trangthai;
        }
        public HonNhan(DataRow row)
        {
            try
            {
                this.mahn = (int)row["MaHN"];
                this.macdchong = (string)row["MaCDChong"];
                this.macdvo = (string)row["MaCDVo"];
                this.loai = (string)row["Loai"];
                this.ngaydangky = row["NgayDangKy"].ToString();
                this.trangthai = (string)row["TrangThai"];
                this.xacnhanlan1 = row["XacNhanLan1"] != null ? (string)row["XacNhanLan1"] : null;
                this.xacnhanlan2 = row["XacNhanLan2"] != null ? (string)row["XacNhanLan2"] : null;
            }
            catch
            {

            }
        }
    }
}