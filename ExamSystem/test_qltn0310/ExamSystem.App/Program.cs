using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using test_qltn0310.ExamSystem.App.Forms;


namespace test_qltn0310
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Khởi động Form PortalSelector
            //Application.Run(new ExamSystem.App.Forms.PortalSelectorForm());
            // Application.Run(new ExamSystem.App.Forms.AdminDashboardForm());

            // Chọn form để test
            string testForm = "Subject"; // đổi thành: "Portal", "Dashboard", "User", "Topbar"

            switch (testForm)
            {
                case "Portal":
                    Application.Run(new PortalSelectorForm());
                    break;
                case "Dashboard":
                    Application.Run(new AdminDashboardForm());
                    break;
                case "User":
                    Application.Run(new UserManagementForm());
                    break;
                case "Subject":
                    Application.Run(new SubjectManagementForm());
                    break;
                default:
                    Application.Run(new PortalSelectorForm());
                    break;
            }
        }

    }
    }


