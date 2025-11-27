using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DOAN14_DEMO
{
    public partial class UCToBoMon : UserControl
    {
        private string connectionString = @"Server=LAPTOP-1I777SIS\SQLEXPRESS;Database=QLGVTP;Trusted_Connection=True;";
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
            btnThoat.Click += btnThoat_Click;
            dataGridView1.CellClick += DataGridView1_CellClick;

            LockControls(true);
        }

        // ----------------------------- KHÓA / MỞ CONTROL --------------------------------
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

        //---------------------------------- CLEAR THÔNG TIN -----------------------------------
        private void ClearFields()
        {
            txtMaTo.Text = "";
            txtTenTo.Text = "";
            txtSDT.Text = "";
            txtMaToTruong.Text = "";
            txtHoTenTruong.Text = "";
            txtEmail.Text = "";
        }


        // ----------------------------------- LOAD DỮ LIỆU ----------------------------
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
                    string query = @"
                    SELECT 
                        T.MaTo,
                        T.TenTo,
                        T.MaToTruong,
                        (G.HovaTendem + ' ' + G.Ten) AS TenToTruong,
                        G.SDT,
                        G.Email
                    FROM TOBOMON T
                    LEFT JOIN GIAOVIEN G ON T.MaToTruong = G.MaGV";

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


        // ------------------------------- CLICK DATAGRIDVIEW → FILL TEXTBOX -----------------------------
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];

                txtMaTo.Text = row.Cells["MaTo"].Value.ToString();
                txtTenTo.Text = row.Cells["TenTo"].Value.ToString();
                txtMaToTruong.Text = row.Cells["MaToTruong"].Value.ToString();
                txtHoTenTruong.Text = row.Cells["TenToTruong"].Value.ToString();
                txtSDT.Text = row.Cells["SDT"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
            }
        }

        //-------------------------------- NÚT THÊM --------------------------------
        private void BtnThem_Click(object sender, EventArgs e)
        {
            isAddingNew = true;
            ClearFields();
            LockControls(false);
            txtMaTo.Focus();
        }

        //-------------------------------- NÚT SỬA --------------------------------
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

        // ------------------------------- NÚT XÓA -----------------------------
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

        // -------------------------------- NÚT LƯU -----------------------------
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
                            "INSERT INTO TOBOMON (MaTo, TenTo, MaToTruong) " +
                            "VALUES (@MaTo, @TenTo, @MaToTruong)";
                        cmd = new SqlCommand(query, conn);
                    }
                    else
                    {
                        string query =
                            "UPDATE TOBOMON SET TenTo=@TenTo, MaToTruong=@MaToTruong WHERE MaTo=@MaTo";
                        cmd = new SqlCommand(query, conn);
                    }

                    cmd.Parameters.AddWithValue("@MaTo", txtMaTo.Text);
                    cmd.Parameters.AddWithValue("@TenTo", txtTenTo.Text);
                    cmd.Parameters.AddWithValue("@MaToTruong", txtMaToTruong.Text);

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

        // -------------------------------- NÚT HỦY -----------------------------
        private void BtnHuy_Click(object sender, EventArgs e)
        {
            LockControls(true);
            ClearFields();
        }

        // -------------------------------- NÚT THOÁT -----------------------------
        private void btnThoat_Click(object sender, EventArgs e)
        {
            ((form2)this.ParentForm).GoHome();
        }
    }
}
