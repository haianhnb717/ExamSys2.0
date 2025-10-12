using System;
using System.Windows.Forms;
using System.Drawing;

namespace test_qltn0310.ExamSystem.App.Forms
{
    partial class StaffLoginForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblRole;
        private ComboBox cmbRole;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPassword;
        private TextBox txtPassword;
        private CheckBox chkRemember;
        private Button btnLogin;
        private Button btnBack;
        private Label lblInfo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblRole = new Label();
            this.cmbRole = new ComboBox();
            this.lblEmail = new Label();
            this.txtEmail = new TextBox();
            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.chkRemember = new CheckBox();
            this.btnLogin = new Button();
            this.btnBack = new Button();
            this.lblInfo = new Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitle.Location = new Point(50, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(280, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Staff Portal";
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblRole
            // 
            this.lblRole.Location = new Point(40, 75);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new Size(100, 23);
            this.lblRole.TabIndex = 1;
            this.lblRole.Text = "Vai trò";
            // 
            // cmbRole
            // 
            this.cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRole.Items.AddRange(new object[] { "lecturer", "head-of-subject" });
            this.cmbRole.Location = new Point(40, 100);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new Size(280, 23);
            this.cmbRole.TabIndex = 2;
            // 
            // lblEmail
            // 
            this.lblEmail.Location = new Point(40, 135);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new Size(100, 23);
            this.lblEmail.TabIndex = 3;
            this.lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new Point(40, 160);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new Size(280, 23);
            this.txtEmail.TabIndex = 4;
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new Point(40, 195);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new Size(100, 23);
            this.lblPassword.TabIndex = 5;
            this.lblPassword.Text = "Mật khẩu";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new Point(40, 220);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new Size(280, 23);
            this.txtPassword.TabIndex = 6;
            // 
            // chkRemember
            // 
            this.chkRemember.Location = new Point(40, 255);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.Size = new Size(150, 23);
            this.chkRemember.TabIndex = 7;
            this.chkRemember.Text = "Ghi nhớ đăng nhập";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new Point(40, 290);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new Size(280, 35);
            this.btnLogin.TabIndex = 8;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new Point(40, 340);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new Size(100, 25);
            this.btnBack.TabIndex = 9;
            this.btnBack.Text = "← Quay lại";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new EventHandler(this.btnBack_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.ForeColor = Color.Gray;
            this.lblInfo.Location = new Point(40, 380);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new Size(280, 55);
            this.lblInfo.TabIndex = 10;
            this.lblInfo.Text = "👩‍🏫 Demo tài khoản:\r\n📧 minh.nv@university.edu.vn (Trưởng bộ môn)\r\n📧 an.nv@university.edu.vn (Giảng viên)";
            // 
            // StaffLoginForm
            // 
            this.AcceptButton = this.btnLogin;
            this.CancelButton = this.btnBack;
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(370, 460);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.cmbRole);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.chkRemember);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblInfo);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Name = "StaffLoginForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Staff Login";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
