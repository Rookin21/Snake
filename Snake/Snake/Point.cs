using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Point
    {
        public int x; // Координата абцисс
        public int y; // Координата ординат
        public char sym; // Выводимый символ

        /// <summary>
        /// Метод для сохранения веденных значений внутри скобки при вызове класса в Main
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_sym"></param>
        public Point(int _x, int _y, char _sym)
        {
            x = _x;
            y = _y;
            sym = _sym;
        }

        public Point(Point p)
        {
            x = p.x;
            y = p.y;    
            sym = p.sym;
        }

        public void Move(int offset, Direction direction)
        {
            if(direction == Direction.RIGHT)
            {
                x = x + offset;
            }
            else if (direction == Direction.LEFT)
            {
                x = x - offset;
            }
            else if (direction == Direction.UP)
            {
                y = y - offset;
            }
            else if (direction == Direction.DOWN)
            {
                y = y + offset;
            }
        }

        public bool IsHit(Point p)
        {
            return p.x == this.x && p.y == this.y;
        }

        /// <summary>
        /// Вывод символа в указанных координатах
        /// </summary>
        public void Draw()
        {
            Console.SetCursorPosition(x, y); // Установка курсора по заданным координатам
            Console.WriteLine(sym); // Вывод символа в указанной точке
        }

        public void Clear()
        {
            sym = ' ';
            Draw();
        }
    }
}
