using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;

namespace WpfInkCanvasExample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Установить режим Ink в качестве стандартного
            this.MyInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            this.inkRadio.IsChecked = true;
        }

        private void RadioButtonClicked(object sender, RoutedEventArgs e)
        {
            // В зависимости от того, какая кнопка отправила событие,
            // поместить InkCanvas в нужный режим оперирования
            switch ((sender as RadioButton)?.Content.ToString())
            {
                // Эти строки должны совпадать со значениями свойства Content 
                // каждого элемента RadioButton
                case "Ink Mode!":
                    this.MyInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "Erase Mode!":
                    this.MyInkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                    break;
                case "Select Mode!":
                    this.MyInkCanvas.EditingMode = InkCanvasEditingMode.Select;
                    break;
            }
        }
    }
}