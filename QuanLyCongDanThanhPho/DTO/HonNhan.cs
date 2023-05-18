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
        private int mahn;
        private int macdchong;
        private int macdvo;
        private string loai;
        private string ngaydangky;
        private int xacnhanlan1;
        private int xacnhanlan2;
        private string trangthai;
        public int Mahn { get => mahn; set => mahn = value; }
        public int Macdchong { get => macdchong; set => macdchong = value; }
        public int Macdvo { get => macdvo; set => macdvo = value; }
        public string Loai { get => loai; set => loai = value; }
        public string Ngaydangky { get => ngaydangky; set => ngaydangky = value; }
        public int Xacnhanlan1 { get => xacnhanlan1; set => xacnhanlan1 = value; }
        public int Xacnhanlan2 { get => xacnhanlan2; set => xacnhanlan2 = value; }
        public string Trangthai { get => trangthai; set => trangthai = value; }
        public HonNhan() { }
        public HonNhan(int mahn, int macdchong, int macdvo, string loai, string ngaydangky, int xacnhanlan1, int xacnhanlan2, string trangthai)
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
                this.macdchong = (int)row["MaCDChong"];
                this.macdvo = (int)row["MaCDVo"];
                this.loai = (string)row["Loai"];
                this.ngaydangky = row["NgayDangKy"].ToString();
                this.trangthai = (string)row["TrangThai"];
                this.xacnhanlan1 = row["XacNhanLan1"] != null ? (int)row["XacNhanLan1"] : -1;
                this.xacnhanlan2 = row["XacNhanLan2"] != null ? (int)row["XacNhanLan2"] : -1;
            }
            catch
            {

            }
        }
    }
}