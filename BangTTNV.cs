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
using WindowsFormsApp1;
using OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;
using OfficeOpenXml.Style;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using OfficeOpenXml.Style;

namespace QLNS
{
    public partial class BangTTNV : Form
    {
        public SqlConnection conn;
        public void ketnoi()
        {
            string str = "server = DELL; database=qlnhansu; integrated security =true";
            //string str = "server = ADMIN\\SQLEXPRESS; database=qlnhansu; integrated security =true";
            conn = new SqlConnection();
            conn.ConnectionString = str;
            conn.Open();
        }

        public BangTTNV()
        {
            InitializeComponent();
        }
        public class Hienthitt
        {
            public static string Manv;
            public static string HoTen;
            public static string ngaylam;
            public static string Phongban;
            public static string mapb;
        }

        public void BangTTNV_Load(object sender, EventArgs e)
        {
            ketnoi();
            NhanSu NS = new NhanSu();
            NS.Hienthi("select nhanvien.IDNHANVIEN ,NHANVIEN.HOTEN,NHANVIEN.ID_PB,NHANVIEN.GIOITINH,NHANVIEN.NGAYSINH,hopdongld.ngaybd, phongban.tenPB,hopdongld.chucvu,loaihdld.TENHD from nhanvien,hopdongld,phongban,loaihdld where nhanvien.ID_PB = phongban.ID_PB and NhanVien.IDNhanVien = HopDongLD.IDNhanVien and loaihdld.LOAIHD=hopdongld.LOAIHD;", dg, conn);
            NS.Getheader(dg);
            dg.Columns["ID_PB"].HeaderText = "Mã phòng ban";
            Function f = new Function();
            f.LayDLCombobox("select * from PHONGBAN", cbpb, "TENPB", "ID_PB", conn);

            Function h = new Function();
            h.loadCB("select * from phongban", cbpbsua, "TENPB", "ID_PB", conn);
            Function f1 = new Function();
            f1.LayDLCombobox("select * from PHONGBAN", cbpbsua, "TENPB", "ID_PB", conn);


        }

        private void dg_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Hienthitt.Manv = dg.Rows[e.RowIndex].Cells[0].Value.ToString();
            Hienthitt.HoTen = dg.Rows[e.RowIndex].Cells[1].Value.ToString();
            Hienthitt.ngaylam = dg.Rows[e.RowIndex].Cells[5].Value.ToString();
            Hienthitt.Phongban = dg.Rows[e.RowIndex].Cells[6].Value.ToString();
            Hienthitt.mapb = dg.Rows[e.RowIndex].Cells[3].Value.ToString();



                HienThiTT nv = new HienThiTT();
            nv.Show();

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
        private void txtnhap_KeyDown(object sender, KeyEventArgs e)
        {
            ketnoi();
            if (e.KeyCode == Keys.Enter)
            {
                NhanSu NS = new NhanSu();
                NS.Hienthi("select nhanvien.IDNHANVIEN , NHANVIEN.HOTEN,NHANVIEN.GIOITINH,NHANVIEN.NGAYSINH,hopdongld.ngaybd, phongban.tenPB,hopdongld.chucvu,loaihdld.TENHD from nhanvien,hopdongld,phongban,loaihdld " +
                    "where nhanvien.ID_PB = phongban.ID_PB and NhanVien.IDNhanVien = HopDongLD.IDNhanVien " + "and loaihdld.LOAIHD=hopdongld.LOAIHD and phongban.ID_PB ='" + cbpb.SelectedValue.ToString() +
                    "' and ( nhanvien.Idnhanvien like '%" + txtnhap.Text + "%' or hoten like N'%" + txtnhap.Text + "')", dg, conn);
            }

        }
        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void bttim_Click(object sender, EventArgs e)
        {
            ketnoi();
            NhanSu NS = new NhanSu();
            NS.Hienthi("select nhanvien.IDNHANVIEN , NHANVIEN.HOTEN,NHANVIEN.GIOITINH,NHANVIEN.NGAYSINH,hopdongld.ngaybd, phongban.tenPB,hopdongld.chucvu,loaihdld.TENHD from nhanvien,hopdongld,phongban,loaihdld " +
                "where nhanvien.ID_PB = phongban.ID_PB and NhanVien.IDNhanVien = HopDongLD.IDNhanVien " + "and loaihdld.LOAIHD=hopdongld.LOAIHD and phongban.ID_PB ='" + cbpb.SelectedValue.ToString() +
                "' and (  hoten like N'%" + txtnhap.Text + "%')", dg, conn);
        }

