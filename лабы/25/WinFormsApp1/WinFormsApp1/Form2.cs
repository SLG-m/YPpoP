using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WinFormsApp1.Form1;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        public Ship EditedShip { get; private set; }
        public bool IsEditMode { get; private set; }

        public Form2()
        {
            InitializeComponent();
            SetupTextBoxConstraints();
        }

        public Form2(Ship ship) : this()
        {
            IsEditMode = true;
            EditedShip = ship;
            textBox1.Text = ship.Name;
            textBox2.Text = ship.Vodoizmesc.ToString();
            textBox3.Text = ship.Type;
        }

        private void SetupTextBoxConstraints()
        {
            // Установка максимальной длины для всех текстбоксов (50 символов)
            textBox1.MaxLength = 50;
            textBox2.MaxLength = 50;
            textBox3.MaxLength = 50;

            // Разрешаем только цифры и точку во втором текстбоксе
            textBox2.KeyPress += TextBox2_KeyPress;
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем:
            // - цифры (0-9)
            // - точку (десятичный разделитель)
            // - Backspace (удаление)
            // - Control+A (выделить все) и другие управляющие команды

            if (!char.IsControl(e.KeyChar))
            {
                bool isDigit = char.IsDigit(e.KeyChar);
                bool isDot = (e.KeyChar == '.' || e.KeyChar == ',');
                bool alreadyHasDot = textBox2.Text.Contains('.') || textBox2.Text.Contains(',');

                if (!isDigit && (!isDot || alreadyHasDot))
                {
                    e.Handled = true; // Блокируем ввод
                }

                // Заменяем запятую на точку для единообразия
                if (isDot)
                {
                    e.KeyChar = '.';
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка, что водоизмещение - число
            if (!double.TryParse(textBox2.Text, out double vodoizmesc))
            {
                MessageBox.Show("Водоизмещение должно быть числом!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }

            // Проверка на отрицательное значение
            if (vodoizmesc <= 0)
            {
                MessageBox.Show("Водоизмещение должно быть положительным числом!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }

            EditedShip = new Ship(textBox1.Text, vodoizmesc, textBox3.Text);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}