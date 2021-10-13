using System;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game;
            MainMenu();

            void MainMenu()
            {
                Console.Write("Please input the size of the field 4-1000\n" +
                              "Width(columns): ");
                string inputWidth = Console.ReadLine();
                Console.Write("Height(rows): ");
                string inputHeight = Console.ReadLine();

                try
                {
                    int width = Int32.Parse(inputWidth);
                    int height = Int32.Parse(inputHeight);

                    if(width > 3 && width < 1000 && height > 3 && height < 1000)
                    {
                        game = new Game(width, height);
                        game.SelectGameToVisualize();
                        game.StartGame();
                    }                    
                }
                catch (Exception)
                {
                    Console.WriteLine("wrong input");
                }
            }            
        }
    }
}
