using System;
using System.Collections.Generic;

namespace Snake
{
    public class Position
    {
        public int Row { get; }
        public int Column { get; }

        /// <summary>
        /// Конструктор для приема параметров позиции
        /// </summary>
        /// <param name="row">Строка</param>
        /// <param name="column">Столбец</param>
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }
        /// <summary>
        /// Значение после одного шага 
        /// </summary>
        /// <returns></returns>
        public Position Translate (Direction dir)
        {
            return new Position(Row + dir.RowOffset, Column + dir.ColumnOffset); // Текущая позиция + отступ
        }

        /// <summary>
        /// Проверка равенства ссылок 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is Position position &&
                   Row == position.Row &&
                   Column == position.Column;
        }

        /// <summary>
        /// Получение уникального хэш кода
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }

        public static bool operator ==(Position left, Position right)
        {
            return EqualityComparer<Position>.Default.Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }
    }
}
