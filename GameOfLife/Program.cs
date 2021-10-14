using System;

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
                              "input: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        int size = NewGameSize();
                        if(size > 3 && size < 1000)
                        {
                            stopped = false;
                            gamemanager.AddGame(size);
                            gamemanager.PlayAllGames();
                        }
                        else
                            Console.WriteLine("wrong input");
                        break;
                    case "2":
                        int size2 = NewGameSize();
                        if (size2 > 3 && size2 < 1000)
                        {
                            stopped = false;
                            gamemanager.AddVisualizedGame(size2);
                            gamemanager.PlayAllGames();
                        }
                        else
                            Console.WriteLine("wrong input");
                        break;
                    case "3":
                        gamemanager.PlayOneGame(GetExistingGameNumber());
                        stopped = false;
                        break;
                    default:
                        Console.WriteLine("wrong input in switch statement");
                        break;
                }                
            }
            int NewGameSize()
            {
                Console.Clear();
                Console.Write("input field size (4-1000): ");
                return Int32.Parse(Console.ReadLine());
            }
            int GetExistingGameNumber()
            {
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
