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

namespace DOAN14_DEMO
{
    public partial class UCDSDANGNHAP : UserControl
    {
        private bool isAddingNew = false;
        public UCDSDANGNHAP()
        {
            InitializeComponent();

            this.Load += UC_Load;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnHuy.Click += btnHuy_Click;
            btnLuu.Click += btnLuu_Click;
            btnThoat.Click += btnThoat_Click;

            dgvAccount.CellClick += dgvAccount_CellClick;
        }
        // ======================= KẾT NỐI SQL SERVER =======================
        private SqlConnection GetConnection()
        {
            return new SqlConnection(
                @"Server=LAPTOP-1I777SIS\SQLEXPRESS;
                  Database=QLGVTP;
                  Trusted_Connection=True;");
        }

        // ======================= LOAD FORM =======================
        private void UC_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadComboBoxes();
            SetEditingState(false);
        }

        private void LoadComboBoxes()
        {
            // Role
            cbRole.Items.Clear();
            cbRole.Items.Add("User");
            cbRole.Items.Add("Admin");

            // Trạng thái
            cbTrangThai.Items.Clear();
            cbTrangThai.Items.Add("Hoạt động");
            cbTrangThai.Items.Add("Khóa");
        }

        // ======================= LOAD DỮ LIỆU =======================
        private void LoadData()
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string query = @"SELECT 
                                    UserID,
                                    TaiKhoan,
                                    MatKhau,
                                    Role,
                                    TrangThaiHH
                                 FROM Users";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvAccount.DataSource = dt;
            }
        }

        // ======================= CLEAR INPUT =======================
        private void ClearInput()
        {
            txtTaiKhoan.Clear();
            txtMatKhau.Clear();

            cbRole.SelectedIndex = -1;
            cbTrangThai.SelectedIndex = -1;
        }


        // ======================= CHẾ ĐỘ NHẬP =======================
        private void SetEditingState(bool editing)
        {
            txtTaiKhoan.Enabled = editing;
            txtMatKhau.Enabled = editing;
            cbRole.Enabled = editing;
            cbTrangThai.Enabled = editing;

            btnLuu.Enabled = editing;
            btnHuy.Enabled = editing;

            btnThem.Enabled = !editing;
            btnSua.Enabled = !editing;
            btnXoa.Enabled = !editing;
        }


        // ======================= NÚT THÊM =======================
        private void btnThem_Click(object sender, EventArgs e)
        {
            isAddingNew = true;
            ClearInput();
            SetEditingState(true);
        }


        // ======================= NÚT SỬA =======================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn tài khoản muốn sửa!");
                return;
            }

            isAddingNew = false;
            SetEditingState(true);
        }


        // ======================= NÚT HỦY =======================
        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInput();
            SetEditingState(false);
        }


        // ======================= KIỂM TRA DỮ LIỆU =======================
        private bool ValidateInput()
        {
            if (txtTaiKhoan.Text.Trim() == "")
            {
                MessageBox.Show("Tài khoản không được bỏ trống!");
                return false;
            }

            if (cbRole.SelectedIndex == -1)
            {
                MessageBox.Show("Hãy chọn phân quyền!");
                return false;
            }

            if (cbTrangThai.SelectedIndex == -1)
            {
                MessageBox.Show("Hãy chọn trạng thái!");
                return false;
            }

            return true;
        }


        // ======================= NÚT LƯU =======================
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            string tk = txtTaiKhoan.Text.Trim();
            string mk = txtMatKhau.Text.Trim();
            string role = cbRole.SelectedItem.ToString();
            string trangthai = cbTrangThai.SelectedItem.ToString();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql;

                if (isAddingNew)
                {
                    sql = @"INSERT INTO Users(TaiKhoan, MatKhau, Role, TrangThaiHH)
                            VALUES (@TaiKhoan, @MatKhau, @Role, @TrangThai)";
                }
                else
                {
                    sql = @"UPDATE Users
                            SET MatKhau=@MatKhau,
                                Role=@Role,
                                TrangThaiHH=@TrangThai
                            WHERE TaiKhoan=@TaiKhoan";
                }

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TaiKhoan", tk);
                cmd.Parameters.AddWithValue("@MatKhau", mk);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@TrangThai", trangthai);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Lưu thành công!");

            LoadData();
            SetEditingState(false);
        }


        // ======================= NÚT XÓA =======================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn tài khoản để xóa!");
                return;
            }

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM Users WHERE TaiKhoan=@TaiKhoan";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TaiKhoan", txtTaiKhoan.Text.Trim());
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Xóa thành công!");

            LoadData();
            ClearInput();
        }


        // ======================= CLICK DGV =======================
        private void dgvAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvAccount.Rows[e.RowIndex];

            txtTaiKhoan.Text = row.Cells["TaiKhoan"].Value.ToString();
            txtMatKhau.Text = row.Cells["MatKhau"].Value.ToString();

            cbRole.SelectedItem = row.Cells["Role"].Value.ToString();
            cbTrangThai.SelectedItem = row.Cells["TrangThaiHH"].Value.ToString();
        }


        // ======================= NÚT THOÁT =======================
        private void btnThoat_Click(object sender, EventArgs e)
        {
            ((form2)this.ParentForm).GoHome();
        }
    }
}

