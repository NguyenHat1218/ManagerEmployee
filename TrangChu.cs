using QLNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using windowsformdangnhap;
using WindowsFormsApp1;

namespace QLNhanSu
{
    public partial class TrangChu : Form
    {
        public TrangChu()
        {
            InitializeComponent();
            if (Login.Truongphong.tp ==0)
            {
               formluong.Hide();
               
                formnv.Hide();
           
                
               button2.Hide();
                
            }
            label4.Text = Login.Truongphong.ten;
            label5.Text = label5.Text + Login.Truongphong.ma;
            avt.Image = Image.FromFile(Login.Truongphong.anh+"");
            avt.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public Form formch;
        public void OpenChildForm(Form childForm)
        {
            if (formch != null)
            {
                formch.Close();
            }
            formch = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_Body.Controls.Add(childForm);
            panel_Body.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void formnv_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BangTTNV());
            label1.Text = formnv.Text;
        }

        private void formcn_Click(object sender, EventArgs e)
        {
            OpenChildForm(new QLNV());
            label1.Text = formcn.Text;
        }

        private void formtk_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ThongKe());
            label1.Text = formtk.Text;
        }

        private void formluong_Click(object sender, EventArgs e)
        {
            OpenChildForm(new LuongNV() );
            label1.Text = formluong.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void formKT_KL_Click(object sender, EventArgs e)
        {
            OpenChildForm(new kyluatvakhenhuong());
            label1.Text = formKT_KL.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new CNHDLD());
            label1.Text = button2.Text;
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                Form f = Application.OpenForms[i];
                if (f.GetType() == typeof(NgoaiNgu) || f.GetType() == typeof(ThongKe) || f.GetType() == typeof(LuongNV) || f.GetType() == typeof(BangTTNV) || f.GetType() == typeof(kyluatvakhenhuong) || f.GetType() == typeof(CNHDLD) || f.GetType() == typeof(QLNV))
                {
                    f.Close();
                }
            }

        }

        private void panel_Body_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new NgoaiNgu());
            label1.Text = button4.Text;
        }

        private void panel_top_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