        // xuat file excel
        private void btin_Click(object sender, EventArgs e)
        {
            dg.AllowUserToAddRows = false;

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("DSNV");
            ExcelRange header = worksheet.Cells[1, 2];
            header.Value = "DANH SÁCH NHÂN VIÊN";
            header.Style.Font.Bold = true;
            header.Style.Font.Color.SetColor(Color.White);
            header.Style.Fill.PatternType = ExcelFillStyle.Solid;
            header.Style.Fill.BackgroundColor.SetColor(Color.Blue);
            worksheet.Column(3).Width = 15;
            worksheet.Column(4).Width = 15;
            worksheet.Column(5).Width = 18;
            worksheet.Column(6).Width = 18;
            worksheet.Column(2).Width = 25;
            worksheet.Column(7).Width = 25;
            worksheet.Column(8).Width = 25;
            worksheet.Column(9).Width = 25;

            string[] headerTexts = { "Mã NV", "Tên nhân viên","Mã phòng ban","Giới tính", "Ngày sinh","Ngày vào làm", "Phòng ban","Chức vụ","Hợp Đồng" };
            for (int i = 0; i < headerTexts.Length; i++)
            {
                worksheet.Cells[3, i + 1].Value = headerTexts[i];
                ExcelRange title = worksheet.Cells[3, i + 1];
                title.Style.Font.Bold = true;
                title.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                title.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            }

            // dih dang ngay sinh
            for (int i = 1; i <= dg.Rows.Count; i++)
            {
                 DateTime ngaySinh = (DateTime)dg.Rows[i - 1].Cells[4].Value ;
                worksheet.Cells[i + 4, 5].Value = ngaySinh;
                worksheet.Cells[i + 4, 5].Style.Numberformat.Format = "dd/MM/yyyy";
                
            }
           
            // kt
            // dinh dang ngay vao lam
            for (int i = 1; i <= dg.Rows.Count; i++)
            {
                DateTime ngaylam = (DateTime)dg.Rows[i - 1].Cells[5].Value;
                worksheet.Cells[i + 4, 6].Value = ngaylam;
                worksheet.Cells[i + 4, 6].Style.Numberformat.Format = "dd/MM/yyyy";
            }
            // kt
            for (int i = 1; i <= dg.Columns.Count; i++)
            {
                worksheet.Cells[5, i].Value = dg.Columns[i - 1].HeaderText;
            }
            for (int i = 1; i <= dg.Rows.Count; i++)
            {
                
                for (int j = 1; j <= dg.Columns.Count; j++)
                {
                    
                    worksheet.Cells[i + 4, j].Value = dg.Rows[i - 1].Cells[j - 1].Value;

                    // canh giua
                    ExcelRange gt = worksheet.Cells[i + 4, j];
                    gt.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    gt.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                }
            }
           
            string fileName = "D:\\LTUD\\DSNV\\Dsnv.xlsx";
            FileInfo excelFile = new FileInfo(fileName);


            excelPackage.SaveAs(excelFile);
            
            // mo file
            FileInfo fileInfo = new FileInfo(fileName);
            ExcelPackage ex= new ExcelPackage(fileInfo);

            // lay sheet dau
            ExcelWorksheet worksh = excelPackage.Workbook.Worksheets[0];

            // hien thi
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;
            Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(fileName);
           
        }

        private void btsua_Click(object sender, EventArgs e)
        {
            string sqlxoa = "update nhanvien set ID_PB='"+cbpbsua.SelectedValue.ToString()+"' where IDNhanVien='"+txmasua.Text+"'";
            SqlCommand comd = new SqlCommand(sqlxoa,conn);
            comd.ExecuteNonQuery();
            BangTTNV_Load(sender, e);
        }

        private void btxoa_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn có muốn xóa nhân viên này không ?");
            // xoa khoa ngoai
            string sqlhd = "delete from hopdongld where IDNHANVIEN = '" + txmaxoa.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlhd, conn);
            cmd.ExecuteNonQuery();
            // xoa ngoai ng
            string sqlnn = "delete from nhanviencotrinhdonn where IDNHANVIEN = '" + txmaxoa.Text + "'";
            SqlCommand commd = new SqlCommand(sqlnn, conn);
            commd.ExecuteNonQuery();
            // xoa nv
            string sqlxoa = "delete from nhanvien where IDNHANVIEN = '" + txmaxoa.Text + "'";
            SqlCommand comd = new SqlCommand(sqlxoa,conn);
            
            comd.ExecuteNonQuery();

            BangTTNV_Load(sender, e);

        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txmaxoa.Text = dg.Rows[e.RowIndex].Cells[0].Value.ToString();
            txmasua.Text = dg.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Themnv them = new Themnv();
            them.Show();
        }

        private void dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtnhap_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
