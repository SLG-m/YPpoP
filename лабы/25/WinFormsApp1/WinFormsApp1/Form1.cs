using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public class Ship
        {
            public string Name;
            public double Vodoizmesc;
            public string Type;

            public Ship(string name, double vodoizmesc, string type)
            {
                Name = name;
                Vodoizmesc = vodoizmesc;
                Type = type;
            }
        }
        public LinkedList<Ship> LoadShipsFromFile(string filePath)
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
        public Form1()
        {
            InitializeComponent();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = @"ships.bin";

            try
            {
                LinkedList<Ship> ships = LoadShipsFromFile(filePath);

                dataGridView1.Rows.Clear();
                foreach (Ship ship in ships)
                {
                    dataGridView1.Rows.Add(ship.Name, ship.Vodoizmesc, ship.Type);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = @"ships.bin";

            try
            {
                // Создаем список кораблей из DataGridView
                LinkedList<Ship> ships = new LinkedList<Ship>();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Пропускаем пустые строки (если они есть)
                    if (!row.IsNewRow)
                    {
                        string name = row.Cells[0].Value?.ToString() ?? "";
                        double vodoizmesc = 0;
                        double.TryParse(row.Cells[1].Value?.ToString(), out vodoizmesc);
                        string type = row.Cells[2].Value?.ToString() ?? "";

                        ships.AddLast(new Ship(name, vodoizmesc, type));
                    }
                }

                // Преобразуем список в строки для сохранения
                StringBuilder sb = new StringBuilder();
                foreach (Ship ship in ships)
                {
                    sb.AppendLine($"{ship.Name}|{ship.Vodoizmesc}|{ship.Type}");
                }

                // Сохраняем в файл
                File.WriteAllBytes(filePath, Encoding.UTF8.GetBytes(sb.ToString()));

                MessageBox.Show("Данные успешно сохранены!", "Сохранение",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            if (form2.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.Rows.Add(form2.EditedShip.Name,
                                      form2.EditedShip.Vodoizmesc,
                                      form2.EditedShip.Type);
            }
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для редактирования!", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            Ship shipToEdit = new Ship(
                selectedRow.Cells[0].Value.ToString(),
                Convert.ToDouble(selectedRow.Cells[1].Value),
                selectedRow.Cells[2].Value.ToString()
                );

            Form2 form2 = new Form2(shipToEdit);
            if (form2.ShowDialog() == DialogResult.OK)
            {
                selectedRow.Cells[0].Value = form2.EditedShip.Name;
                selectedRow.Cells[1].Value = form2.EditedShip.Vodoizmesc;
                selectedRow.Cells[2].Value = form2.EditedShip.Type;
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для удаления!", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Удалить выбранную запись?", "Подтверждение",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string programInfo = @"Практическая работа №25

Программа предназначена для учета и управления данными о кораблях:
- Добавление новых записей
- Редактирование существующих данных
- Удаление записей
- Сохранение и загрузка данных

Разработчик: Булах Алексей Александровичь
Группа: П32
Вариант: 4";

            MessageBox.Show(programInfo,
                           "О программе",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Information);
        }
    }
}
