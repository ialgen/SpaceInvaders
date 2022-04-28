using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SpaceInvader
{
    public class HomePage
    {
        public int X_left_top = 10;
        public int Y_left_top = 4;
        public int Size_x = 60;
        public int Size_y = 35;
        public int X_margin = 3;
        public int Y_margin = 1;

        public HomePage(){}
        public HomePage(int x_left_top, int y_left_top, int size_x, int size_y)
        {
            X_left_top = x_left_top;
            Y_left_top = y_left_top;
            Size_x = size_x;
            Size_y = size_y;
        }
        public (bool,int) Launch(bool restart,(double, int)Results)
        {
            this.Draw(restart, Results, true);
            bool GO = Record_Play(Console.ReadKey().Key);
            if (GO==false) Environment.Exit(0);
            Thread.Sleep(1000);
            Console.Clear();
            this.Draw(restart, Results, false); 
            int Speed = Record_Speed(Console.ReadKey().Key);
            Thread.Sleep(1000);
            Console.Clear();
            return (GO, Speed);

        }
        
        public void Draw(bool restart, (double, int) Results, bool phase_1)
        {
            Box homepagebox = new Box(X_left_top, Y_left_top, Size_x, Size_y);
            homepagebox.Draw();
            //Creation des différents blocs au fur et à mesure
            //1.
            int working_on_row = Y_left_top + Y_margin;
            Random_Invaders(Size_x, X_left_top, X_margin, working_on_row);

            //2.
            working_on_row = working_on_row + 2 * (Invader.Size.y + 1);
            Random_Shoots(Size_x, X_left_top, X_margin, working_on_row);

            //3.
            working_on_row = working_on_row + 5;
            Game_Title(X_margin, working_on_row);

            //4
            working_on_row = working_on_row + 6;
           
            if (phase_1==true)
            {
                if (restart==true)
                {
                    EndGame_Announce(Results, X_left_top, Size_x, X_margin, working_on_row);
                }
                else
                {
                    Phase_1(X_left_top, Size_x, X_margin, working_on_row);
                }
            }
            else
            {
                Phase_2(X_left_top, Size_x, X_margin, working_on_row);
            }

        }
        public static void Random_Invaders(int size_x, int x_pos,int x_margin,int working_on_row)
        {
            Random random = new Random();
            int n_lig = (int)Math.Floor(Convert.ToDouble(size_x / (Invader.Size.x + 2)));
            for (int cpt2 = 0; cpt2 < n_lig; cpt2++)
            {
                if (random.Next(0, 2) == 1) new Invader(x_pos + x_margin + cpt2 * (Invader.Size.x + 2), working_on_row).Draw();
                if (random.Next(0, 2) == 1) new Invader(x_pos + x_margin + cpt2 * (Invader.Size.x + 2), working_on_row + (Invader.Size.y + 1)).Draw();
            }
        }
        public static void Random_Shoots(int size_x, int x_pos, int x_margin, int working_on_row)
        {
            Random random = new Random();
            for (int y = 0; y < 4; y++)
            {
                Console.SetCursorPosition(x_pos + 2 * x_margin, working_on_row + y);
                for (int x = 0; x < size_x - 4 * x_margin; x++)
                {
                    if (random.Next(0, 2) == 1)
                    {
                        if (x <= size_x / 2 - 2 * x_margin)
                        {
                            Console.Write("\\");
                        }
                        else
                        {
                            Console.Write("/");
                        };
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }
        public static void Game_Title(int x_margin, int working_on_row)
        {
            string[] title ={
                "11110111101111011110111100001111001111001111001111001100001100000110011110010001",
                "10000100101001010000100000001001001000001001001000001100001100000110010010011001",
                "11110111101111010000111100001111001111001111001111001100001100000110010010011101",
                "00010100001001010000100000001110001000001001001000001100001100000110010010010111",
                "11110100001001011110111100001011001111001111001111001111001111100110011110010011"
            };
            char[] chars;
            int[][] title_converted = new int[title.GetLength(0)][];
            int cpt = 0;
            Int32[] ints;

            foreach (string titleligne in title)
            {
                chars = titleligne.ToCharArray();
                ints = chars.Select(c => (int)Char.GetNumericValue(c)).ToArray();
                title_converted[cpt] = ints;
                cpt++;
            };

            for (int i = 0; i < title_converted.GetLength(0); i++)
            {
                Console.SetCursorPosition(x_margin, working_on_row + i);
                for (int j = 0; j < title_converted[i].GetLength(0); j++)
                {
                    if (title_converted[i][j] == 1)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    Console.Write(" ");
                }
            };
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void Phase_2(int x_left_top, int size_x, int x_margin, int working_on_row)
        {
            Console.SetCursorPosition(x_left_top + x_margin, working_on_row + 2);
            Console.WriteLine("Which kind of fighter pilot are you ? (difficulty level)");
            Console.SetCursorPosition(size_x / 2 - 8, working_on_row + 4);
            Console.WriteLine("Luke Skywalker (hard / Press L)");
            Console.SetCursorPosition(size_x / 2 - 8, working_on_row + 5);
            Console.WriteLine("Han Solo (medium /Press H)");
            Console.SetCursorPosition(size_x / 2 - 8, working_on_row + 6);
            Console.WriteLine("Poe Dameron (easy / Press P)");
            Console.SetCursorPosition(size_x / 2 - 8, working_on_row + 8);
            Console.Write("Rebellion doesn't wait ! Your answer : ");
        }
        public static void Phase_1(int x_left_top, int size_x, int x_margin, int working_on_row)
        {

            Console.SetCursorPosition(size_x / 2 - 5, working_on_row + 2);
            Console.WriteLine("READY TO SAVE THE GALAXY ? ");
            Console.SetCursorPosition(x_left_top + 3*x_margin, working_on_row + 5);
            Console.WriteLine("Press Enter to (re)play");
            Console.SetCursorPosition(x_left_top + 3*x_margin, working_on_row + 6);
            Console.WriteLine("Or anything else if you don't want to play");
            Console.SetCursorPosition(size_x / 2 - 3, working_on_row + 8);
            Console.Write("Your answer :  ");
        }
        public static void EndGame_Announce((double, int) Results, int x_left_top, int size_x, int x_margin, int working_on_row)
        {

            Console.SetCursorPosition(size_x / 2 - 3, working_on_row + 2);
            Console.WriteLine("It's AN ENDGAME !");
            Console.SetCursorPosition(size_x / 2, working_on_row + 4);
            Console.WriteLine("Your score is : ",Results.Item2, " !");
            Console.SetCursorPosition(x_left_top + x_margin, working_on_row + 5);
            Console.WriteLine("You survive against Empire during : ", Results, " seconds"); 
            Console.SetCursorPosition(x_left_top + 3 * x_margin, working_on_row + 7);
            Console.WriteLine("Do you want write to try another game ?");
            Console.SetCursorPosition(x_left_top + 3 * x_margin, working_on_row + 9);
            Console.WriteLine("Press Enter to (re)play");
            Console.SetCursorPosition(x_left_top + 3 * x_margin, working_on_row + 10);
            Console.WriteLine("Or anything else if you don't want to play");
            Console.SetCursorPosition(size_x / 2 - 3, working_on_row + 12);
            Console.Write("Your answer :  ");
        }
        public int Record_Speed(ConsoleKey Key)
        {
            switch(Key)
            {
                case ConsoleKey.L:
                    return 3;
                case ConsoleKey.H:
                    return 2;
                default:
                    return 1;
            }
        }
        public bool Record_Play(ConsoleKey Key)
        {
            switch (Key)
            {
                case ConsoleKey.Enter:
                    return true;
                default:
                    return false;
            }
        }
    }
}
