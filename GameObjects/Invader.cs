using System;
using System.Collections.Generic;
using System.Threading;

namespace SpaceInvader
{
    public class Invader
    // Cette classe definit le comportement et le characteristiques du vaisseau de defense que controle le joueur.
    {
        string[] Shape = new string[]
              {
                  "|\\     /|",
                  "| )*^*( |",
                  "|/     \\|",
              };
        public static (int x, int y) Size = (9, 3);
        public (int x, int y) Location;
        public int StepOnPath;
        public List<Bullet> Bullets = new List<Bullet>();

        public Invader(int x_pos, int y_pos)
        // Constructeur de la classe.
        {
            Location = (x_pos, y_pos);
            StepOnPath = 0;
        }

        public (int x_min, int y_min, int x_max, int y_max) HitBox()
        // Zone delimiant le vaisseau pour la calcul des collisions.
        {
            return (Location.x, Location.y, Location.x + Size.x, Location.y + Size.y);
        }

        public void Draw()
        // Affichage du vaisseau dans la console.
        {
            int cpt = 0;
            foreach (string Row in Shape)
            {
                Console.SetCursorPosition(Location.x, Location.y + cpt);
                Console.Write(Row);
                cpt++;
            }
        }

        public void Shoot()
        // Tir de missile.
        {
            if (Bullets.Count < 3)
            {
                Bullets.Add(new Bullet((Location.x + 5, Location.y + 3), '*'));
            }
        }

        public void Move()
        // Mouvement du vaisseau sur le chemin (definit dans la classe Game) des vaisseaux de type Invader.
        {
            StepOnPath += 1;
        }

        public void AnimateDeath()
        // Animation lors de la collision entre un missile et le vaisseau.
        // Le vaisseau s'affiche brievement en rouge.
        {
            Console.ForegroundColor = ConsoleColor.Red;
            int cpt = 0;
            foreach (string Stage in Shape)
            {
                Console.SetCursorPosition(Location.x, Location.y + cpt);
                Console.Write(Stage);
                cpt++;
            }
            Thread.Sleep(10);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void AnimatedVictory()
        // Animation lors de la victoire des attanquants.
        // Le vaisseau clignote plusieurs fois entre vert et blanc.
        {
            ConsoleColor[] colors = { ConsoleColor.Green, ConsoleColor.White };

            for (int i = 1; i< 10; i++)
            {
                Console.ForegroundColor = colors[i%2];
                int cpt = 0;
                foreach (string Stage in Shape)
                {
                    Console.SetCursorPosition(Location.x, Location.y + cpt);
                    Console.Write(Stage);
                    cpt++;
                }
                Thread.Sleep(50);
                Console.ForegroundColor = colors[(i-1)%2];
            }
        }
    }
}
