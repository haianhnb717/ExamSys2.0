using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_qltn0310.ExamSystem.App.Forms
{
    public partial class AdminLoginForm : Form
    {
        public AdminLoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var password = txtPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Demo credentials (tương tự code React Figma Make)
            if (email == "admin@university.edu.vn" && password == "admin123")
            {
                MessageBox.Show("Đăng nhập thành công! Chào mừng Admin.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // TODO: mở DashboardForm
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Email hoặc mật khẩu không đúng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Quay lại portal selection (tùy bạn cài form khác)
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
