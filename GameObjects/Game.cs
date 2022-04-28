using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceInvader
{
    public class Game
    {
        public Box Screen;
        public DefenderShip DefenderShip;
        public List<Invader> Invaders;
        public (int x, int y)[] InvadersPath;
        public int Score = 0;
        public uint DifficultySpeed;
        public DateTime StartTime;
        public double ClockTime;
        public bool EndGame = false;


        public Game(Box screen, DefenderShip defenderShip, List<Invader> invaders, DateTime startTime)
        {
            Screen = screen;
            DefenderShip = defenderShip;
            Invaders = invaders;
            InvadersPath = InitPath(screen);
            StartTime = startTime;
        }
        

        public static void UpdateBullets(DefenderShip defenderShip)
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

        public void UpdateInvaders()
        {
            if (Math.Abs(ClockTime % 1) < 0.1)
            {
                //LastInvader(screen);
                UpdateInvadersLocations();
            }
            
            BulletInvaderCollisions();

            if (Math.Abs(ClockTime % 2) < 0.1)
            {
                Invaders.Add(new Invader(InvadersPath[0].x, InvadersPath[0].y));
            }
        }
        public void Test_Invader_winning_position()
        {
            var nPerCol = InvadersPerColumn(Screen.Size_y);
            var nPerLine = InvadersPerLine(Screen.Size_x);
            if (Math.Abs(ClockTime % 1) < 0.1)
            {
                foreach (Invader invader in Invaders)
                {
                    if ((InitPath(Screen)[nPerLine * nPerCol-1].Item1 == invader.Location.x) &(InitPath(Screen)[nPerLine * nPerCol - 1].Item2 == invader.Location.y))
                    {
                        EndGame = true;
                        ClockTime = (DateTime.Now - StartTime).TotalSeconds;
                    }
                }
            }
           
        }
        private void UpdateInvadersLocations()
        {
            foreach (Invader invader in Invaders)
            {
                invader.Move();
                invader.Location = InvadersPath[invader.StepOnPath];
            }
        }

        public void BulletInvaderCollisions()
        {
            List<int> destroyedInvaders = new List<int>();
            List<int> destroyedBullets = new List<int>();
            int invaderIndex = 0;
            foreach (Invader invader in Invaders)
            {
                int bulletIndex = 0;
                foreach (Bullet bullet in DefenderShip.Bullets)
                {
                    bool x_collision = invader.HitBox().x_min < bullet.Location.x & invader.HitBox().x_max > bullet.Location.x;
                    bool y_collision = invader.HitBox().y_min < bullet.Location.y & invader.HitBox().y_max > bullet.Location.y;

                    if (x_collision & y_collision)
                    {
                        destroyedInvaders.Add(invaderIndex);
                        Score = Score + 10;
                        destroyedBullets.Add(bulletIndex);
                    }
                    bulletIndex++;
                }
                invaderIndex++;
            }
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

        private int InvadersPerLine(int boxWidth)
        {
            return (int)Math.Floor(Convert.ToDouble(boxWidth / (Invader.Size.x + 2)));
        }

        private int InvadersPerColumn(int boxHeight)
        {
            return (int)Math.Floor(Convert.ToDouble((boxHeight - DefenderShip.Size.y) / (Invader.Size.y + 1)));
            //return (int)Math.Floor(Convert.ToDouble(boxHeight / (Invader.Size.y + 1)));
        }

        private (int, int)[] InitPath(Box screen)
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
