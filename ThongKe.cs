using QLNS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ThongKe : Form
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
        public ThongKe()
        {
            InitializeComponent();
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            /*Load la thay*/
            Function t = new Function();
            Ketnoi();
            t.HienThiDG("select nhanvien.IDNHANVIEN , NHANVIEN.HOTEN,NHANVIEN.GIOITINH,NHANVIEN.NGAYSINH,hopdongld.ngaybd, phongban.tenPB,hopdongld.chucvu,loaihdld.TENHD from nhanvien,hopdongld,phongban,loaihdld where nhanvien.ID_PB = phongban.ID_PB and NhanVien.IDNhanVien = HopDongLD.IDNhanVien and loaihdld.LOAIHD=hopdongld.LOAIHD;", dataGridView1, conn);
            //  t.Getheader(dataGridView1);
            NhanSu nv = new NhanSu();
            nv.Getheader(dataGridView1);

        }

        private void ToanCTy_Click(object sender, EventArgs e)
        {
            //So nhan vien toan cty
            Function t = new Function();
            String lenh = t.ToanCTy();
            t.HienThiDG(lenh, dataGridView1, conn);
            dataGridView1.Columns["Tong_nhan_su_cong_ty"].HeaderText = "Tổng nhân sự công ty";
        }

        private void PhongBan_Click(object sender, EventArgs e)
        {
            //Thong ke nhan vien theo phong ban
            Function t = new Function();
            String lenh = t.PhongBan();
            t.HienThiDG(lenh, dataGridView1, conn);
            dataGridView1.Columns["ID_phong_ban"].HeaderText = "Mã phòng ban";
            dataGridView1.Columns["Ten_phong_ban"].HeaderText = "Tên phòng ban";
            dataGridView1.Columns["So_nhan_vien"].HeaderText = "Số nhân viên";
        }

        private void Tuoi_Click(object sender, EventArgs e)
        {
            //Thong ke khoang tuoi
            Function t = new Function();
            if (t.ktraKhongAm(textBoxAge1) == true && t.ktraKhongAm(textBoxAge2) == true)
            {
                String lenh = t.KhoangTuoi(textBoxAge1, textBoxAge2);
                t.HienThiDG(lenh, dataGridView1, conn);
                dataGridView1.Columns["idnhanvien"].HeaderText = "Mã nhân viên";
                dataGridView1.Columns["hoten"].HeaderText = "Tên nhân viên";
                dataGridView1.Columns["GIOITINH"].HeaderText = "Giới tính";
                dataGridView1.Columns["ngaysinh"].HeaderText = "Ngày sinh";
            }
        }

        private void GioiTinh_Click(object sender, EventArgs e)
        {
            //Thong ke nhan vien theo ti le gioi tinh
            Function t = new Function();
            String lenh = t.GioiTinh();
            t.HienThiDG(lenh, dataGridView1, conn);
            dataGridView1.Columns["Gioi_tinh"].HeaderText = "Giới tính";
            dataGridView1.Columns["So_nhan_vien"].HeaderText = "Số nhân viên";
        }

        private void TuoiNhoDenLon_Click(object sender, EventArgs e)
        {
            //Sap xep tuoi tu nho den lon
            Function t = new Function();
            String lenh = t.SapXepTuoi();
            t.HienThiDG(lenh, dataGridView1, conn);
            dataGridView1.Columns["idnhanvien"].HeaderText = "Mã nhân viên";
            dataGridView1.Columns["hoten"].HeaderText = "Tên nhân viên";
            dataGridView1.Columns["tuoi"].HeaderText = "Tuổi";
        }

        private void TuoiTBNhoLon_Click(object sender, EventArgs e)
        {
            //Avg, min, max tuoi
            Function t = new Function();
            String lenh = t.KetTapTuoi();
            t.HienThiDG(lenh, dataGridView1, conn);
            dataGridView1.Columns["Tuoi_trung_binh"].HeaderText = "Tuổi trung bình";
            dataGridView1.Columns["Tuoi_nho_nhat"].HeaderText = "Tuổi nhỏ nhất";
            dataGridView1.Columns["Tuoi_lon_nhat"].HeaderText = "Tuổi lớn nhất";
        }

        private void NghiPhep_Click(object sender, EventArgs e)
        {
            Ketnoi();
            string nbd = ngayBD.Value.ToString("MM-dd-yyyy");
            string nkt = ngayKT.Value.ToString("MM-dd-yyyy");
            Function t = new Function();
            String lenh = "select ngaynghiphep So_ngay_nghi_phep, n.IDNHANVIEN, n.HOTEN from chamcong c join nhanvien n on c.IDNHANVIEN = n.IDNHANVIEN where thang between '"+ nbd + "' and '"+ nkt + "' order by ngaynghiphep desc;";
            t.HienThiDG(lenh, dataGridView1, conn);
            dataGridView1.Columns["idnhanvien"].HeaderText = "Mã nhân viên";
            dataGridView1.Columns["hoten"].HeaderText = "Tên nhân viên";
            dataGridView1.Columns["So_ngay_nghi_phep"].HeaderText = "Số ngày nghỉ phép";
            SqlCommand comd = new SqlCommand(lenh, conn);
            comd.ExecuteNonQuery();
         
        }

        private void KhongPhep_Click(object sender, EventArgs e)
        {
            Ketnoi();
            //Thong ke ngay nghi khong phep
            string nbd = ngayBD.Value.ToString("MM-dd-yyyy");
            string nkt = ngayKT.Value.ToString("MM-dd-yyyy");
            Function t = new Function();
            String lenh = "select songaynghikhongphep So_ngay_nghi_khong_phep, n.IDNHANVIEN, n.HOTEN from chamcong c join nhanvien n on c.IDNHANVIEN = n.IDNHANVIEN where thang between '" + nbd + "' and '" + nkt + "' order by songaynghikhongphep desc;";
            t.HienThiDG(lenh, dataGridView1, conn);
            dataGridView1.Columns["idnhanvien"].HeaderText = "Mã nhân viên";
            dataGridView1.Columns["hoten"].HeaderText = "Tên nhân viên";
            dataGridView1.Columns["So_ngay_nghi_khong_phep"].HeaderText = "Số ngày nghỉ không phép";
            SqlCommand comd = new SqlCommand(lenh, conn);
            comd.ExecuteNonQuery();
        }

        private void ThamNien_Click(object sender, EventArgs e)
        {
            //Thong ke tham nien tu lon den nho
            Function t = new Function();
            String lenh = t.ThamNien();
            t.HienThiDG(lenh, dataGridView1, conn);
            dataGridView1.Columns["idnhanvien"].HeaderText = "Mã nhân viên";
            dataGridView1.Columns["hoten"].HeaderText = "Tên nhân viên";
            dataGridView1.Columns["Tham_nien"].HeaderText = "Thâm niên";
        }

        private void HetHDLD_Click(object sender, EventArgs e)
        {
            //Thong ke nhan vien sap het han hop dong
            Function t = new Function();
            if (t.ktraKhongAm(NamBD) == true && t.ktraKhongAm(NamKT) == true)
            {
                String lenh = t.HanHopDong(NamBD, NamKT);
                t.HienThiDG(lenh, dataGridView1, conn);
                dataGridView1.Columns["idnhanvien"].HeaderText = "Mã nhân viên";
                dataGridView1.Columns["hoten"].HeaderText = "Tên nhân viên";
                dataGridView1.Columns["So_nam_con_lai"].HeaderText = "Số năm còn lại";
                dataGridView1.Columns["So_thang_con_lai"].HeaderText = "Số tháng còn lại";
            }
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            //Reset
            Function t = new Function();
            t.HienThiDG("select nhanvien.IDNHANVIEN , NHANVIEN.HOTEN,NHANVIEN.GIOITINH,NHANVIEN.NGAYSINH,hopdongld.ngaybd, phongban.tenPB,hopdongld.chucvu,loaihdld.TENHD from nhanvien,hopdongld,phongban,loaihdld where nhanvien.ID_PB = phongban.ID_PB and NhanVien.IDNhanVien = HopDongLD.IDNhanVien and loaihdld.LOAIHD=hopdongld.LOAIHD;", dataGridView1, conn);
           // t.Getheader(dataGridView1);
            NhanSu nv = new NhanSu();
            nv.Getheader(dataGridView1);

        }

        private void ChucVu_Click(object sender, EventArgs e)
        {
            //Thong ke nhan vien theo chuc vu
            Function t = new Function();
            String lenh = t.ChucVu();
            t.HienThiDG(lenh, dataGridView1, conn);
            dataGridView1.Columns["Chuc_vu"].HeaderText = "Chức vụ";
            dataGridView1.Columns["So_nhan_vien"].HeaderText = "Số nhân viên";
        }

        private void HocVan_Click(object sender, EventArgs e)
        {
            //Hoc van
            Function t = new Function();
            String lenh = t.HocVan();
            //String lenh = "select trinhdohocvan Trinh_do_hoc_van, count(IDNhanVien) So_nhan_vien from nhanvien group by trinhdohocvan order by count(IDNhanVien) desc;"
            t.HienThiDG(lenh, dataGridView1, conn);
            dataGridView1.Columns["Trinh_do_hoc_van"].HeaderText = "Trình độ học vấn";
            dataGridView1.Columns["So_nhan_vien"].HeaderText = "Số nhân viên";
        }

        private void NgoaiNgu_Click(object sender, EventArgs e)
        {
            //Ngoai ngu
            Function t = new Function();
            String lenh = t.NgoaiNgu();
            t.HienThiDG(lenh, dataGridView1, conn);
            dataGridView1.Columns["Ten_ngoai_ngu"].HeaderText = "Tên ngoại ngữ";
            dataGridView1.Columns["So_nhan_vien"].HeaderText = "Số nhân viên";
        }

        private void TinHoc_Click(object sender, EventArgs e)
        {
            //Tin hoc
            Function t = new Function();
            String lenh = t.TinHoc();
            t.HienThiDG(lenh, dataGridView1, conn);
            dataGridView1.Columns["Bang_tin_hoc"].HeaderText = "Bằng tin học";
            dataGridView1.Columns["So_nhan_vien"].HeaderText = "Số nhân viên";
        }

        private void ThangSinh_Click(object sender, EventArgs e)
        {
            //Thong ke theo thang sinh
            Function t = new Function();
            String lenh = t.ThangSinh();
            t.HienThiDG(lenh, dataGridView1, conn);
            dataGridView1.Columns["Thang_sinh"].HeaderText = "Tháng sinh";
            dataGridView1.Columns["Tong_so_nhan_vien"].HeaderText = "Số nhân viên";
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Function t = new Function();
            String lenh = t.ThongTinChiTiet(dataGridView1, e);
            if (lenh != "null")
            {
                t.HienThiDG(lenh, dataGridView1, conn);
                dataGridView1.Columns["idnhanvien"].HeaderText = "Mã nhân viên";
                dataGridView1.Columns["hoten"].HeaderText = "Tên nhân viên";
                dataGridView1.Columns["GIOITINH"].HeaderText = "Giới tính";
                dataGridView1.Columns["ngaysinh"].HeaderText = "Ngày sinh";
            }
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
    }
}
