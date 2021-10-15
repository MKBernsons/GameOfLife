﻿using System;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager gamemanager = new GameManager();
            bool stopped = true;

            while (true)
            {
                if (stopped)
                {
                    Menu();
                }
                else
                {
                    string input = Console.ReadLine();
                    gamemanager.StopAllGames();
                    stopped = true;
                }
            }

            void Menu()
            {
                Console.Write("Choose what to do, input the corresponding number:\n" +
                              "1 - Start a new game\n" +
                              "2 - Start a visualized game\n" +
                              "3 - Play an existing game\n" +
                              "4 - Save one game\n" +
                              "5 - Load one game\n" +
                              "6 - Play all games at once\n" +
                              "7 - create 1000 games with size 12\n" +
                              "input: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        int size = NewGameSize();
                        if(size >= 4 && size <= 1000)
                        {
                            stopped = false;
                            gamemanager.PlayOneGame(gamemanager.AddGame(size));
                        }
                        else
                            Console.WriteLine("wrong input");
                        break;
                    case "2":
                        int size2 = NewGameSize();
                        if (size2 >= 4 && size2 <= 1000)
                        {
                            stopped = false;
                            gamemanager.PlayOneGame(gamemanager.AddVisualizedGame(size2), true);
                        }
                        else
                            Console.WriteLine("wrong input");
                        break;
                    case "3":
                        gamemanager.PlayOneGame(GetExistingGameNumber());
                        stopped = false;
                        break;
                    case "4":
                        if(gamemanager.GamesExist())
                            gamemanager.SaveOneGame(gamemanager.GetGameById(GetExistingGameNumber()));
                        else
                            Console.WriteLine("there are no games yet");
                        break;
                    case "5":
                        gamemanager.LoadOneGame();
                        break;
                    case "6":
                        stopped = false;
                        gamemanager.PlayAllGames();
                        break;
                    case "7":
                        gamemanager.AddAmountOfGames(AmountOfGames());
                        break;
                    default:
                        Console.WriteLine("wrong input in switch statement");
                        break;
                }                
            }

            int NewGameSize()
            {
                Console.Clear();
                Console.Write("input field size (4-1000, default = 12) : ");
                try
                {
                    return Int32.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    return 12;
                }
            }

            int AmountOfGames()
            {
                Console.Clear();
                Console.Write("input 1 - 1000 amount of games to create, wrong input defaults to 1 game\n" +
                              "input: ");                
                try
                {
                    int input = Int32.Parse(Console.ReadLine());
                    if (input > 0 && input <= 1000)
                        return input;
                    else
                        throw new Exception();
                }
                catch (Exception)
                {
                    return 1;
                }
            }

            int GetExistingGameNumber()
            {
                Console.WriteLine("input id: ");
                int input = -1;
                gamemanager.ShowAllGames();
                try
                {
                    input = Int32.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("wrong input");
                }
                return input;
            }
        }
    }
}
