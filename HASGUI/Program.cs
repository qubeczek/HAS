using System;
using System.Windows.Forms;
using HASGUI.Forms;
using HASGUI.Utils;

namespace HASGUI
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm win = new MainForm();
            PLCUnit.Start();
            Application.Run(win);
        }
	}
}
