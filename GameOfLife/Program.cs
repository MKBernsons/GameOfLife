using System;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game;
            Menu();

            void Menu()
            {
                Console.Write("Choose what to do, input the corresponding number:\n" +
                              "1 - Start a new game\n" +
                              "input: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        int size = NewGameSize();
                        if(size > 3 && size < 1000)
                        {
                            game = new Game(size);
                            game.SelectGameToVisualize();
                            game.StartGame();
                        }
                        else
                            Console.WriteLine("wrong input");
                        break;
                    default:
                        Console.WriteLine("wrong input");
                        break;
                }                
            }
            int NewGameSize()
            {
                Console.Clear();
                Console.Write("input field size (4-1000): ");
                return Int32.Parse(Console.ReadLine());
            }
        }
    }
}
