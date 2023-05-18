using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCongDanThanhPho
{
    public class ChiTietHoKhau
    {
        private int maho;
        private int macd;
        private int tinhtrangcutru;
        private string quanhevoichuho;
        private string ngaydangky;
        private string trangthai;
        public int Maho { get => maho; set => maho = value; }
        public int Macd { get => macd; set => macd = value; }
        public int Tinhtrangcutru { get => tinhtrangcutru; set => tinhtrangcutru = value; }
        public string Quanhevoichuho { get => quanhevoichuho; set => quanhevoichuho = value; }
        public string Ngaydangky { get => ngaydangky; set => ngaydangky = value; }
        public string Trangthai { get => trangthai; set => trangthai = value; }
        public ChiTietHoKhau() { }
        public ChiTietHoKhau(int maho, int macd, int tinhtrangcutru, string quanhevoichuho, string ngaydangky, string trangthai)
        {
            this.maho = maho;
            this.macd = macd;
            this.tinhtrangcutru = tinhtrangcutru;
            this.quanhevoichuho = quanhevoichuho;
            this.ngaydangky = ngaydangky;
            this.trangthai = trangthai;
        }
    }
}
