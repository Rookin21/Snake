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

        public MainWindow()
        {
            InitializeComponent();
            gridImages = SetupGrid();
            gameState = new GameState(rows, cols);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Draw();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

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
    }
}
