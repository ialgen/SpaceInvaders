using System;
using System.Collections.Generic;

namespace SpaceInvader
{
    public class DefenderShip
    // Cette classe definit le comportement et le characteristiques du vaisseau de defense controle par le joueur.
    {
        string[] Shape = new string[]
            {
                "     _|_     ",
                "    / _ \\    ",
                " __/ (_) \\__ ",
                "/||\\ / \\ /||\\",
                " ** ^^^^^ ** ",
            };
        public static (int x, int y) Size = (13, 5);
        public (int x, int y) Location;
        public List<Bullet> Bullets = new List<Bullet>();
        public int Speed;

        public DefenderShip(int x_pos, int y_pos, int speed)
        // Constructeur de la classe.
        {
            Location = (x_pos, y_pos);
            Speed = speed;
        }

        public (int x, int y) HitBox()
        // Zone delimiant le vaisseau pour la calcul des collisions.
        {
            return (Location.x + Size.x, Location.y + Size.y);
        }

        public void Draw()
        // Affichage du vaisseau dans la console.
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
        // Tir de missile.
        {
            if (Bullets.Count == 0)
            {
                Bullets.Add(new Bullet((Location.x + 6, Location.y - 1), '|'));
            }
        }

        // Mouvements lateraux.
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
