using QLNS;
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
using WindowsFormsApp1;

namespace QLNhanSu
{
    public partial class CNHDLD : Form
    {
        public SqlConnection conn;
        public void Ketnoi()
        {
            string str = "server = DELL; database=qlnhansu; integrated security =true";
            //string str = "server = ADMIN\\SQLEXPRESS; database=qlnhansu; integrated security =true";
            conn = new SqlConnection();
            conn.ConnectionString = str;
            conn.Open();
        }
        public CNHDLD()
        {
            InitializeComponent();
        }

        private void CNHDLD_Load(object sender, EventArgs e)
        {
            Ketnoi();
            NhanSu nv = new NhanSu();
            nv.Hienthi("select * from hopdongld", dg, conn);
            Function h = new Function();
            h.loadCB("select * FROM loaihdld", lhd, "TENHD", "LOAIHD", conn);
            Function f1 = new Function();
            f1.LayDLCombobox("select * from loaihdld", lhd, "TENHD", "LOAIHD", conn);
            string sqlma = "SELECT TOP 1 hopdongld.ID_HOPDONG as ma FROM hopdongld WHERE LEFT(hopdongld.ID_HOPDONG, 2) = 'HD' ORDER BY CAST(RIGHT(hopdongld.ID_HOPDONG, 2) AS INT) DESC";
            SqlCommand cmd = new SqlCommand(sqlma, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                matam.Text  = reader.GetString(0);
                matam.Hide();
            }
            conn.Close();
            Ketnoi();
            string congma = " DECLARE @ID_HD VARCHAR(10) = '"+ matam.Text + "' DECLARE @new_ID_HD VARCHAR(10) SET @new_ID_HD = 'HD' + RIGHT('00' + CAST(RIGHT(@ID_HD, 2) + 1 AS VARCHAR(2)), 2) SELECT @new_ID_HD ";
            SqlCommand comd = new SqlCommand(congma, conn);
            SqlDataReader reader1 = comd.ExecuteReader();
            while (reader1.Read())
            {
                mahd.Text = reader1.GetString(0);
            }

            manv.Text = Themnv.Manv.ma.ToString();
            nv.GetheaderHDong(dg);
        }
        private void them_Click(object sender, EventArgs e)
        {
            Ketnoi();
            
            DateTime selectedDate = ngaybd.Value.Date;
            string nbd = selectedDate.ToString("yyyy-MM-dd");
            DateTime selectedDate1 = ngkt.Value.Date;
            string nkt = selectedDate1.ToString("yyyy-MM-dd");
            string sqlthem = "insert into hopdongld values('"+mahd.Text+"','"+manv.Text+"','"+lhd.SelectedValue.ToString()+"',N'"+tenhd.Text+ "','"+nbd.ToString()+"','"+nkt.ToString()+"',N'" + ghichu.Text+"',N'"+cv.Text+"')";
            SqlCommand comd = new SqlCommand(sqlthem, conn);
            comd.ExecuteNonQuery();
            MessageBox.Show("Đã thêm hợp đồng lao động!");
           
            this.Close();
            /*
             BangTTNV nv = new BangTTNV();
            nv.BangTTNV_Load(sender, e);
           */
        }
        private void dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sua_Click(object sender, EventArgs e)
        {
            Ketnoi();
            DateTime selectedDate = ngaybd.Value.Date;
            string nbd = selectedDate.ToString("yyyy-MM-dd");
            DateTime selectedDate1 = ngkt.Value.Date;
            string nkt = selectedDate1.ToString("yyyy-MM-dd");
            string sua = "update hopdongld set LOAIHD='"+ lhd.SelectedValue.ToString() + "',TENHOPDONG='"+tenhd.Text+ "',NGAYBD='"+nbd.ToString()+ "',NGAYKT='"+nkt.ToString()+ "',GHICHU=N'" + ghichu.Text+ "',CHUCVU=N'"+cv.Text+"' where ID_HOPDONG = '" + mahd.Text + "'";
            SqlCommand comd = new SqlCommand(sua, conn);
            comd.ExecuteNonQuery();
            MessageBox.Show("Đã cập nhật hợp đồng!");
        }

        private void xoa_Click(object sender, EventArgs e)
        {
            Ketnoi();
            string xoa = "delete from hopdongld where ID_HOPDONG = '" + mahd.Text + "'";
            SqlCommand comd = new SqlCommand(xoa, conn);
            comd.ExecuteNonQuery();
            MessageBox.Show("Bạn có muốn xóa hợp đồng này!");
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           mahd.Text=dg.Rows[e.RowIndex].Cells[0].Value.ToString();
            manv.Text= dg.Rows[e.RowIndex].Cells[1].Value.ToString();
            lhd.Text= dg.Rows[e.RowIndex].Cells[2].Value.ToString();
            tenhd.Text = dg.Rows[e.RowIndex].Cells[3].Value.ToString();
            ngaybd.Text= dg.Rows[e.RowIndex].Cells[4].Value.ToString();
            ngkt.Text= dg.Rows[e.RowIndex].Cells[5].Value.ToString();
            ghichu.Text= dg.Rows[e.RowIndex].Cells[6].Value.ToString();
            cv.Text= dg.Rows[e.RowIndex].Cells[07].Value.ToString();
        }

        private void dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dg_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tenhd_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
