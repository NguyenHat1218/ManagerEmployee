using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using static System.Net.Mime.MediaTypeNames.Image;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.VisualStyles;
using static QLNS.Login;
using QLNS;
using QLNhanSu;

namespace WindowsFormsApp1
{
    public partial class QLNV : Form
    {
        public SqlConnection conn;
        public String file = " ";
        public SqlCommand cmd = new SqlCommand();

        public void getConnection()
        {
            string connect = "server=DELL; database=QLNhanSu; integrated security = True";
            //string connect = "server = DESKTOP-P7FD0VQ; database=abc; integrated security =true";
            //string connect = "server = ADMIN\\SQLEXPRESS; database=qlnhansu; integrated security =true";
            conn = new SqlConnection();
            conn.ConnectionString = connect;
            conn.Open();
        }
        public QLNV()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getConnection();
            Function h = new Function();
            //h.hienThiDT(dtgv, "select *  from nhanvien", conn);
            h.loadCB("select * from phongban", comboBox1, "TENPB", "ID_PB", conn);
            dateTimePicker1.CustomFormat = "yyyy/MM/dd";
            SqlDataAdapter dt = new SqlDataAdapter("select IDnhanvien, hoten, id_pb, ngaysinh, gioitinh, diachi, stk, sdt, hesoluong, phucap, tinhtranghonnhan, bangtinhoc, trinhdohocvan, cccd, quoctich, linkanh from nhanvien where IDnhanvien ='" + Login.Truongphong.ma + "'", conn);
            DataSet ds = new DataSet();
            dt.Fill(ds, "NV");
            txtma.Text = ds.Tables[0].Rows[0]["IDnhanvien"].ToString();
            txtten.Text = ds.Tables[0].Rows[0]["hoten"].ToString();
            comboBox1.SelectedValue = ds.Tables[0].Rows[0]["id_pb"].ToString();
            dateTimePicker1.Text = ds.Tables[0].Rows[0]["ngaysinh"].ToString();
            if(ds.Tables[0].Rows[0]["gioitinh"].ToString() == "Nam") radioButton1.Checked=true;
            else radioButton2.Checked = true;
            txtdiachi.Text = ds.Tables[0].Rows[0]["diachi"].ToString();
            txtstk.Text = ds.Tables[0].Rows[0]["stk"].ToString();
            txtsdt.Text = ds.Tables[0].Rows[0]["sdt"].ToString();

            txthsl.Text = ds.Tables[0].Rows[0]["hesoluong"].ToString();
            txtphucap.Text = ds.Tables[0].Rows[0]["phucap"].ToString();
            txttthn.Text = ds.Tables[0].Rows[0]["tinhtranghonnhan"].ToString();
            cbbth.Text = ds.Tables[0].Rows[0]["bangtinhoc"].ToString();
            cbtdhv.Text = ds.Tables[0].Rows[0]["trinhdohocvan"].ToString();
            txtcccd.Text = ds.Tables[0].Rows[0]["cccd"].ToString();
            txtqt.Text = ds.Tables[0].Rows[0]["quoctich"].ToString();

            pictureBox1.Image = Image.FromFile(ds.Tables[0].Rows[0]["linkanh"].ToString() + "");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        //Hàm hiện ảnh lên picture box
        public void showImage(PictureBox PictureBox1, string path)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Choose Image(*.jpg;*.jpeg;*.tif;*.jfif)|*.jpg;*.jpeg;*.tif;*.jfif";
            if (op.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(op.FileName);
                file = op.FileName;
                txb_Path.Text = file;
            }
        }


        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnchose_Click(object sender, EventArgs e)
        {
             /* OpenFileDialog op = new OpenFileDialog();

               if(op.ShowDialog() == DialogResult.OK)
               {
                 openFileDialog1.Filter = "JPEG files | *.jpg | PNG files | *.png | GIF files | *.gif | TIFF files | *.tif | BMP files | *.bmp";
                 file = op.FileName;
                 //int width =Convert.ToInt32();

                 Image img = Image.FromFile(file);
                 pictureBox1.Image = img;

               }*/
            showImage(pictureBox1, txb_Path.Text);
            /* SaveFileDialog dialog = new SaveFileDialog();
             if (dialog.ShowDialog() == DialogResult.OK)
             {
                 int width = Convert.ToInt32(pictureBox1.Width);
                 int height = Convert.ToInt32(pictureBox1.Height);
                 using (Bitmap bmp = new Bitmap(width, height))
                 {
                     pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0, width, height));
                     bmp.Save(dialog.FileName, ImageFormat.Jpeg);
                 }
             }*/

        }

