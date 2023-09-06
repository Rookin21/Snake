using System;
using System.Collections.Generic;

namespace Snake
{
    public class GameState
    {
        public int Rows { get; } // Строки
        public int Columns { get; } // Столбцы
        public GridValue[,] Grid { get; } // Размер игрового поля
        public Direction Dir { get; private set; } // Направление куда пойдет змея
        public int Score { get; private set; } // Счет
        public bool GameOver {  get; private set; } // Флаг окончания игры

        /// <summary>
        /// Связный список для определения пространства, занимаемое змее. 
        /// Связный список используется по той причине, что требуется удалять последний элемент и добавлять первый. 
        /// Первый элемент голова. Последний хвост.
        /// </summary>
        private readonly LinkedList<Position> _SnakePositions = new LinkedList<Position>();  
        private readonly Random _Random = new Random(); // Требуется для расположения еды

        /// <summary>
        /// Конструктор для приема параметров состояния игры
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public GameState(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Grid = new GridValue[rows, columns];
            Dir = Direction.Right; // При старте игры - змея движется вправо

            AddSnake();
            AddFood();
        }      
        /// <summary>
        /// Добавление змейки на игровое поле
        /// </summary>
        private void AddSnake()
        {
            int r = Rows / 2; // Для добавления змеи в середину строки

            // Цикл для запуск змеи длинной 3 клетки
            for (int c = 1; c <= 3; c++)
            {
                Grid[r, c] = GridValue.Snake; // Начальное положение змеи на поле
                _SnakePositions.AddFirst(new Position(r, c)); // Добавление положения в связный список
            }
        }

        /// <summary>
        /// Определение пустой ячейки
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Position> EmptyPositions()
        {
            for (int r = 0;  r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    if (Grid[r,c] == GridValue.Empty) // Если ячейка пустая
                    {
                        yield return new Position(r, c); // Определяем возвращаемый элемент
                    }
                }
            }
        }

        /// <summary>
        /// Расположение
        /// </summary>
        private void AddFood()
        { 
            List<Position> empty = new List<Position>(EmptyPositions()); // Лист с пустыми ячейками

            if (empty.Count == 0) // Если нет пустых ячеек для еды
            {
                return;
            }

            Position pos = empty[_Random.Next(empty.Count)]; // Перекладывание пустой ячейки в случайное значение
            Grid[pos.Row, pos.Column] = GridValue.Food; // Установка положение еды в соответствующий массив
        }

        /// <summary>
        /// Определение местоположения головы
        /// </summary>
        /// <returns></returns>
        public Position HeadPosition()
        {
            return _SnakePositions.First.Value;
        }

        /// <summary>
        /// Определение местоположения хвоста
        /// </summary>
        /// <returns></returns>
        public Position TailPosition()
        {
            return _SnakePositions.Last.Value;
        }

        /// <summary>
        /// Определение местоположения тела
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Position> SnakePositions()
        {
            return _SnakePositions;
        }

        /// <summary>
        /// Удлинение длины тела змейки, путем добавления новой головы
        /// </summary>
        /// <param name="pos"></param>
        private void AddHead(Position pos)
        {
            _SnakePositions.AddFirst(pos); 
            Grid[pos.Row, pos.Column] = GridValue.Snake;
        }

        /// <summary>
        /// Удаление хвоста
        /// </summary>
        private void RemoveTail()
        {
            Position tail = _SnakePositions.Last.Value; // Определение положение хвоста
            Grid[tail.Row, tail.Column] = GridValue.Empty; // Очищаем эту ячейку
            _SnakePositions.RemoveLast(); // Удаляем из списка
        }

        /// <summary>
        /// Изменение направления
        /// </summary>
        /// <param name="dir"></param>
        public void ChangeDiretion(Direction dir)
        {
            Dir = dir; // Перекладка свойства направления в параметр направления
        }
    }
}
