using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class VerticaleLine : Figure
    {
        /// <summary>
        /// Создание вертикальной линии
        /// </summary>
        /// <param name="yUp">Начало ординаты</param>
        /// <param name="yDown">Конец ординаты</param>
        /// <param name="x">абцисса</param>
        /// <param name="sym">символ</param>
        public VerticaleLine(int yUp, int yDown, int x, char sym)
        {
            pList = new List<Point>();

            for (int y = yUp; y <= yDown; y++) // Значение x будет получать значение от левого к правому иксу
            {
                Point p = new Point(x, y, sym);
                pList.Add(p);  // Добавление координат в список точек
            }

        }
    }
}
