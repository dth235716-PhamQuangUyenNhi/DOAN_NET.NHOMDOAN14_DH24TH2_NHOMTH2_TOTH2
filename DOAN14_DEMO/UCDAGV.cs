using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DOAN14_DEMO
{
    public partial class UCDAGV : UserControl
    {
        private bool isAddingNew = false;

        public UCDAGV()
        {
            InitializeComponent();

            // Gán sự kiện
            this.Load += UCQuanlyGiaoVien_Load;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnHuy.Click += btnHuy_Click;
            btnLuu.Click += btnLuu_Click;
            btnThoat.Click += btnThoat_Click;

            dataGridViewGV.CellClick += dataGridViewGV_CellClick;
        }

        // ======================= KẾT NỐI SQL SERVER =======================
        private SqlConnection GetConnection()
        {
            return new SqlConnection(
                @"Server=LAPTOP-1I777SIS\SQLEXPRESS;
                  Database=QLGVTP;
                  Trusted_Connection=True;");
        }

        // ======================= LOAD DỮ LIỆU LÊN LƯỚI =======================
        private void UCQuanlyGiaoVien_Load(object sender, EventArgs e)
        {
            LoadData();
            SetEditingState(false);
        }

        private void LoadData()
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = @"SELECT MaGV, HovaTendem, Ten, NgaySinh, Email, Phai 
                                 FROM GIAOVIEN";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridViewGV.DataSource = dt;
            }
        }

        // ======================= CLEAR INPUT =======================
        private void ClearInput()
        {
            txtMaGV.Clear();
            txtHoTenDem.Clear();
            txtTen.Clear();
            txtEmail.Clear();
            dtpNgaySinh.Value = DateTime.Now;

            rdNam.Checked = false;
            rdNu.Checked = false;
        }

        // ======================= BẬT / TẮT CHẾ ĐỘ NHẬP =======================
        private void SetEditingState(bool editing)
        {
            txtMaGV.Enabled = editing;
            txtHoTenDem.Enabled = editing;
            txtTen.Enabled = editing;
            txtEmail.Enabled = editing;
            dtpNgaySinh.Enabled = editing;

            rdNam.Enabled = editing;
            rdNu.Enabled = editing;

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
            if (txtMaGV.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn giáo viên muốn sửa!");
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

        // ======================= NÚT LƯU =======================
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            string magv = txtMaGV.Text.Trim();
            string hodem = txtHoTenDem.Text.Trim();
            string ten = txtTen.Text.Trim();
            string email = txtEmail.Text.Trim();
            string gioitinh = rdNam.Checked ? "Nam" : "Nữ";
            DateTime ngaysinh = dtpNgaySinh.Value;

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string query;

                if (isAddingNew)
                {
                    query = @"INSERT INTO GIAOVIEN(MaGV, HovaTendem, Ten, NgaySinh, Email, Phai)
                              VALUES (@MaGV, @HoTenDem, @Ten, @NgaySinh, @Email, @Phai)";
                }
                else
                {
                    query = @"UPDATE GIAOVIEN
                              SET HovaTendem=@HoTenDem,
                                  Ten=@Ten,
                                  NgaySinh=@NgaySinh,
                                  Email=@Email,
                                  Phai=@Phai
                              WHERE MaGV=@MaGV";
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaGV", magv);
                cmd.Parameters.AddWithValue("@HoTenDem", hodem);
                cmd.Parameters.AddWithValue("@Ten", ten);
                cmd.Parameters.AddWithValue("@NgaySinh", ngaysinh);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Phai", gioitinh);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Lưu thành công!");

            LoadData();
            SetEditingState(false);
        }

        // ======================= KIỂM TRA DỮ LIỆU =======================
        private bool ValidateInput()
        {
            if (txtMaGV.Text.Trim() == "")
            {
                MessageBox.Show("Mã giáo viên không được để trống!");
                return false;
            }

            if (!rdNam.Checked && !rdNu.Checked)
            {
                MessageBox.Show("Hãy chọn giới tính!");
                return false;
            }

            return true;
        }

        // ======================= NÚT XÓA =======================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaGV.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn giáo viên cần xóa!");
                return;
            }

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM GIAOVIEN WHERE MaGV=@MaGV";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaGV", txtMaGV.Text.Trim());
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Xóa thành công!");

            LoadData();
            ClearInput();
        }

        // ======================= CLICK VÀO DGR =======================
        private void dataGridViewGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridViewGV.Rows[e.RowIndex];

            txtMaGV.Text = row.Cells["MaGV"].Value.ToString();
            txtHoTenDem.Text = row.Cells["HovaTendem"].Value.ToString();
            txtTen.Text = row.Cells["Ten"].Value.ToString();
            txtEmail.Text = row.Cells["Email"].Value.ToString();

            dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);

            string gt = row.Cells["Phai"].Value.ToString();
            rdNam.Checked = (gt == "Nam");
            rdNu.Checked = (gt == "Nữ");
        }

        // ======================= NÚT THOÁT (XÓA UC) =======================
        private void btnThoat_Click(object sender, EventArgs e)
        {
            ((form2)this.ParentForm).GoHome();
        }
    }
}
