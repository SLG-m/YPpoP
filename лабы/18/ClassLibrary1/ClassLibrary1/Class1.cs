using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionLibrary
{
    public class GraphFunction
    {
        public double CalculateY(double x)
        {
            if (x <= -6)
            {
                // Прямая, продлевающая предыдущий участок. Допустим, наклон -0.5 и точка (-6, 0)
                return -0.5 * (x + 6);
            }
            else if (x > -6 && x <= 0)
            {
                // Линейное убывание от (-6, 0] до [0, -6) — наклон -1
                return -0.5 * (x + 6);
            }
            else if (x > 0 && x <= 3)
            {
                double y = -Math.Sqrt(9 - x * x);
                return y;
            }
            else if (x > 3 && x <= 6)
            {
                // Часть окружности: центр [6, 0), радиус 3
                double underRoot = 9 - Math.Pow(x - 6, 2);
                return underRoot >= 0 ? Math.Sqrt(underRoot) : double.NaN;
            }
            else if (x > 6)
            {
                return 0;
            }
            else
            {
                // За пределами графика
                return double.NaN;
            }
        }
    }
}
