using System;
using System.Windows;
using System.Windows.Input;

namespace WpfShipApp
{
    public partial class ShipEditWindow : Window
    {
        public MainWindow.Ship EditedShip { get; private set; }
        public bool IsEditMode { get; private set; }

        public ShipEditWindow()
        {
            InitializeComponent();
        }

        public ShipEditWindow(MainWindow.Ship ship) : this()
        {
            IsEditMode = true;
            EditedShip = ship;
            NameTextBox.Text = ship.Name;
            VodoizmescTextBox.Text = ship.Vodoizmesc.ToString();
            TypeTextBox.Text = ship.Type;
        }

        private void VodoizmescTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Разрешаем только цифры и точку
            bool isDot = e.Text == "." || e.Text == ",";
            bool alreadyHasDot = VodoizmescTextBox.Text.Contains(".") || VodoizmescTextBox.Text.Contains(",");

            if (!char.IsDigit(e.Text, 0) && !isDot)
            {
                e.Handled = true;
            }
            else if (isDot && alreadyHasDot)
            {
                e.Handled = true;
            }

            // Заменяем запятую на точку
            if (e.Text == ",")
            {
                e.Handled = true;
                VodoizmescTextBox.Text += ".";
                VodoizmescTextBox.CaretIndex = VodoizmescTextBox.Text.Length;
            }
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(VodoizmescTextBox.Text) ||
                string.IsNullOrWhiteSpace(TypeTextBox.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Проверка, что водоизмещение - число
            if (!double.TryParse(VodoizmescTextBox.Text, out double vodoizmesc))
            {
                MessageBox.Show("Водоизмещение должно быть числом!", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                VodoizmescTextBox.Focus();
                return;
            }

            // Проверка на отрицательное значение
            if (vodoizmesc <= 0)
            {
                MessageBox.Show("Водоизмещение должно быть положительным числом!", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                VodoizmescTextBox.Focus();
                return;
            }

            EditedShip = new MainWindow.Ship(NameTextBox.Text, vodoizmesc, TypeTextBox.Text);
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}