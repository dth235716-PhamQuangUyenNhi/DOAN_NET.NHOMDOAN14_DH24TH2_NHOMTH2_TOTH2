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
    public partial class UCLichPhanCong : UserControl
    {
        private bool isAddingNew = false;
        public UCLichPhanCong()
        {
            InitializeComponent();
            InitializeComponent();

            this.Load += UCLichPhanCong_Load;

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnHuy.Click += btnHuy_Click;
            btnLuu.Click += btnLuu_Click;
            btnThoat.Click += btnThoat_Click;

            dataGridViewpc.CellClick += dataGridViewpc_CellClick;
        }

        // ------------------------ KẾT NỐI SQL SERVER --------------------------
        private SqlConnection GetConnection()
        {
            return new SqlConnection(
                @"Server=LAPTOP-1I777SIS\SQLEXPRESS;
                  Database=QLGVTP;
                  Trusted_Connection=True;");
        }

        private void UCLichPhanCong_Load(object sender, EventArgs e)
        {
            LoadMonHoc();
            LoadData();
            LoadNamHocToMenu();
            SetEditingState(false);

        }

        private void LoadMonHoc()
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaMon, TenMon FROM MONHOC", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cbMon.DataSource = dt;
                cbMon.DisplayMember = "TenMon";
                cbMon.ValueMember = "MaMon";
            }
        }

        private void LoadData()
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql =
                @"SELECT 
                    LP.NamHoc,
                    LP.MaGV,
                    GV.HovaTendem + ' ' + GV.Ten AS HoTenGV,
                    L.TenLop,
                    TBM.TenTo AS BoMon,
                    L.Siso AS Siso,        
                    LP.Ghichu
                FROM LICHPHANCONG LP
                INNER JOIN GIAOVIEN GV ON LP.MaGV = GV.MaGV
                INNER JOIN LOP L ON LP.MaLop = L.MaLop
                INNER JOIN TOBOMON TBM ON GV.MaGV = TBM.MaToTruong
                ORDER BY LP.NamHoc, L.TenLop";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridViewpc.DataSource = dt;
            }
        }

        // ---------------------------- CLEAR INPUT -----------------------------
        private void ClearInput()
        {
            txtNamHoc.Clear();
            txtMaGV.Clear();
            txtMaLop.Clear();
            txtGhiChu.Clear();
            cbMon.SelectedIndex = -1;
        }

        // ------------------------- ENABLE / DISABLE ------------------------
        private void SetEditingState(bool editing)
        {
            txtNamHoc.Enabled = editing;
            txtMaGV.Enabled = editing;
            txtMaLop.Enabled = editing;
            txtGhiChu.Enabled = editing;
            cbMon.Enabled = editing;

            btnLuu.Enabled = editing;
            btnHuy.Enabled = editing;

            btnThem.Enabled = !editing;
            btnSua.Enabled = !editing;
            btnXoa.Enabled = !editing;
        }


        // ----------------------- LOAD NĂM HỌC -----------------------
        private void LoadNamHocToMenu()
        {
            menuNam.DropDownItems.Clear();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string sql = "SELECT DISTINCT NamHoc FROM LICHPHANCONG ORDER BY NamHoc DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int nam = reader.GetInt32(0);

                    ToolStripMenuItem item = new ToolStripMenuItem(nam.ToString());
                    item.Tag = nam;
                    item.Click += NamHocMenu_Click;

                    menuNam.DropDownItems.Add(item);
                }
            }
        }

        private void NamHocMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            int nam = (int)item.Tag;

            LoadDataByYear(nam);
        }

        private void LoadDataByYear(int namhoc)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql =
                @"SELECT 
                    LP.NamHoc,
                    LP.MaGV,
                    GV.HovaTendem + ' ' + GV.Ten AS HoTenGV,
                    L.TenLop,
                    TBM.TenTo AS BoMon,
                    L.Siso AS Siso,        
                    LP.Ghichu
                FROM LICHPHANCONG LP
                INNER JOIN GIAOVIEN GV ON LP.MaGV = GV.MaGV
                INNER JOIN LOP L ON LP.MaLop = L.MaLop
                INNER JOIN TOBOMON TBM ON GV.MaGV = TBM.MaToTruong
                WHERE LP.NamHoc = @namhoc
                ORDER BY LP.NamHoc, L.TenLop";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@namhoc", namhoc);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridViewpc.DataSource = dt;
            }
        }

        // --------------------------- NÚT THÊM --------------------------
        private void btnThem_Click(object sender, EventArgs e)
        {
            isAddingNew = true;
            ClearInput();
            SetEditingState(true);
        }

        // ---------------------------- NÚT SỬA ----------------------------
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaGV.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn lịch phân công muốn sửa!");
                return;
            }

            isAddingNew = false;
            SetEditingState(true);
        }

        // -------------------------- NÚT HỦY ----------------------------
        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInput();
            SetEditingState(false);
        }

        // ----------------------------- VALIDATE ---------------------------
        private bool ValidateInput()
        {
            if (txtMaGV.Text.Trim() == "")
            {
                MessageBox.Show("Mã giáo viên không được để trống!");
                return false;
            }
            if (txtMaLop.Text.Trim() == "")
            {
                MessageBox.Show("Mã lớp không được để trống!");
                return false;
            }
            if (cbMon.SelectedIndex == -1)
            {
                MessageBox.Show("Hãy chọn môn học!");
                return false;
            }
            if (txtNamHoc.Text.Trim() == "")
            {
                MessageBox.Show("Năm học không được để trống!");
                return false;
            }

            return true;
        }


        // --------------------------- NÚT LƯU ------------------------
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            string magv = txtMaGV.Text.Trim();
            string mamon = cbMon.SelectedValue.ToString();
            string malop = txtMaLop.Text.Trim();
            int namhoc = Convert.ToInt32(txtNamHoc.Text.Trim());
            string ghichu = txtGhiChu.Text.Trim();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string query;

                if (isAddingNew)
                {
                    // MaLich = L + năm học, VD: L2025
                    string malich = "L" + namhoc;

                    query =
                        @"INSERT INTO LICHPHANCONG (MaLich, MaGV, MaMon, MaLop, NamHoc, Ghichu)
                          VALUES (@MaLich, @MaGV, @MaMon, @MaLop, @NamHoc, @Ghichu)";
                }
                else
                {
                    // Sửa theo MaGV + MaMon + MaLop + NamHoc
                    query =
                        @"UPDATE LICHPHANCONG
                          SET Ghichu=@Ghichu
                          WHERE MaGV=@MaGV AND MaMon=@MaMon AND MaLop=@MaLop AND NamHoc=@NamHoc";
                }

                SqlCommand cmd = new SqlCommand(query, conn);

                if (isAddingNew)
                    cmd.Parameters.AddWithValue("@MaLich", "L" + namhoc);

                cmd.Parameters.AddWithValue("@MaGV", magv);
                cmd.Parameters.AddWithValue("@MaMon", mamon);
                cmd.Parameters.AddWithValue("@MaLop", malop);
                cmd.Parameters.AddWithValue("@NamHoc", namhoc);
                cmd.Parameters.AddWithValue("@Ghichu", ghichu);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Lưu thành công!");
            LoadData();
            SetEditingState(false);
        }

        // ------------------------------ NUT XOA =-------------------------

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaGV.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn bản ghi cần xóa!");
                return;
            }

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string sql =
                    @"DELETE FROM LICHPHANCONG
                      WHERE MaGV=@MaGV AND MaMon=@MaMon AND MaLop=@MaLop AND NamHoc=@NamHoc";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@MaGV", txtMaGV.Text.Trim());
                cmd.Parameters.AddWithValue("@MaMon", cbMon.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@MaLop", txtMaLop.Text.Trim());
                cmd.Parameters.AddWithValue("@NamHoc", Convert.ToInt32(txtNamHoc.Text.Trim()));

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Xóa thành công!");
            LoadData();
            ClearInput();
        }

        // ------------------------ CLICK VÀO DÒNG DATA GRID VIEW ----------------------
        private void dataGridViewpc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridViewpc.Rows[e.RowIndex];

            txtNamHoc.Text = row.Cells["NamHoc"].Value.ToString();
            txtMaGV.Text = row.Cells["MaGV"].Value.ToString();
            txtMaLop.Text = row.Cells["MaLop"].Value.ToString();
            txtGhiChu.Text = row.Cells["Ghichu"].Value.ToString();

            cbMon.SelectedValue = row.Cells["MaMon"].Value.ToString();
        }

        // -------------------- NÚT THOÁT -------------------------
        private void btnThoat_Click(object sender, EventArgs e)
        {
            ((form2)this.ParentForm).GoHome();
        }

       
    }
}
