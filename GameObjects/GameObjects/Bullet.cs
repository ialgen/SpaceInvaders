using System;

namespace SpaceInvader
{
    public class Bullet
    {
        public (int x, int y) Location;
        public char Shape;

        public Bullet((int x, int y) location, char shape)
        {
            Location = location;
            Shape = shape;
        }

        public void MoveUp()
        {
            Location.y--;
        }

        public void MoveDown()
        {
            Location.y++;
        }

        public void Draw()
        {
            Console.SetCursorPosition(Location.x, Location.y);
            Console.Write(Shape);
        }
    }
}
