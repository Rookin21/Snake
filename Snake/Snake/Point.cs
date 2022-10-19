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

        /// <summary>
        /// Вывод символа в указанных координатах
        /// </summary>
        public void Draw()
        {
            Console.SetCursorPosition(x, y); // Установка курсора по заданным координатам
            Console.WriteLine(sym); // Вывод символа в указанной точке
        }
    }
}
