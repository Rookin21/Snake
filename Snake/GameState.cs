﻿using System;
using System.Collections.Generic;

namespace Snake
{
    public class GameState
    {
        public int Rows { get; } // Строки
        public int Columns { get; } // Столбцы
        public GridValue[,] Grid { get; } // Размер игрового поля
        public Direction Dir { get; } // Направление куда пойдет змея
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

        private void AddFood()
        { 
            List<Position> empty = new List<Position>(EmptyPositions()); // Лист с пустыми ячейками

            if (empty.Count == 0) // Если нет пустых ячеек, то конец игры
            {
                return;
            }

            Position pos = empty[_Random.Next(empty.Count)]; // Перекладывание пустой ячейки в случайное значение
            Grid[pos.Row, pos.Column] = GridValue.Food; // Установка положение еды в соответствующий массив
        }
    }
}
