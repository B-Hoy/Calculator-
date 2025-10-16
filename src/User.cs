using Stripe;
using System.ComponentModel;
using Colour = System.Windows.Media.Color;
namespace Calculator_.src
{
	internal class User : INotifyPropertyChanged
	{
		int id;
		public int ID{
			get{
				return this.id;
			}
			set{
				if (this.id != value)
				{
					id = value;
				}
			}
		}
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
		public bool isactiveuser;
		public bool IsActiveUser{
			get{
				return this.isactiveuser;
			}
			set{
				if (this.isactiveuser != value){
					this.isactiveuser = value;
				}
			}
		}
		public int favcolour;
		public int FavouriteColour
		{
			get{
				return this.favcolour;
			}
			set{
				if (this.favcolour != value){
					this.favcolour = value;
					this.SendNotif("BrushColour");
				}
			}
		}
		public Colour BrushColour{
			get
			{
				byte[] temp = BitConverter.GetBytes(this.favcolour);
				return Colour.FromRgb(temp[2], temp[1], temp[0]);
			}
		}
		public void ChargeUserWithPrompt()
		{

		}
		public void ChargeUserCard(int cents, string reason){
			string[] expiry = this.CCE.Split("/");
			TokenCardOptions tokenOpts = new()
			{
				Number = this.CCN,
				ExpMonth = expiry[0],
				ExpYear = expiry[1],
				Cvc = this.CVC
			};
			TokenService tserv = new();
			TokenCreateOptions tco = new()
			{
				Card = tokenOpts
			};
			//Token t = tserv.Create(tco);
			ChargeCreateOptions chargeOpts = new()
			{
				Amount = cents,
				Currency = "aud",
				//Source = t.Id,
				Source = "tok_visa",
				Description = reason
			};
			ChargeService cserv = new();
			Charge c = cserv.Create(chargeOpts);
		}
		public User(){
			id = 0;
			fname = "John";
			lname = "Smith";
			gender = "Non-binary";
			ccn = "XXXX-XXXX-XXXX-XXXX";
			cce = "01/30";
			cvc = "123";
			username = "thelegend27";
			dob = "2000-01-01";
			favcolour = 0;
			isactiveuser = true;
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
