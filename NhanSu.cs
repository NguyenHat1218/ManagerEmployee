using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Data.SqlClient;

namespace QLNS
{
    internal class NhanSu
    {
        public void Hienthi(string strsql,DataGridView dg, SqlConnection conn)
        {
            SqlDataAdapter dt = new SqlDataAdapter(strsql, conn);
            DataSet ds = new DataSet();
            dt.Fill(ds,"NV");
            dg.DataSource = ds;
            dg.DataMember= "NV";

     
        }
        public void Getheader(DataGridView dg)
        {
            dg.Columns["idnhanvien"].HeaderText = "Mã nhân viên";
            dg.Columns["hoten"].HeaderText = "Tên nhân viên";
            //dg.Columns["ID_PB"].HeaderText = "Mã phòng ban"; Hiển thị riêng bên TTNV
            dg.Columns["GIOITINH"].HeaderText = "Giới tính";
            dg.Columns["ngaysinh"].HeaderText = "Ngày sinh";
            dg.Columns["ngaybd"].HeaderText = "Ngày vào làm";
            dg.Columns["tenpb"].HeaderText = "Phòng ban";
            dg.Columns["chucvu"].HeaderText = "Chức vụ";
            dg.Columns["Tenhd"].HeaderText = "Loại hợp đồng";
        }

        public void GetheaderHDong(DataGridView dg)
        {
            dg.Columns["idnhanvien"].HeaderText = "Mã nhân viên";
            dg.Columns["id_hopdong"].HeaderText = "Mã hợp đồng";
            //dg.Columns["ID_PB"].HeaderText = "Mã phòng ban"; Hiển thị riêng bên TTNV
            dg.Columns["Loaihd"].HeaderText = "Loại hợp đồng";
            dg.Columns["tenhopdong"].HeaderText = "Tên hợp đồng";
            dg.Columns["ngaybd"].HeaderText = "Ngày bắt đầu";
            dg.Columns["ngaykt"].HeaderText = "Ngày kết thúc";
            dg.Columns["ghichu"].HeaderText = "Ghi chú";
            dg.Columns["chucvu"].HeaderText = "Chức vụ";
        }

    }
}
