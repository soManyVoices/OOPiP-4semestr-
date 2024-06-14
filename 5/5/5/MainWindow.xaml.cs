using CalcLib;
using System.Windows;

namespace CalculatorApp
{
    public partial class MainWindow : Window
    {
        private Calculator calculator;

        public MainWindow()
        {
            InitializeComponent();
            calculator = new Calculator();
            calculator.CalculationPerformed += Calculator_CalculationPerformed;
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            string input = InputTextBox.Text;
            calculator.PerformCalculationsFromString(input);
        }

        private void Calculator_CalculationPerformed(object sender, double result)
        {
            ResultTextBlock.Text = $"Result: {result}";
        }
    }

    
}
