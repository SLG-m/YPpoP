using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using Microsoft.Win32;
using System.IO;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;

namespace WpfInkCanvasExample
{
    public partial class MainWindow : Window
    {
        private Stack<StrokeCollection> _undoStack = new Stack<StrokeCollection>();
        private Stack<StrokeCollection> _redoStack = new Stack<StrokeCollection>();
        private bool _isProcessingUndoRedo = false;
        private const int MaxUndoSteps = 100;

        public MainWindow()
        {
            InitializeComponent();

            // Настройка по умолчанию
            MyInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            inkRadio.IsChecked = true;

            // Инициализация цветов
            colorComboBox.SelectionChanged += ColorComboBox_SelectionChanged;
            MyInkCanvas.DefaultDrawingAttributes = new DrawingAttributes()
            {
                Color = Colors.Black
            };

            // Сохраняем начальное состояние
            SaveState();

            // Подписка на события
            MyInkCanvas.Strokes.StrokesChanged += Strokes_StrokesChanged;
            PreviewKeyDown += MainWindow_PreviewKeyDown;
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Ctrl+Z - Undo
            if (e.Key == Key.Z && Keyboard.Modifiers == ModifierKeys.Control &&
                Keyboard.Modifiers != ModifierKeys.Shift)
            {
                Undo();
                e.Handled = true;
            }
            // Ctrl+Shift+Z - Redo
            else if (e.Key == Key.Z && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control &&
                     (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                Redo();
                e.Handled = true;
            }
        }

        private void Strokes_StrokesChanged(object sender, StrokeCollectionChangedEventArgs e)
        {
            if (!_isProcessingUndoRedo)
            {
                SaveState();
                _redoStack.Clear();
            }
        }

        private void SaveState()
        {
            _undoStack.Push(new StrokeCollection(MyInkCanvas.Strokes));

            // Ограничиваем размер стека
            if (_undoStack.Count > MaxUndoSteps)
            {
                var newStack = new Stack<StrokeCollection>();
                foreach (var item in _undoStack.Reverse().Take(MaxUndoSteps))
                {
                    newStack.Push(item);
                }
                _undoStack = newStack;
            }
        }

        private void Undo()
        {
            if (_undoStack.Count > 1)
            {
                _isProcessingUndoRedo = true;
                _redoStack.Push(_undoStack.Pop());
                MyInkCanvas.Strokes = new StrokeCollection(_undoStack.Peek());
                _isProcessingUndoRedo = false;
            }
        }

        private void Redo()
        {
            if (_redoStack.Count > 0)
            {
                _isProcessingUndoRedo = true;
                var state = _redoStack.Pop();
                _undoStack.Push(new StrokeCollection(state));
                MyInkCanvas.Strokes = new StrokeCollection(state);
                _isProcessingUndoRedo = false;
            }
        }

        private void RadioButtonClicked(object sender, RoutedEventArgs e)
        {
            switch ((sender as RadioButton)?.Content.ToString())
            {
                case "Ink Mode":
                    MyInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "Erase Mode":
                    MyInkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                    break;
                case "Select Mode":
                    MyInkCanvas.EditingMode = InkCanvasEditingMode.Select;
                    break;
            }
        }

        private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedColor = (colorComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            var da = new DrawingAttributes();

            switch (selectedColor)
            {
                case "Red": da.Color = Colors.Red; break;
                case "Green": da.Color = Colors.Green; break;
                case "Blue": da.Color = Colors.Blue; break;
                default: da.Color = Colors.Red; break;
            }

            MyInkCanvas.DefaultDrawingAttributes = da;
        }

        private void SaveData_Click(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog()
            {
                Filter = "Ink Serialized Format (*.isf)|*.isf|All files (*.*)|*.*",
                DefaultExt = ".isf"
            };

            if (saveDialog.ShowDialog() == true)
            {
                using (var fs = new FileStream(saveDialog.FileName, FileMode.Create))
                {
                    MyInkCanvas.Strokes.Save(fs);
                }
            }
        }

        private void LoadData_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog()
            {
                Filter = "Ink Serialized Format (*.isf)|*.isf|All files (*.*)|*.*",
                DefaultExt = ".isf"
            };

            if (openDialog.ShowDialog() == true)
            {
                using (var fs = new FileStream(openDialog.FileName, FileMode.Open))
                {
                    MyInkCanvas.Strokes = new StrokeCollection(fs);
                    SaveState(); // Сохраняем загруженное состояние
                }
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            MyInkCanvas.Strokes.Clear();
            SaveState(); // Сохраняем состояние после очистки
        }

        private void Undo_Click(object sender, RoutedEventArgs e) => Undo();
        private void Redo_Click(object sender, RoutedEventArgs e) => Redo();
    }
}