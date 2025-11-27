using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DOAN14_QLGVPT
{
    public partial class UCToBoMon : UserControl
    {
        private string connectionString = @"Server=LAPTOP-1I777SIS\SQLEXPRESS;Database=QLGVPT;Trusted_Connection=True;";
        private bool isAddingNew = false;

        public UCToBoMon()
        {
            InitializeComponent();
            this.Load += UCToBoMon_Load;

            btnThem.Click += BtnThem_Click;
            btnXoa.Click += BtnXoa_Click;
            btnSua.Click += BtnSua_Click;
            btnHuy.Click += BtnHuy_Click;
            btnLuu.Click += BtnLuu_Click;

            dataGridView1.CellClick += DataGridView1_CellClick;

            LockControls(true);
        }

        // ============================================================
        // 1. KHÓA / MỞ CONTROL
        // ============================================================
        private void LockControls(bool isLocked)
        {
            txtMaTo.Enabled = !isLocked;
            txtTenTo.Enabled = !isLocked;
            txtSDT.Enabled = !isLocked;
            txtMaToTruong.Enabled = !isLocked;
            txtHoTenTruong.Enabled = !isLocked;
            txtEmail.Enabled = !isLocked;

            btnLuu.Enabled = !isLocked;
            btnHuy.Enabled = !isLocked;

            btnThem.Enabled = isLocked;
            btnSua.Enabled = isLocked;
            btnXoa.Enabled = isLocked;
        }

        // ============================================================
        // 2. CLEAR THÔNG TIN
        // ============================================================
        private void ClearFields()
        {
            txtMaTo.Text = "";
            txtTenTo.Text = "";
            txtSDT.Text = "";
            txtMaToTruong.Text = "";
            txtHoTenTruong.Text = "";
            txtEmail.Text = "";
        }

        // ============================================================
        // 3. LOAD DỮ LIỆU
        // ============================================================
        private void UCToBoMon_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM TOBOMON";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }

        // ============================================================
        // 4. CLICK DATAGRIDVIEW → FILL TEXTBOX
        // ============================================================
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaTo.Text = dataGridView1.Rows[e.RowIndex].Cells["MaTo"].Value.ToString();
                txtTenTo.Text = dataGridView1.Rows[e.RowIndex].Cells["TenTo"].Value.ToString();
                txtSDT.Text = dataGridView1.Rows[e.RowIndex].Cells["SDT"].Value.ToString();
                txtMaToTruong.Text = dataGridView1.Rows[e.RowIndex].Cells["MaToTruong"].Value.ToString();
                txtHoTenTruong.Text = dataGridView1.Rows[e.RowIndex].Cells["TenToTruong"].Value.ToString();
                txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells["Email"].Value.ToString();
            }
        }

        // ============================================================
        // 5. NÚT THÊM
        // ============================================================
        private void BtnThem_Click(object sender, EventArgs e)
        {
            isAddingNew = true;
            ClearFields();
            LockControls(false);
            txtMaTo.Focus();
        }

        // ============================================================
        // 6. NÚT SỬA
        // ============================================================
        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (txtMaTo.Text == "")
            {
                MessageBox.Show("Vui lòng chọn dòng cần sửa!");
                return;
            }

            isAddingNew = false;
            LockControls(false);
            txtMaTo.Enabled = false;  // Không cho sửa khóa chính
        }

        // ============================================================
        // 7. NÚT XÓA
        // ============================================================
        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaTo.Text == "")
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM TOBOMON WHERE MaTo=@MaTo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaTo", txtMaTo.Text);
                    cmd.ExecuteNonQuery();
                }

                LoadData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message);
            }
        }

        // ============================================================
        // 8. NÚT LƯU (THÊM HOẶC SỬA)
        // ============================================================
        private void BtnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd;

                    if (isAddingNew)
                    {
                        string query =
                            "INSERT INTO TOBOMON (MaTo, TenTo, SDT, MaToTruong, TenToTruong, Email) " +
                            "VALUES (@MaTo, @TenTo, @SDT, @MaToTruong, @TenToTruong, @Email)";

                        cmd = new SqlCommand(query, conn);
                    }
                    else
                    {
                        string query =
                            "UPDATE TOBOMON SET TenTo=@TenTo, SDT=@SDT, MaToTruong=@MaToTruong, " +
                            "TenToTruong=@TenToTruong, Email=@Email WHERE MaTo=@MaTo";

                        cmd = new SqlCommand(query, conn);
                    }

                    cmd.Parameters.AddWithValue("@MaTo", txtMaTo.Text);
                    cmd.Parameters.AddWithValue("@TenTo", txtTenTo.Text);
                    cmd.Parameters.AddWithValue("@SDT", txtSDT.Text);
                    cmd.Parameters.AddWithValue("@MaToTruong", txtMaToTruong.Text);
                    cmd.Parameters.AddWithValue("@TenToTruong", txtHoTenTruong.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                    cmd.ExecuteNonQuery();
                }

                LoadData();
                LockControls(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message);
            }
        }

        // ============================================================
        // 9. NÚT HỦY
        // ============================================================
        private void BtnHuy_Click(object sender, EventArgs e)
        {
            LockControls(true);
            ClearFields();
        }
    }
}
