namespace DOAN14_QLGVPT
{
    partial class UCTimkiem
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_thoat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_timkiem = new System.Windows.Forms.Button();
            this.txttim = new System.Windows.Forms.TextBox();
            this.btn_xoa = new System.Windows.Forms.Button();
            this.dataGridViewKQ = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKQ)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Teal;
            this.panel1.Controls.Add(this.btn_thoat);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(939, 71);
            this.panel1.TabIndex = 3;
            // 
            // btn_thoat
            // 
            this.btn_thoat.Location = new System.Drawing.Point(806, 14);
            this.btn_thoat.Name = "btn_thoat";
            this.btn_thoat.Size = new System.Drawing.Size(121, 47);
            this.btn_thoat.TabIndex = 1;
            this.btn_thoat.Text = "<<<";
            this.btn_thoat.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(15, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(266, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "NHẬP VÀ TÌM KIẾM";
            // 
            // btn_timkiem
            // 
            this.btn_timkiem.Location = new System.Drawing.Point(21, 97);
            this.btn_timkiem.Name = "btn_timkiem";
            this.btn_timkiem.Size = new System.Drawing.Size(145, 62);
            this.btn_timkiem.TabIndex = 4;
            this.btn_timkiem.Text = "Tìm kiếm";
            this.btn_timkiem.UseVisualStyleBackColor = true;
            // 
            // txttim
            // 
            this.txttim.Location = new System.Drawing.Point(172, 109);
            this.txttim.Name = "txttim";
            this.txttim.Size = new System.Drawing.Size(628, 40);
            this.txttim.TabIndex = 5;
            // 
            // btn_xoa
            // 
            this.btn_xoa.Location = new System.Drawing.Point(806, 97);
            this.btn_xoa.Name = "btn_xoa";
            this.btn_xoa.Size = new System.Drawing.Size(121, 62);
            this.btn_xoa.TabIndex = 6;
            this.btn_xoa.Text = "Xóa";
            this.btn_xoa.UseVisualStyleBackColor = true;
            // 
            // dataGridViewKQ
            // 
            this.dataGridViewKQ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewKQ.Location = new System.Drawing.Point(0, 272);
            this.dataGridViewKQ.Name = "dataGridViewKQ";
            this.dataGridViewKQ.RowHeadersWidth = 62;
            this.dataGridViewKQ.RowTemplate.Height = 28;
            this.dataGridViewKQ.Size = new System.Drawing.Size(939, 290);
            this.dataGridViewKQ.TabIndex = 7;
            this.dataGridViewKQ.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(361, 205);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 33);
            this.label2.TabIndex = 8;
            this.label2.Text = "Kết quả tìm kiếm:";
            // 
            // UCTimkiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 33F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridViewKQ);
            this.Controls.Add(this.btn_xoa);
            this.Controls.Add(this.txttim);
            this.Controls.Add(this.btn_timkiem);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "UCTimkiem";
            this.Size = new System.Drawing.Size(939, 562);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKQ)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_thoat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_timkiem;
        private System.Windows.Forms.TextBox txttim;
        private System.Windows.Forms.Button btn_xoa;
        private System.Windows.Forms.DataGridView dataGridViewKQ;
        private System.Windows.Forms.Label label2;
    }
}
