using System.ComponentModel;
using System.Drawing;
using System.Windows.Media;
using Colours = System.Windows.Media.Colors;
using SolidColourBrush = System.Windows.Media.SolidColorBrush;
namespace Calculator_.src
{
	internal class User : INotifyPropertyChanged
	{
		private string fname;
		public string Fname{ 
		get
		{
			return this.fname;
		} 
		set
		{
			if (this.fname != value){
				fname = value;
					this.SendNotif("Fname");
				}
			}
		}
		private string lname;
		public string Lname
		{
			get
			{
				return this.lname;
			}
			set
			{
				if (this.lname != value)
				{
					lname = value;
					this.SendNotif("Lname");
				}
			}
		}
		private string gender;
		public string Gender
		{
			get
			{
				return this.gender;
			}
			set
			{
				if (this.gender != value)
				{
					gender = value;
					this.SendNotif("Gender");
				}
			}
		}
		private string dob;
		public string DOB
		{
			get
			{
				return this.dob;
			}
			set
			{
				if (this.dob != value)
				{
					dob = value;
					this.SendNotif("DOB");
				}
			}
		}
		private string username;
		public string Username
		{
			get
			{
				return this.username;
			}
			set
			{
				if (this.username != value)
				{
					username = value;
					this.SendNotif("Username");
				}
			}
		}

		public string ccn;
		public string CCN
		{
			get
			{
				return this.ccn;
			}
			set
			{
				if (this.ccn != value)
				{
					ccn = value;
					this.SendNotif("CCN");
				}
			}
		}
		public string cce;
		public string CCE
		{
			get
			{
				return this.cce;
			}
			set
			{
				if (this.cce != value)
				{
					cce = value;
					this.SendNotif("CCE");
				}
			}
		}
		public string cvc;
		public string CVC
		{
			get
			{
				return this.cvc;
			}
			set
			{
				if (this.cvc != value)
				{
					cvc = value;
					this.SendNotif("CVC");
				}
			}
		}
		public SolidColorBrush favcolour;
		public SolidColorBrush FavouriteColour
		{
			get{
				return this.favcolour;
			}
			set{
				if (this.favcolour != value){
					this.favcolour = value;
				}
			}
		}
		public User(){
			fname = "John";
			lname = "Smith";
			gender = "Non-binary";
			ccn = "XXXX-XXXX-XXXX-XXXX";
			cce = "01/30";
			cvc = "123";
			username = "thelegend27";
			dob = "2000-01-01";
			favcolour = new(Colours.Black);
		}

		public event PropertyChangedEventHandler? PropertyChanged;
		public void SendNotif(string varName){
			if (this.PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(varName));
			}
		}
	}
}
