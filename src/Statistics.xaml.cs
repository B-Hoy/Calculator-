using System.Windows;

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
            
        }
        
        private void GenerateGraph()
        {

        }

        private void PrevClick(object sender, RoutedEventArgs e)
        {
            
                
            
        }

        private void NextClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
