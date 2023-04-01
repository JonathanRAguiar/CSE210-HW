using System;
using System.Windows.Forms;

using hash.Game;

namespace hash
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Battlefield());
        }
    }
}