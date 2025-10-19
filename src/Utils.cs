using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace Calculator_.src
{
	static internal class Utils
	{
        [Flags]
		public enum Operators
        {
            None =     0x00000000,
            Multiply = 0x00000001,
            Divide =   0x00000002,
            Square =   0x00000004,
			Root =	   0x00000008,
			Log =	   0x00000010,
			Poly =     0x00000020
        }
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
