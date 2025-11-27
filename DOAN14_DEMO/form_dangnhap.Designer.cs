namespace DOAN14_DEMO
{
    partial class form_dangnhap
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnDangnhap = new System.Windows.Forms.Button();
            this.txt_pass = new System.Windows.Forms.TextBox();
            this.txt_dn = new System.Windows.Forms.TextBox();
            this.btn_anhien = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(188, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 45);
            this.label1.TabIndex = 1;
            this.label1.Text = "Đăng nhập";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(325, 217);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(136, 47);
            this.button2.TabIndex = 16;
            this.button2.Text = "Thoát";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 91);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(141, 33);
            this.label6.TabIndex = 12;
            this.label6.Text = "Tài khoản: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 153);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 33);
            this.label7.TabIndex = 11;
            this.label7.Text = "Mật khẩu: ";
            // 
            // btnDangnhap
            // 
            this.btnDangnhap.Location = new System.Drawing.Point(121, 217);
            this.btnDangnhap.Margin = new System.Windows.Forms.Padding(4);
            this.btnDangnhap.Name = "btnDangnhap";
            this.btnDangnhap.Size = new System.Drawing.Size(147, 47);
            this.btnDangnhap.TabIndex = 13;
            this.btnDangnhap.Text = "Đăng nhập";
            this.btnDangnhap.UseVisualStyleBackColor = true;
            this.btnDangnhap.Click += new System.EventHandler(this.btnDangnhap_Click);
            // 
            // txt_pass
            // 
            this.txt_pass.Location = new System.Drawing.Point(153, 150);
            this.txt_pass.Margin = new System.Windows.Forms.Padding(4);
            this.txt_pass.Name = "txt_pass";
            this.txt_pass.Size = new System.Drawing.Size(353, 40);
            this.txt_pass.TabIndex = 15;
            // 
            // txt_dn
            // 
            this.txt_dn.Location = new System.Drawing.Point(153, 84);
            this.txt_dn.Margin = new System.Windows.Forms.Padding(4);
            this.txt_dn.Name = "txt_dn";
            this.txt_dn.Size = new System.Drawing.Size(353, 40);
            this.txt_dn.TabIndex = 14;
            // 
            // btn_anhien
            // 
            this.btn_anhien.Location = new System.Drawing.Point(514, 150);
            this.btn_anhien.Margin = new System.Windows.Forms.Padding(4);
            this.btn_anhien.Name = "btn_anhien";
            this.btn_anhien.Size = new System.Drawing.Size(37, 40);
            this.btn_anhien.TabIndex = 6;
            this.btn_anhien.Text = "👁 ";
            this.btn_anhien.UseVisualStyleBackColor = true;
            this.btn_anhien.Click += new System.EventHandler(this.btn_anhien_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 319);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 33);
            this.label2.TabIndex = 17;
            // 
            // form_dangnhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 33F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 351);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_anhien);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnDangnhap);
            this.Controls.Add(this.txt_pass);
            this.Controls.Add(this.txt_dn);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "form_dangnhap";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.form_dangnhap_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnDangnhap;
        private System.Windows.Forms.TextBox txt_pass;
        private System.Windows.Forms.TextBox txt_dn;
        private System.Windows.Forms.Button btn_anhien;
        private System.Windows.Forms.Label label2;
    }
}

