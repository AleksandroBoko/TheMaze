using System;
using System.Windows.Forms;

namespace TheMaze.WinForms
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameMenu());
        }
    }
}
