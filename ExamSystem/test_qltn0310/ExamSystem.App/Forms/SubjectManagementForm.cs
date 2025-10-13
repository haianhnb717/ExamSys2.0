using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace test_qltn0310.ExamSystem.App.Forms
{
    public partial class SubjectManagementForm : Form
    {
        private List<Subject> subjects;
        private Subject selectedSubject;

        public SubjectManagementForm()
        {
            InitializeComponent();
            InitializeUI();
            LoadData();
            DisplaySubjects(subjects);
        }

        private void InitializeUI()
        {
            Text = "Quản lý môn học";
            Width = 1200;
            Height = 700;
            StartPosition = FormStartPosition.CenterScreen;

            // Search box
            Label lblSearch = new Label
            {
                Text = "Tìm kiếm:",
                Location = new Point(20, 20)
            };
            Controls.Add(lblSearch);

            TextBox txtSearch = new TextBox
            {
                Name = "txtSearch",
                Width = 300,
                Location = new Point(100, 18)
            };
            txtSearch.TextChanged += (s, e) =>
            {
                string keyword = txtSearch.Text.ToLower();
                var filtered = subjects.Where(sub =>
                    sub.Name.ToLower().Contains(keyword) ||
                    sub.Code.ToLower().Contains(keyword) ||
                    sub.Department.ToLower().Contains(keyword)).ToList();
                DisplaySubjects(filtered);
            };
            Controls.Add(txtSearch);

            // Button: Create subject
            Button btnCreate = new Button
            {
                Text = "➕ Tạo môn học mới",
                Location = new Point(420, 15),
                Width = 180,
                Height = 30
            };
            btnCreate.Click += (s, e) => CreateNewSubject();
            Controls.Add(btnCreate);

            // Table
            DataGridView dgv = new DataGridView
            {
                Name = "dgvSubjects",
                Location = new Point(20, 60),
                Width = 750,
                Height = 550,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoGenerateColumns = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgv.Columns.Add("Code", "Mã môn");
            dgv.Columns.Add("Name", "Tên môn học");
            dgv.Columns.Add("Department", "Khoa");
            dgv.Columns.Add("HeadOfDepartment", "Trưởng bộ môn");
            dgv.Columns.Add("QuestionBankCount", "# Ngân hàng");

            dgv.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    var subject = dgv.Rows[e.RowIndex].DataBoundItem as Subject;
                    if (subject != null) ShowSubjectDetails(subject);
                }
            };

            Controls.Add(dgv);

            // Details panel
            GroupBox grpDetails = new GroupBox
            {
                Text = "Chi tiết môn học",
                Name = "grpDetails",
                Location = new Point(800, 60),
                Width = 360,
                Height = 550
            };
            Controls.Add(grpDetails);
        }

        private void LoadData()
        {
            subjects = new List<Subject>
            {
                new Subject(1, "CS101", "Lập trình căn bản", "Khoa CNTT", "TS. Nguyễn Văn An", 105, "2024-01-15",
                    "Môn học cung cấp kiến thức cơ bản về lập trình, thuật toán và ngôn ngữ C++."),
                new Subject(2, "MTH201", "Giải tích 2", "Khoa Toán", "PGS. Trần Thị Bình", 90, "2024-02-20",
                    "Môn học về giải tích toán học nâng cao, tập trung vào tích phân và chuỗi số."),
                new Subject(3, "PHY301", "Vật lý đại cương 1", "Khoa Vật lý", "TS. Lê Văn Cường", 105, "2024-01-10",
                    "Môn học cơ bản về cơ học cổ điển, nhiệt học và sóng."),
                new Subject(4, "ENG102", "English Communication", "Khoa Ngoại ngữ", "TS. Mary Johnson", 125, "2024-03-05",
                    "Phát triển kỹ năng giao tiếp tiếng Anh: nói, nghe, đọc, viết."),
                new Subject(5, "BUS201", "Quản trị kinh doanh", "Khoa Kinh tế", "PGS. Hoàng Minh Đức", 82, "2024-02-15",
                    "Kiến thức cơ bản về quản trị doanh nghiệp và chiến lược marketing.")
            };
        }

        private void DisplaySubjects(List<Subject> list)
        {
            var dgv = Controls.Find("dgvSubjects", true).FirstOrDefault() as DataGridView;
            if (dgv != null)
            {
                dgv.DataSource = null;
                dgv.DataSource = list;
            }
        }

        private void ShowSubjectDetails(Subject subject)
        {
            selectedSubject = subject;

            var grp = Controls.Find("grpDetails", true).FirstOrDefault() as GroupBox;
            grp.Controls.Clear();

            Label lblCode = new Label { Text = $"Mã môn: {subject.Code}", Location = new Point(20, 30), AutoSize = true };
            Label lblName = new Label { Text = $"Tên môn: {subject.Name}", Location = new Point(20, 60), AutoSize = true };
            Label lblDept = new Label { Text = $"Khoa: {subject.Department}", Location = new Point(20, 90), AutoSize = true };
            Label lblHead = new Label { Text = $"Trưởng bộ môn: {subject.HeadOfDepartment}", Location = new Point(20, 120), AutoSize = true };
            Label lblQuestions = new Label { Text = $"Tổng câu hỏi: {subject.TotalQuestions}", Location = new Point(20, 150), AutoSize = true };
            Label lblDesc = new Label { Text = $"Mô tả: {subject.Description}", Location = new Point(20, 180), AutoSize = true, Width = 300 };

            grp.Controls.AddRange(new Control[] { lblCode, lblName, lblDept, lblHead, lblQuestions, lblDesc });
        }

        private void CreateNewSubject()
        {
            var dialog = new CreateSubjectDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Subject newSubj = dialog.NewSubject;
                newSubj.Id = subjects.Count + 1;
                subjects.Add(newSubj);
                DisplaySubjects(subjects);
            }
        }
    }

    // ======= Model Class =======
    public class Subject
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string HeadOfDepartment { get; set; }
        public int TotalQuestions { get; set; }
        public string CreatedDate { get; set; }
        public string Description { get; set; }

        public int QuestionBankCount { get { return TotalQuestions > 0 ? TotalQuestions / 30 : 0; } }

        public Subject(int id, string code, string name, string dept, string head, int total, string date, string desc)
        {
            Id = id;
            Code = code;
            Name = name;
            Department = dept;
            HeadOfDepartment = head;
            TotalQuestions = total;
            CreatedDate = date;
            Description = desc;
        }
    }

    // ======= Create Subject Dialog =======
    public class CreateSubjectDialog : Form
    {
        public Subject NewSubject { get; private set; }

        private TextBox txtCode, txtName, txtDept, txtHead, txtDesc;

        public CreateSubjectDialog()
        {
            Text = "Tạo môn học mới";
            Width = 400;
            Height = 350;
            StartPosition = FormStartPosition.CenterParent;

            Label lbl1 = new Label { Text = "Mã môn học:", Location = new Point(20, 20) };
            txtCode = new TextBox { Location = new Point(140, 18), Width = 200 };

            Label lbl2 = new Label { Text = "Tên môn học:", Location = new Point(20, 60) };
            txtName = new TextBox { Location = new Point(140, 58), Width = 200 };

            Label lbl3 = new Label { Text = "Khoa:", Location = new Point(20, 100) };
            txtDept = new TextBox { Location = new Point(140, 98), Width = 200 };

            Label lbl4 = new Label { Text = "Trưởng bộ môn:", Location = new Point(20, 140) };
            txtHead = new TextBox { Location = new Point(140, 138), Width = 200 };

            Label lbl5 = new Label { Text = "Mô tả:", Location = new Point(20, 180) };
            txtDesc = new TextBox { Location = new Point(140, 178), Width = 200, Height = 60, Multiline = true };

            Button btnOK = new Button { Text = "Tạo", Location = new Point(140, 250) };
            btnOK.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtCode.Text) || string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                NewSubject = new Subject(0, txtCode.Text, txtName.Text, txtDept.Text, txtHead.Text, 0, DateTime.Now.ToString("yyyy-MM-dd"), txtDesc.Text);
                DialogResult = DialogResult.OK;
                Close();
            };

            Button btnCancel = new Button { Text = "Hủy", Location = new Point(240, 250) };
            btnCancel.Click += (s, e) => Close();

            Controls.AddRange(new Control[] { lbl1, txtCode, lbl2, txtName, lbl3, txtDept, lbl4, txtHead, lbl5, txtDesc, btnOK, btnCancel });
        }
}
}
