﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace SpaceInvader
{
    class Program
    {
        static void Main()
        {
            HomePage homepage = new HomePage();
            int Size_x = 90;
            (double, int) Results = (0, 0);
            (bool, double) GameData = homepage.Launch(false, Results);

            while (true)
            {
                Game game = new Game(new Box(0,0, Size_x, 20), new DefenderShip(45, 15, 3), new List<Invader>(), DateTime.Now, GameData.Item2);
            
                game.Screen.Draw();
            
                while (game.EndGame() != true)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;

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

                    game.Update(game.DefenderShip);

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

                    Console.SetCursorPosition(Size_x + 2, 0);
                    Console.Write(Math.Round(game.ClockTime, 3));

                    game.ClockTime = (DateTime.Now - game.StartTime).TotalSeconds;
                    Thread.Sleep(100);
                }

                Console.WriteLine(game.ClockTime.ToString());
                GameData = homepage.Launch(true, (game.ClockTime, game.Score));
                Console.ReadKey();
            }
        }
    }
}
