using System.ComponentModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Colour = System.Windows.Media.Color;
using Colours = System.Windows.Media.Colors;


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

		private readonly bool FirstUser;

		public UserProfile(bool f)
		{
			FirstUser = f;
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
			Regex cardNum = new("^[0-9]{4}(-[0-9]{4}){3}$");
			if (!cardNum.IsMatch(NewUser.CCN)){
				MessageBox.Show("Credit Card Number is invalid, please try again");
				return;
			}
			Regex cardCode = new("^[0-9]{3}$");
			if (!cardCode.IsMatch(NewUser.CVC)){
				MessageBox.Show("CVC is invalid, please try again");
				return;
			}
			// assign gender
			
			RadioButton b1 = (RadioButton)this.FindName("MaleButton");
			RadioButton b2 = (RadioButton)this.FindName("MaleButton");
			if (b1.IsChecked != null && b2.IsChecked == true)
			{
				NewUser.Gender = "Male";
			}
			else if (b1.IsChecked != null && b2.IsChecked == true)
			{
				NewUser.Gender = "Female";
			}
			Database.EFHook.Users.Add(NewUser);
			Database.EFHook.SaveChanges();
			// we need save changes to actually write to the database
			e.Cancel = false;
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
				NewUser.FavouriteColour = Convert.ToInt32(e.NewValue);

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
