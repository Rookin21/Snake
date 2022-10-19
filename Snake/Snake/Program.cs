using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Program
    {
        static void Main()
        {
            Console.SetBufferSize(80, 26);

            // Отрисовка рамки
            HorizontalLine upLine = new HorizontalLine(0, 78, 0, '-');
            HorizontalLine downLine = new HorizontalLine(0, 78, 24, '_');
            VerticaleLine leftLine = new VerticaleLine(0, 24, 0, '|');
            VerticaleLine rightLine = new VerticaleLine(0, 24, 78, '|');

            upLine.DrawLine();
            downLine.DrawLine();
            leftLine.DrawLine();    
            rightLine.DrawLine();

            Console.ReadKey(true);

        }


    }
}
