using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace Calculator_.src
{
	/// <summary>
	/// Interaction logic for UserProfile.xaml
	/// </summary>
	public partial class UserProfile : Window
	{
		public UserProfile()
		{
			InitializeComponent();

			Loaded += IsLoaded_KillClose;
		}
		void IsLoaded_KillClose(object sender, RoutedEventArgs e)
		{
			[DllImport("user32.dll")]
			static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
			[DllImport("user32.dll")]
			static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

			var hwnd = new WindowInteropHelper(this).Handle;
			IntPtr hMenu = GetSystemMenu(hwnd, false);
			EnableMenuItem(hMenu, 0xF060, 0x00000001);
		}
		private void FormSubmit(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
