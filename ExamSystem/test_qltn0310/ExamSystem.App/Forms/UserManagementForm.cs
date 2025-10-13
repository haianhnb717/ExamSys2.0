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
    public partial class UserManagementForm : Form
    {

        private List<User> users;
        private List<User> filteredUsers;

        // Controls
        private TextBox txtSearch;
        private ComboBox cmbRole;
        private ComboBox cmbDepartment;
        private DataGridView dgvUsers;
        private Label lblSelected;
        private Label lblTitle;
        private Label lblSubtitle;
        private Button btnAddUser;
        private Button btnExportCSV;

        // Right panel
        private Panel pnlDetails;
        private Label lblDetailName;
        private Label lblDetailEmail;
        private Label lblDetailRole;
        private Label lblDetailDept;
        private Label lblDetailStatus;
        private Label lblDetailLogin;
        private Button btnEdit;
        private Button btnResetPwd;

        public UserManagementForm()
        {
            InitializeComponent();
            BuildUI();
            LoadData();
            ApplyFilters();
        }

        private void BuildUI()
        {
            this.Text = "Quản lý người dùng";
            this.Width = 1100;
            this.Height = 650;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // Header
            lblTitle = new Label()
            {
                Text = "Quản lý người dùng",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            lblSubtitle = new Label()
            {
                Text = "Quản lý tài khoản giảng viên và admin",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.Gray,
                Location = new Point(22, 50),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblSubtitle);

            btnExportCSV = new Button()
            {
                Text = "📥 Xuất CSV",
                Font = new Font("Segoe UI", 9),
                Location = new Point(750, 25),
                Width = 120,
                Height = 35
            };
            btnAddUser = new Button()
            {
                Text = "➕ Thêm người dùng",
                Font = new Font("Segoe UI", 9),
                Location = new Point(880, 25),
                Width = 180,
                Height = 35
            };
            this.Controls.Add(btnExportCSV);
            this.Controls.Add(btnAddUser);

            // Search + filters
            txtSearch = new TextBox()
            {
                //PlaceholderText = "Tìm theo tên hoặc email...",
                Location = new Point(20, 90),
                Width = 250
            };
            cmbRole = new ComboBox()
            {
                Location = new Point(280, 90),
                Width = 180,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbDepartment = new ComboBox()
            {
                Location = new Point(470, 90),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cmbRole.Items.AddRange(new string[] { "Tất cả vai trò", "Admin", "Trưởng bộ môn", "Giảng viên" });
            cmbDepartment.Items.AddRange(new string[] { "Tất cả khoa", "Khoa học máy tính", "Công nghệ thông tin", "Toán ứng dụng", "Quản trị hệ thống" });

            cmbRole.SelectedIndex = 0;
            cmbDepartment.SelectedIndex = 0;

            txtSearch.TextChanged += (s, e) => ApplyFilters();
            cmbRole.SelectedIndexChanged += (s, e) => ApplyFilters();
            cmbDepartment.SelectedIndexChanged += (s, e) => ApplyFilters();

            this.Controls.Add(txtSearch);
            this.Controls.Add(cmbRole);
            this.Controls.Add(cmbDepartment);

            // DataGridView
            dgvUsers = new DataGridView()
            {
                Location = new Point(20, 130),
                Width = 700,
                Height = 440,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };
            dgvUsers.CellClick += (s, e) => ShowUserDetail();

            this.Controls.Add(dgvUsers);

            lblSelected = new Label()
            {
                Text = "",
                Location = new Point(20, 580),
                AutoSize = true,
                ForeColor = Color.Gray
            };
            this.Controls.Add(lblSelected);

            // Right panel
            pnlDetails = new Panel()
            {
                Location = new Point(740, 130),
                Width = 320,
                Height = 440,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(15)
            };
            this.Controls.Add(pnlDetails);

            var lblDetailTitle = new Label()
            {
                Text = "Chi tiết người dùng",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true
            };
            pnlDetails.Controls.Add(lblDetailTitle);

            lblDetailName = MakeLabel(20, 50, "", 11, true);
            lblDetailEmail = MakeLabel(20, 80, "", 9);
            lblDetailRole = MakeLabel(20, 120, "", 9);
            lblDetailDept = MakeLabel(20, 150, "", 9);
            lblDetailStatus = MakeLabel(20, 180, "", 9);
            lblDetailLogin = MakeLabel(20, 210, "", 9);

            pnlDetails.Controls.AddRange(new Control[] { lblDetailName, lblDetailEmail, lblDetailRole, lblDetailDept, lblDetailStatus, lblDetailLogin });

            btnEdit = new Button()
            {
                Text = "✏️ Chỉnh sửa thông tin",
                Location = new Point(20, 260),
                Width = 260
            };
            btnResetPwd = new Button()
            {
                Text = "🔁 Đặt lại mật khẩu",
                Location = new Point(20, 300),
                Width = 260
            };
            pnlDetails.Controls.Add(btnEdit);
            pnlDetails.Controls.Add(btnResetPwd);
        }

        private Label MakeLabel(int x, int y, string text, int size = 10, bool bold = false)
        {
            return new Label()
            {
                Text = text,
                Location = new Point(x, y),
                Font = new Font("Segoe UI", size, bold ? FontStyle.Bold : FontStyle.Regular),
                AutoSize = true
            };
        }


        private void LoadData()
        {
            users = new List<User>
            {
                new User("1", "Nguyễn Văn An", "nguyen.van.an@university.edu.vn", "Giảng viên", "Khoa học máy tính", "active", "2024-09-15 10:30"),
                new User("2", "Trần Thị Bình", "tran.thi.binh@university.edu.vn", "Trưởng bộ môn", "Khoa học máy tính", "active", "2024-09-15 08:45"),
                new User("3", "Lê Văn Cường", "le.van.cuong@university.edu.vn", "Giảng viên", "Công nghệ thông tin", "inactive", "2024-09-10 14:20"),
                new User("4", "Phạm Thị Dung", "pham.thi.dung@university.edu.vn", "Admin", "Quản trị hệ thống", "active", "2024-09-15 11:00"),
                new User("5", "Hoàng Văn Em", "hoang.van.em@university.edu.vn", "Giảng viên", "Toán ứng dụng", "active", "2024-09-14 16:30"),
                new User("6", "Vũ Thị Phượng", "vu.thi.phuong@university.edu.vn", "Giảng viên", "Khoa học máy tính", "pending", "Chưa đăng nhập"),
                new User("7", "Đỗ Văn Giang", "do.van.giang@university.edu.vn", "Trưởng bộ môn", "Công nghệ thông tin", "active", "2024-09-15 09:15"),
                new User("8", "Ngô Thị Hạnh", "ngo.thi.hanh@university.edu.vn", "Giảng viên", "Toán ứng dụng", "active", "2024-09-13 13:45")
            };
        }

        private void ApplyFilters()
        {
            string search = txtSearch.Text.ToLower();
            string role = cmbRole.SelectedItem.ToString();
            string dept = cmbDepartment.SelectedItem.ToString();

            filteredUsers = users
                .Where(u =>
                    (u.Name.ToLower().Contains(search) || u.Email.ToLower().Contains(search)) &&
                    (role == "Tất cả vai trò" || u.Role == role) &&
                    (dept == "Tất cả khoa" || u.Department == dept)
                ).ToList();

            dgvUsers.DataSource = filteredUsers
                .Select(u => new
                {
                    u.Name,
                    u.Email,
                    u.Role,
                    u.Department,
                    Trạng_thái = u.StatusDisplay,
                    Đăng_nhập_cuối = u.LastLogin
                }).ToList();

            lblSelected.Text = $"Tổng: {filteredUsers.Count} người dùng";
        }

        private void ShowUserDetail()
        {
            if (dgvUsers.CurrentRow == null) return;
            var name = dgvUsers.CurrentRow.Cells["Name"].Value.ToString();
            var user = users.FirstOrDefault(u => u.Name == name);
            if (user == null) return;

            lblDetailName.Text = user.Name;
            lblDetailEmail.Text = user.Email;
            lblDetailRole.Text = $"Vai trò: {user.Role}";
            lblDetailDept.Text = $"Khoa/Bộ môn: {user.Department}";
            lblDetailStatus.Text = $"Trạng thái: {user.StatusDisplay}";
            lblDetailLogin.Text = $"Đăng nhập cuối: {user.LastLogin}";
        }
    }

    public class User
    {
        public string Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string Role { get; }
        public string Department { get; }
        public string Status { get; }
        public string LastLogin { get; }

        public string StatusDisplay
        {
            get
            {
                switch (Status)
                {
                    case "active":
                        return "Hoạt động";
                    case "inactive":
                        return "Không hoạt động";
                    case "pending":
                        return "Chờ kích hoạt";
                    default:
                        return Status;
                }
            }
        }


        public User(string id, string name, string email, string role, string department, string status, string lastLogin)
        {
            Id = id;
            Name = name;
            Email = email;
            Role = role;
            Department = department;
            Status = status;
            LastLogin = lastLogin;
        }
    }
}
