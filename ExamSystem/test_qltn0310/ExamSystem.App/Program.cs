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
            Application.Run(new ExamSystem.App.Forms.PortalSelectorForm());
        }
    }
}


