using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Calculator_
{
    public partial class MainWindow : Window
    {
        private string currentNumber = "";
        private string operation = "";
        private double result = 0;
        private bool isOperatorClicked = false;
        public int UnlockedDigits = 1;

        public MainWindow()
        {
            InitializeComponent();
            if (SystemParameters.WorkArea.Height < SystemParameters.WorkArea.Width)
            {
                this.Height = SystemParameters.WorkArea.Height / 4 * 3;
                // I am aware that WorkArea.Width exists, this is in case of a non-16:9 display to keep the window ratio consistent
                this.Width = this.Height / 9 * 16;
            }else{
				this.Width = SystemParameters.WorkArea.Width / 4 * 3;
                this.Height = this.Width / 16 * 9;
			}
            SQLiteCommand tableProbe = Database.RawHook.CreateCommand();
            tableProbe.CommandText = "SELECT count(id) FROM Users;";
            bool result = Convert.ToBoolean(tableProbe.ExecuteScalar());
            if (!result){
				UserProfile initData = new(true)
				{
					Title = "Create first user"
				};
				initData.ShowDialog();
            }
		}
    }
}