        private void btnthem_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtma.Clear();
            txtten.Clear();
            comboBox1.Text = "";
            txtstk.Clear();
            txtsdt.Clear();
            txtdiachi.Clear();
            txttthn.Clear();
            radioButton1.Text = null;
            radioButton2.Text = null;
            cbbth.Text = "";
            cbtdhv.Text = "";
            txthsl.Clear();
            txtphucap.Clear();
            txtcccd.Clear();
            txtqt.Clear();
            pictureBox1.InitialImage = null;
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
           /* if(MessageBox.Show("Bạn có chắc muốn xóa nhân viên "+txtma.Text,"Xác nhận"+txtten.Text,MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                getConnection();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Delete from nhanvien where idnhanvien= '" + txtma.Text + "'";
                    var kq = cmd.ExecuteNonQuery();
                    if (kq != 0)
                    {
                        MessageBox.Show("Xóa thành công nhân viên.");
                        getConnection();
                        Function h = new Function();
                        h.hienThiDT(dtgv, "select *  from nhanvien", conn);
                    }
                    else
                    {
                        MessageBox.Show("Hãy thử lại sau.");
                    }
                }
                conn.Close();
            }*/
        }

       /* private void button6_Click(object sender, EventArgs e)
        {
           // Home h = new Home();
            //h. = this;
           // h.Show();
        }*/

        private void btncapnhat_Click(object sender, EventArgs e)
        {
            //SqlCommand cmd = new SqlCommand("update nhanvien set cccd='" + txtcccd.Text + "',hoten='" + txtten.Text + "' where idnhanvien='" + txtma.Text + "'", conn);
            // SqlCommand cmd = new SqlCommand
             
             cmd = conn.CreateCommand();

            string gender;
            gender = "";
            if (radioButton1.Checked == true)
            {
                gender = "Nam";
            }
            else if (radioButton2.Checked == true)
            {   gender = "Nu"; }
            if (file == " ") file = Login.Truongphong.anh;
            else Login.Truongphong.anh = file;
            cmd.CommandText = "UPDATE nhanvien set id_pb='" + comboBox1.SelectedValue + "',cccd='" + txtcccd.Text + "',hoten=N'" + txtten.Text + "',linkanh = '" + file + "', Ngaysinh = '" + DateTime.Parse(dateTimePicker1.Text) + "',Gioitinh = '" + gender + "', stk = '" + txtstk.Text + "', sdt = '" + txtsdt.Text + "', diachi =N'" + txtdiachi.Text + "', tinhtranghonnhan =N'" + txttthn.Text + "', bangtinhoc =N'" + cbbth.Text + "', trinhdohocvan =N'" + cbtdhv.Text + "', hesoluong = '" + txthsl.Text + "', phucap = '" + txtphucap.Text + "', quoctich =N'" + txtqt.Text +"' where idnhanvien = '" + txtma.Text + "'";
            //SqlCommand cmd = new SqlCommand "update nhanvien set cccd='" + txtcccd.Text + "',hoten='" + txtten.Text + "' where idnhanvien='" + txtma.Text + "'";
            cmd.ExecuteNonQuery();
            //string nhanvien = "select * from nhanvien";
            //show_Datagrid(query_NHAN_VIEN, dataGridView_NV);
            Function h = new Function();
            MessageBox.Show("Cập nhật thành công");
            //h.hienThiDT(dtgv, "select *  from nhanvien", conn);
            //MessageBox.Show("Sửa nhân viên thành công!");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }


       

        private void txtma_TextChanged(object sender, EventArgs e)
        {

        }

        private void btndoipass_Click(object sender, EventArgs e)
        {
            DoiMatKhau hienthi = new DoiMatKhau();
            hienthi.Show();
        }
    }
}
