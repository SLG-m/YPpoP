//using Azure.Messaging;
using DeviceFileLoggerWinForms.Models;
using DeviceFileLoggerWinForms.Data;
using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Forms;
using Azure.Messaging;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadFiles();
        }
        private void LoadFiles()
        {
            using var db = new FilesContext();
            // Сохраняем оригинальные объекты File в Tag каждой строки
            var files = db.Files.OrderBy(m => m.CreationDate).ToList();

            dataGridView1.DataSource = files.Select(f => new
            {
                f.Id,
                f.FileName,
                f.FileSize,
                CreationDate = f.CreationDate.ToString("dd.MM.yyyy"),
                CreationTime = f.CreationTime.ToString(@"hh\:mm"),
                OriginalFile = f // Добавляем оригинальный объект в анонимный тип
            }).ToList();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var form = new Form2();
            if (form.ShowDialog() == DialogResult.OK)
                LoadFiles();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow?.DataBoundItem != null)
            {
                // Получаем оригинальный объект File из свойства OriginalFile
                dynamic item = dataGridView1.CurrentRow.DataBoundItem;
                var fileToDelete = item.OriginalFile as DeviceFileLoggerWinForms.Models.File;

                var confirm = MessageBox.Show("Удалить файл?", "Подтверждение", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    using var db = new FilesContext();
                    db.Files.Remove(fileToDelete);
                    db.SaveChanges();
                    LoadFiles();
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow?.DataBoundItem != null)
            {
                // Получаем оригинальный объект File из свойства OriginalFile
                dynamic item = dataGridView1.CurrentRow.DataBoundItem;
                var fileToEdit = item.OriginalFile as DeviceFileLoggerWinForms.Models.File;

                var form = new Form2(fileToEdit);
                if (form.ShowDialog() == DialogResult.OK)
                    LoadFiles();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            LoadFiles();
        }
    }
}
