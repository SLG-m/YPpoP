using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void filesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Загрузка данных при запуске формы
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Сохранение данных по нажатию кнопки
            SaveData();
        }

        private void LoadData()
        {
            try
            {
                // Загрузка данных из таблицы Files
                this.filesTableAdapter.Fill(this.database1DataSet.Files);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveData()
        {
            try
            {
                // Валидация данных
                this.Validate();
                // Завершение редактирования
                this.filesBindingSource.EndEdit();
                // Обновление всех таблиц в наборе данных
                this.tableAdapterManager.UpdateAll(this.database1DataSet);

                MessageBox.Show("Данные успешно сохранены!", "Успех",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Добавим метод для добавления новой записи
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                // Добавляем новую строку
                this.filesBindingSource.AddNew();

                // Можно установить значения по умолчанию для новых полей
                // Например, текущую дату и время
                database1DataSet.Files.Last().CreationDate = DateTime.Now.Date;
                database1DataSet.Files.Last().CreationTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении записи: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Добавим метод для удаления записи
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить эту запись?", "Подтверждение",
                              MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    this.filesBindingSource.RemoveCurrent();
                    SaveData(); // Сохраняем изменения
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении записи: {ex.Message}", "Ошибка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
