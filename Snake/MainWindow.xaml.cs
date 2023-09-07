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

            for (int r = 0; r < rows; r++)
            {
                for(int c = 0; c < cols; c++)
                {
                    // Для каждой позиции создаем новую картинку

                    Image image = new Image
                    {
                        Source = Images.Empty // Изначально будет пустая картинка
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
                }
            }
        }

        private async Task ShowCoundDown()
        {
            for (int i = 3; i >= 1; i--)
            {
                OverlayText.Text = i.ToString();
                await Task.Delay(500);
            }
        }
    }
}
