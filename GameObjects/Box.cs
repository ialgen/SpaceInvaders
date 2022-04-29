using System;
namespace SpaceInvader
{
    public class Box
    // Cette classe permet definit l'espace dans lequel le jeu se deroule dans la console.
    {
        // Coordonnees coin haut gauche de l'espace de jeu.
        public int X_left_top;
        public int Y_left_top;
        // Taille de l'espace de jeu.
        public int Size_x;
        public int Size_y;

        public Box(int x_left_top, int y_left_top, int size_x, int size_y)
        // Constructeur de la classe.
        {
            X_left_top = x_left_top;
            Y_left_top= y_left_top;
            Size_x = size_x;
            Size_y = size_y;
        }

        public void Draw()
        // Affichage de l'espace de jeu.
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(X_left_top, Y_left_top);

            // Affichage horizontal.
            for (int x = 0; x < Size_x; x++)
            {
                Console.SetCursorPosition(X_left_top + x, Y_left_top);
                Console.Write("-");
                Console.SetCursorPosition(X_left_top + x, Y_left_top + Size_y - 1);
                Console.Write("-");

            }
            // Affichage vertical.
            for (int y = 0; y < Size_y; y++)
            {
                Console.SetCursorPosition(X_left_top, Y_left_top + y);
                Console.Write("|");
                Console.SetCursorPosition(X_left_top + Size_x, Y_left_top + y);
                Console.Write("|");
            }
        }
    }
}
