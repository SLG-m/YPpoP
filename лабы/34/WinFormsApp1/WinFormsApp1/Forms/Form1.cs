//using Azure.Messaging;
using DeviceFileLoggerWinForms.Models;
using DeviceFileLoggerWinForms.Data;
using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Forms;

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
            dataGridView1.DataSource = db.Files
                .OrderBy(m => m.CreationDate)
                .Select(f => new
                {
                    f.Id,
                    f.FileName,
                    f.FileSize,
                    CreationDate = f.CreationDate.ToString("dd.MM.yyyy"), // Форматируем дату
                    CreationTime = f.CreationTime.ToString(@"hh\:mm")     // Форматируем время
                })
                .ToList();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var form = new Form2();
            if (form.ShowDialog() == DialogResult.OK)
                LoadFiles();
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {

        }
        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
