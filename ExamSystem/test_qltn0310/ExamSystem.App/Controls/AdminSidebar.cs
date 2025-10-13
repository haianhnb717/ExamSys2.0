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
    public partial class AdminSidebar : UserControl
    {
        private bool isCollapsed = false;
        private Panel menuPanel;
        private Button toggleButton;

        public event Action<string> PageChanged;
        public event Action<int> SidebarResized; // 🔹 Event để form biết khi sidebar đổi kích thước

        private const int ExpandedWidth = 250;
        private const int CollapsedWidth = 140;

        public AdminSidebar() => BuildUI();

        private void BuildUI()
        {
            this.BackColor = Color.White;
            this.Width = ExpandedWidth;
            this.Dock = DockStyle.Left;
            this.BorderStyle = BorderStyle.FixedSingle;

            // Header
            var header = new Panel
            {
                Height = 50,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(245, 245, 245)
            };

            var lblTitle = new Label
            {
                Text = "Exam System",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                AutoSize = true,
                Left = 15,
                Top = 15
            };

            toggleButton = new Button
            {
                Text = "⇔",
                Width = 35,
                Height = 30,
                Top = 10,
                Left = ExpandedWidth - 50,
                FlatStyle = FlatStyle.Flat
            };
            toggleButton.FlatAppearance.BorderSize = 0;
            toggleButton.Click += (s, e) => ToggleSidebar();

            header.Controls.Add(lblTitle);
            header.Controls.Add(toggleButton);
            this.Controls.Add(header);

            // Menu Panel
            menuPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(5)
            };
            this.Controls.Add(menuPanel);

            AddMenuItem("dashboard", "Dashboard", "Dash");
            AddMenuItem("users", "Quản lý người dùng", "Người dùng");
            AddMenuItem("subjects", "Môn học", "Môn học");
            AddMenuItem("question-bank", "Ngân hàng câu hỏi", "Câu hỏi");
            AddMenuItem("blueprints", "Ma trận đề thi", "Đề thi");
            AddMenuItem("exams", "Kỳ thi & Lịch trình", "Kỳ thi");
            AddMenuItem("monitoring", "Giám sát trực tiếp", "Giám sát");
            AddMenuItem("reports", "Báo cáo thống kê", "Báo cáo");
            AddMenuItem("settings", "Cài đặt hệ thống", "Cài đặt");
        }

        private void AddMenuItem(string id, string fullLabel, string shortLabel)
        {
            var btn = new Button
            {
                Text = fullLabel,
                Tag = new Tuple<string, string, string>(id, fullLabel, shortLabel),
                Dock = DockStyle.Top,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(15, 0, 0, 0)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(235, 235, 235);
            btn.Click += (s, e) => PageChanged?.Invoke(id);

            menuPanel.Controls.Add(btn);
            menuPanel.Controls.SetChildIndex(btn, 0);
        }

        private void ToggleSidebar()
        {
            isCollapsed = !isCollapsed;

            this.Width = isCollapsed ? CollapsedWidth : ExpandedWidth;

            foreach (Control ctrl in menuPanel.Controls)
            {
                if (ctrl is Button btn)
                {
                    var data = (Tuple<string, string, string>)btn.Tag;
                    btn.Text = isCollapsed ? data.Item3 : data.Item2;
                }
            }

            // Cập nhật vị trí nút toggle
            toggleButton.Left = this.Width - 50;

            SidebarResized?.Invoke(this.Width); // 🔹 báo cho form biết
        }
    }
}

