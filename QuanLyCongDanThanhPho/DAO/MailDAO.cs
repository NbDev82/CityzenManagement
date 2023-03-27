using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCongDanThanhPho
{
    internal class MailDAO
    {
        private static MailDAO instance;
        public static MailDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new MailDAO();
                return instance;
            }
        }
        public DataTable LayDanhSachGui(string nguoigui)
        {
            string sqlStr = string.Format("SELECT MaMail as [Mã mail], TieuDe as [Tiêu đề], Ngay as [Ngày], NguoiGui as [Người gửi], NguoiNhan as [Người nhận], NoiDung as [Nội dung] FROM Mail WHERE NguoiGui = N'{0}'", nguoigui);
            return DBConnection.Instance.LayDanhSach(sqlStr);
        }

        public DataTable LayDanhSachNhan(string nguoinhan)
        {
            string sqlStr = string.Format("SELECT MaMail as [Mã mail], TieuDe as [Tiêu đề], Ngay as [Ngày], NguoiGui as [Người gửi], NguoiNhan as [Người nhận], NoiDung as [Nội dung] FROM Mail WHERE NguoiNhan = N'{0}'", nguoinhan);
            return DBConnection.Instance.LayDanhSach(sqlStr);
        }

        public DataTable LayDanhSachNguoiNhan()
        {
            string sqlStr = string.Format("SELECT TenTK FROM CongDan");
            return DBConnection.Instance.LayDanhSach(sqlStr);
        }

        public void Them(Mail mail)
        {
            string sqlStr = string.Format("INSERT INTO dbo.Mail(TieuDe, Ngay, NguoiGui, NguoiNhan, NoiDung) VALUES(N'{0}', N'{1}', N'{2}', LOWER(N'{3}'), N'{4}')", mail.Tieude, mail.Ngay, mail.Nguoigui, mail.Nguoinhan, mail.Noidung);
            DBConnection.Instance.Execute(sqlStr);
        }

        public void Xoa(Mail mail)
        {
            string sqlStr = string.Format("DELETE FROM dbo.Mail WHERE MaMail = {0}", mail.Mamail);
            DBConnection.Instance.Execute(sqlStr);
        }

        public void Sua(Mail mail)
        {
            string sqlStr = string.Format("UPDATE dbo.Mail SET TieuDe = N'{0}', NoiDung = N'{1}' WHERE MaMail = {2}", mail.Tieude, mail.Noidung, mail.Mamail);
            DBConnection.Instance.Execute(sqlStr);
        }

        public DataTable TimKiemDanhSachGui(string tentk, string find)
        {
            string sqlStr = string.Format("SELECT MaMail as [Mã mail], TieuDe as [Tiêu đề], Ngay as [Ngày], NguoiGui as [Người gửi], NguoiNhan as [Người nhận], NoiDung as [Nội dung] FROM dbo.Mail WHERE NguoiGui = N'{0}' AND (LOWER(MaMail) LIKE LOWER(N'%{1}%') OR LOWER(TieuDe) LIKE LOWER(N'%{1}%') OR LOWER(Ngay) LIKE LOWER(N'%{1}%') OR LOWER(NguoiGui) LIKE LOWER(N'%{1}%') OR LOWER(NguoiNhan) LIKE LOWER(N'%{1}%') OR LOWER(NoiDung) LIKE LOWER(N'%{1}%'))", tentk, find);
            return DBConnection.Instance.TimKiem(sqlStr);
        }

        public DataTable TimKiemDanhSachNhan(string tentk, string find)
        {
            string sqlStr = string.Format("SELECT MaMail as [Mã mail], TieuDe as [Tiêu đề], Ngay as [Ngày], NguoiGui as [Người gửi], NguoiNhan as [Người nhận], NoiDung as [Nội dung] FROM dbo.Mail WHERE NguoiNhan = N'{0}' AND (LOWER(MaMail) LIKE LOWER(N'%{1}%') OR LOWER(TieuDe) LIKE LOWER(N'%{1}%') OR LOWER(Ngay) LIKE LOWER(N'%{1}%') OR LOWER(NguoiGui) LIKE LOWER(N'%{1}%') OR LOWER(NguoiNhan) LIKE LOWER(N'%{1}%') OR LOWER(NoiDung) LIKE LOWER(N'%{1}%'))", tentk, find);
            return DBConnection.Instance.TimKiem(sqlStr);
        }
    }
}
