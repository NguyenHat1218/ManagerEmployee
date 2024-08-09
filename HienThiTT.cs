using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLNS
{
    public partial class HienThiTT : Form
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
        public HienThiTT()
        {
            InitializeComponent();
        }
        public Form formch;
        public void OpenChildForm(Form childForm)
        {
            if(formch!= null)
            {
                formch.Close();
            }
            formch = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle= FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel1bd.Controls.Add(childForm);
            panel1bd.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        public class Manv
        {

            public static string ma = BangTTNV.Hienthitt.Manv;
            public static string link;

        }
        private void HienThiTT_Load(object sender, EventArgs e)
        {
            Ketnoi();
            txma.Text = BangTTNV.Hienthitt.Manv;
            txten.Text= BangTTNV.Hienthitt.HoTen;
            txvaolam.Text = BangTTNV.Hienthitt.ngaylam;
            txdc.Text = BangTTNV.Hienthitt.HoTen;
            txpb.Text =BangTTNV.Hienthitt.Phongban;
            int ma = int.Parse(txma.Text);



            string sql = "select * from nhanvien where IDNHANVIEN ='"+ma+ "'";
            SqlCommand comd = new SqlCommand(sql,conn);
            SqlDataReader reader = comd.ExecuteReader();

            while (reader.Read())
            {
                txcccd.Text = reader.GetString(2);
                string anh = reader.GetString(4);
                pbanh.Image = Image.FromFile(anh);
               pbanh.SizeMode = PictureBoxSizeMode.StretchImage;
                DateTime ngaysinh = reader.GetDateTime(5);
                dtngaysinh.Text = ngaysinh.ToString();
                cbgioitinh.Text =reader.GetString(6);
                txtk.Text = reader.GetString(7);
                txsdt.Text = reader.GetString(8);   
                txdc.Text = reader.GetString(9);
                cbonnhan.Text = reader.GetString(10);
                txtinhoc.Text = reader.GetString(11);
                cbhocvan.Text = reader.GetString(12);
                txhsl.Text = reader.GetDecimal(13).ToString();
                 decimal pc = reader.GetDecimal(14);
                txphucap.Text = pc.ToString();
                txquoctich.Text = reader.GetString(15);
             
            }
            conn.Close();
            Ketnoi();
            string sql1 = "select trinhdongoaingu.TENNN, trinhdongoaingu.BAC from trinhdongoaingu,nhanvien where nhanvien.IDNHANVIEN='" + ma+ "'";
            SqlCommand comd1 = new SqlCommand(sql1, conn);
            SqlDataReader reader1 = comd1.ExecuteReader();
            while (reader1.Read())
            {
                txnn.Text = reader1.GetString(0);
                txbac.Text = reader1.GetString(1);
            }

            //   tx1.Text = Manv.link;
            this.StartPosition = FormStartPosition.Manual;
           int x = (Screen.PrimaryScreen.WorkingArea.Width - this.Width)/2 ;
           int y = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
          this.Location = new Point(x, y);
        }

        private void MenuItemhd_Click(object sender, EventArgs e)
        {
            OpenChildForm(new HDLD());

        }

        private void MenuItemnv_Click(object sender, EventArgs e)
        {
            OpenChildForm(new HienThiTT());
            panel2.Visible = false;
            menuStrip1.Visible = false;
        }
        public void anh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // m.n lưu ảnh ở đâu thì đổi lại đường dẫn ở đó của biến imagePath không nó lỗi
                pbanh.Image = Image.FromFile(openFileDialog1.FileName);
                string imagePath = "D:\\LTUD\\QLNhanSu\\Resources\\" + openFileDialog1.SafeFileName;
                Manv.link = imagePath;
                File.Copy(openFileDialog1.FileName, imagePath, true);

            }

        }

        private void btsua_Click(object sender, EventArgs e)
        {
            Ketnoi();
            string sua = "update nhanvien set  CCCD ='" + txcccd.Text + "',HOTEN =N'" + txten.Text+ "', NGAYSINH = '"+ DateTime.Parse(dtngaysinh.Text) + "',GIOITINH =N'"+ cbgioitinh.Text + "',STK='"+ txtk.Text + "', SDT='"+ txsdt.Text + "'," +
                "DIACHI=N'"+ txdc.Text + "',TINHTRANGHONNHAN=N'"+ cbonnhan.Text + "',BANGTINHOC=N'"+ txtinhoc.Text + "'," +
                "TRINHDOHOCVAN=N'" + cbhocvan.Text + "',HESOLUONG='"+ txhsl.Text + "',PHUCAP='"+ txphucap.Text + "'," +
                "QUOCTICH=N'"+ txquoctich.Text + "' where IDNHANVIEN ='"+ txma.Text + "'";
            SqlCommand comd = new SqlCommand(sua, conn);
            comd.ExecuteNonQuery();

            // sua ảnh
            //string sqlsuaanh = "INSERT INTO Images (Name, Path) VALUES (@Name, @Path)";
            string suaanh = "update nhanvien set linkanh = '"+ Manv.link + "' where IDNHANVIEN ='"+ txma.Text + "'";
            SqlCommand cmd = new SqlCommand(suaanh, conn);
            cmd.ExecuteNonQuery();
            // ket thuc sua
          
            // HienThiTT_Load
            MessageBox.Show("Cập nhật nhân viên thành công!");
           BangTTNV nv = new BangTTNV();
            nv.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    
    }
}
