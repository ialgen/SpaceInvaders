using System;
using System.Collections.Generic;
using System.Threading;


namespace SpaceInvader
{
    class Program
    {
        static void Main()
        {
            Game game = new(new Box(120, 50), new DefenderShip(60, 45, 3), new List<Invader>());

            game.Invaders.Add(new Invader(10, 10));

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
                Console.Write(Math.Round(game.Seconds, 3));

                Console.SetCursorPosition(122, 2);
                Console.Write(game.passage);
                Console.SetCursorPosition(122, 4);
                Console.Write(game.passage2);

                game.Seconds += 0.1;
                Thread.Sleep(100);
            }
        }
    }
}
