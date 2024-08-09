using QLNhanSu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QLNS.HienThiTT;
using static QLNS.Login;

namespace QLNS
{
    public partial class Login : Form
    {
        public SqlConnection conn;
        public void Ketnoi()
        {
            string str = "server = DELL; database=qlnhansu; integrated security =true";
            //string str = "server = DESKTOP-P7FD0VQ; database=abc; integrated security =true";
            //string str = "server = ADMIN\\SQLEXPRESS; database=qlnhansu; integrated security =true";
            conn = new SqlConnection();
            conn.ConnectionString = str;
            conn.Open();
        }
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            int x = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            int y = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            this.Location = new Point(x, y);
        }
        public class Truongphong
        {
            public static int tp;
            public static string ten;
            public static string ma;
            public static string anh;
        }
        private void btdn_Click(object sender, EventArgs e)
        {
            Ketnoi();
            SqlDataAdapter dt = new SqlDataAdapter("select IDnhanvien manv, hoten tennv, linkanh from nhanvien where sdt ='"+txten.Text+ "' AND password='" + txmk.Text + "' ", conn);
            DataSet ds = new DataSet();
            dt.Fill(ds,"NV");
            Truongphong.tp = 0;
            
            if (ds.Tables[0].Rows.Count>0 )
            {
                Truongphong.ma = ds.Tables[0].Rows[0]["manv"].ToString();
                Truongphong.ten = ds.Tables[0].Rows[0]["tennv"].ToString();
                Truongphong.anh = ds.Tables[0].Rows[0]["linkanh"].ToString();
                SqlDataAdapter dttp = new SqlDataAdapter("select*from nhanvien n join hopdongld h on n.idnhanvien = h.idnhanvien where sdt ='" + txten.Text + "' AND password='" + txmk.Text + "' AND ID_PB = 'PB07' AND  CHUCVU= N'Trưởng phòng'", conn);
                DataSet dstp = new DataSet();
                dttp.Fill(dstp, "NVTP");
                if (dstp.Tables[0].Rows.Count > 0)
                {
                    Truongphong.tp = 1;
                }

                this.Hide();
                TrangChu hienthi = new TrangChu();
              // BangTTNV hienthi = new BangTTNV();
                hienthi.Show();
            }
            else
            {
                MessageBox.Show("Nhập sai tên đăng nhập hoặc mật khẩu. Vui lòng nhập lại!");
                txmk.Clear();
                txten.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           txmk.Clear();
            txten.Clear();
            
        }

        private void txten_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
