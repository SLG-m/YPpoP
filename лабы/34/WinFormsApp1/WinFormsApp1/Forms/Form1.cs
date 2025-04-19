//using Azure.Messaging;
using DeviceFileLoggerWinForms.Models;
using DeviceFileLoggerWinForms.Data;
using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadMFiles();
        }
        private void LoadMFiles()
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
    }
}
