using System.Windows;
using System.IO;
using Calculator_.src;
using System.Data.SQLite;
using Stripe;

namespace Calculator_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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