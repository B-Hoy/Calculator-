using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using Colour = System.Windows.Media.Color;

namespace Calculator_.src
{
	/// <summary>
	/// Interaction logic for UserProfile.xaml
	/// </summary>
	public partial class UserProfile : Window
	{
		private readonly int[] daysArr = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
		private readonly User NewUser;
		private readonly ComboBox months;
		private readonly ComboBox days;
		private bool PastConstructor = false;
		private bool ButtonPressed = false;
		private bool WipedFname;
		private bool WipedLname;
		private bool WipedGender;
		private bool WipedUsername;
		private bool WipedCCN;
		private bool WipedCVC;

		public UserProfile()
		{
			InitializeComponent();
			Loaded += this.DisableWindowClosing;
			NewUser = new();
			this.DataContext = NewUser;
			WipedFname = WipedLname = WipedGender = WipedUsername = WipedCCN = WipedCVC = false;
			ComboBox? temp = (ComboBox?)this.FindName("Months");
			if (temp == null){
				Utils.ExitError("Could not determine location of \"Months\" field", 2);
			}
			months = temp!;
			for (int i = 1; i <= 12; i++){
				months.Items.Add(i);
			}
			months.SelectedItem = months.Items[0];
			temp = (ComboBox?)this.FindName("Days");
			if (temp == null){
				Utils.ExitError("Could not determine location of \"Days\" field", 2);
			}
			days = temp!;
			for (int i = 1; i <= 31; i++)
			{
				days.Items.Add(i);
			}
			days.SelectedItem = days.Items[0];
			PastConstructor = true;
		}
		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			e.Cancel = true;
			// verify all the inputs, ensure that it was the submit button that sent us here, and let it rip
			if (!this.ButtonPressed){
				return;
			}
		}
		private void GenTextChanged(object sender, DependencyPropertyChangedEventArgs e){
			if (!this.WipedGender)
			{
				this.WipedGender = true;
				this.NewUser.Gender = "";
			}
		}
		private void CVCTextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (!this.WipedCVC)
			{
				this.WipedCVC = true;
				this.NewUser.CVC = "";
			}
		}
		private void MonthsSelectionChanged(object sender, SelectionChangedEventArgs e){
			if (PastConstructor)
			{
				int amount = daysArr[(int)e.AddedItems[0]! - 1];
				int prevVal = (int)days.SelectedValue;
				days.Items.Clear();
				for (int i = 1; i <= amount; i++)
				{
					days.Items.Add(i);
				}
				if (prevVal > amount)
				{
					days.SelectedItem = days.Items[amount - 1];
				}
				else
				{
					days.SelectedItem = days.Items[prevVal - 1];
				}
			}
		}
		private void CCNTextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (!this.WipedCCN)
			{
				this.WipedCCN = true;
				this.NewUser.CCN = "";
			}
		}
		private void FnameTextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (!this.WipedFname)
			{
				this.WipedFname = true;
				this.NewUser.Fname = "";
			}
		}
		private void LnameTextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (!this.WipedLname)
			{
				this.WipedLname = true;
				this.NewUser.Lname = "";
			}
		}
		private void ColourSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e){
			if (PastConstructor)
			{

				byte[] bytes = BitConverter.GetBytes(Convert.ToInt32(e.NewValue));
				NewUser.FavouriteColour.Color = Colour.FromRgb(bytes[2], bytes[1], bytes[0]);
				//MessageBox.Show(bytes[0].ToString("X") + "|" + bytes[1].ToString("X") + "|" + bytes[2].ToString("X") + "|" + bytes[3].ToString("X"));
				//int t = (int)e.NewValue;
				//MessageBox.Show(e.NewValue.ToString());
			}
		}
		private void UsernameTextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (!this.WipedUsername)
			{
				this.WipedUsername = true;
				this.NewUser.Username = "";
			}
		}
		private void FormSubmit(object sender, RoutedEventArgs e)
		{
			this.ButtonPressed = true;
			this.Close();
			this.ButtonPressed = false;
		}

	}
}
