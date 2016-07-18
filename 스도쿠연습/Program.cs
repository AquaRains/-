using System;
using System.Windows.Forms;

namespace 스도쿠연습
{
    class Program
    {

        internal static Form1 form1;
        [STAThread]

        static void Main()
        {
         

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form1 = new Form1();
            Application.Run(form1);

        }
        
    }


    
}
