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

namespace DOAN14_DEMO
{
    public partial class UCDoiMK : UserControl
    {
        private string connectionString = @"Server=LAPTOP-1I777SIS\SQLEXPRESS; Database=QLGVTP; Trusted_Connection=True;";
        private string originalUser;
        private string originalPassword;
        public UCDoiMK()
        {
            InitializeComponent();
            this.Load += UCDoiMK_Load;
            btnSua.Click += btnSua_Click;
            btnLuu.Click += btnLuu_Click;
            btnHuy.Click += btnHuy_Click;
            btnThoat.Click += btnThoat_Click;
            chkHienMatKhau.CheckedChanged += chkHienMatKhau_CheckedChanged;
            txtMatKhauCu.TextChanged += txtMatKhauCu_TextChanged;

            // Khi khởi tạo, để chế độ không chỉnh sửa
            DisableEditMode();
        }

        private void UCDoiMK_Load(object sender, EventArgs e)
        {
            LoadAccount();

        }

        // Load thông tin tài khoản hiện tại vào các ô
        private void LoadAccount()
        {
            originalUser = form_dangnhap.CurrentUser ?? "";       
            originalPassword = form_dangnhap.CurrentPassword ?? "";

            txtTenDangNhap.Text = originalUser;
            txtMatKhauHienTai.Text = originalPassword;
            txtMatKhauHienTai.PasswordChar = '*';
            txtMatKhauHienTai.ReadOnly = true;


            DisableEditMode();
        }


        // Bật chế độ chỉnh sửa
        private void EnableEditMode()
        {
            txtTenDangNhap.Enabled = true;
            btnLuu.Visible = true;
            btnHuy.Visible = true;

            // hiển thị ô mật khẩu cũ / mới nhưng khóa ô mật khẩu mới cho tới khi xác thực
            lblMatKhauCu.Visible = true;
            lblMatKhauMoi.Visible = true;
            txtMatKhauCu.Visible = true;
            txtMatKhauMoi.Visible = true;
            txtMatKhauCu.Enabled = true;
            txtMatKhauMoi.Enabled = false;
        }

        // Tắt chế độ chỉnh sửa
        private void DisableEditMode()
        {
            txtTenDangNhap.Enabled = false;
            btnLuu.Visible = false;
            btnHuy.Visible = false;

            lblMatKhauCu.Visible = false;
            lblMatKhauMoi.Visible = false;
            txtMatKhauCu.Visible = false;
            txtMatKhauMoi.Visible = false;
            txtMatKhauCu.Text = string.Empty;
            txtMatKhauMoi.Text = string.Empty;
            txtMatKhauMoi.Enabled = false;

            // hiển thị mật khẩu hiện tại ẩn bằng dấu *
            txtMatKhauHienTai.PasswordChar = '*';
            chkHienMatKhau.Checked = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            EnableEditMode();
        }

        private void txtMatKhauCu_TextChanged(object sender, EventArgs e)
        {
            //Khi nhập mật khẩu cũ đúng thì mở ô nhập mật khẩu mới
            if (txtMatKhauCu.Text == originalPassword)
            {
                txtMatKhauMoi.Enabled = true;
            }
            else
            {
                txtMatKhauMoi.Enabled = false;
                txtMatKhauMoi.Text = string.Empty;
            }
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhauHienTai.PasswordChar = chkHienMatKhau.Checked ? '\0' : '*';
        }

        private void SaveChanges()
        {
            string newUser = txtTenDangNhap.Text.Trim();
            string newPass = string.IsNullOrWhiteSpace(txtMatKhauMoi.Text) ? originalPassword : txtMatKhauMoi.Text;

            if (txtMatKhauCu.Text != originalPassword)
            {
                MessageBox.Show("Mật khẩu cũ không đúng!", "Lỗi");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "UPDATE TaiKhoan SET TenDangNhap=@newU, MatKhau=@newP WHERE TenDangNhap=@oldU",
                    conn);

                cmd.Parameters.AddWithValue("@newU", newUser);
                cmd.Parameters.AddWithValue("@newP", newPass);

                cmd.Parameters.AddWithValue("@oldU", originalUser);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    form_dangnhap.CurrentUser = newUser;
                    form_dangnhap.CurrentPassword = newPass;

                    MessageBox.Show("Lưu thành công!");

                    LoadAccount();
                    DisableEditMode();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy tài khoản!");
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtTenDangNhap.Text = originalUser;
            txtMatKhauHienTai.Text = originalPassword;

            DisableEditMode();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            ((form2)this.ParentForm).GoHome();
        }

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
