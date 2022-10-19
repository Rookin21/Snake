using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    /// <summary>
    /// Создание рамки
    /// </summary>
    internal class HorizontalLine : Figure
    {
        /// <summary>
        /// Создание горизонтальной линии
        /// </summary>
        /// <param name="xLeft">абцисса левого конца</param>
        /// <param name="xRight">абцисса правого конца</param>
        /// <param name="y">ордината</param>
        /// <param name="sym">символ</param>
        public HorizontalLine(int xLeft, int xRight, int y, char sym)
        {
            pList = new List<Point>();  

            for (int x = xLeft; x <= xRight; x++) // Значение x будет получать значение от левого к правому иксу
            {
                Point p = new Point(x, y, sym);
                pList.Add(p);  // Добавление координат в список точек
            }

        }      
    }
}
