using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal class Function
    {
        public void hienThiDT(DataGridView dg, string query, SqlConnection conn)
        {
            SqlDataAdapter adap = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            adap.Fill(ds, "qlbh");
            dg.DataSource = ds;
            dg.DataMember = "qlbh";
        }
        public void themNV(int idnhanvien, string id_pb, string cccd, string hoten, string linkanh, string ngaysinh, string gioitinh,
            string stk,string sdt, string diachi, string tinhtranghonnhan,string bangtinhoc,string trinhdohocvan, float hesoluong , 
            decimal phucap, string quoctich, SqlConnection conn)
        {
            string query = "insert into nhanvien values('" + idnhanvien + "','" + id_pb + "','" + cccd + "','" + hoten + "','" + linkanh + "'," +
                "'" + ngaysinh + "','" + gioitinh + "','" + stk + "','" + sdt + "','" + diachi + "','" + tinhtranghonnhan + "','" + bangtinhoc + "','" + trinhdohocvan + "','" + hesoluong + "'," +
                "'" + phucap + "','" + quoctich + "')";

            MessageBox.Show("Thông tin nhân viên đã được lưu.");
            try
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void loadCB(string query, ComboBox cb, string hienthi, string giatri, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader read = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(read);
            cb.DataSource = table;
            cb.DisplayMember = hienthi;
            cb.ValueMember = giatri;
        }
     
        public void delProduct(string mahang, SqlConnection conn)
        {
            string query = "delete from mat_hang where mahang = '" + mahang + "'";
            MessageBox.Show(query);
            try
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        ////phuong hiếu
        public void HienThiDG(string cautruyvan, DataGridView dg, SqlConnection conn)
        {
            SqlDataAdapter dt = new SqlDataAdapter(cautruyvan, conn);
            DataSet dase = new DataSet();
            dt.Fill(dase, "sql");
            dg.DataSource = dase;
            dg.DataMember = "sql";
        }
        public void Getheader(DataGridView dg)
        {
            dg.Columns["idnhanvien"].HeaderText = "Mã nhân viên";
            dg.Columns["hoten"].HeaderText = "Tên nhân viên";
            dg.Columns["GIOITINH"].HeaderText = "Giới tính";
            dg.Columns["ngaysinh"].HeaderText = "Ngày sinh";
            dg.Columns["ngaybd"].HeaderText = "Ngày vào làm";
            dg.Columns["tenpb"].HeaderText = "Phòng ban";
            dg.Columns["chucvu"].HeaderText = "Chức vụ";
            dg.Columns["Tenhd"].HeaderText = "Loại hợp đồng";
        }
        public Boolean ktraKhongAm(TextBox textbox)
        {
            int txt;
            if (int.TryParse(textbox.Text, out txt))
            {
                if (txt >= 0) { return true; }
                else
                {
                    MessageBox.Show("Vui long nhap so khong am!");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Vui long nhap mot so!");
                return false;
            }
        }

        public String ToanCTy()
        {//So nhan vien toan cty
            return "select count(IDNhanvien) Tong_nhan_su_cong_ty from NHANVIEN;";
        }
        public String PhongBan()
        {//Thong ke nhan vien theo phong ban
            return "select p.ID_PB ID_phong_ban, TenPB Ten_phong_ban, count(IDNhanVien) So_nhan_vien from NHANVIEN n join phongban p ON n.ID_PB=p.ID_PB group by p.ID_PB, TenPB order by count(IDNhanVien) desc;";
        }
        public String KhoangTuoi(TextBox textBoxAge1, TextBox textBoxAge2)
        {//Thong ke khoang tuoi
            return "select IDNHANVIEN, HOTEN, GIOITINH, NGAYSINH from NHANVIEN where DATEDIFF(year, NgaySinh, GETDATE()) BETWEEN " + textBoxAge1.Text + " AND " + textBoxAge2.Text + ";";
        }

        public String SapXepTuoi()
        {//Sap xep tuoi tu nho den lon
            return "select IDNhanVien, Hoten, DATEDIFF(year, NgaySinh, GETDATE()) tuoi from NHANVIEN order by DATEDIFF(year, NgaySinh, GETDATE());";
        }

        public String KetTapTuoi()
        {//Avg, min, max tuoi
            return "select avg(DATEDIFF(year, NgaySinh, GETDATE())) Tuoi_trung_binh, min(DATEDIFF(year, NgaySinh, GETDATE())) Tuoi_nho_nhat, max(DATEDIFF(year, NgaySinh, GETDATE())) Tuoi_lon_nhat from NHANVIEN;";
        }
        public String ThangSinh()
        {//Thong ke theo thang sinh
            return "select MONTH(NgaySinh) Thang_sinh, count(IDNhanVien) Tong_so_nhan_vien from NHANVIEN group by MONTH(NgaySinh) order by MONTH(NgaySinh);";
        }
        public String GioiTinh()
        {//Thong ke nhan vien theo ti le gioi tinh
            return "select GioiTinh Gioi_tinh, count(IDNhanVien) So_nhan_vien from NHANVIEN group by GioiTinh order by count(IDNhanVien) desc";
        }
        //=====================SUA=========================
        public String NgayNghiPhep(DateTimePicker ngayBD, DateTimePicker ngayKT)
        {//Thong ke ngay nghi phep
           
            return "select ngaynghiphep So_ngay_nghi_phep, n.IDNHANVIEN, n.HOTEN from chamcong c join nhanvien n on c.IDNHANVIEN = n.IDNHANVIEN where thang between  '"+ ngayBD.Value.ToShortDateString() + "' and '" + ngayKT.Value.ToShortDateString() + "' order by ngaynghiphep desc;";
        }
        public String NgayNghiKhongPhep(DateTimePicker ngayBD, DateTimePicker ngayKT)
        {//Thong ke ngay nghi khong phep
            return "select songaynghikhongphep So_ngay_nghi_khong_phep, n.IDNHANVIEN, n.HOTEN from chamcong c join nhanvien n on c.IDNHANVIEN = n.IDNHANVIEN where thang between '" + ngayBD.Value.ToShortDateString() + "' and '" + ngayBD.Value.ToShortDateString() + "' order by songaynghikhongphep desc;";
        }
        public String HocVan()
        {//Hoc van
            return "select trinhdohocvan Trinh_do_hoc_van, count(IDNhanVien) So_nhan_vien from nhanvien group by trinhdohocvan order by count(IDNhanVien) desc;";
        }
        public String TinHoc()
        {//Tin hoc
            return "select bangtinhoc Bang_tin_hoc, count(IDNhanVien) So_nhan_vien from nhanvien group by bangtinhoc order by count(IDNhanVien) desc;";
        }
        public String NgoaiNgu()
        {//Ngoai ngu
            return "select t.tennn Ten_ngoai_ngu, bac Bac, count(n.IDNhanVien) So_nhan_vien from nhanvien n join nhanviencotrinhdonn c on n.IDNhanVien=c.IDNhanVien join trinhdongoaingu t on t.tennn = c.tennn group by t.tennn, bac order by count(n.IDNhanVien) desc;";
        }
        public String ThamNien()
        {//Thong ke tham nien tu lon den nho
            return "select DATEDIFF(year, ngayBD, GETDATE()) Tham_nien, n.IDNHANVIEN, n.HOTEN from hopdongld h join nhanvien n on h.IDNHANVIEN = n.IDNHANVIEN  order by DATEDIFF(year, ngayBD, GETDATE()) desc;";
        }
        public String HanHopDong(TextBox NamBD, TextBox NamKT)
        {//Thong ke nhan vien sap het han hop dong
            // Thong ke nhan vien sap het han hop dong
            //String y = "(DATEDIFF(year, GETDATE(), ngayKT))";
            String x = "(MONTH(ngayKT) - MONTH(GETDATE()) +12)";
            String thang = x + " - 12 * CONVERT(INT, " + x + "/12)";
            String nam = "YEAR(ngayKT)-1-YEAR(GETDATE())+CONVERT(INT, " + x + "/12)";
            return "select " + nam + " So_nam_con_lai, " + thang + " So_thang_con_lai, n.IDNHANVIEN, n.HOTEN from hopdongld h join nhanvien n on h.IDNHANVIEN = n.IDNHANVIEN where DATEDIFF(year, GETDATE(), ngayKT) between '" + NamBD.Text + "' and '" + NamKT.Text + "' order by DATEDIFF(year, GETDATE(), ngayKT);";
        }
        public String ChucVu()
        {//Thong ke nhan vien theo chuc vu
            return "select chucvu Chuc_vu, count(IDNhanVien) So_nhan_vien from hopdongld group by chucvu order by count(IDNhanVien) desc;";
        }
        public String ThongTinChiTiet(DataGridView dataGridView1, DataGridViewCellEventArgs e)
        {
            //Ten gia tri cot dau tien
            String value = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            //Gia tri ten cot
            String col = dataGridView1.Columns[0].HeaderText;
            string lenh;
            if (col == "Tháng sinh")
            {
                lenh = "select IDNHANVIEN, HOTEN, GIOITINH, NGAYSINH from NHANVIEN where MONTH(NgaySinh) = '" + value + "';";
            }
            else if (col == "Mã phòng ban")
            {
                lenh = "select IDNHANVIEN, HOTEN, GIOITINH, NGAYSINH from NHANVIEN where ID_PB = '" + value + "';";
            }
            else if (col == "Giới tính")
            {
                lenh = "select IDNHANVIEN, HOTEN, GIOITINH, NGAYSINH from NHANVIEN where GioiTinh = '" + value + "';";
            }
            else if (col == "Chức vụ")
            {
                lenh = "select n.IDNHANVIEN, HOTEN, GIOITINH, NGAYSINH from hopdongld h join nhanvien n on h.IDNhanVien = n.IDNhanVien where chucvu = N'" + value + "';";
            }
            else if (col == "Trình độ học vấn")
            {
                lenh = "select IDNHANVIEN, HOTEN, GIOITINH, NGAYSINH from NHANVIEN where trinhdohocvan = N'" + value + "';";
            }
            else if (col == "Bằng tin học")
            {
                lenh = "select IDNHANVIEN, HOTEN, GIOITINH, NGAYSINH from NHANVIEN where bangtinhoc = '" + value + "';";
            }
            else if (col == "Tên ngoại ngữ")
            {
                lenh = "select n.IDNHANVIEN, HOTEN, GIOITINH, NGAYSINH from nhanvien n join nhanviencotrinhdonn c on n.IDNhanVien=c.IDNhanVien join trinhdongoaingu t on t.tennn = c.tennn where t.tennn = '" + value + "';";
            }
            else
            {
                lenh = "null";
            }
            return lenh;
        }
        // tinh luong
        public void TieuDeAllNV(DataGridView dg)
        {
            dg.Columns["idnhanvien"].HeaderText = "Mã nhân viên";
            dg.Columns["CCCD"].HeaderText = "Số căn cước công dân";
            dg.Columns["hoten"].HeaderText = "Tên nhân viên";
            dg.Columns["ngaysinh"].HeaderText = "Ngày sinh";
            dg.Columns["GIOITINH"].HeaderText = "Giới tính";
            dg.Columns["STK"].HeaderText = "Số tài khoản";
            dg.Columns["SDT"].HeaderText = "Số điện thoại";
            dg.Columns["diachi"].HeaderText = "Địa chỉ";
        }

        public string LuongNV(string thang, string PB)
        {
            string sql = "SELECT n.IDNHANVIEN, HOTEN,  CCCD, SDT, DIACHI, STK, GIOITINH, NGAYSINH, ";
            if (PB == "PB01")
            {
                sql = sql + " FORMAT((30000*HESOLUONG-(NGAYNGHIPHEP*0.02+SONGAYNGHIKHONGPHEP*0.03)*30000*HESOLUONG)+PHUCAP*100, '###,###,##0.00') luong";
            }
            else if (PB == "PB02")
            {
                sql = sql + " FORMAT((22000*HESOLUONG-(NGAYNGHIPHEP*0.02+SONGAYNGHIKHONGPHEP*0.03)*22000*HESOLUONG)+PHUCAP*100, '###,###,##0.00') luong";
            }
            else if (PB == "PB03")
            {
                sql = sql + " FORMAT((18000*HESOLUONG-(NGAYNGHIPHEP*0.02+SONGAYNGHIKHONGPHEP*0.03)*18000*HESOLUONG)+PHUCAP*100, '###,###,##0.00') luong";
            }
            else
            {
                sql = sql + " FORMAT((15000*HESOLUONG-(NGAYNGHIPHEP*0.02+SONGAYNGHIKHONGPHEP*0.03)*15000*HESOLUONG)+PHUCAP*100, '###,###,##0.00') luong";
            }
            sql = sql + " FROM NHANVIEN n JOIN CHAMCONG c ON n.IDNHANVIEN = c.IDNHANVIEN WHERE THANG='" + thang + "' AND ID_PB ='" + PB + "'";
            return sql;
            //MessageBox.Show(sql);
        }

        public void LayDLCombobox(string cautruyvan, ComboBox cb, string hienthiten, string giatrima, SqlConnection conn)
        {
            SqlCommand comd = new SqlCommand(cautruyvan, conn);
            SqlDataReader reader = comd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            cb.DataSource = table;
            cb.DisplayMember = hienthiten; //TEN_PHONG_BAN
            cb.ValueMember = giatrima; //MA_PHONG_BAN
        }
        ///PH_END
    }


}
