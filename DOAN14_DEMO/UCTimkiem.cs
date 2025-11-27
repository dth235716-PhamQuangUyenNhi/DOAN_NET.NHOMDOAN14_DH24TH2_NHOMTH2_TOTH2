using DOAN14_DEMO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DOAN14_DEMO
{
    public partial class UCTimkiem : UserControl
    {
        public UCTimkiem()
        {
            InitializeComponent();

            btn_timkiem.Click += btn_timkiem_Click;
            btn_xoa.Click += btn_xoa_Click;
            btn_thoat.Click += btn_thoat_Click;
        }

        // ======================= KẾT NỐI SQL SERVER =======================
        private SqlConnection GetConnection()
        {
            return new SqlConnection(
                @"Server= LAPTOP-1I777SIS\SQLEXPRESS ;
                  Database=QLGVTP;
                  Trusted_Connection=True;");
        }

        // ======================= NÚT TÌM KIẾM =======================
        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            string maGV = txttim.Text.Trim();

            if (maGV == "")
            {
                MessageBox.Show("Vui lòng nhập mã giáo viên!");
                return;
            }

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                string query = @"SELECT *
                                 FROM GIAOVIEN
                                 WHERE MaGV = @MaGV";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@MaGV", maGV);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridViewKQ.DataSource = dt;
            }
        }

        // ======================= NÚT XÓA (CLEAR) =======================
        private void btn_xoa_Click(object sender, EventArgs e)
        {
            txttim.Clear();
            dataGridViewKQ.DataSource = null;
        }

        // ======================= NÚT THOÁT =======================
        private void btn_thoat_Click(object sender, EventArgs e)
        {
            ((form2)this.ParentForm).GoHome();
        }
private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
