using Stripe;
using System.ComponentModel;
using Colour = System.Windows.Media.Color;
namespace Calculator_.src
{
	public class User : INotifyPropertyChanged
	{
        [Flags]
        public enum Operators
        {
            None = 0x00000000,
            Multiply = 0x00000001,
            Divide = 0x00000002,
            Square = 0x00000004,
            Root = 0x00000008,
            Log = 0x00000010,
            Poly = 0x00000020
        }
        private readonly List<IDataPoint> myEvents;
		private int id;
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
		private string email;
		public string Email
		{
			get
			{
				return this.email;
			}
			set
			{
				if (this.email != value)
				{
					this.email = value;
					this.SendNotif("Email");
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
		private string ccn;
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
		private string cce;
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
		private string cvc;
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
		public int UnlockedDigits = 1;
		public Operators OperatorsUnlocked;
		public double Score;
        // "Use of external APIs or tools"
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
			Token t = true ? new() : tserv.Create(tco);
			t.Id = "tok_visa";

            ChargeCreateOptions chargeOpts = new()
			{
				Amount = cents,
				Currency = "aud",
				Source = t.Id,
				Description = reason
			};
			ChargeService cserv = new();
            _ = cserv.CreateAsync(chargeOpts);
        }
		public User()
		{
			this.id = 0;
			this.fname = "";
			this.lname = "";
			this.email = "";
			this.gender = "";
			this.username = "";
			this.dob = "";
			this.ccn = "";
			this.cce = "";
			this.cvc = "";
			this.favcolour = 1;
			this.isactiveuser = false;
			this.OperatorsUnlocked = 0;
			this.Score = 0;
			myEvents = [];
		}
		public User(int id, string email, string fname, string lname, string gender, string dob, string username, bool isactiveuser, int favcolour, string ccn, string cce, string cvc)
        {
			this.id = id;
			this.fname = fname;
			this.email = email;
			this.lname = lname;
			this.gender = gender;
			this.username = username;
			this.dob = dob;
			this.favcolour = favcolour;
			this.isactiveuser = isactiveuser;
			this.cce = cce;
			this.ccn = ccn;
			this.cvc = cvc;
			myEvents = [];
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
