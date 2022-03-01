using System;
namespace SpaceInvader
{
    public class Box
    {
        public int Size_x;
        public int Size_y;

        public Box(int size_x, int size_y)
        {
            Size_x = size_x;
            Size_y = size_y;
        }

        public void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, 0);
            for (int x = 0; x < Size_x; x++)
            {
                Console.SetCursorPosition(x, 0);
                Console.Write("-");
                Console.SetCursorPosition(x, Size_y);
                Console.Write("-");

            }
            for (int y = 0; y < Size_y; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.Write("|");
                Console.SetCursorPosition(Size_x, y);
                Console.Write("|");
            }
        }
    }
}
