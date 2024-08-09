using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using WindowsFormsApp1;

namespace windowsformdangnhap
{
    public partial class kyluatvakhenhuong : Form
    {
        public SqlConnection conn;
        public void Ketnoi()
        {
            string str = "SERVER = DELL; database = qlnhansu; integrated Security = true";
            //string str = "server = ADMIN\\SQLEXPRESS; database=qlnhansu; integrated security =true";
            conn = new SqlConnection();
            conn.ConnectionString = str;
            conn.Open();
        }
        public kyluatvakhenhuong()
        {
            InitializeComponent();
        }


        private void kyluatvakhenhuong_Load(object sender, EventArgs e)
        {
            Ketnoi();
            Function h = new Function();
           // Ham h = new Ham();
            h.HienThiDG("Select c.*, HOTEN from CHAMCONG c join NHANVIEN n ON c.IDNHANVIEN=n.IDNHANVIEN", dataGridView1, conn);
            dataGridView1.Columns["IDNHANVIEN"].HeaderText = "Mã nhân viên";
            dataGridView1.Columns["THANG"].HeaderText = "Thời gian";
            dataGridView1.Columns["HOTEN"].HeaderText = "Họ tên nhân viên";
            dataGridView1.Columns["NGAYNGHIPHEP"].HeaderText = "Số ngày nghỉ có phép";
            dataGridView1.Columns["SONGAYNGHIKHONGPHEP"].HeaderText = "Số ngày nghỉ không phép";
        }

        private void buttonkyluat_Click(object sender, EventArgs e)
        {
            Ketnoi();
            Function h = new Function();
            label1.Text = "Danh sách nhân viên bị kỷ luật";
            h.HienThiDG("Select n.HOTEN,NGAYNGHIPHEP from CHAMCONG c join NHANVIEN n ON c.IDNHANVIEN=n.IDNHANVIEN where NGAYNGHIPHEP>8", dataGridView1, conn);
            dataGridView1.Columns["HOTEN"].HeaderText = "Họ tên nhân viên";
            dataGridView1.Columns["NGAYNGHIPHEP"].HeaderText = "Số ngày nghỉ có phép";
        }

        private void buttonkhenhuong_Click(object sender, EventArgs e) 
        {
            Ketnoi();
            Function h = new Function();
            label1.Text = "Danh sách nhân viên được khen thưởng";
            h.HienThiDG("Select n.HOTEN,SONGAYNGHIKHONGPHEP from CHAMCONG c join NHANVIEN n ON c.IDNHANVIEN=n.IDNHANVIEN where SONGAYNGHIKHONGPHEP=0", dataGridView1, conn);
            dataGridView1.Columns["HOTEN"].HeaderText = "Họ tên  nhân viên";
            dataGridView1.Columns["SONGAYNGHIKHONGPHEP"].HeaderText = "Số ngày nghỉ không phép";
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                e.CellStyle.BackColor = Color.PowderBlue;
            }
            else
            {
                e.CellStyle.BackColor = Color.White;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
