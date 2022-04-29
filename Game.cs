using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceInvader
{
    public class Game
    // Cette classe definit toutes les etapes constituant un partie.
    {
        public Box Screen;                      // Espace de jeu.
        public DefenderShip DefenderShip;       // Vaisseau de type DefenderShip.
        public List<Invader> Invaders;          // Vaisseaux de type Invader.
        public (int x, int y)[] InvadersPath;   // Positions suivis par les vaisseaux de type Invader.
        public int Score = 0;                   // Score du joueur.
        public double DifficultySpeed;          // Variable definissant la vitesse de deplacement des Invader.
        public DateTime StartTime;              // Date de lancement du jeu.
        public double ClockTime;                // Duree du jeu.
        public double LastInvadersMoveTime;     // Date du dernier mouvement des Invader.

        public Game(Box screen, DefenderShip defenderShip, List<Invader> invaders, DateTime startTime, double difficultySpeed)
        // Constructeur de la classe
        {
            Screen = screen;
            DefenderShip = defenderShip;
            Invaders = invaders;
            InvadersPath = InitPath(screen);
            StartTime = startTime;
            DifficultySpeed = difficultySpeed;
        }

        public void Update(DefenderShip defenderShip)
        // Actualisation des positions des elements mobiles du jeu.
        {
            UpdateBullets(defenderShip);
            UpdateInvaders();
        }

        private static void UpdateBullets(DefenderShip defenderShip)
        // Actualisations des position des missiles.
        {
            for (int i = 0; i < defenderShip.Bullets.Count; i++)
            {
                defenderShip.Bullets[i].MoveUp();
                if (defenderShip.Bullets[i].Location.y < 0)
                {
                    defenderShip.Bullets.RemoveAt(i);
                }
            }
        }

        private void UpdateInvaders()
        // Actulisation des positions de vaisseaux Invader.
        {
            // Actualisation se fait selon le parametre de vitesse du jeu
            if (Math.Abs(ClockTime - LastInvadersMoveTime) > DifficultySpeed)
            {
                UpdateInvadersLocations();
                LastInvadersMoveTime = ClockTime;
            }
            
            BulletInvaderCollisions();

            // Apparition d'un nouvel ennemi.
            if (Math.Abs(ClockTime - LastInvadersMoveTime) > DifficultySpeed*2)
            {
                Invaders.Add(new Invader(InvadersPath[0].x, InvadersPath[0].y));
            }
        }

        private void UpdateInvadersLocations()
        // Actulisation des positions de vaisseaux Invader.
        {
            foreach (Invader invader in Invaders)
            {
                invader.Move();
                invader.Location = InvadersPath[invader.StepOnPath];
            }
        }

        public void BulletInvaderCollisions()
        // Detection des collisions entre Invader et Bullet.
        {
            List<int> destroyedInvaders = new List<int>();
            List<int> destroyedBullets = new List<int>();
            int invaderIndex = 0;
            foreach (Invader invader in Invaders)
            {
                int bulletIndex = 0;
                foreach (Bullet bullet in DefenderShip.Bullets)
                {
                    // Condition de collision sur l'axe horizontal.
                    bool x_collision = invader.HitBox().x_min <= bullet.Location.x & invader.HitBox().x_max > bullet.Location.x;
                    // Condition de collision sur l'axe vertical.
                    bool y_collision = invader.HitBox().y_min <= bullet.Location.y & invader.HitBox().y_max > bullet.Location.y;

                    if (x_collision & y_collision)
                    {
                        destroyedInvaders.Add(invaderIndex);
                        Score++;
                        destroyedBullets.Add(bulletIndex);
                    }
                    bulletIndex++;
                }
                invaderIndex++;
            }
            // Destruction des vaisseaux touches.
            int index = 0;
            while (index < Invaders.Count)
            {
                if (destroyedInvaders.Contains(index))
                {
                    Invaders[index].AnimateDeath();
                    Invaders.RemoveAt(index);
                }
                index++;
            }
            // Destruction des missiles touchant leur cible.
            index = 0;
            while (index < DefenderShip.Bullets.Count)
            {
                if (destroyedBullets.Contains(index))
                {
                    DefenderShip.Bullets.RemoveAt(index);
                }
                index++;
            }
        }

        public bool EndGame()
        // Verifie si un vaisseau Invader est arrive a la derniere case.
        // Renvoie un bool determinant si le jeu est fini.
        {
            foreach (Invader invader in Invaders)
            {
                if (invader.Location.x == InvadersPath[InvadersPath.Count() - 1].x & invader.Location.y == InvadersPath[InvadersPath.Count() - 1].y)
                {
                    ClockTime = (DateTime.Now - StartTime).TotalSeconds;
                    InvasionAnimation();
                    return true;
                }
            }
            return false;
        }

        private void InvasionAnimation()
        // Animation de fin du jeu.
        {
            foreach (Invader invader in Invaders)
            {
                invader.AnimatedVictory();
            }
        }

        // Fonction determinant la grilles de positions possibles pourr les vaisseau de type Invader.
        private int InvadersPerLine(int boxWidth)
        // Largeur de la grille.
        {
            return (int)Math.Floor(Convert.ToDouble(boxWidth / (Invader.Size.x + 2)));
        }
        private int InvadersPerColumn(int boxHeight)
        // Hauteur de la grille.
        {
            return (int)Math.Floor(Convert.ToDouble((boxHeight - DefenderShip.Size.y) / (Invader.Size.y + 1)));
        }
        private (int, int)[] InitPath(Box screen)
        // Organisation des position en "serpentin" le long de la grille.
        // Les deplacements des vaisseaus de type Invader sont definit par ce chemin de positions.
        {
            var nPerCol = InvadersPerColumn(screen.Size_y);
            var nPerLine = InvadersPerLine(screen.Size_x);
            (int x, int y)[] path = new (int x, int y)[nPerLine * nPerCol];

            int x_margin = (screen.Size_x - (nPerLine * (Invader.Size.x + 2))) / 2;
            int[] x_pos = Enumerable.Range(0, nPerLine).Select(x => x * (Invader.Size.x + 2) + x_margin).ToArray();
            int[] y_pos = Enumerable.Range(0, nPerCol).Select(x => x * (Invader.Size.y + 1) + 1).ToArray();

            int cpt = 0;
            for (int y = 0; y < nPerCol; y++)
            {
                for (int x = 0; x < nPerLine; x++)
                {
                    path[cpt] = (x_pos[x], y_pos[y]);
                    cpt++;
                }
                Array.Reverse(x_pos);
            }
            return path;
        }
    }
}
