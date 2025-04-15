using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using Microsoft.Win32;

namespace WpfShipApp
{
    public partial class MainWindow : Window
    {
        public class Ship
        {
            public string Name { get; set; }
            public double Vodoizmesc { get; set; }
            public string Type { get; set; }

            public Ship(string name, double vodoizmesc, string type)
            {
                Name = name;
                Vodoizmesc = vodoizmesc;
                Type = type;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private LinkedList<Ship> LoadShipsFromFile(string filePath)
        {
            LinkedList<Ship> ships = new LinkedList<Ship>();

            byte[] fileBytes = File.ReadAllBytes(filePath);
            string fileContent = Encoding.UTF8.GetString(fileBytes);
            string[] lines = fileContent.Split(
                new[] { '\n', '\r' },
                StringSplitOptions.RemoveEmptyEntries
            );

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 3 && double.TryParse(parts[1], out double vodoizmesc))
                {
                    ships.AddLast(new Ship(parts[0], vodoizmesc, parts[2]));
                }
            }

            return ships;
        }

        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "ships.bin";

            try
            {
                LinkedList<Ship> ships = LoadShipsFromFile(filePath);
                ShipsDataGrid.ItemsSource = ships;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "ships.bin";

            try
            {
                LinkedList<Ship> ships = new LinkedList<Ship>();
                foreach (Ship ship in ShipsDataGrid.Items)
                {
                    ships.AddLast(ship);
                }

                StringBuilder sb = new StringBuilder();
                foreach (Ship ship in ships)
                {
                    sb.AppendLine($"{ship.Name}|{ship.Vodoizmesc}|{ship.Type}");
                }

                File.WriteAllBytes(filePath, Encoding.UTF8.GetBytes(sb.ToString()));

                MessageBox.Show("Данные успешно сохранены!", "Сохранение",
                               MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                               MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AddMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ShipEditWindow editWindow = new ShipEditWindow();
            if (editWindow.ShowDialog() == true)
            {
                var ships = ShipsDataGrid.ItemsSource as LinkedList<Ship> ?? new LinkedList<Ship>();
                ships.AddLast(editWindow.EditedShip);
                ShipsDataGrid.ItemsSource = null;
                ShipsDataGrid.ItemsSource = ships;
            }
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ShipsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите строку для редактирования!", "Ошибка",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Ship selectedShip = (Ship)ShipsDataGrid.SelectedItem;
            ShipEditWindow editWindow = new ShipEditWindow(selectedShip);
            if (editWindow.ShowDialog() == true)
            {
                selectedShip.Name = editWindow.EditedShip.Name;
                selectedShip.Vodoizmesc = editWindow.EditedShip.Vodoizmesc;
                selectedShip.Type = editWindow.EditedShip.Type;

                ShipsDataGrid.Items.Refresh();
            }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ShipsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите строку для удаления!", "Ошибка",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show("Удалить выбранную запись?", "Подтверждение",
                               MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var ships = ShipsDataGrid.ItemsSource as LinkedList<Ship>;
                ships.Remove((Ship)ShipsDataGrid.SelectedItem);
                ShipsDataGrid.ItemsSource = null;
                ShipsDataGrid.ItemsSource = ships;
            }
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            string programInfo = @"Практическая работа №25

Программа предназначена для учета и управления данными о кораблях:
- Добавление новых записей
- Редактирование существующих данных
- Удаление записей
- Сохранение и загрузка данных

Разработчик: Булах Алексей Александрович
Группа: П32
Вариант: 4";

            MessageBox.Show(programInfo,
                           "О программе",
                           MessageBoxButton.OK,
                           MessageBoxImage.Information);
        }
    }
}