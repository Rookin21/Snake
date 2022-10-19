using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Figure
    {
        protected List<Point> pList; // Набор точек для создании линии

        /// <summary>
        /// Вывод на экран каждой точки
        /// </summary>
        public void DrawLine()
        {
            foreach (Point p in pList)
            {
                p.Draw();
            }

        }
    }
}
