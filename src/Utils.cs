using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace Calculator_.src
{
	static internal class Utils
	{
		public static void ExitError(string errorMessage, int errorCode){
			Console.Error.WriteLine(errorMessage);
			Environment.Exit(errorCode);
		}

        static public void DisableWindowClosing(this Window win, object sender, RoutedEventArgs e)
		{
			[DllImport("user32.dll")]
			static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
			[DllImport("user32.dll")]
			static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

			var hwnd = new WindowInteropHelper(win).Handle;
			IntPtr hMenu = GetSystemMenu(hwnd, false);
			EnableMenuItem(hMenu, 0xF060, 0x00000001);


		}
	}
}
