using CalculatorWPF.MathMagic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ResultProccesor _resultProccesor;
        public MainWindow()
        {
            InitializeComponent();
            InitializeResultProccesor();
        }

        private void InitializeResultProccesor()
        {
            _resultProccesor = new ResultProccesor();
        }
        private void ButtonNumber_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string content = btn.Content.ToString();
            _resultProccesor.AddNumberToTextBlock(content);
        }

        private void ButtonOperation_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            _resultProccesor.PrepareForOperation(btn.Content.ToString());
        }

        private void buttonsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string numbers = "0 1 2 3 4 5 6 7 8 9";

            foreach (Button b in buttonsWindow.Children)
            {
                if (b.Content != null)
                {
                    if (numbers.Contains(b.Content.ToString()))
                    {
                        b.Click += new RoutedEventHandler(ButtonNumber_Click);
                    }
                    else
                    {
                        b.Click += new RoutedEventHandler(ButtonOperation_Click);
                    }
                }
            }
        }

        private void lightMode_Click(object sender, RoutedEventArgs e)
        {
            // определяем путь к файлу ресурсов
            var uri = new Uri("light.xaml", UriKind.Relative);
            // загружаем словарь ресурсов
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            // очищаем коллекцию ресурсов приложения
            Application.Current.Resources.Clear();
            // добавляем загруженный словарь ресурсов
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            darkMode.IsEnabled = true;
            lightMode.IsEnabled = false;
        }

        private void darkMode_Click(object sender, RoutedEventArgs e)
        {
            var uri = new Uri("dark.xaml", UriKind.Relative);
            // загружаем словарь ресурсов
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            // очищаем коллекцию ресурсов приложения
            Application.Current.Resources.Clear();
            // добавляем загруженный словарь ресурсов
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            darkMode.IsEnabled = false;
            lightMode.IsEnabled = true;
        }
    }
}
