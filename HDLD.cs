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

namespace QLNS
{
    public partial class HDLD : Form
    {
        public SqlConnection conn;
        public void Ketnoi()
        {
            string str = "server = DELL; database=QLNhanSu; integrated security =true";
            //string str = "server = ADMIN\\SQLEXPRESS; database=qlnhansu; integrated security =true";
            conn = new SqlConnection();
            conn.ConnectionString = str;
            conn.Open();
        }
        public HDLD()
        {
            InitializeComponent();
        }

  

        private void HDLD_Load(object sender, EventArgs e)
        {
            Ketnoi();
           // txbd.CustomFormat = "yyyy/MM/dd";
            string Mnv = HienThiTT.Manv.ma;
            int MaNhanVien = int.Parse(Mnv);

            string sql = "select loaihdld.TENHD, hopdongld.TENHOPDONG, hopdongld.NGAYBD,hopdongld.NGAYKT,hopdongld.CHUCVU,hopdongld.GHICHU  from hopdongld, loaihdld, nhanvien where hopdongld.LOAIHD = loaihdld.LOAIHD AND hopdongld.IDNHANVIEN = nhanvien.IDNHANVIEN and nhanvien.IDNHANVIEN = '"+MaNhanVien+"' ";
            SqlCommand comd = new SqlCommand(sql, conn);
            SqlDataReader reader = comd.ExecuteReader();

            while (reader.Read())
            {
                txloai.Text = reader.GetString(0);
                txtenhd.Text = reader.GetString(1);
                DateTime nbd = reader.GetDateTime(2);
                txbd.Text = nbd.ToString();
                DateTime nkt = reader.GetDateTime(3);
                txkt.Text = nkt.ToString();
                txcv.Text = reader.GetString(4);
                txgc.Text = reader.GetString(5);

            }
            this.StartPosition = FormStartPosition.Manual;
            int x = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            int y = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            this.Location = new Point(x, y);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
