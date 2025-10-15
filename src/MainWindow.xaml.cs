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
            UpdateResultDisplay(currentNumber);
        }

        private void UpdateResultDisplay(string text)
        {
            TextBlock[] resultDigits = { ResultDigit0, ResultDigit1, ResultDigit2, ResultDigit3, ResultDigit4, ResultDigit5, ResultDigit6, ResultDigit7 };
            Border[] resultDigitBorders = { ResultDigit0Border, ResultDigit1Border, ResultDigit2Border, ResultDigit3Border, ResultDigit4Border, ResultDigit5Border, ResultDigit6Border, ResultDigit7Border };

            for (int i = 0; i < resultDigits.Length; i++)
            {
                resultDigits[i].Text = "";
                if (i < UnlockedDigits)
                {
                    resultDigitBorders[i].Background = new SolidColorBrush(Colors.White);
                }
                else
                {
                    resultDigitBorders[i].Background = new SolidColorBrush(Colors.LightGray);
                }
            }

            int len = text.Length;
            for (int i = 0; i < len; i++)
            {
                char c = text[len - 1 - i];
                if (i < resultDigits.Length)
                {
                    resultDigits[i].Text = c.ToString();
                }
            }
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            string buttonContent = (sender as Button).Content.ToString();
            if (currentNumber.Length >= UnlockedDigits && !isOperatorClicked)
            {
                MessageBox.Show($"Only {UnlockedDigits} Digit(s) unlocked! Please purchase additonal digits and try again!", "Error.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (isOperatorClicked)
            {
                currentNumber = "";
                isOperatorClicked = false;
            }
            currentNumber += buttonContent;
            UpdateResultDisplay(currentNumber);
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            if (currentNumber != "")
            {
                result = double.Parse(currentNumber);
                isOperatorClicked = true;
                operation = (sender as Button).Content.ToString();
                UpdateResultDisplay(operation);
            }
        }

        private void Purchase_Click(object sender, RoutedEventArgs e)
        {
            
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://buy.stripe.com/test_fZuaEW6QX1AGfSH3Z500000",
                UseShellExecute = true
            });

            MessageBoxResult result = MessageBox.Show("Have you completed your purchase? Click OK to unlock more digits.", "Purchase", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                UnlockedDigits++;
            }
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (currentNumber != "" && !isOperatorClicked && operation != "")
            {
                double secondNumber = double.Parse(currentNumber);
                if (operation == "+" && (result + secondNumber) == (result * secondNumber))
                {
                    ButtonMultiply.IsEnabled = true;
                }

                if (operation == "-" && (result - secondNumber) == (result / secondNumber))
                {
                    ButtonDivide.IsEnabled = true;
                }

                switch (operation)
                {
                    case "+":
                        result += secondNumber;
                        break;
                    case "-":
                        result -= secondNumber;
                        break;
                    case "x":
                        result *= secondNumber;
                        break;
                    case "/":
                        if(secondNumber != 0)
                        {
                            result /= secondNumber;
                        }
                        else
                        {
                            MessageBox.Show("Cannot divide by zero");
                            Clear_Click(null, null);
                            return;
                        }
                        break;
                }
                currentNumber = result.ToString();
                UpdateResultDisplay(currentNumber);
                operation = "";

                if (result.ToString().Split('.')[0].Length > UnlockedDigits)
                {
                    MessageBox.Show($"Only {UnlockedDigits} Digit(s) unlocked! Please purchase additonal digits and try again!", "Error.", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Clear_Click(null, null);
                    return;
                }
            }
            isOperatorClicked = false;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            currentNumber = "";
            result = 0;
            operation = "";
            UpdateResultDisplay(currentNumber);
            isOperatorClicked = false;
        }

        private void Decimal_Click(object sender, RoutedEventArgs e)
        {
            if (!currentNumber.Contains("."))
            {
                if (currentNumber == "")
                {
                    currentNumber = "0";
                }
                currentNumber += ".";
                UpdateResultDisplay(currentNumber);
            }
        }
    }
}
