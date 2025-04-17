using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using Microsoft.Win32;
using System.IO;

namespace WpfInkCanvasExample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Установка режима Ink по умолчанию
            this.MyInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            this.inkRadio.IsChecked = true;

            // Обработчик изменения цвета
            colorComboBox.SelectionChanged += ColorComboBox_SelectionChanged;
        }

        private void RadioButtonClicked(object sender, RoutedEventArgs e)
        {
            switch ((sender as RadioButton)?.Content.ToString())
            {
                case "Ink Mode":
                    this.MyInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "Erase Mode":
                    this.MyInkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                    break;
                case "Select Mode":
                    this.MyInkCanvas.EditingMode = InkCanvasEditingMode.Select;
                    break;
            }
        }

        private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MyInkCanvas == null) return;

            var selectedColor = (colorComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            var drawingAttributes = new DrawingAttributes();

            switch (selectedColor)
            {
                case "Red":
                    drawingAttributes.Color = Colors.Red;
                    break;
                case "Green":
                    drawingAttributes.Color = Colors.Green;
                    break;
                case "Blue":
                    drawingAttributes.Color = Colors.Blue;
                    break;
                default:
                    drawingAttributes.Color = Colors.Black;
                    break;
            }

            MyInkCanvas.DefaultDrawingAttributes = drawingAttributes;
        }

        private void SaveData_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Ink Serialized Format (*.isf)|*.isf|All files (*.*)|*.*",
                DefaultExt = ".isf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    MyInkCanvas.Strokes.Save(fs);
                }
            }
        }

        private void LoadData_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Ink Serialized Format (*.isf)|*.isf|All files (*.*)|*.*",
                DefaultExt = ".isf"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    MyInkCanvas.Strokes = new StrokeCollection(fs);
                }
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            MyInkCanvas.Strokes.Clear();
        }
    }
}