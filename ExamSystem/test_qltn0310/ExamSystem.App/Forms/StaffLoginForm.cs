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
    public partial class StaffLoginForm : Form
    {
        public StaffLoginForm()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var password = txtPassword.Text;
            var role = cmbRole.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Demo credentials
            var validCredentials = new Dictionary<string, (string Password, string Role, string Name)>
        {
            { "minh.nv@university.edu.vn", ("minh123", "head-of-subject", "PGS.TS. Nguyễn Văn Minh") },
            { "an.nv@university.edu.vn", ("an123", "lecturer", "TS. Nguyễn Văn An") },
            { "binh.tt@university.edu.vn", ("binh123", "lecturer", "TS. Trần Thị Bình") }
        };

            if (validCredentials.ContainsKey(email) && validCredentials[email].Password == password)
            {
                MessageBox.Show($"Chào mừng {validCredentials[email].Name} ({validCredentials[email].Role})!",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
