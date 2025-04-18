using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private TranslateTransform3D _translation;
        private TranslateTransform3D _textTranslation;
        private AxisAngleRotation3D _rotationX;
        private AxisAngleRotation3D _rotationY;
        private AxisAngleRotation3D _rotationZ;
        private RotateTransform3D _rotateX;
        private RotateTransform3D _rotateY;
        private RotateTransform3D _rotateZ;
        private double _time;

        public MainWindow()
        {
            InitializeComponent();
            CreateHexCutPrism();
            CompositionTarget.Rendering += Animate;
        }

        private void CreateHexCutPrism()
        {
            var mesh = new MeshGeometry3D();
            double z1 = -0.5;
            double z2 = 0.5;

            Point3D[] basePoints = new Point3D[]
            {
                new Point3D(-1, 0, z1),
                new Point3D(-1, 1, z1),
                new Point3D(1, 1, z1),
                new Point3D(1.5, 0.5, z1),
                new Point3D(1.5, -1, z1),
                new Point3D(0, -1, z1),
            };

            Point3D[] topPoints = new Point3D[6];
            for (int i = 0; i < 6; i++)
                topPoints[i] = new Point3D(basePoints[i].X, basePoints[i].Y, z2);

            foreach (var p in basePoints) mesh.Positions.Add(p);
            foreach (var p in topPoints) mesh.Positions.Add(p);

            // Нижняя грань
            mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(2); mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(3); mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(4); mesh.TriangleIndices.Add(5);

            // Верхняя грань
            mesh.TriangleIndices.Add(6); mesh.TriangleIndices.Add(8); mesh.TriangleIndices.Add(7);
            mesh.TriangleIndices.Add(6); mesh.TriangleIndices.Add(9); mesh.TriangleIndices.Add(8);
            mesh.TriangleIndices.Add(6); mesh.TriangleIndices.Add(10); mesh.TriangleIndices.Add(9);
            mesh.TriangleIndices.Add(6); mesh.TriangleIndices.Add(11); mesh.TriangleIndices.Add(10);

            // Боковые стороны
            for (int i = 0; i < 6; i++)
            {
                int next = (i + 1) % 6;
                int lower1 = i;
                int lower2 = next;
                int upper1 = i + 6;
                int upper2 = next + 6;

                mesh.TriangleIndices.Add(lower1); mesh.TriangleIndices.Add(upper1); mesh.TriangleIndices.Add(upper2);
                mesh.TriangleIndices.Add(lower1); mesh.TriangleIndices.Add(upper2); mesh.TriangleIndices.Add(lower2);
            }

            // Градиентный материал
            var gradientBrush = new LinearGradientBrush();
            gradientBrush.GradientStops.Add(new GradientStop(Colors.CadetBlue, 0));
            gradientBrush.GradientStops.Add(new GradientStop(Colors.MediumPurple, 1));
            var material = new DiffuseMaterial(gradientBrush);

            var model = new GeometryModel3D(mesh, material);

            // Трансформации: вращение по трем осям + смещение
            _rotationX = new AxisAngleRotation3D(new Vector3D(1, 0, 0), 0);
            _rotationY = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);
            _rotationZ = new AxisAngleRotation3D(new Vector3D(0, 0, 1), 0);

            _rotateX = new RotateTransform3D(_rotationX);
            _rotateY = new RotateTransform3D(_rotationY);
            _rotateZ = new RotateTransform3D(_rotationZ);

            _translation = new TranslateTransform3D(-1, 0, 0); // начальное положение

            var transformGroup = new Transform3DGroup();
            transformGroup.Children.Add(_rotateX);
            transformGroup.Children.Add(_rotateY);
            transformGroup.Children.Add(_rotateZ);
            transformGroup.Children.Add(_translation);

            model.Transform = transformGroup;

            var visual = new ModelVisual3D { Content = model };
            MyViewport.Children.Add(visual);

            CreateText();
        }

        private void CreateText()
        {
            var text = new TextBlock
            {
                Text = "Bylackh",
                FontSize = 20,
                Foreground = Brushes.Red
            };

            var textMaterial = new DiffuseMaterial(new VisualBrush(text));
            var textGeom = new GeometryModel3D
            {
                Geometry = MeshGenerator.CreateTexturedPlane(),
                Material = textMaterial,
                BackMaterial = textMaterial
            };

            _textTranslation = new TranslateTransform3D(1, 0, 0); // рядом с фигурой
            textGeom.Transform = _textTranslation;

            var visual = new ModelVisual3D { Content = textGeom };
            MyViewport.Children.Add(visual);
        }

        private void Animate(object sender, EventArgs e)
        {
            _time += 0.03;

            // Анимация смещения
            double offset = Math.Sin(_time) * 2.0;
            _translation.OffsetX = -1 + offset;
            _textTranslation.OffsetX = 1 + offset;

            // Анимация вращения по всем осям
            _rotationX.Angle = (_rotationX.Angle + 1) % 360;
            _rotationY.Angle = (_rotationY.Angle + 1.5) % 360; // немного разная скорость для Y
            _rotationZ.Angle = (_rotationZ.Angle + 0.8) % 360; // и для Z
        }
    }

    // Вспомогательный класс для создания плоскости для текста
    public static class MeshGenerator
    {
        public static MeshGeometry3D CreateTexturedPlane()
        {
            var mesh = new MeshGeometry3D();
            mesh.Positions = new Point3DCollection(new[]
            {
                new Point3D(0, 0, 0),
                new Point3D(0, 1, 0),
                new Point3D(2, 1, 0),
                new Point3D(2, 0, 0),
            });
            mesh.TriangleIndices = new Int32Collection { 0, 1, 2, 0, 2, 3 };
            mesh.TextureCoordinates = new PointCollection {
                new Point(0, 1), new Point(0, 0),
                new Point(1, 0), new Point(1, 1)
            };
            return mesh;
        }
    }
}