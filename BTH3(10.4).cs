using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
namespace BTH3
{
    public partial class Form1 : Form
    {
        SqlCommand cmd;
        string str;
        SqlConnection cn = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            str = "Server = .; Database = QLBanHang; Integrated Security = true;";
            Connect();
            List<object> list = GetData();
            dgv.DataSource = list;
            
            txtmaloai.DataBindings.Add("Text", list, "MaLoai");
            txttenloai.DataBindings.Add("Text", list, "TenLoai");

        }
        private void Connect()
        {
            cn = new SqlConnection(str);
            try
            {
                if (cn != null && cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                    MessageBox.Show("Kết nối thành công");
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (ConfigurationErrorsException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Disconnect()
        {
            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
                MessageBox.Show("Đã đóng kết nối");
            }
        }

        private void btxoa_Click(object sender, EventArgs e)
        {
            Connect();
           string maloai = txtmaloai.Text.Trim();
           string strsql = "DELETE FROM [QLBanHang].[dbo].[LoaiSP] WHERE [MaLoaiSP] = '" + maloai + "'";
           try
           {
               cmd = new SqlCommand(strsql, cn);
               cmd.ExecuteNonQuery();
               MessageBox.Show("Đã Xóa");
           }
           catch (IOException ex)
           {
               MessageBox.Show(ex.Message);
           }
           dgv.DataSource = GetData();
           Disconnect();
        }

        private void btthem_Click(object sender, EventArgs e)
        {
            Connect();
            int maloai = int.Parse(txtmaloai.Text.Trim());
            string tenloai = txttenloai.Text.Trim();
            string strsql = "INSERT INTO [QLBanHang].[dbo].[LoaiSP] ([MaLoaiSP],[TenLoaiSP])VALUES (" + maloai + ",N'" + tenloai + "')";  
            try
            {
                cmd = new SqlCommand(strsql, cn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã Thêm");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            dgv.DataSource = GetData();
            Disconnect();
        }
        private List<object> GetData()
        {
            string strsql = "SELECT * FROM LoaiSP";
            SqlCommand cmd = new SqlCommand(strsql, cn);
            SqlDataReader dr = cmd.ExecuteReader();
            List<object> list = new List<object>();
            while (dr.Read())
            {
                string maloaisp = dr["MaLoaiSP"].ToString();
                string tenloaisp = dr["TenLoaiSP"].ToString();
                var prod = new
                {
                    MaLoai = maloaisp,
                    TenLoai = tenloaisp
                };
                list.Add(prod);
            }
            dr.Close();

            return list;

        }
    }
}
