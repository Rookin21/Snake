using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Snake
{
    /// <summary>
    /// Контейнер для картинок
    /// </summary>
    public static class Images
    {
        // Переменные для картинок
        public readonly static ImageSource Empty = _LoadImage("Empty.png");
        public readonly static ImageSource Body = _LoadImage("Body.png");
        public readonly static ImageSource Head = _LoadImage("Head.png");
        public readonly static ImageSource Food = _LoadImage("Food.png");
        public readonly static ImageSource DeadBody = _LoadImage("DeadBody.png");
        public readonly static ImageSource DeadHead = _LoadImage("DeadHead.png");

        /// <summary>
        /// Загрузка картинок
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns></returns>
        private static ImageSource _LoadImage(string fileName)
        {
            return new BitmapImage(new Uri($"Assets/{fileName}", UriKind.Relative));
        }
    }
}
