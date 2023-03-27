using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCongDanThanhPho
{
    public class Mail
    {
        int mamail;
        string tieude;
        string ngay;
        string nguoigui;
        string nguoinhan;
        string noidung;

        public int Mamail { get => mamail; set => mamail = value; }
        public string Tieude { get => tieude; set => tieude = value; }
        public string Ngay { get => ngay; set => ngay = value; }
        public string Nguoigui { get => nguoigui; set => nguoigui = value; }
        public string Nguoinhan { get => nguoinhan; set => nguoinhan = value; }
        public string Noidung { get => noidung; set => noidung = value; }

        public Mail() { }

        public Mail(int mamail, string tieude, string ngay, string nguoigui, string nguoinhan, string noidung)
        {
            this.mamail = mamail;
            this.tieude = tieude;
            this.ngay = ngay;
            this.nguoigui = nguoigui;
            this.nguoinhan = nguoinhan;
            this.noidung = noidung;
        }

        public Mail(string tieude, string ngay, string nguoigui, string nguoinhan, string noidung)
        {
            this.tieude = tieude;
            this.ngay = ngay;
            this.nguoigui = nguoigui;
            this.nguoinhan = nguoinhan;
            this.noidung = noidung;
        }
    }
}
