using System.Windows;
using System.IO;
using Calculator_.src;
using System.Data.SQLite;

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
            if (SystemParameters.WorkArea.Height < SystemParameters.WorkArea.Width)
            {
                this.Height = SystemParameters.WorkArea.Height / 4 * 3;
                // I am aware that WorkArea.Width exists, this is in case of a non-16:9 display to keep the window ratio consistent
                this.Width = this.Height / 9 * 16;
            }else{
				this.Width = SystemParameters.WorkArea.Width / 4 * 3;
                this.Height = this.Width / 16 * 9;
			}
            SQLiteCommand tableProbe = Database.Hook.CreateCommand();
            tableProbe.CommandText = """SELECT count(name) FROM sqlite_master WHERE type = 'table'""";
            bool result = Convert.ToBoolean(tableProbe.ExecuteScalar());
            if (!result){
				UserProfile initData = new()
				{
					Title = "Create first user"
				};
				initData.ShowDialog();
            }
		}
    }
}