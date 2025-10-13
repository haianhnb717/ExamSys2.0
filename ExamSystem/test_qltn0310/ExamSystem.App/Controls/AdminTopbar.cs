using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_qltn0310.ExamSystem.App.Controls
{
    public partial class AdminTopbar : UserControl
    {
        private TextBox searchBox;
        private Button btnNotification;
        private Label badgeLabel;
        private PictureBox avatarPicture;
        private Label lblName;
        private Label lblRole;
        private Button btnDropdown;
        private ContextMenuStrip profileMenu;

        public AdminTopbar()
        {
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            this.Height = 60;
            this.Dock = DockStyle.Top;
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;

            // === Search box ===
            searchBox = new TextBox
            {
                // PlaceholderText = "Tìm kiếm câu hỏi, đề thi, người dùng...",
                Font = new Font("Segoe UI", 9),
                Location = new Point(20, 15),
                Width = 300,
                BackColor = Color.FromArgb(245, 245, 245),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(searchBox);

            // === Notification button ===
            btnNotification = new Button
            {
                FlatStyle = FlatStyle.Flat,
                Width = 40,
                Height = 40,
                Location = new Point(340, 10),
                BackgroundImageLayout = ImageLayout.Center,
                Text = "🔔", // hoặc dùng icon resource
                Font = new Font("Segoe UI Emoji", 16),
                ForeColor = Color.Black
            };
            btnNotification.FlatAppearance.BorderSize = 0;
            this.Controls.Add(btnNotification);

            // Badge label
            badgeLabel = new Label
            {
                Text = "3",
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Red,
                TextAlign = ContentAlignment.MiddleCenter,
                Width = 18,
                Height = 18,
                Location = new Point(btnNotification.Right - 10, btnNotification.Top + 2),
                BorderStyle = BorderStyle.None
            };
            badgeLabel.Region = new Region(new System.Drawing.Drawing2D.GraphicsPath());
            badgeLabel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.FillEllipse(Brushes.Red, 0, 0, badgeLabel.Width - 1, badgeLabel.Height - 1);
                TextRenderer.DrawText(e.Graphics, badgeLabel.Text, badgeLabel.Font,
                    new Rectangle(0, 0, badgeLabel.Width, badgeLabel.Height), Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };
            this.Controls.Add(badgeLabel);

            // === Avatar ===
            avatarPicture = new PictureBox
            {
                Image = Image.FromFile("avatars/admin.jpg"), // hoặc đặt resource
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 40,
                Height = 40,
                Location = new Point(400, 10),
                Cursor = Cursors.Hand
            };
            this.Controls.Add(avatarPicture);

            // === Name + Role ===
            lblName = new Label
            {
                Text = "Admin",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(450, 12),
                AutoSize = true
            };
            lblRole = new Label
            {
                Text = "Quản trị viên",
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray,
                Location = new Point(450, 28),
                AutoSize = true
            };
            this.Controls.Add(lblName);
            this.Controls.Add(lblRole);

            // === Dropdown Button ===
            btnDropdown = new Button
            {
                Text = "▼",
                Font = new Font("Segoe UI", 9),
                FlatStyle = FlatStyle.Flat,
                Width = 30,
                Height = 30,
                Location = new Point(540, 15)
            };
            btnDropdown.FlatAppearance.BorderSize = 0;
            this.Controls.Add(btnDropdown);

            // === Context menu (Dropdown) ===
            profileMenu = new ContextMenuStrip();
            profileMenu.Items.Add("Thông tin cá nhân", null, OnProfileClick);
            profileMenu.Items.Add("Thông báo", null, OnNotificationsClick);
            profileMenu.Items.Add(new ToolStripSeparator());
            var logout = new ToolStripMenuItem("Đăng xuất", null, OnLogoutClick);
            logout.ForeColor = Color.Red;
            profileMenu.Items.Add(logout);

            btnDropdown.Click += (s, e) =>
            {
                profileMenu.Show(btnDropdown, new Point(0, btnDropdown.Height));
            };
        }

        // === Event Handlers ===
        private void OnProfileClick(object sender, EventArgs e)
        {
            MessageBox.Show("Mở thông tin cá nhân");
        }

        private void OnNotificationsClick(object sender, EventArgs e)
        {
            MessageBox.Show("Xem thông báo");
        }

        private void OnLogoutClick(object sender, EventArgs e)
        {
            MessageBox.Show("Đăng xuất");
        }

    }
}
