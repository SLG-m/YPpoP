using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string filePath = @"E:\Учёба\УП по программированию\лабы\24\WindowsFormsApp1\WindowsFormsApp1";
        private void button1_Click(object sender, EventArgs e)
        {
            // Проверка, какой флажок выбран, и добавление значения в ListBox
            if (radioButton1.Checked && comboBox1.SelectedItem != null)
            {
                listBox1.Items.Add(comboBox1.SelectedItem.ToString());
            }
            else if (radioButton2.Checked && comboBox2.SelectedItem != null)
            {
                listBox1.Items.Add(comboBox2.SelectedItem.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Полные пути к файлам list1.txt и list2.txt
            string file1 = Path.Combine(filePath, "list1.txt");
            string file2 = Path.Combine(filePath, "list2.txt");

            // Загрузка данных из файлов в ComboBox
            if (File.Exists(file1))
            {
                comboBox1.Items.AddRange(File.ReadAllLines(file1));
            }
            else
            {
                MessageBox.Show($"Файл {file1} не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (File.Exists(file2))
            {
                comboBox2.Items.AddRange(File.ReadAllLines(file2));
            }
            else
            {
                MessageBox.Show($"Файл {file2} не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            
            // Удаление выбранного элемента из ListBox при двойном клике
            if (listBox1.SelectedItem != null)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }
    }
}
