﻿using QuanLyCongDanThanhPho.DAO;
using QuanLyCongDanThanhPho.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCongDanThanhPho
{
    public partial class fTamTruTamVang : Form
    {
        CongDan cd;
        enum Authentication
        {
            Manager =1,
            Citizen = 0
        }
        enum State
        {
            Access = 1,
            UnAccess = 0
        }
        enum Mode
        {
            TamVang = 0,
            TamTru = 1,
            QuanLy = 2
        }
        public fTamTruTamVang(CongDan cd)
        {
            InitializeComponent();
            this.cd = cd;
        }
        private void fTamTruTamVang_Load(object sender, EventArgs e)
        {
            try
            {
                DataRow dt = TamTruTamVangDAO.Instance.GetPersonalData(cd.Macd);
                if (dt == null)
                {
                    this.Enabled = false;
                    throw new Exception("Công dân chưa sử dụng được chức năng này! \n Hãy đăng ký CCCD trước");
                }
                txtMaCD.Text = dt["Mã CD"].ToString();
                txtTen.Text = dt["Họ tên"].ToString();
                dtpkNgaySinh.Text = dt["Ngày sinh"].ToString();
                txtTinh.Text = dt["Tỉnh"].ToString();
                txtHuyen.Text = dt["Huyện"].ToString();
                txtXa.Text = dt["Xã"].ToString();
                txtCCCD.Text = dt["Mã CCCD"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }
        public bool CheckTamVang()
        {
            if(dtpkHanChot.Value.Date <= DateTime.Now.Date)
                return false;
            TimeSpan ts = dtpkHanChot.Value.Date.Subtract(dtpkHanDau.Value.Date);
            int distance = ts.Days;
            if (!string.IsNullOrEmpty(txtLyDoTamVang.Text) && distance > 0)
            {
                return true;
            }
            return false;
        }
        public bool CheckTamTru()
        {
            if (string.IsNullOrEmpty(cbxTinh.Text))
                return false;
            if (string.IsNullOrEmpty(cbxHuyen.Text))
                return false;
            if (string.IsNullOrEmpty(cbxXa.Text))
                return false;
            if (string.IsNullOrEmpty(txtLyDoTamTru.Text))
                return false;
            return true;
        }
        public bool Check()
        {
            
            int mode = tCMode.SelectedIndex;
            if(mode == Convert.ToInt32(Mode.TamTru))
            {
                return CheckTamTru();
            }
            else if(mode == Convert.ToInt32(Mode.TamVang))
            {
                return CheckTamVang();
            }
            else
            {
                return false;
            }
        }
        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                MessageBox.Show("Kiểm tra thành công");
            }
            else
            {
                MessageBox.Show("Kiểm tra thất bại");
            }
        }
        public void WireEvents(Control Controls)
        {
            foreach (Control control in Controls.Controls)
            {
                if (control is Panel Panel)
                {
                    WireEvents(Panel);
                }
                if(control is TableLayoutPanel)
                {
                    WireEvents(control);
                }
                if (control is TextBox textBox)
                {
                    textBox.Text = null;
                }
                else if (control is ComboBox comboBox)
                {
                    comboBox.Text = null;
                }
                else if (control is DateTimePicker dateTimePicker)
                {
                    dateTimePicker.Value = DateTime.Now;
                }else if(control is RichTextBox richTextBox)
                {
                    richTextBox.Text = null;
                }
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            int mode = tCMode.SelectedIndex;
            if (mode == Convert.ToInt32(Mode.TamTru) || mode == Convert.ToInt32(Mode.TamVang))
            {
                WireEvents(tCMode.SelectedTab);
            }
            else
            {
                MessageBox.Show("Không hợp lệ");
            }
        }
        public void SendTamTru()
        {
            try
            {
                int MaCD = int.Parse(txtMaCD.Text);
                string SoCCCD = txtCCCD.Text;
                string Tinh = cbxTinh.Text;
                string Huyen = cbxHuyen.Text;
                string Xa = cbxXa.Text;
                string LyDo = txtLyDoTamTru.Text;
                string NgayBatDau = dtpkNgayTamTru.Value.Date.ToString();
                TamTru tt = new TamTru(MaCD, SoCCCD, Tinh, Huyen, Xa, LyDo, NgayBatDau);
                if (TamTruDAO.Instance.Add(tt))
                    MessageBox.Show("Gửi thành công");
                else
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("Gửi thất bại");
            }
        }
        public void SendTamVang()
        {
            try
            {
                int MaCD = int.Parse(txtMaCD.Text);
                string SoCCCD = txtCCCD.Text;
                string Tinh = txtTinh.Text;
                string Huyen = txtHuyen.Text;
                string Xa = txtXa.Text;
                string LyDo = txtLyDoTamTru.Text;
                string NgayBatDau = dtpkHanDau.Value.Date.ToString();
                string NgayKetThuc = dtpkHanChot.Value.Date.ToString();
                TamVang tt = new TamVang(MaCD, SoCCCD, Tinh, Huyen, Xa, LyDo, NgayBatDau, NgayKetThuc);
                if (TamVangDAO.Instance.Add(tt))
                    MessageBox.Show("Gửi thành công");
                else
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("Gửi thất bại");
            }
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            int mode = tCMode.SelectedIndex;
            if (mode == Convert.ToInt32(Mode.TamTru))
            {
                SendTamTru();
            }
            else if (mode == Convert.ToInt32(Mode.TamVang))
            {
                SendTamVang();
            }
            else
            {
                MessageBox.Show("Thao tác không hợp lệ");
            }
        }
        private void btnState_Click(object sender, EventArgs e)
        {
            try
            {
                int mode = tCMode.SelectedIndex;
                if (mode == Convert.ToInt32(Mode.TamTru))
                {
                    DataTable dt = TamTruDAO.Instance.GetPartDataByMaCD(cd.Macd);
                    MessageBox.Show($"Mã CCCD: {dt.Rows[0]["Mã CCCD"]} \n" +
                                    $"Mã đơn tạm trú: {dt.Rows[0]["ID"]} \n" +
                                    $"Trạng thái: {dt.Rows[0]["Trạng thái"]} \n");
                }
                else if (mode == Convert.ToInt32(Mode.TamVang))
                {
                    DataTable dt = TamVangDAO.Instance.GetPartDataByMaCD(cd.Macd);
                    MessageBox.Show($"Mã CCCD: {dt.Rows[0]["Mã CCCD"]} \n" +
                                    $"Mã đơn tạm vắng: {dt.Rows[0]["ID"]} \n" +
                                    $"Trạng thái: {dt.Rows[0]["Trạng thái"]} \n");
                }
                else
                {
                    MessageBox.Show("Thao tác không hợp lệ");
                }
            }
            catch
            {
                MessageBox.Show("Đơn không tồn tại");
            }
        }
        public void FindFormsOfTemporaryResidence()
        {
            try
            {
                if (string.IsNullOrEmpty(txtTimKiem.Text))
                {
                    throw new Exception();
                }
                else
                {
                    int MaCD = int.Parse(txtTimKiem.Text);
                    DataTable User = TamTruDAO.Instance.GetPartDataByMaCD(MaCD);
                    dtgvThongTin.DataSource = User;
                }
            }
            catch
            {
                MessageBox.Show("Không hợp lệ");
            }
        }
        public void FindFormsOfTemporaryAbsence()
        {
            try
            {
                if (string.IsNullOrEmpty(txtTimKiem.Text))
                {
                    throw new Exception();
                }
                else
                {
                    int MaCD = int.Parse(txtTimKiem.Text);
                    DataTable User = TamVangDAO.Instance.GetPartDataByMaCD(MaCD);

                    dtgvThongTin.DataSource = User;
                }
            }
            catch
            {
                MessageBox.Show("Không hợp lệ");
            }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            int mode = rdoTamTru.Checked ? 1 : 0;
            if (mode == Convert.ToInt32(Mode.TamTru))
            {
                FindFormsOfTemporaryResidence();
            }
            else
            {
                FindFormsOfTemporaryAbsence();
            }
        }
        private void btnXem_Click(object sender, EventArgs e)
        {
            int mode = rdoTamTru.Checked ? 1 : 0;
            if (mode == Convert.ToInt32(Mode.TamTru))
            {
                dtgvThongTin.DataSource = TamTruDAO.Instance.GetEntireData();
            }
            else
            {
                dtgvThongTin.DataSource = TamVangDAO.Instance.GetEntireData();
            }
        }
        private void rdoTamTru_CheckedChanged(object sender, EventArgs e)
        {
            dtgvThongTin.DataSource = null;
            if (rdoTamTru.Checked)
            {
                btnKiemTraQuaHan.Visible = false;
                btnKiemTraChuaDuyet.Visible = false;
            }
            else
            {
                btnKiemTraQuaHan.Visible = true;
                btnKiemTraChuaDuyet.Visible = true;
            }
        }
        private void btnDuyet_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, int> state = new Dictionary<string, int>();
                state["Đã duyệt"] = 1;
                state["Chưa duyệt"] = 0;

                int index = dtgvThongTin.CurrentRow.Index;
                DataTable dt = (DataTable)dtgvThongTin.DataSource;
                if (dt == null)
                    throw new Exception("Danh sách trống");
                if (state[dt.Rows[index]["Trạng thái"].ToString()] == Convert.ToInt32(State.Access))
                {
                    throw new Exception("Đơn đã duyệt");
                }
                int mode = rdoTamTru.Checked ? 1 : 0;
                if (mode == Convert.ToInt32(Mode.TamTru))
                {
                    TamTru tt = new TamTru(dt.Rows[index]);
                    if (TamTruDAO.Instance.SetAccess(tt))
                    {
                        MessageBox.Show("Duyệt thành công");
                        dtgvThongTin.DataSource = null;
                    }
                    else
                    {
                        MessageBox.Show("Duyệt không thành công");
                    }
                }
                else
                {
                    TamVang tv = new TamVang(dt.Rows[index]);
                    DateTime date = (DateTime)dt.Rows[index]["Thời gian kết thúc"];
                    if(date < DateTime.Now.Date)
                    {
                        MessageBox.Show("Đã hết hạn duyệt");
                        return;
                    }
                    if (TamVangDAO.Instance.SetAccess(tv))
                    {
                        MessageBox.Show("Thành công");
                        dtgvThongTin.DataSource = null;
                    }
                    else
                    {
                        MessageBox.Show("Không thành công");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thành công \n " + ex.Message);
            }
        }

        private void btnTuChoi_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, int> state = new Dictionary<string, int>();
                state["Đã duyệt"] = 1;
                state["Chưa duyệt"] = 0;
                int index = dtgvThongTin.CurrentRow.Index;
                DataTable dt = dtgvThongTin.DataSource as DataTable;
                if(dt == null)
                    throw new Exception("Danh sách trống");
                if (state[dt.Rows[index]["Trạng thái"].ToString()] == Convert.ToInt32(State.Access))
                {
                    throw new Exception("Đơn đã duyệt");
                }
                int mode = rdoTamTru.Checked ? 1 : 0;
                if (mode == Convert.ToInt32(Mode.TamTru))
                {
                    TamTru tt = new TamTru(dt.Rows[index]);
                    if (TamTruDAO.Instance.Delete(tt))
                    {
                        MessageBox.Show("Từ chối thành công");
                        dtgvThongTin.DataSource = null;
                    }
                    else
                    {
                        MessageBox.Show("Từ chối không thành công");
                    }
                }
                else
                {
                    TamVang tv = new TamVang(dt.Rows[index]);
                    if (TamVangDAO.Instance.Delete(tv))
                    {
                        MessageBox.Show("Thành công");
                        dtgvThongTin.DataSource = null;
                    }
                    else
                    {
                        MessageBox.Show("Không thành công");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thành công \n " + ex.Message);
            }
        }
        private void btnKiemTraQuaHan_Click(object sender, EventArgs e)
        {
            dtgvThongTin.DataSource = TamVangDAO.Instance.GetListExpired();
        }
        private void btnKiemTraChuaDuyet_Click(object sender, EventArgs e)
        {
            dtgvThongTin.DataSource = TamVangDAO.Instance.GetListExpiredPermission();
        }
        private void tCMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int mode = tCMode.SelectedIndex;
                if (mode == Convert.ToInt32(Mode.QuanLy))
                {
                    int quyenhan = AccountsDAO.Instance.GetQuyenHan(cd.Macd);
                    if (quyenhan == Convert.ToInt32(Authentication.Citizen))
                    {
                        tCMode.SelectedIndex = 0;
                        throw new Exception("Bạn không có quyền này");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
