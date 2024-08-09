using QLNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLNhanSu
{
    public partial class DoiMatKhau : Form
    {
        public SqlConnection conn;
       public  SqlCommand cmd = new SqlCommand();
        public void getConnection()
        {
            string connect = "server = DELL; database=qlnhansu; integrated security =true";
            //string connect = "server = ADMIN\\SQLEXPRESS; database=qlnhansu; integrated security =true";
            //string connect = "server=DESKTOP-P7FD0VQ; database=abc; integrated security = True";
            conn = new SqlConnection();
            conn.ConnectionString = connect;
            conn.Open();
        }
        public DoiMatKhau()
        {
            InitializeComponent();
           /*// lay ma nhan vien khi đăng nhap
            if (Login.Truongphong.tp == 0)
            {
                // formluong.Hide();
            }
            label4.Text = Login.Truongphong.ten;
            label5.Text = label5.Text + Login.Truongphong.ma;*/
        }

        private void DoiMatKhau_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          

            if (txtmkcu.Text == txtmkmoi.Text)
            {
                MessageBox.Show("Mật khẩu mới phải khác mật khẩu cũ.");
            }
            else if (string.IsNullOrEmpty(txtmkmoi.Text))
            {
                MessageBox.Show("Mật khẩu mới không được để trống.");
            }
            else if (txtmkmoi.Text != txtnmkmoi.Text)
            {
                MessageBox.Show("Mật khẩu mới không khớp. Vui lòng nhập lại.");
                txtmkmoi.Text = "";
                txtnmkmoi.Text = "";
            }
            else
            {
                //  using (SqlCommand cmd = new SqlCommand())
                // {
                //"UPDATE nhanvien SET password = @newPassword WHERE idnhanvien = @empId", conn
                getConnection();
                cmd = conn.CreateCommand();
                    
                cmd.CommandText = "UPDATE nhanvien set password =N'" + txtmkmoi.Text + "' where idnhanvien = '" + Login.Truongphong.ma + "' and password = '" + txtmkcu.Text + "'";

                //cmd.Parameters.AddWithValue("@newPassword", txtmkmoi.Text);
                // cmd.Parameters.AddWithValue("@empId", Login.Truongphong.ma);
                var rowsA = cmd.ExecuteNonQuery();
                    if (rowsA > 0)
                    {
                        MessageBox.Show("Cập nhật mật khẩu thành công.");
                       // Function h = new Function();
                       // h.hienThiDT(dtgv, "SELECT * FROM nhanvien", conn);
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật mật khẩu không thành công.");
                    }
               // }
            }
        }

        private void btthem_Click(object sender, EventArgs e)
        {
            txtmkcu.Text = "";
            txtmkmoi.Text = "";
            txtnmkmoi.Text = "";
        }
    }
}
