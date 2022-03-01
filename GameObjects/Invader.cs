using System;
using System.Collections.Generic;

namespace SpaceInvader
{
    public class Invader
    {
        string[] Shape;
        public static (int x, int y) Size = (9, 3);
        public (int x, int y) Location;
        public int StepOnPath;
        public List<Bullet> Bullets = new();

        public Invader(int x_pos, int y_pos)
        {
            Shape = new string[]
              {
                  "|\\     /|",
                  "| )*^*( |",
                  "|/     \\|",
              };
            Location = (x_pos, y_pos);
            StepOnPath = 0;
        }

        public (int x_min, int y_min, int x_max, int y_max) HitBox()
        {
            return (Location.x, Location.y, Location.x + Size.x, Location.y + Size.y);
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
            if (Bullets.Count < 3)
            {
                Bullets.Add(new Bullet((Location.x + 5, Location.y + 3), '*'));
            }
        }

        public void Move()
        {
            StepOnPath += 1;
        }
    }
}
