using System;
using static GameOfLife.GameManager;
using static GameOfLife.SaveManager;

namespace GameOfLife
{
    public static class User
    {
        private static bool stopped = true;

        public static void Start()
        {
            SetupSaveManager();
            while (true)
            {
                if (stopped)
                {
                    Menu();
                }
                else
                {
                    Console.ReadLine();
                    StopAllGames();
                    stopped = true;
                }
            }
        }

        private static void Menu()
        {
            Console.Write("\nChoose what to do, input the corresponding number:\n" +
                          "1 - Start a new game\n" +
                          "2 - Start a new visualized game\n" +
                          "3 - Play an existing game\n" +
                          "4 - Play an existing game visualized\n" +
                          "5 - Save one game\n" +
                          "6 - Load one game\n" +
                          "7 - Play all games at once in the background\n" +
                          "8 - Create up to 1000 games with size 12\n" +
                          "9 - Save all games\n" +
                          "10 - Load all games\n" +
                          "11 - Choose multiple games to display and run the rest in the background\n" +
                          "input: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    int size = NewGameSize();
                    if (size >= 4 && size <= 1000)
                    {
                        stopped = false;
                        PlayOneGame(AddGame(size));
                        Console.WriteLine("Playing a game in the background");
                    }
                    else
                        Console.WriteLine("invalid game size");
                    break;
                case "2":
                    int size2 = NewGameSize();
                    if (size2 >= 4 && size2 <= 1000)
                    {
                        stopped = false;
                        PlayOneGame(AddVisualizedGame(size2), true);
                    }
                    else
                        Console.WriteLine("invalid game size");
                    break;
                case "3":
                    PlayOneGame(GetExistingGameNumber());
                    stopped = false;
                    break;
                case "4":
                    PlayOneGame(GetExistingGameNumber(), true);
                    stopped = false;
                    break;
                case "5":
                    if (GamesExist())// checks if there are more than 0 games created
                        SaveOneGame(GetGameById(GetExistingGameNumber()));
                    else
                        Console.WriteLine("there are no games yet");
                    break;
                case "6":
                    LoadOneGame();
                    break;
                case "7":
                    stopped = false;
                    PlayAllGames();
                    break;
                case "8":
                    AddAmountOfGames(AmountOfGamesToCreate());
                    break;
                case "9":
                    SaveAllGames();
                    break;
                case "10":
                    LoadAllGames();
                    break;
                case "11":
                    stopped = false;
                    int[] games = new int[NumberOfGamesToShow()];
                    games = GetExistingGameNumberArray(games.Length);
                    PlayAllGamesAndVisualizeSome(games);
                    break;
                default:
                    Console.WriteLine("wrong input :(\n");
                    break;
            }
        }

        private static int NewGameSize()
        {
            Console.Clear();
            Console.Write("input field size (4-1000, default = 12) : ");
            try
            {
                return Int32.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("invalid input, defaulting to 12");
                return 12;
            }
        }

        private static int AmountOfGamesToCreate()
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

        private static int NumberOfGamesToShow()
        {
            Console.Clear();
            Console.Write("input 1 - 20 games to show, wrong input defaults to 1 game\n" +
                          "input: ");
            try
            {
                int input = Int32.Parse(Console.ReadLine());
                if (input > 0 && input <= 20)
                    return input;
                else
                    throw new Exception();
            }
            catch (Exception)
            {
                return 1;
            }
        }

        private static int GetExistingGameNumber()
        {
            int input = -1;
            if (PrintAllGameIds())
            {
                Console.Write("input id: ");
                try
                {
                    input = Int32.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("wrong input");
                }
            }
            return input;
        }

        private static int[] GetExistingGameNumberArray(int size)
        {
            int[] ids = new int[size];

            if (GamesExist())
            {
                for (int i = 0; i < size; i++)
                {
                    ids[i] = GetExistingGameNumber();
                    if (ids[i] == -1)
                        break;
                }
            }

            return ids;
        }
    }
}
