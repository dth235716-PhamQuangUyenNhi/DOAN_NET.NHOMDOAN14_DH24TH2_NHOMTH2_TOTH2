using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DOAN14_DEMO
{
    public partial class form_dangnhap : Form
    {
        public static string CurrentUser = "";

        public form_dangnhap()
        {
            InitializeComponent();
        }

        private void form_dangnhap_Load(object sender, EventArgs e)
        {
            txt_pass.PasswordChar = '*';
            label2.Visible = false;
        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            string user = txt_dn.Text.Trim();
            string pass = txt_pass.Text.Trim();

            // Kiểm tra nhập đủ
            if (user == "" || pass == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Chuỗi kết nối
            string connectionString =
                @"Server=LAPTOP-1I777SIS\SQLEXPRESS;Database=QLGVTP;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT MatKhau FROM Users WHERE TaiKhoan = @tk", conn);

                cmd.Parameters.Add("@tk", SqlDbType.NVarChar).Value = user;

                object result = cmd.ExecuteScalar();

                if (result == null)
                {
                    MessageBox.Show("Tài khoản không tồn tại!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string correctPass = result.ToString();

                if (pass != correctPass)
                {
                    MessageBox.Show("Sai mật khẩu!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Đăng nhập thành công
                CurrentUser = user;

                form2 frm = new form2();
                frm.FormClosed += (s, args) => this.Close();  // Đóng toàn bộ khi form2 tắt

                this.Hide();
                frm.Show();
            }
        }

        private void btn_anhien_Click(object sender, EventArgs e)
        {
            txt_pass.PasswordChar = (txt_pass.PasswordChar == '*') ? '\0' : '*';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
