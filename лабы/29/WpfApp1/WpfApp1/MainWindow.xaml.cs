using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateHexCutPrism();
        }

        private void CreateHexCutPrism()
        {
            var mesh = new MeshGeometry3D();
            double z1 = -0.5;
            double z2 = 0.5;

            // Координаты "усечённого" шестиугольника (основание)
            Point3D[] basePoints = new Point3D[]
            {
                new Point3D(-1, 0, z1),     // 0
                new Point3D(-1, 1, z1),   // 1
                new Point3D(1, 1, z1),      // 2
                new Point3D(1.5, 0.5, z1),  // 3
                new Point3D(1.5, -1, z1),     // 4
                new Point3D(0, -1, z1),     // 5
            };

            // Верхняя грань — та же форма, но выше по Z
            Point3D[] topPoints = new Point3D[6];
            for (int i = 0; i < 6; i++)
                topPoints[i] = new Point3D(basePoints[i].X, basePoints[i].Y, z2);

            // Добавим все вершины
            foreach (var p in basePoints) mesh.Positions.Add(p);
            foreach (var p in topPoints) mesh.Positions.Add(p);

            // Нижняя грань (0–5)
            mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(2); mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(3); mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(4); mesh.TriangleIndices.Add(5);

            // Верхняя грань (6–11)
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

            var material = new DiffuseMaterial(new SolidColorBrush(Colors.SteelBlue));
            var model = new GeometryModel3D(mesh, material);

            // Добавим небольшой поворот
            var axis = new Vector3D(0, 1, 0);
            var rotate = new RotateTransform3D(new AxisAngleRotation3D(axis, 50));
            model.Transform = rotate;

            var visual = new ModelVisual3D { Content = model };
            MyViewport.Children.Add(visual);
        }
    }
}
