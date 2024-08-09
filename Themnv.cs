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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLNhanSu
{
    public partial class Themnv : Form
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
        public Themnv()
        {
            InitializeComponent();
        }
        public class Manv
        {

            public static int ma ;

        }
        private void anh_Click(object sender, EventArgs e)
        {
            HienThiTT nv = new HienThiTT();
            nv.anh_Click(sender, e);
        }
        private void Themnv_Load(object sender, EventArgs e)
        {
            Ketnoi();
            Function h = new Function();
            h.loadCB("select * FROM trinhdongoaingu", nn, "TENNN", "bac", conn);
            Function f1 = new Function();
            f1.LayDLCombobox("select * from trinhdongoaingu", nn, "TENNN", "bac", conn);
            Function f2 = new Function();
            f2.loadCB("select * FROM phongban", cbpb, "TENPB", "ID_PB", conn);
            f2.LayDLCombobox("select * from phongban", cbpb, "TENPB", "ID_PB", conn);
            // tao ma nhan vien 
            string sqlma = "select max(IDNHANVIEN) +1 as mmax FROM nhanvien ";
            SqlCommand comd = new SqlCommand(sqlma, conn);
            SqlDataReader reader = comd.ExecuteReader();
          
            while (reader.Read())
            {
                Manv.ma = reader.GetInt32(0);
                manv.Text = Manv.ma.ToString();
            }
           // tx1.Text = HienThiTT.Manv.link;
        }

        private void btthem_Click(object sender, EventArgs e)
        {
            Ketnoi();
            //them nv
            DateTime selectedDate = dtngaysinh.Value;
            string sqlthem = "insert into nhanvien(IDNHANVIEN,ID_PB,HOTEN,linkanh,CCCD,NGAYSINH,GIOITINH ,STK ,SDT,DIACHI,TINHTRANGHONNHAN ,BANGTINHOC,TRINHDOHOCVAN,HESOLUONG,PHUCAP,QUOCTICH) " +
                 "values('" + int.Parse(manv.Text)+ "','" + cbpb.SelectedValue.ToString() + "',N'" + ten.Text + "','"+ HienThiTT.Manv.link + "','" + txcccd.Text + "',@dateValue,N'" + cbgioitinh.Text + "'," +
                 "'" + txtk.Text + "','" + txsdt.Text + "',N'" + txdc.Text + "',N'" + cbonnhan.Text + "',N'" + cbth.Text + "',N'" + cbhocvan.Text + "','" + txhsl.Text + "','" + txphucap.Text + "',N'" + cbqt.Text + "')";
       
            SqlCommand comd = new SqlCommand(sqlthem, conn);
            comd.Parameters.Add("@dateValue", SqlDbType.DateTime).Value = selectedDate;
            comd.ExecuteNonQuery();

            // them tdnn
            conn.Close();
            Ketnoi();
            string sqlt = "insert into nhanviencotrinhdonn(tennn, idnhanvien) " +
                  "values('" + nn.Text + "','" + int.Parse(manv.Text) + "')";
            SqlCommand cmd = new SqlCommand(sqlt, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Thêm nhân viên thành công!");
           
        }
        /*
        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }*/

        private void button1_Click(object sender, EventArgs e)
        {
            CNHDLD HD = new CNHDLD();
            HD.Show();
            this.Close();
            
        }

        private void cbqt_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
