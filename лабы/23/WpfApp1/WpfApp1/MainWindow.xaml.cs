using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private Polyline currentLine;
        private Brush currentBrush = Brushes.Black;

        public MainWindow()
        {
            InitializeComponent();

            DrawingCanvas.MouseLeftButtonDown += DrawingCanvas_MouseLeftButtonDown;
            DrawingCanvas.MouseMove += DrawingCanvas_MouseMove;
            DrawingCanvas.MouseRightButtonDown += DrawingCanvas_MouseRightButtonDown;
            DrawingCanvas.MouseLeftButtonUp += DrawingCanvas_MouseLeftButtonUp;
        }

        private void DrawingCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                DrawingCanvas.Children.Clear();
                return;
            }

            currentLine = new Polyline
            {
                Stroke = currentBrush,
                StrokeThickness = 2
            };
            DrawingCanvas.Children.Add(currentLine);

            var point = e.GetPosition(DrawingCanvas);
            currentLine.Points.Add(point);
        }

        private void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && currentLine != null)
            {
                var point = e.GetPosition(DrawingCanvas);
                currentLine.Points.Add(point);
            }
        }

        private void DrawingCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            currentBrush = new SolidColorBrush(Color.FromRgb(
                (byte)Random.Shared.Next(256),
                (byte)Random.Shared.Next(256),
                (byte)Random.Shared.Next(256)));
        }

        private void DrawingCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            currentLine = null;
        }
    }
}