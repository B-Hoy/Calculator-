using System.Windows;
using System.Windows.Controls;

namespace Calculator_.src
{
    public partial class PickOperator : Window
    {
        public Operators SelectedOperator { get; private set; }
        private readonly MainWindow mainWindow;

        public PickOperator(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void OperatorSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Set the SelectedOperator based on the selected item
            if (OperatorSelector.SelectedItem is ComboBoxItem selectedItem)
            {
                switch (selectedItem.Content.ToString())
                {
                    case "x":
                        SelectedOperator = Operators.Multiply;
                        break;
                    case "/":
                        SelectedOperator = Operators.Divide;
                        break;
                    case "%":
                        SelectedOperator = Operators.Percent;
                        break;
                    case "x²":
                        SelectedOperator = Operators.Square;
                        break;
                    case "√x":
                        SelectedOperator = Operators.Root;
                        break;
                    default:
                        SelectedOperator = Operators.None;
                        break;
                }
            }

            // Enable to Purchase button if a selection has been made
            if (SelectedOperator != Operators.None)
            {
                PurchaseButton.IsEnabled = true;
            }
            else
            {
                PurchaseButton.IsEnabled = false;
            }
        }

        private void PurchasePressed(object sender, RoutedEventArgs e)
        { 
            PurchaseCompleted(sender, e);
        }

        private void PurchaseCompleted(object sender, RoutedEventArgs e)
        {
            if (SelectedOperator != Operators.None)
            {
                mainWindow.UnlockOperator(SelectedOperator);
            }
            Close();
        }
    }
}