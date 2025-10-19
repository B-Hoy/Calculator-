using Calculator_.src;
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
        private readonly User u;

        public MainWindow(User u)
        {
            this.u = u;
            InitializeComponent();
            UpdateResultDisplay(currentNumber);
            EnableUnlockedOperators();

        }
        private void EnableUnlockedOperators()
        {
            if (u.OperatorsUnlocked & Utils.Operators.Divide)
            {

            }
        }

        private void UpdateResultDisplay(string text)
        {
            TextBlock[] resultDigits = [ResultDigit0, ResultDigit1, ResultDigit2, ResultDigit3, ResultDigit4, ResultDigit5, ResultDigit6, ResultDigit7];
            Border[] resultDigitBorders = [ResultDigit0Border, ResultDigit1Border, ResultDigit2Border, ResultDigit3Border, ResultDigit4Border, ResultDigit5Border, ResultDigit6Border, ResultDigit7Border];

            for (int i = 0; i < resultDigits.Length; i++)
            {
                resultDigits[i].Text = "";
                if (i < u.UnlockedDigits)
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
            string buttonContent = ((Button)sender).Content.ToString()!;
            if (currentNumber.Length >= u.UnlockedDigits && !isOperatorClicked)
            {
                MessageBox.Show($"Only {u.UnlockedDigits} Digit(s) unlocked! Please purchase additonal digits and try again!", "Error.", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                operation = ((Button)sender).Content.ToString()!;
                UpdateResultDisplay(operation);
            }
        }

        private void Purchase_Click(object sender, RoutedEventArgs e)
        {
            Shop s = new(u)
            {
                Owner = this
            };
            s.ShowDialog();
            Clear_Click(null, null);
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (currentNumber != "" && !isOperatorClicked && operation != "")
            {
                double secondNumber = double.Parse(currentNumber);
                if (operation == "+" && (result + secondNumber) == (result * secondNumber) && CheckResultLength(result * secondNumber))
                {
                    ButtonMultiply.IsEnabled = true;
                }

                if (operation == "-" && (result - secondNumber) == (result / secondNumber) && CheckResultLength(result / secondNumber))
                {
                    ButtonDivide.IsEnabled = true;
                    ButtonPercent.IsEnabled = true;
                }


                if (operation == "x" && (result * secondNumber) == (result * result) && CheckResultLength(result * result))
                {
                    ButtonSquared.IsEnabled = true;
                    ButtonSqrt.IsEnabled = true;
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
                    case "%":
                        result = (result / 100) * secondNumber;
                        break;
                }

                if (!CheckResultLength(result)) return;
                u.Score += result;
                currentNumber = result.ToString();
                UpdateResultDisplay(currentNumber);
                operation = "";
            }
            isOperatorClicked = false;
        }

        private void Clear_Click(object? sender, RoutedEventArgs? e)
        {
            currentNumber = "";
            result = 0;
            operation = "";
            UpdateResultDisplay(currentNumber);
            isOperatorClicked = false;
        }
        private void Stats_Click(object sender, RoutedEventArgs e)
        {
            Statistics s = new()
            {
                Owner = this
            };
            s.ShowDialog();
            Clear_Click(null, null);
        }

        private void Decimal_Click(object sender, RoutedEventArgs e)
        {
            if (!currentNumber.Contains('.'))
            {
                if (currentNumber == "")
                {
                    currentNumber = "0";
                }
                currentNumber += ".";
                if (currentNumber.Length > u.UnlockedDigits + 1) // +1 for the decimal point
                {
                    MessageBox.Show($"Only {u.UnlockedDigits} Digit(s) unlocked! Please purchase additonal digits and try again!", "Error.", MessageBoxButton.OK, MessageBoxImage.Warning);
                    currentNumber = currentNumber[..^1]; // remove the decimal point
                    Clear_Click(null, null);
                    return;
                }
                UpdateResultDisplay(currentNumber);
            }
        }

        private void Squared_Click(object sender, RoutedEventArgs e)
        {
            if (currentNumber != "")
            {
                double number = double.Parse(currentNumber);
                result = number * number;

                currentNumber = result.ToString();
                if (!CheckResultLength(result)) return;
                UpdateResultDisplay(currentNumber);
            }
        }
        
        private void Sqrt_Click(object sender, RoutedEventArgs e)
        {
            if (currentNumber != "")
            {
                double number = double.Parse(currentNumber);
                if (number >= 0)
                {
                    result = Math.Sqrt(number);

                    currentNumber = result.ToString();
                    if (!CheckResultLength(result)) return;
                    UpdateResultDisplay(currentNumber);
                }
                else
                {
                    MessageBox.Show("Cannot calculate the square root of a negative number");
                    Clear_Click(null, null);
                }
            }
        }

        private void Percent_Click(object sender, RoutedEventArgs e)
        {
            if (currentNumber != "")
            {
                double number = double.Parse(currentNumber);
                result = number / 100;

                //if (!CheckResultLength(result)) return;

                currentNumber = result.ToString();
                UpdateResultDisplay(currentNumber);
            }
        }


        private bool CheckResultLength(double value)
        {
            //string integerPart = value.ToString().Split('.')[0];
            string integerPart = value.ToString();
            if (integerPart.StartsWith('-'))
            {
                integerPart = integerPart[1..];
            }

            if (result >= 99999999)
            {
                // truncate to 8 digits <-- dumbass
                // You cant truncate a double without making it into a different number, idiot
                //result = double.Parse(result.ToString().Substring(0, 8));
                MessageBox.Show("Calculator 2 has a max of 8 digits! Your result was too long, so it was deleted. Please wait until Calculator 3!", "Error.", MessageBoxButton.OK, MessageBoxImage.Warning);
                Clear_Click(null, null);
                return false;
            }

            // if the number is a float with more decimal places than unlocked digits, truncate it
            // haven't been able to test this yet
            if (value.ToString().Contains("."))
            {
                string[] parts = value.ToString().Split('.');
                string decimalPart = parts[1];
                if (decimalPart.Length > UnlockedDigits)
                {
                    decimalPart = decimalPart.Substring(0, UnlockedDigits);
                    value = double.Parse(parts[0] + "." + decimalPart);
                    currentNumber = value.ToString();
                    UpdateResultDisplay(currentNumber);

                    // Debug Message to display truncated float
                    Debug.WriteLine($"Truncated float to {UnlockedDigits} decimal places: {currentNumber}");


                    return true;
                }
            }

            // Debug message to console
            //Debug.WriteLine($"Result length: {integerPart.Length}, UnlockedDigits: {UnlockedDigits}");

            if (integerPart.Length > u.UnlockedDigits)
            {
                MessageBox.Show($"Only {u.UnlockedDigits} Digit(s) unlocked! Please purchase additonal digits and try again!", "Error.", MessageBoxButton.OK, MessageBoxImage.Warning);
                Clear_Click(null, null);
                return false;
            }
            return true;
        }
    }
}
