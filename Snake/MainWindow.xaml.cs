using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Распеределение картинок к соответствующим ячейкам
        private readonly Dictionary<GridValue, ImageSource> gridValToImage = new()
        {
            { GridValue.Empty, Images.Empty }, // Если пустая ячейка, то картинка "Empty" 
            { GridValue.Snake, Images.Body }, // Если ячейка со змеей, то картинка "Body" 
            { GridValue.Food, Images.Food }, // Если ячейка со едой, то картинка "Food" 
        };

        /// <summary>
        /// Повороты головы при движении в определенном направлении
        /// </summary>
        private readonly Dictionary<Direction, int> dirToRotation = new()
        {
            { Direction.Up, 0 },
            { Direction.Right, 90 },
            { Direction.Down, 180 },
            { Direction.Left, 270 }
        };

        private readonly int rows = 15, cols = 15; // количество рядов и столбцов
        private readonly Image[,] gridImages; // Массив для вывода картинок
        private GameState gameState; // Вызов класса GameState
        private bool gameRunning; // Флаг начала игры

        public MainWindow()
        {
            InitializeComponent();
            gridImages = SetupGrid();
            gameState = new GameState(rows, cols);
        }

        private async Task RunGame()
        {
            Draw();
            await ShowCoundDown();
            Overlay.Visibility = Visibility.Hidden; // Скрываем текст после запуска игры
            await GameLoop();
            await ShowGameOver();
            gameState = new GameState(rows, cols);
        }

        /// <summary>
        /// Метод для однократного нажатия кнопки в начале игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Overlay.Visibility == Visibility.Visible) // Если надпись видима, то передаем событие
            {
                e.Handled = true;
            }

            if (!gameRunning)
            {
                gameRunning = true;
                await RunGame();
                gameRunning = false;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Left:
                    gameState.ChangeDiretion(Direction.Left); 
                    break;
                case Key.Right:
                    gameState.ChangeDiretion(Direction.Right);
                    break;
                case Key.Up:
                    gameState.ChangeDiretion(Direction.Up);
                    break;
                case Key.Down:
                    gameState.ChangeDiretion(Direction.Down);
                    break;
            }
        }

        /// <summary>
        /// Реализация движения змейки
        /// </summary>
        /// <returns></returns>
        private async Task GameLoop()
        {
            while (!gameState.GameOver) // Пока не закончится игра
            {
                await Task.Delay(100); // Для скорости игры
                gameState.Move();
                Draw();
            }
        }

        /// <summary>
        /// Вызов необходимых картинок на игровое поле
        /// </summary>
        /// <returns></returns>
        private Image[,] SetupGrid()
        {
            Image[,] images = new Image[rows, cols];
            GameGrid.Rows = rows;
            GameGrid.Columns = cols;
            GameGrid.Width = GameGrid.Height * (cols / (double)rows);

            for (int r = 0; r < rows; r++)
            {
                for(int c = 0; c < cols; c++)
                {
                    // Для каждой позиции создаем новую картинку

                    Image image = new Image
                    {
                        Source = Images.Empty, // Изначально будет пустая картинка
                        RenderTransformOrigin = new Point(0.5, 0.5) // Центрирование картинок для избавления от бага со смещением
                    };

                    images[r, c] = image; // Складываем эту картинку в массив
                    GameGrid.Children.Add(image); // Добавляем в потомка
                }
            }

            return images;
        }

        private void Draw()
        {
            DrawGrid();
            DrawSnakeHead();
            ScoreText.Text = $"SCORE {gameState.Score}";
        }

        private void DrawGrid()
        {
            // Прохождение по все ячейкам
            for (int r = 0; r < rows; r++) 
            {
                for (int c = 0; c < cols; c++)
                { 
                    GridValue gridVal = gameState.Grid[r, c]; // Получаем значение ячейки на конкретной позиции
                    gridImages[r, c].Source = gridValToImage[gridVal]; // Вызов соответствующей картинки из словаря
                    gridImages[r, c].RenderTransform = Transform.Identity; // Идентифицирует позицию с головой для поворота только этой ячейки
                }
            }
        }

        /// <summary>
        /// Добавление головы змейки
        /// </summary>
        private void DrawSnakeHead()
        {
            Position headPos = gameState.HeadPosition(); // Позиция головы
            Image image = gridImages[headPos.Row, headPos.Column]; // Ориентация головы по позиции
            image.Source = Images.Head; // Перекладка источника и картинки

            int rotation = dirToRotation[gameState.Dir]; // Получение из словаря угла поворота головы
            image.RenderTransform = new RotateTransform(rotation); // Поворот картинки на нужный угол
        }

        private async Task DrawDeadSnake()
        {
            List<Position> positions = new List<Position>(gameState.SnakePositions()); // Список, который будет содержать все позиции змеи

            for (int i = 0; i < positions.Count; i++) // Сколько итераций - столько и позиций
            {
                Position pos = positions[i];
                ImageSource source = (i == 0) ? Images.DeadHead : Images.DeadBody; // Если 0, то картинки мертвой головы и тела
                gridImages[pos.Row, pos.Column].Source = source; // Конкретная картинка - под конкретную позицию
                await Task.Delay(50);
            }
        }

        /// <summary>
        /// Обратный отсчет
        /// </summary>
        /// <returns></returns>
        private async Task ShowCoundDown()
        {
            for (int i = 3; i >= 1; i--)
            {
                OverlayText.Text = i.ToString();
                await Task.Delay(500);
            }
        }

        /// <summary>
        /// Конец игры
        /// </summary>
        /// <returns></returns>
        private async Task ShowGameOver()
        {
            await DrawDeadSnake();
            await Task.Delay(1000);
            Overlay.Visibility = Visibility.Visible;
            OverlayText.Text = "PRESS ANY KEY TO START";
        }
    }
}
