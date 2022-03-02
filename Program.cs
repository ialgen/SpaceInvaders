using System;
using System.Collections.Generic;
using System.Threading;


namespace SpaceInvader
{
    class Program
    {
        static void Main()
        {
            Game game = new Game(new Box(120, 20), new DefenderShip(60, 15, 3), new List<Invader>(), DateTime.Now);

            while (true)
            {
                Console.Clear();

                if (Console.KeyAvailable)
                {
                    var arrow = Console.ReadKey(false).Key;
                    switch (arrow)
                    {
                        case ConsoleKey.RightArrow:
                            if (game.DefenderShip.Location.x < game.Screen.Size_x)
                            {
                                game.DefenderShip.MoveRight();
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            if (game.DefenderShip.Location.x > 0)
                            {
                                game.DefenderShip.MoveLeft();
                            }
                            break;
                        case ConsoleKey.Spacebar:
                            game.DefenderShip.Shoot();
                            break;
                        default:
                            break;
                    }
                }

                Game.UpdateBullets(game.DefenderShip);
                game.UpdateInvaders();

                foreach (Bullet bullet in game.DefenderShip.Bullets)
                {
                    bullet.Draw();
                }
                foreach (Invader invader in game.Invaders)
                {
                    invader.Draw();
                }
                game.DefenderShip.Draw();
                game.Screen.Draw();

                Console.SetCursorPosition(122, 0);
                Console.Write(Math.Round(game.ClockTime, 3));

                game.ClockTime = (DateTime.Now - game.StartTime).TotalSeconds;
                Thread.Sleep(100);
            }
        }
    }
}
