using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CalculatorWPF.MathMagic
{
    public enum Operation
    {
        Substract = 1,
        Sum,
        Mult,
        Divide,
        Empty
    }
    public class ResultProccesor
    {
        public MainWindow MainWindow { get; set; }
        public string FirstNumber { get; set; }
        public Operation Operation { get; set; } = Operation.Empty;
        public string OperationMathSign { get; set; }
        public string SecondNumber { get; set; }
        public string Result { get; set; }
        public ResultProccesor()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    MainWindow = (window as MainWindow);
                }
            }
        }
        public void AddNumberToTextBlock(string number)
        {
            MainWindow.txtResult.FontSize = 60;
            if (MainWindow.txtResult.Text == "0")
            {
                MainWindow.txtResult.Text = number;
            }
            else
            {
                //Ограничение в 12 символов
                if (MainWindow.txtResult.Text.Length < 12)
                {
                    MainWindow.txtResult.Text += number;
                }
            }

            if (FirstNumber == null || Operation == Operation.Empty)
            {
                FirstNumber = MainWindow.txtResult.Text;
            }
            else
            {
                SecondNumber = MainWindow.txtResult.Text;
            }

        }

        public void PrepareForOperation(string operation)
        {
            //Очищаем поле для второго числа
            if (FirstNumber == null || FirstNumber == "Error")
            {
                FirstNumber = "0";
                Operation = Operation.Empty;
                return;
            }
            if (operation == "CE")
            {
                if (FirstNumber != null)
                {
                    SecondNumber = "0";
                }
                else
                {
                    FirstNumber = "0";
                }
                Operation = Operation.Empty;
                MainWindow.txtResult.Text = "0";
                MainWindow.history.Text = "";
                return;
            }
            if (operation == "C")
            {
                BackToNormalState();
                return;
            }
            if (operation == ".")
            {
                if (!MainWindow.txtResult.Text.Contains('.'))
                {
                    MainWindow.txtResult.Text += ".";
                }
                return;
            }

            if (operation == "<")
            {
                bool isFirstNChosen = MainWindow.txtResult.Text == FirstNumber ? 
                    true: false;

                MainWindow.txtResult.Text = MainWindow.txtResult.Text
                    .Remove(MainWindow.txtResult.Text.Length - 1);

                if (isFirstNChosen)
                {
                    FirstNumber = MainWindow.txtResult.Text;
                }
                else
                {
                    SecondNumber = MainWindow.txtResult.Text;
                }

                if (MainWindow.txtResult.Text.Length == 0)
                {
                    MainWindow.txtResult.Text = "0";
                }
                return;
            }


            MainWindow.txtResult.Text = "0";
            switch (operation)
            {
                case "-":
                    Operation = Operation.Substract;
                    OperationMathSign = "-";
                    break;
                case "+":
                    Operation = Operation.Sum;
                    OperationMathSign = "+";
                    break;
                case "*":
                    Operation = Operation.Mult;
                    OperationMathSign = "*";
                    break;
                case "/":
                    Operation = Operation.Divide;
                    OperationMathSign = "/";
                    break;
                case "=":
                    Result = FindResult();
                    MainWindow.txtResult.Text = Result;
                    // Тут присвоить историю 
                    MainWindow.history.Text += $"{FirstNumber} {OperationMathSign} " +
                        $"{SecondNumber} = {Result}|";
                    // Critically important
                    FirstNumber = Result;
                    BackToNormalState();
                    break;
                default:
                    Operation = Operation.Empty;
                    break;
            }

        }
        private void BackToNormalState()
        {
            FirstNumber = null;
            SecondNumber = null;
            Result = null;
            MainWindow.txtResult.Text = "0";
            Operation = Operation.Empty;
            MainWindow.txtResult.FontSize = 60;
        }

        private string FindResult()
        {
            bool isDouble1 = double.TryParse(FirstNumber, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out double d1);
            bool isDouble2 = double.TryParse(SecondNumber, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out double d2);

            if (isDouble1 && isDouble2)
            {
                return PerformOperationDouble(d1, d2);
            }
            
            MainWindow.txtResult.FontSize = 15;
            return "Error";
        }

        private string PerformOperationDouble(double num1, double num2)
        {
            double res = 0.0;
            if (Operation == Operation.Divide)
            {
                res = (num1 / num2);          
            }
            if (Operation == Operation.Mult)
            {
                res = (num1 * num2);
            }
            if (Operation == Operation.Substract)
            {
                res = (num1 - num2);
            }
            if (Operation == Operation.Sum)
            {
                res = (num1 + num2);
            }
            if (res == 0.0)
            {
                MainWindow.txtResult.FontSize = 15;
                return "Error";
            }

            return res.ToString().Replace(',', '.');
        }


    }
}
