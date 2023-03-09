using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindAndEscape
{
    internal class GameMechanics
    {
        public static void Mechanics()
        {
            int boardSize = 10;
            int cursorX = 0;
            int cursorY = 0;

            // Rysowanie planszy
            DrawBoard(boardSize, cursorX, cursorY);

            // Pętla główna programu
            while (true)
            {
                // Odczytywanie klawisza wciśniętego przez użytkownika
                ConsoleKeyInfo key = Console.ReadKey(true);

                // Poruszanie się kursora
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (cursorX > 0)
                            cursorX--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (cursorX < boardSize - 1)
                            cursorX++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (cursorY > 0)
                            cursorY--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (cursorY < boardSize - 1)
                            cursorY++;
                        break;
                }

                // Ponowne rysowanie planszy
                Console.Clear();
                DrawBoard(boardSize, cursorX, cursorY);
            }
        }

        static void DrawBoard(int size, int cursorX, int cursorY)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == cursorY && j == cursorX)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                    }
                    Console.Write("+---");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine("+");
                for (int j = 0; j < size; j++)
                {
                    if (i == cursorY && j == cursorX)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                    }
                    Console.Write("|   ");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine("|");
            }
            for (int j = 0; j < size; j++)
            {
                if (cursorY == size && j == cursorX)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                }
                Console.Write("+---");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.WriteLine("+");
        }
    }
}
