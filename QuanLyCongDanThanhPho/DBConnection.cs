using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCongDanThanhPho
{
    internal class DBConnection
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.cnnStr);
        private static DBConnection instance;
        public static DBConnection Instance
        {
            get
            {
                if (instance == null)
                    instance = new DBConnection();
                return instance;
            }
        }
        public int GetMaHNMax(string strSQL)
        {
            try
            {
                DataTable dt = LayDanhSach(strSQL);
                int mahn = (int)dt.Rows[0]["max"];
                return mahn;
            }
            catch
            {
                MessageBox.Show("ERROR");
                return -1;
            }
        }
        public bool CheckXacNhan(string strSQL)
        {
            try
            {
                DataTable dt = LayDanhSach(strSQL);
                if (dt.Rows.Count > 0)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        public DataTable LayDanhSach(string sqlStr)
        {
            try
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, conn);
                DataTable data = new DataTable();
                adapter.Fill(data);
                return data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lấy danh sách thất bại\n" + ex);
            }
            finally
            {
                conn.Close();
            }
            return null;
        }

        public bool Execute(string query)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                if (cmd.ExecuteNonQuery() > 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thất bại\n" + ex);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable TimKiem(string sqlStr)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, conn);
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }
    }
}
