using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;

namespace CalcLib
{
    public class Calculator
    {
        private readonly Regex operationRegex = new Regex(@"^\s*(-?\d+(\.\d+)?)\s*([-+*/])\s*(-?\d+(\.\d+)?)\s*$");
        private readonly Dictionary<char, Func<double, double, double>> operations;

        public delegate void CalculationPerformedEventHandler(object sender, double result);
        public event CalculationPerformedEventHandler CalculationPerformed;

        public Calculator()
        {
            operations = new Dictionary<char, Func<double, double, double>>
        {
            {'+', (a, b) => a + b},
            {'-', (a, b) => a - b},
            {'*', (a, b) => a * b},
            {'/', (a, b) => a / b}
        };
        }

        public void PerformCalculationsFromString(string input)
        {
            Match match = operationRegex.Match(input);
            if (match.Success)
            {
                double operand1 = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
                double operand2 = double.Parse(match.Groups[4].Value, CultureInfo.InvariantCulture);
                char operationSymbol = match.Groups[3].Value[0];

                if (operations.ContainsKey(operationSymbol))
                {
                    double result = operations[operationSymbol](operand1, operand2);
                    OnCalculationPerformed(result);
                }
                else
                {
                    MessageBox.Show($"Invalid operation: {operationSymbol}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Неправильный формат ввода", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected virtual void OnCalculationPerformed(double result)
        {
            CalculationPerformed?.Invoke(this, result);
        }
    }
}
