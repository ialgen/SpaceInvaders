using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SpaceInvader
{
    public class HomePage
    // Cette classe definit la page d'accueil du jeu et de fin de manche
    {
        // Coordonnees coin haut gauche de la box formant la page d accueil
        public int X_left_top = 10;
        public int Y_left_top = 4;
        // Taille de la page d 'accueil
        public int Size_x = 60;
        public int Size_y = 35;
        // Marges entre bordures de la page et certains affichages dedans (vaisseaux,tirs, textes) 
        public int X_margin = 3;
        public int Y_margin = 1;

        public HomePage(){}
        // Constructeur de la classe avec valeurs par défaut

        public HomePage(int x_left_top, int y_left_top, int size_x, int size_y)
        // Constructeur de la classe 
        {
            X_left_top = x_left_top;
            Y_left_top = y_left_top;
            Size_x = size_x;
            Size_y = size_y;
        }

        public (bool,double) Launch(bool restart, (double, int) Results)
        //Gestion de l avant et l'apres partie de jeu
        {
            //1.Affichage de la page d'accueil avec message (jouer ou rejouer ?) 
            Console.Clear();
            this.Draw(restart, Results, true);
            //Recuperation de la requete joueur
            bool GO = RecordPlay(Console.ReadKey().Key);
            Thread.Sleep(2000);

            if (GO==false) Environment.Exit(0);

            //2.Affichage de la page d'accueil avec choix difficulté du jeu
            Console.Clear();
            this.Draw(restart, Results, false);
            //Recuperation de la requete joueur
            double Speed = RecordSpeed(Console.ReadKey().Key);
            Thread.Sleep(1000);
            Console.Clear();

            return (GO, Speed);
        }
        
        public void Draw(bool restart, (double, int) Results, bool phase_1)
        // Affichage de la page d'accueil
        // plusieurs pages d accueil : Phase1 /Phase2 /EndGame_Announce
        {

            Box homepagebox = new Box(X_left_top, Y_left_top, Size_x, Size_y);
            homepagebox.Draw();
            //Creation des différents blocs au fur et à mesure
            //1.Affichage des invaders
            int working_on_row = Y_left_top + Y_margin;
            Random_Invaders(Size_x, X_left_top, X_margin, working_on_row);

            //2.Affichage des tirs
            working_on_row = working_on_row + 2 * (Invader.Size.y + 1);
            Random_Shoots(Size_x, X_left_top, X_margin, working_on_row);

            //3.Affichage du titre
            working_on_row = working_on_row + 5;
            Game_Title(X_margin, X_left_top, Size_x, working_on_row);

            //4.Affichage des messages au joueur
            working_on_row = working_on_row + 6;
           
            if (phase_1==true)
            {
                if (restart==true)
                {
                    //Annnonce fin de manche (resultats et si il veut rejouer)
                    EndGame_Announce(Results, X_left_top, Size_x, X_margin, working_on_row);
                }
                else
                {
                    //Affichage initiale (si on veut jouer pour la 1ere fois)
                    Phase_1(X_left_top, Size_x, X_margin, working_on_row);
                }
            }
            else
            {
                //Affichage choix de la difficulte du jeu
                Phase_2(X_left_top, Size_x, X_margin, working_on_row);
            }

        }
        public static void Random_Invaders(int size_x, int x_pos,int x_margin,int working_on_row)
        // Apparition aleatoire d invaders
        {
            Random random = new Random();
            //nombre d invaders par ligne
            int n_lig = (int)Math.Floor(Convert.ToDouble(size_x / (Invader.Size.x + 2)));

            // Affichage de 2 lignes d invaders 
            for (int cpt2 = 0; cpt2 < n_lig; cpt2++)
            {
                //1 chance sur 2 pour qu un invader soit affiche
                if (random.Next(0, 2) == 1) new Invader(x_pos + x_margin + cpt2 * (Invader.Size.x + 2), working_on_row).Draw();
                if (random.Next(0, 2) == 1) new Invader(x_pos + x_margin + cpt2 * (Invader.Size.x + 2), working_on_row + (Invader.Size.y + 1)).Draw();
            }
        }
        public static void Random_Shoots(int size_x, int x_pos, int x_margin, int working_on_row)
        // Apparition aleatoire de tirs ennemis statiques
        {
            Random random = new Random();
            //parcours des lignes 
            for (int y = 0; y < 4; y++)
            {
                Console.SetCursorPosition(x_pos + 2 * x_margin, working_on_row + y);
                //parcours des colonnes
                for (int x = 0; x < size_x - 4 * x_margin; x++)
                {
                    //1 chance sur 2 qui tir soit affiche
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
        public static void Game_Title(int x_margin, int x_left_top, int size_x, int working_on_row)
        // Affichage du titre 
        {
            //Titre sur plusieurs lignes 
            //Adaptable a tout titre il suffit de l'ecrire(avec des 1) sur un fichier txt de remplacer les espaces
            //par des 0,le copier/coller dans un string sur c# et ce code s'occupe sur reste

            string[] title ={
                "111101111011110111101111000011110111101111011110110001100001101111010001",
                "100001001010010100001000000010010100001001010000110001100001101001011001",
                "111101111011110100001111000011110111101111011110110001100001101001011101",
                "000101000010010100001000000011100100001001010000110001100001101001010111",
                "111101000010010111101111000010110111101111011110111101111101101111010011"
            };//SPACE REBELLION (nom du jeu)
            char[] chars;
            int[][] title_converted = new int[title.GetLength(0)][];
            int cpt = 0;
            Int32[] ints;

            //Convertit ces string en jagged arrays de type int
            foreach (string titleligne in title)
            {
                chars = titleligne.ToCharArray();
                ints = chars.Select(c => (int)Char.GetNumericValue(c)).ToArray();
                title_converted[cpt] = ints;
                cpt++;
            };

            //Lecture du jagged array et Affichage du titre 
            for (int i = 0; i < title_converted.GetLength(0); i++)
            {
                int start_col = (size_x + 2 * x_left_top) / 2 - title[0].Length / 2+1;
                Console.SetCursorPosition(start_col, working_on_row + i);
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
                    //Si l'element en position (i,j) dans le jagged array == 1
                    //la cellule ou est le curseur, sera jaune et noir a l inverse
                    Console.Write(" ");
                }
            };
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void Phase_2(int x_left_top, int size_x, int x_margin, int working_on_row)
        //Affichage message difficultés de jeu
        {
            string[] txt = { "Luke Skywalker (hard / Press L)", "Han Solo (medium /Press H)", "Poe Dameron (easy / Press P) (default setting)", "Rebellion doesn't wait ! YOUR ANSWER: _"};
            string txt_first_line = "Which kind of fighter pilot are you ? (difficulty level)";
            int start_col = (size_x) / 2 - txt_first_line.Length / 2 + 1;
            Console.SetCursorPosition(start_col + x_left_top, working_on_row + 2);
            Console.WriteLine(txt_first_line);
            //Parcours les differents messages et les positionnent correctement par rapport à la boxe et la taille du message
            for (int i = 0; i < txt.GetLength(0); i++)
            {
                start_col = (size_x) / 2 - txt[i].Length / 2 + 1;
                Console.SetCursorPosition(start_col + x_left_top, working_on_row + 4 + i);
                Console.WriteLine(txt[i]);
            }
            Console.SetCursorPosition(start_col + x_left_top + txt[txt.GetLength(0) - 1].Length - 1, working_on_row + 3 + txt.GetLength(0));
            
        }
        public static void Phase_1(int x_left_top, int size_x, int x_margin, int working_on_row)
        //Affichage message initial
        {
            string[] txt = { "Press SPACEBAR to (re)play", "Or anything else if you don't want to play", "YOUR ANSWER : _"};
            string txt_first_line = "READY TO SAVE THE GALAXY ?";
            int start_col = (size_x) / 2 - txt_first_line.Length / 2 + 1; 
            Console.SetCursorPosition(start_col+x_left_top, working_on_row + 2);
            Console.WriteLine(txt_first_line);
            //Parcours les differents messages et les positionnent correctement par rapport à la boxe et la taille du message
            for (int i = 0; i < txt.GetLength(0); i++)
            {
                start_col = (size_x) / 2 - txt[i].Length / 2 + 1;
                Console.SetCursorPosition(start_col + x_left_top, working_on_row + 5 + i);
                Console.WriteLine(txt[i]);
            }

            Console.SetCursorPosition(start_col + x_left_top+ txt[txt.GetLength(0)-1].Length-1, working_on_row + 4 + txt.GetLength(0));

        }
        public static void EndGame_Announce((double, int) Results, int x_left_top, int size_x, int x_margin, int working_on_row)
         //Affichage message de fin de manche 
         {
            string[] txt_Results = { "Your score is : " + Results.Item2.ToString(), "You survived against the Empire during : " + Results.Item1.ToString() + " seconds"};
            string[] txt_replay = { "Press SPACEBAR to (re)play", "Or anything else if you don't want to play", "YOUR ANSWER : _" };
            string txt_first_line = "It's an ENDGAME !";
            int start_col = (size_x) / 2 - txt_first_line.Length / 2 + 1;
            Console.SetCursorPosition(start_col + x_left_top, working_on_row + 2);
            Console.Write(txt_first_line);
            //affichage des resultats a la manche precedente
            for (int i = 0; i < txt_Results.GetLength(0); i++)
            {
                start_col = (size_x) / 2 - txt_Results[i].Length / 2 + 1;
                Console.SetCursorPosition(start_col + x_left_top, working_on_row + 4 + i);
                Console.WriteLine(txt_Results[i]);
            }
            //affichage des messages pour rejouer 
            for (int i = 0; i < txt_replay.GetLength(0); i++)
            {
                start_col = (size_x) / 2 - txt_replay[i].Length / 2 + 1;
                Console.SetCursorPosition(start_col + x_left_top, working_on_row + 7 + i);
                Console.WriteLine(txt_replay[i]);
            }
            Console.SetCursorPosition(start_col + x_left_top + txt_replay[txt_replay.GetLength(0) - 1].Length - 1, working_on_row + 4 + txt_Results.GetLength(0) + txt_replay.GetLength(0));

        }
        public double RecordSpeed(ConsoleKey Key)
        // Etabli le parametre DifficulySpeed de la prochaine instance de type Game.
        {
            switch(Key)
            {
                case ConsoleKey.L:
                    return 0.7;
                case ConsoleKey.H:
                    return 1;
                default:
                    return 1.5;
            }
        }
        public bool RecordPlay(ConsoleKey Key)
        //Etabli si le joueur (re)joue ou non
        {
            switch (Key)
            {
                case ConsoleKey.Spacebar:
                    Console.Write("Yes");
                    return true;
                default:
                    Console.Write("No");
                    return false;
            }
        }
    }
}
