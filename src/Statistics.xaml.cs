using System.Windows;
using System.Windows.Controls.DataVisualization.Charting;

namespace Calculator_.src
{

    public partial class Statistics : Window
    {
        private User currentUser;

        public Statistics(User user)
        {
            InitializeComponent();
            currentUser = user;
            GenerateGraph();
            userName.Text = user.Username;
        }
        
        private void GenerateGraph()
        {
            List<KeyValuePair<double, double>> stats = [];
            int a = 0;
            foreach (DataPoint d in Database.EFHook.Points.Where(e => e.ParentId == currentUser.ID)){ 
                stats.Add(new(d.GetValue(), d.GetFreq()));
                a++;
            }
            TheLine.ItemsSource = stats;
        }

        private void PrevClick(object sender, RoutedEventArgs e)
        {
            
                
            
        }

        private void NextClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
