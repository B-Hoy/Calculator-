using System.Windows;
using System;

namespace Calculator_.src
{

    public partial class Statistics : Window
    {
        private User currentUser;

        public Statistics(User user)
        {
            InitializeComponent();
            currentUser = user;
            GenerateRandomBars();
            GenerateRandomCalculations();
        }

        // All this stuff just generates dummy data 

        private void GenerateRandomCalculations()
        {
            var calculations = new System.Collections.Generic.List<string>();
            Random random = new Random();
            string[] operators = { "+", "-", "*", "/" };

            for (int i = 0; i < 10; i++)
            {
                int num1 = random.Next(1, 21);
                int num2 = random.Next(1, 21);
                string op = operators[random.Next(operators.Length)];
                string calculation = "";

                switch (op)
                {
                    case "+":
                        calculation = $"{num1} + {num2} = {num1 + num2}";
                        break;
                    case "-":
                        calculation = $"{num1} - {num2} = {num1 - num2}";
                        break;
                    case "*":
                        calculation = $"{num1} * {num2} = {num1 * num2}";
                        break;
                    case "/":
                        num2 = random.Next(1, 11);
                        num1 = num1 * num2; 
                        calculation = $"{num1} / {num2} = {num1 / num2}";
                        break;
                }
                calculations.Add(calculation);
            }
            calculationsList.ItemsSource = calculations;
        }

        private void GenerateRandomBars()
        {
            Random random = new Random();
            var dataPoints = new System.Collections.Generic.List<Tuple<DateTime, int>>();
            for (int i = 0; i < 10; i++)
            {
                dataPoints.Add(new Tuple<DateTime, int>(new DateTime(2025, 1, i + 1), random.Next(1, 11)));
            }

            double canvasHeight = barChartCanvas.ActualHeight;
            if (double.IsNaN(canvasHeight) || canvasHeight <= 0)
            {
                canvasHeight = 300;
            }
            double maxValue = 10;

            for (int i = 0; i < dataPoints.Count; i++)
            {
                double barHeight = (dataPoints[i].Item2 / maxValue) * (canvasHeight - 60);
                System.Windows.Shapes.Rectangle rectangle = new System.Windows.Shapes.Rectangle
                {
                    Fill = new System.Windows.Media.SolidColorBrush(currentUser.BrushColour),
                    Height = barHeight,
                    Width = 30
                };
                barChartCanvas.Children.Add(rectangle);
                System.Windows.Controls.Canvas.SetLeft(rectangle, i * 40);
                System.Windows.Controls.Canvas.SetBottom(rectangle, 20);

                System.Windows.Controls.TextBlock dateTextBlock = new System.Windows.Controls.TextBlock
                {
                    Text = dataPoints[i].Item1.ToString("dd/MM"),
                    Width = 40,
                    TextAlignment = TextAlignment.Center
                };
                barChartCanvas.Children.Add(dateTextBlock);
                System.Windows.Controls.Canvas.SetLeft(dateTextBlock, i * 40 - 5);
                System.Windows.Controls.Canvas.SetBottom(dateTextBlock, 0);

                System.Windows.Controls.TextBlock valueTextBlock = new System.Windows.Controls.TextBlock
                {
                    Text = dataPoints[i].Item2.ToString(),
                    Width = 30,
                    TextAlignment = TextAlignment.Center
                };
                barChartCanvas.Children.Add(valueTextBlock);
                System.Windows.Controls.Canvas.SetLeft(valueTextBlock, i * 40);
                System.Windows.Controls.Canvas.SetBottom(valueTextBlock, barHeight + 25);
            }
        }
    }
}
