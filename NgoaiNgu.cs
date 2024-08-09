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
    public partial class NgoaiNgu : Form
    {
        public SqlConnection conn;
        public void Ketnoi()
        {
            string connect = "server=DELL; database=QLNhanSu; integrated security = True";
            //string connect = "server = ADMIN\\SQLEXPRESS; database=qlnhansu; integrated security =true";
            conn = new SqlConnection();
            conn.ConnectionString = connect;
            conn.Open();

        }
        public NgoaiNgu()
        {
            InitializeComponent();
        }
        
        private void NgoaiNgu_Load(object sender, EventArgs e)
        {
            Function t = new Function();
            Ketnoi();
            t.HienThiDG("select nhanvien.IDNHANVIEN , NHANVIEN.HOTEN,trinhdongoaingu.tennn,trinhdongoaingu.bac,NHANVIEN.GIOITINH,NHANVIEN.NGAYSINH,hopdongld.ngaybd, phongban.tenPB,hopdongld.chucvu,loaihdld.TENHD from nhanvien,hopdongld,phongban,loaihdld, nhanviencotrinhdonn, trinhdongoaingu where nhanvien.ID_PB = phongban.ID_PB and NhanVien.IDNhanVien = HopDongLD.IDNhanVien and loaihdld.LOAIHD=hopdongld.LOAIHD and NhanVien.IDNhanVien = nhanviencotrinhdonn.IDNhanVien and nhanviencotrinhdonn.TenNN = trinhdongoaingu.TenNN;", dataGridView1, conn);
            //  t.Getheader(dataGridView1);
            NhanSu nv = new NhanSu();
            nv.Getheader(dataGridView1);
            dataGridView1.Columns["tennn"].HeaderText = "Ngôn ngữ";
            dataGridView1.Columns["bac"].HeaderText = "Bậc";
            string sql = "SELECT tennn," +
                "  CASE" +
                    "  WHEN tennn like 'English%' THEN N'Tiếng Anh ' + bac" +
                    "  WHEN tennn like 'French%' THEN N'Tiếng Pháp ' + bac" +
                    "  WHEN tennn like 'Japanese%' THEN N'Tiếng Nhật ' + bac" +
                    "  ELSE tennn " +
                "  END AS nn " +
                "FROM trinhdongoaingu";
            t.LayDLCombobox(sql, comboBox1, "nn", "tennn", conn);
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
            //Kiểm tra nếu cột hiện tại là cột "tennn" và giá trị bắt đầu bằng "English"
            if (dataGridView1.Columns[e.ColumnIndex].Name == "tennn" && e.Value != null && e.Value.ToString().StartsWith("English"))
            {
                //Thay đổi giá trị hiển thị của ô thành "Tiếng Anh"
                e.Value = "Tiếng Anh";
                e.FormattingApplied = true; //Đánh dấu đã thay đổi giá trị hiển thị
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "tennn" && e.Value != null && e.Value.ToString().StartsWith("French"))
            {
                //Thay đổi giá trị hiển thị của ô thành "Tiếng Anh"
                e.Value = "Tiếng Pháp";
                e.FormattingApplied = true; //Đánh dấu đã thay đổi giá trị hiển thị
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "tennn" && e.Value != null && e.Value.ToString().StartsWith("Japanese"))
            {
                //Thay đổi giá trị hiển thị của ô thành "Tiếng Anh"
                e.Value = "Tiếng Nhật";
                e.FormattingApplied = true; //Đánh dấu đã thay đổi giá trị hiển thị
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlthem = "insert into nhanviencotrinhdonn(tennn, idnhanvien) " +
                     "values('" + comboBox1.SelectedValue.ToString() + "','" + textBox1.Text + "')";

                SqlCommand comd = new SqlCommand(sqlthem, conn);
                var rowsA = comd.ExecuteNonQuery();
                if (rowsA > 0)
                {
                    MessageBox.Show("Thêm chứng chỉ ngoại ngữ thành công.");
                }
                else
                {
                    MessageBox.Show("Thêm chứng chỉ ngoại ngữ không thành công.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm chứng chỉ ngoại ngữ không thành công. Vui lòng kiểm tra lại.");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sqlthem = "delete from nhanviencotrinhdonn where " +
                 "tennn ='" + comboBox1.SelectedValue.ToString() + "' and idnhanvien = '" + textBox1.Text + "'";

            SqlCommand comd = new SqlCommand(sqlthem, conn);
            var rowsA = comd.ExecuteNonQuery();
            if (rowsA > 0)
            {
                MessageBox.Show("Xóa chứng chỉ ngoại ngữ thành công.");
                // Function h = new Function();
                // h.hienThiDT(dtgv, "SELECT * FROM nhanvien", conn);
            }
            else
            {
                MessageBox.Show("Xóa chứng chỉ ngoại ngữ không thành công.");
            }
        }
    }
}
