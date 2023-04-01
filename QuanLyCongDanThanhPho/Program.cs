using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCongDanThanhPho
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CongDan cd = CongDanDAO.Instance.KiemTraDangNhap("nguyenvantrong", "nguyenvantrong");
            Application.Run(new fNguoiDung(cd));
        }


        static public List<string> tinhthanh = new List<string>()
        {
            "Hà Nội", "TP Hồ Chí Minh", "Đà Nẵng", "Hải Phòng",
            "Bắc Giang", "Bắc Kạn", "Bạc Liêu", "Bắc Ninh",
            "Bến Tre", "Bình Định", "Bình Dương", "Bình Phước",
            "Bình Thuận", "Cà Mau", "Cần Thơ", "Cao Bằng",
            "Đắk Lắk", "Đắk Nông", "Điện Biên", "Đồng Nai",
            "Đồng Tháp", "Gia Lai", "Hà Giang", "Hà Nam",
            "Hà Tĩnh", "Hải Dương", "Hậu Giang", "Hòa Bình",
            "Hưng Yên", "Khánh Hòa", "Kiên Giang", "Kon Tum",
            "Lai Châu", "Lâm Đồng", "Lạng Sơn", "Lào Cai",
            "Long An", "Nam Định", "Nghệ An", "Ninh Bình",
            "Ninh Thuận", "Phú Thọ", "Quảng Bình", "Quảng Nam",
            "Quảng Ngãi", "Quảng Ninh", "Quảng Trị", "Sóc Trăng",
            "Sơn La", "Tây Ninh", "Thái Bình", "Thái Nguyên",
            "Thanh Hóa", "Thừa Thiên Huế", "Tiền Giang", "Trà Vinh",
            "Tuyên Quang", "Vĩnh Long", "Vĩnh Phúc", "Yên Bái",
        };

        static public List<string> dantoc = new List<string>()
        {
            "Kinh", "Tày", "Thái", "Mường", "Nùng", "Hoa", "Khmer", "Dao",
            "Bana", "Êđê", "Sán Chay", "Chăm", "Sán Dìu", "Co", "La Hủ",
            "La Chí", "La Ha", "Pu Péo", "Rơ Măm", "Ba Na", "Brâu", "Cơ Tu",
            "Giáy", "Hà Nhì", "Mảng", "Pa Then", "Phù Lá", "Si La", "Xa Phó",
            "Thổ", "Chu Ru", "Lào", "La", "Lự", "Mạ", "Mảng", "Pa Then",
            "Pà Thẻn", "Pù Chả", "Rơ Ngao", "Tà Ôi", "Tày Trắng",
            "Thái Đen", "Thái Đỏ", "Thái Trắng", "Thổ", "Xinh Mun",
            "Xơ Đăng", "Bru-Vân Kiều", "Chứt", "Cơ Lao", "Cống", "Hrê",
            "Khơ Mú", "La Hu", "Mạ", "Mnông", "ơ Đu", "Pạ Lông", "Phù Lá",
            "Rơ Măm", "Rơ Ngao", "Sedang", "Tà Lăng", "Tà Ôi", "Xơ Mông",
        };
    }
}
