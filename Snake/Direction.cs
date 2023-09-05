using System;
using System.Collections.Generic;

namespace Snake
{
    public class Direction
    {
        public readonly static Direction Left = new Direction(0, -1); // Смещение влево
        public readonly static Direction Right = new Direction(0, 1); // Смещение вправо
        public readonly static Direction Up = new Direction(-1, 0); // Смещение вверх
        public readonly static Direction Down = new Direction(1, 0); // Смещение вниз

        public int RowOffset { get; } 
        public int ColumnOffset { get; }

        /// <summary>
        /// Коструктор для приема параметров направления
        /// </summary>
        /// <param name="rowOffset">Отсуп строки</param>
        /// <param name="columnOffset">Отступ столбца</param>
        private Direction(int rowOffset, int columnOffset)
        {
            RowOffset = rowOffset;
            ColumnOffset = columnOffset;
        }
        /// <summary>
        /// Противоположное направление
        /// </summary>
        /// <returns></returns>
        public Direction Opposite()
        {
            return new Direction(-RowOffset, -ColumnOffset);
        }

        public override bool Equals(object obj)
        {
            return obj is Direction direction &&
                   RowOffset == direction.RowOffset &&
                   ColumnOffset == direction.ColumnOffset;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RowOffset, ColumnOffset);
        }

        public static bool operator ==(Direction left, Direction right)
        {
            return EqualityComparer<Direction>.Default.Equals(left, right);
        }

        public static bool operator !=(Direction left, Direction right)
        {
            return !(left == right);
        }
    }
}
