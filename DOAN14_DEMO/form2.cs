using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOAN14_DEMO
{
    public partial class form2 : Form
    {
        public form2()
        {
            InitializeComponent();
            NavigationService.ShowTrangChu = ShowUCTrangChu;
        }

        private void form2_Load(object sender, EventArgs e)
        {

        }

        public static class NavigationService
        {
            public static Action ShowTrangChu;
        }

        private void ShowUCTrangChu()
        {
            // Code để hiển thị UCTrangChu
            UCBackground uc = new UCBackground();
            panelmain.Controls.Clear();
            panelmain.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
        }

        public void LoadUserControl(UserControl uc)
        {
            panelmain.Controls.Clear();

            uc.AutoSize = false;           // Ngăn tự thay đổi size
            uc.Dock = DockStyle.Fill;      // Luôn vừa panel
            uc.Margin = new Padding(0);    // Bỏ viền dư

            panelmain.Controls.Add(uc);    // Thêm UC mới
        }

        public void GoHome()
        {
            LoadUserControl(new UCBackground());
        }

        private void danhSáchGiaoViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UCDAGV());
        }

        private void tổBộMônToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UCToBoMon());
        }

        private void lịchPhânCôngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UCLichPhanCong());
        }

        private void tìmKiếmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UCTimkiem());
        }

        private void danhSáchTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UCDSDANGNHAP());
        }

        private void đăngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UCDoiMK());
        }

        private void đăngXuâtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            form_dangnhap frm = new form_dangnhap();
            frm.Show();

        }

        private void trangChủToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UCBackground());
        }
    }
}
