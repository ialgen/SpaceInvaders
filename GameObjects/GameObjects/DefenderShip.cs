using System;
using System.Collections.Generic;

namespace SpaceInvader
{
    public class DefenderShip
    {
        string[] Shape;
        public (int x,int y) Location;
        public static (int x, int y) Size = (13, 5);
        public List<Bullet> Bullets = new List<Bullet>();
        public int Speed;


        public DefenderShip(int x_pos, int y_pos, int speed)
        {
            Shape = new string[]
              {
                  "     _|_     ",
                  "    / _ \\    ",
                  " __/ (_) \\__ ",
                  "/||\\ / \\ /||\\",
                  " ** ^^^^^ ** ",
              };
            Location = (x_pos, y_pos);
            Speed = speed;
        }

        public (int x, int y) HitBox()
        {
            return (Location.x + Size.x, Location.y + Size.y);
        }

        public void Draw()
        {
            int cpt = 0;
            foreach (string Stage in Shape)
            {
                Console.SetCursorPosition(Location.x, Location.y + cpt);
                Console.Write(Stage);
                cpt++;
            }
        }

        public void Shoot()
        {
            if (Bullets.Count == 0)
            {
                Bullets.Add(new Bullet((Location.x + 6, Location.y - 1), '|'));
            }
        }

        public void MoveLeft()
        {
            Location.x -= Speed;
        }

        public void MoveRight()
        {
            Location.x += Speed;
        }
    }
}
