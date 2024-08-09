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
    public partial class LuongNV : Form
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
        public LuongNV()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void LuongNV_Load(object sender, EventArgs e)
        {
            Ketnoi();
            //DateTimePicker dateTimePicker1 = new DateTimePicker();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/yyyy";
            this.Controls.Add(dateTimePicker1);
            dateTimePicker1.BringToFront();


            Function t = new Function();

            t.HienThiDG("SELECT IDNHANVIEN, HOTEN,  CCCD, SDT, DIACHI, STK, GIOITINH, NGAYSINH FROM NHANVIEN", dataGridView1, conn);
            t.LayDLCombobox("select * from PHONGBAN", comboBox1, "TENPB", "ID_PB", conn);
            t.TieuDeAllNV(dataGridView1);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Function t = new Function();
            string ngaythang = dateTimePicker1.Value.Year.ToString() + "-" + dateTimePicker1.Value.Month.ToString() + "-01";
            string phongban = comboBox1.SelectedValue.ToString();
            string sql = t.LuongNV(ngaythang, phongban);
            t.HienThiDG(sql, dataGridView1, conn);
            dataGridView1.Columns["luong"].HeaderText = "Lương";

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
