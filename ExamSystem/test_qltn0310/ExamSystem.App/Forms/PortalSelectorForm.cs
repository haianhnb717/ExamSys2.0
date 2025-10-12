using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace test_qltn0310.ExamSystem.App.Forms
{
    public class PortalSelectorForm : Form
    {
        private FlowLayoutPanel flowPortals;

        public PortalSelectorForm()
        {
            this.Text = "Exam Management System - Portal Selector";
            this.MinimumSize = new Size(950, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true; // tránh flicker khi vẽ gradient
            this.Paint += FormGradientBackground;
            this.BackColor = Color.White;

            BuildUI();
        }

        // 🎨 Gradient background (nhẹ, giống Figma)
        private void FormGradientBackground(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(255, 245, 247, 255),
                Color.FromArgb(255, 230, 240, 255),
                45f))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void BuildUI()
        {

            // Header
            Label lblTitle = new Label
            {
                Text = "Exam Management System",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                AutoSize = false,
                Width = 900,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 20)
            };

            Label lblSubtitle = new Label
            {
                Text = "NEU Exam Portal - Hệ thống quản lý thi trực tuyến\nChọn cổng đăng nhập phù hợp với vai trò của bạn",
                Font = new Font("Segoe UI", 10),
                AutoSize = false,
                Width = 900,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 80)
            };

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblSubtitle);

            // FlowLayoutPanel hiển thị 3 card
            flowPortals = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = true,
                Padding = new Padding(50, 140, 50, 20),
                BackColor = Color.Transparent
            };
            this.Controls.Add(flowPortals);

            // 3 Portal Cards
            flowPortals.Controls.Add(CreatePortalCard(
                "Admin Portal", "Dành cho quản trị viên hệ thống",
                new string[] { "Quản lý toàn bộ hệ thống", "Quản lý người dùng", "Giám sát kỳ thi" },
                Color.LightSkyBlue, () => OnSelectPortal("admin")));

            flowPortals.Controls.Add(CreatePortalCard(
                "Staff Portal", "Trưởng bộ môn & Giảng viên",
                new string[] { "Quản lý môn học", "Tạo đề thi & câu hỏi", "Báo cáo kết quả" },
                Color.MediumOrchid, () => OnSelectPortal("staff")));

            flowPortals.Controls.Add(CreatePortalCard(
                "Student Portal", "Dành cho sinh viên",
                new string[] { "Tham gia thi trực tuyến", "Xem kết quả thi", "Lịch thi của tôi" },
                Color.MediumSeaGreen, () => OnSelectPortal("student")));
        }

        private Panel CreatePortalCard(string title, string desc, string[] features, Color btnColor, Action onClick)
        {
            Panel card = new Panel
            {
                Width = 260,
                Height = 340,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Margin = new Padding(30, 10, 30, 10),
                Padding = new Padding(15)
            };

            // Tiêu đề
            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Width = 230,
                Height = 30,
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(15, 15),
                ForeColor = Color.FromArgb(50, 50, 70)
            };
            card.Controls.Add(lblTitle);

            // Mô tả
            Label lblDesc = new Label
            {
                Text = desc,
                Font = new Font("Segoe UI", 9),
                Width = 230,
                Height = 35,
                Location = new Point(15, 55),
                ForeColor = Color.DimGray
            };
            card.Controls.Add(lblDesc);

            // Danh sách tính năng
            int y = 95;
            foreach (var f in features)
            {
                Label lblFeature = new Label
                {
                    Text = "• " + f,
                    Font = new Font("Segoe UI", 9),
                    Width = 220,
                    Height = 22,
                    Location = new Point(25, y),
                    ForeColor = Color.FromArgb(70, 70, 90)
                };
                card.Controls.Add(lblFeature);
                y += 25;
            }

            // Nút đăng nhập
            Button btnLogin = new Button
            {
                Text = "Đăng nhập",
                Width = 210,
                Height = 40,
                BackColor = btnColor,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(20, 260),
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += (s, e) => onClick();

            // Hover effect
            btnLogin.MouseEnter += (s, e) => {
                btnLogin.BackColor = ControlPaint.Light(btnColor, 0.2f);
            };
            btnLogin.MouseLeave += (s, e) => {
                btnLogin.BackColor = btnColor;
            };

            card.Controls.Add(btnLogin);
            return card;
        }

        private void OnSelectPortal(string role)
        {
            if (role == "admin")
            {
                this.Hide();
                using (var login = new AdminLoginForm())
                {
                    if (login.ShowDialog(this) == DialogResult.OK)
                    {
                        using (var dashboard = new AdminDashboardForm())
                        {
                            dashboard.ShowDialog(this);
                        }
                    }
                }
                this.Show();
            }
            else if (role == "staff")
            {
                this.Hide();
                using (var login = new StaffLoginForm())
                {
                    if (login.ShowDialog(this) == DialogResult.OK)
                    {
                        MessageBox.Show("Đăng nhập Staff thành công!");
                    }
                }
                this.Show();
            }
            else if (role == "student")
            {
                MessageBox.Show("Student Portal chưa được triển khai.", "Thông báo");
            }
        }
    }
}
