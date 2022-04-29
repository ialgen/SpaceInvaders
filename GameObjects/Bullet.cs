using System;

namespace SpaceInvader
{
    public class Bullet
    // Cette classe definit le comportement des missiles.
    {
        public (int x, int y) Location;
        public char Shape;

        public Bullet((int x, int y) location, char shape)
        // Constructeur de la classe.
        {
            Location = location;
            Shape = shape;
        }

        public void MoveUp()
        // Mouvement ascendant.
        {
            Location.y--;
        }

        public void MoveDown()
        // Mouvement descendant.
        {
            Location.y++;
        }

        public void Draw()
        // Affichage instance dans la console.
        {
            Console.SetCursorPosition(Location.x, Location.y);
            Console.Write(Shape);
        }
    }
}
