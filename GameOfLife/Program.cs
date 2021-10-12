using System;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(12, 12);
            game.SelectGameToVisualize();
            game.StartGame();
        }
    }
}
