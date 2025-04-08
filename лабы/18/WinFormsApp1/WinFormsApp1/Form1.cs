using System.Reflection;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        dynamic ClassLib;
        public Form1()
        {
            InitializeComponent();
            LoadDll();
        }
        private void LoadDll()
        {
            try
            {
                string path = "ClassLibrary1.dll";
                var asm = Assembly.LoadFrom(path);
                var type = asm.GetType("FunctionLibrary.GraphFunction");
                ClassLib = Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки DLL: " + ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out double x))
            {
                try
                {
                    double y = ClassLib.CalculateY(x);
                    textBox2.Text = $"Y = {y:F2}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Введите корректное число!");

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}