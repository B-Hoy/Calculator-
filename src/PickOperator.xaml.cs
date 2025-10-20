using System.Windows;
using System.Windows.Controls;

namespace Calculator_.src
{
    public partial class PickOperator : Window
    {
        public Operators SelectedOperator { get; private set; }
        private readonly Shop shop;

        public PickOperator(Shop shop)
        {
            InitializeComponent();
            this.shop = shop;
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
            // Close this window and send SelectedOperator to OperatorClick function in Shop
            shop.OperatorUnlock(sender, e, SelectedOperator);
            this.Close();
        }
    }
}