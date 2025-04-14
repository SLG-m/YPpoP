using System;
using System.IO;
using System.Windows;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private string filePath = @"E:\Учёба\УП по программированию\лабы\26\WpfApp1\WpfApp1";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // Проверка, какой флажок выбран, и добавление значения в ListBox
            if (radioButton1.IsChecked == true && comboBox1.SelectedItem != null)
            {
                listBox1.Items.Add(comboBox1.SelectedItem.ToString());
            }
            else if (radioButton2.IsChecked == true && comboBox2.SelectedItem != null)
            {
                listBox1.Items.Add(comboBox2.SelectedItem.ToString());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Полные пути к файлам list1.txt и list2.txt
            string file1 = Path.Combine(filePath, "list1.txt");
            string file2 = Path.Combine(filePath, "list2.txt");

            // Загрузка данных из файлов в ComboBox
            try
            {
                if (File.Exists(file1))
                {
                    comboBox1.ItemsSource = File.ReadAllLines(file1);
                }
                else
                {
                    MessageBox.Show($"Файл {file1} не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (File.Exists(file2))
                {
                    comboBox2.ItemsSource = File.ReadAllLines(file2);
                }
                else
                {
                    MessageBox.Show($"Файл {file2} не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке файлов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void listBox1_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Удаление выбранного элемента из ListBox при двойном клике
            if (listBox1.SelectedItem != null)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }
    }
}