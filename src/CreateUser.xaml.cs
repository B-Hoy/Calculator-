using Microsoft.Extensions.Options;
using Stripe;
using System.ComponentModel;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Colour = System.Windows.Media.Color;
using SolidColourBrush = System.Windows.Media.SolidColorBrush;

namespace Calculator_.src
{
    public partial class CreateUser : Window
    {
        private class CreateUserVars : INotifyPropertyChanged
        {
            private string firstname = "First Name";
            private string lastname = "Last Name";
            private string gender = "Non-binary";
            private string dob = "2000-09-30";
            private string username = "thelegend27";
            private bool willbeactive = false;

            public event PropertyChangedEventHandler? PropertyChanged;

            public string FirstName
            {
                get
                {
                    return this.firstname;
                }
                set
                {
                    if (this.firstname != value)
                    {
                        this.firstname = value;
                        OnPropertyChanged("FirstName");
                    }
                }
            }
            public string LastName
            {
                get
                {
                    return this.lastname;
                }
                set
                {
                    if (this.lastname != value)
                    {
                        this.lastname = value;
                        OnPropertyChanged("LastName");
                    }
                }
            }
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
                        this.gender = value;
                        OnPropertyChanged("Gender");
                    }
                }
            }
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
                        this.dob = value;
                        OnPropertyChanged("DOB");
                    }
                }
            }
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
                        this.username = value;
                        OnPropertyChanged("Username");
                    }
                }
            }
            private string email = "example@email.com";
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
                        OnPropertyChanged("Email");
                    }
                }
            }
            private string ccn = "XXXX-XXXX-XXXX-XXXX";
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
                        this.ccn = value;
                        OnPropertyChanged("CCN");
                    }
                }
            }
            private string cvc = "123";
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
                        this.cvc = value;
                        OnPropertyChanged("CVC");
                    }
                }
            }
            public bool WillBeActive
            {
                get
                {
                    return this.willbeactive;
                }
                set
                {
                    if (this.willbeactive != value)
                    {
                        this.willbeactive = value;
                        OnPropertyChanged("WillBeActive");
                    }
                }
            }
            public SolidColourBrush slidercolour = new(Colour.FromRgb(0, 255, 0));
            public SolidColourBrush SliderColour
            {
                get
                {
                    return this.slidercolour;
                }
                set
                {
                    if (this.slidercolour != value)
                    {
                        this.slidercolour = value;
                        OnPropertyChanged("SliderColour");
                    }
                }
            }


            public void OnPropertyChanged(string PropertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
        private readonly CreateUserVars vars;
        private bool fnameChanged = false;
        private bool lnameChanged = false;
        private bool genChanged = false;
        private bool emailChanged = false;
        private bool unameChanged = false;
        private bool CVCchanged = false;
        private bool CCNchanged = false;
        private bool windowWontClose = true;
        private Int32 ColourAsInt = 0;

        public CreateUser()
        {
            InitializeComponent();
            vars = new();
            this.DataContext = vars;
            this.Loaded += this.DisableWindowClosing;
        }
        private void EmailChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!emailChanged)
            {
                vars.Email = "";
                emailChanged = true;
            }
        }
        private void FocusFirstName(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!fnameChanged)
            {
                vars.FirstName = "";
                fnameChanged = true;
            }
        }
        private void FocusLastName(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!lnameChanged)
            {
                vars.LastName = "";
                lnameChanged = true;
            }
        }
        private void FocusGender(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!genChanged)
            {
                vars.Gender = "";
                genChanged = true;
            }
        }
        private void FocusUsername(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!unameChanged)
            {
                vars.Username = "";
                unameChanged = true;
            }
        }
        private void ColourChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        { 
            ColourAsInt = Convert.ToInt32(e.NewValue);
            byte[] temp = BitConverter.GetBytes(ColourAsInt);
            vars.SliderColour.Color = Colour.FromRgb(temp[0], temp[1], temp[2]);
            vars.OnPropertyChanged("SliderColour");
        }
        private void FocusCCN(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!CCNchanged)
            {
                vars.CCN = "";
                CCNchanged = true;
            }
        }
        private void FocusCVC(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!CVCchanged)
            {
                vars.CVC = "";
                CVCchanged = true;
            }
        }
        private void SubmitPage(object sender, RoutedEventArgs e)
        {
            if (!CCNRegex().IsMatch(vars.CCN))
            {
                MessageBox.Show("Credit card number is invalid, please try again!\nThe format must be XXXX-XXXX-XXXX-XXXX");
                return;
            }
            if (!CVCRegex().IsMatch(vars.CVC))
            {
                MessageBox.Show("CVC is invalid, please try again!");
            }
            string cce = ((ComboBoxItem)this.CCEMonths.SelectedItem).Content.ToString()!;
            cce += $"/{((ComboBoxItem)this.CCEYears.SelectedItem).Content}";
            Database.AddUser(vars.Email, vars.FirstName, vars.LastName, vars.Gender, vars.DOB, vars.Username, vars.WillBeActive, ColourAsInt, vars.CCN, cce, vars.CVC);

            windowWontClose = false;
            this.Close();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = windowWontClose;
        }

        [GeneratedRegex("^\\d{4}-\\d{4}-\\d{4}-\\d{4}$")]
        private static partial Regex CCNRegex();
        [GeneratedRegex("^\\d{3}$")]
        private static partial Regex CVCRegex();
    }
}
