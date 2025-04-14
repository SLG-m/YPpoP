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
        }
        public Form2(Ship ship) : this()
        {
            IsEditMode = true;
            EditedShip = ship;
            textBox1.Text = ship.Name;
            textBox2.Text = ship.Vodoizmesc.ToString();
            textBox3.Text = ship.Type;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
            !double.TryParse(textBox2.Text, out double vodoizmesc) ||
            string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Проверьте введенные данные!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
