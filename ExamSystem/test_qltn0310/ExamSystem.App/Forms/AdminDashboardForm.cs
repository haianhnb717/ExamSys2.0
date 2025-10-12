using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace test_qltn0310.ExamSystem.App.Forms
{
    public partial class AdminDashboardForm : Form
    {
        public AdminDashboardForm()
        {
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            this.Text = "Admin Dashboard";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.White;

            // Header
            var lblTitle = new Label
            {
                Text = "Dashboard",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                AutoSize = true,
                Top = 20,
                Left = 20
            };
            this.Controls.Add(lblTitle);

            var lblSubtitle = new Label
            {
                Text = "Tổng quan hệ thống quản lý thi cử",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Top = 60,
                Left = 22
            };
            this.Controls.Add(lblSubtitle);

            // ComboBox date filter
            var cbDateRange = new ComboBox
            {
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Left = this.ClientSize.Width - 200,
                Top = 30,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            cbDateRange.Items.AddRange(new string[] { "7 ngày qua", "30 ngày qua", "3 tháng qua" });
            cbDateRange.SelectedIndex = 0;
            this.Controls.Add(cbDateRange);

            // KPI row (use FlowLayoutPanel)
            var kpiPanel = new FlowLayoutPanel
            {
                Top = 100,
                Left = 20,
                Width = this.ClientSize.Width - 40,
                Height = 120,
                FlowDirection = FlowDirection.LeftToRight,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            this.Controls.Add(kpiPanel);

            kpiPanel.Controls.Add(CreateKpiCard("Tổng số môn học", "24", Color.RoyalBlue));
            kpiPanel.Controls.Add(CreateKpiCard("Kỳ thi đang diễn ra", "5", Color.SeaGreen));
            kpiPanel.Controls.Add(CreateKpiCard("Câu hỏi chờ duyệt", "42", Color.OrangeRed));
            kpiPanel.Controls.Add(CreateKpiCard("Người dùng", "120/800", Color.MediumPurple));

            // Chart submissions
            var chartSubmissions = new Chart { Width = 500, Height = 300, Top = 250, Left = 20 };
            var chartArea = new ChartArea("area");
            chartSubmissions.ChartAreas.Add(chartArea);
            var series = new Series("Submissions")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2,
                Color = Color.DodgerBlue
            };
            series.Points.AddXY("T2", 45);
            series.Points.AddXY("T3", 52);
            series.Points.AddXY("T4", 38);
            series.Points.AddXY("T5", 65);
            series.Points.AddXY("T6", 72);
            series.Points.AddXY("T7", 28);
            series.Points.AddXY("CN", 15);
            chartSubmissions.Series.Add(series);
            this.Controls.Add(chartSubmissions);

            // Pie chart difficulty
            var chartPie = new Chart { Width = 400, Height = 300, Top = 250, Left = 550 };
            var area2 = new ChartArea();
            chartPie.ChartAreas.Add(area2);
            var pie = new Series("Difficulty")
            {
                ChartType = SeriesChartType.Pie
            };
            pie.Points.AddXY("Dễ", 40);
            pie.Points[0].Color = Color.ForestGreen;
            pie.Points.AddXY("Trung bình", 45);
            pie.Points[1].Color = Color.Goldenrod;
            pie.Points.AddXY("Khó", 15);
            pie.Points[2].Color = Color.Crimson;
            chartPie.Series.Add(pie);
            this.Controls.Add(chartPie);

            // DataGridView upcoming exams
            var dgvExams = new DataGridView
            {
                Top = 600,
                Left = 20,
                Width = 600,
                Height = 200,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White
            };
            dgvExams.Columns.Add("code", "Mã đề");
            dgvExams.Columns.Add("subject", "Môn học");
            dgvExams.Columns.Add("date", "Ngày thi");
            dgvExams.Columns.Add("duration", "Thời gian");
            dgvExams.Columns.Add("status", "Trạng thái");

            dgvExams.Rows.Add("EXAM001", "Database Systems", "2025-10-01 09:00", "60m", "Published");
            dgvExams.Rows.Add("EXAM002", "Operating Systems", "2025-10-03 14:00", "90m", "Draft");
            this.Controls.Add(dgvExams);

            // Progress bar blueprint
            var lblProg = new Label
            {
                Text = "Tiến độ tạo ma trận đề",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true,
                Top = 600,
                Left = 650
            };
            this.Controls.Add(lblProg);

            var pb1 = CreateProgress("Database Systems", 85, 650, 640);
            var pb2 = CreateProgress("Operating Systems", 60, 650, 680);
            this.Controls.Add(pb1);
            this.Controls.Add(pb2);

            // Recent activities list
            var lstRecent = new ListBox
            {
                Top = 850,
                Left = 20,
                Width = this.ClientSize.Width - 40,
                Height = 200
            };
            lstRecent.Items.Add("GV Nguyễn Văn A đã tải lên 5 câu hỏi mới (2 giờ trước)");
            lstRecent.Items.Add("GV Trần Thị B đã tạo ma trận đề (4 giờ trước)");
            lstRecent.Items.Add("Admin đã phê duyệt 12 câu hỏi (6 giờ trước)");
            this.Controls.Add(lstRecent);
        }

        private Panel CreateKpiCard(string title, string value, Color color)
        {
            var card = new Panel
            {
                Width = 200,
                Height = 100,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10)
            };
            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                AutoSize = true,
                Top = 10,
                Left = 10
            };
            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = color,
                AutoSize = true,
                Top = 40,
                Left = 10
            };
            card.Controls.Add(lblTitle);
            card.Controls.Add(lblValue);
            return card;
        }

        private Panel CreateProgress(string subject, int percent, int x, int y)
        {
            var panel = new Panel
            {
                Width = 400,
                Height = 30,
                Left = x,
                Top = y
            };
            var lbl = new Label
            {
                Text = $"{subject} ({percent}%)",
                AutoSize = true,
                Top = 5,
                Left = 0
            };
            var pb = new ProgressBar
            {
                Value = percent,
                Width = 300,
                Height = 20,
                Left = 100,
                Top = 5
            };
            panel.Controls.Add(lbl);
            panel.Controls.Add(pb);
            return panel;
        }
    }
}
