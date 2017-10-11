using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace BTH5
{
    public partial class Form1 : Form
    {
        SqlConnection cn = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string cnstr = ConfigurationManager.ConnectionStrings["QLBanHang"].ConnectionString;
            cn = new SqlConnection(cnstr);
            DataTable dt = new DataTable();
            //dt = GetData();
            //dgv.DataSource = dt;
            //txtMaNV.DataBindings.Add("Text", dt, "MaNV");
            
        }
        public void Connect()
        {

            try
            {
                if (cn != null && cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Disconnect()
        {
            if (cn != null && cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connect();
            int id = int.Parse(txtMaNV.Text);
            try
            {
                SqlCommand cmd = new SqlCommand("uspDeleteNhanvien", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", id));
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            { Disconnect(); }

        }
        //private DataTable GetData()
        //{
        //    string strsql = "SELECT * FROM NhanVien";
        //    DataTable dt = new DataTable();
        //    SqlDataAdapter da = new SqlDataAdapter(strsql, cn);
        //    da.Fill(dt);
        //    return dt;
        //}
        private DataTable GetData(string strsql)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(strsql, cn);
            int count;
            count = da.Fill(dt);
            if(count == 0)
                MessageBox.Show("Không tìm thấy");
            return dt;
        }

        private void bttim_Click(object sender, EventArgs e)
        {
            string timkiem = txttimkiem .Text + "%";
            DataTable dt = new DataTable();
            if (rbtheoho.Checked == true)
            {
                string strsql = "SELECT *FROM [QLBanHang].[dbo].[Nhanvien] WHERE HoNV LIKE N'" + timkiem + "'";
                dt = GetData(strsql);
                dgv.DataSource = dt;
            }
            if (rbtheoten.Checked == true)
            {
                string strsql = "SELECT *FROM [QLBanHang].[dbo].[Nhanvien] WHERE Ten LIKE N'" + timkiem + "'";
                dt = GetData(strsql);
                dgv.DataSource = dt;
            }
            if (rbtheoma.Checked == true)
            {
                string strsql = "SELECT *FROM [QLBanHang].[dbo].[Nhanvien] WHERE MaNV LIKE N'" + timkiem + "'";
                dt = GetData(strsql);
                dgv.DataSource = dt;
            }
        }
        
    }
}
