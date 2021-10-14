using System;
using System.Threading;

namespace GameOfLife
{
    public class Game
    {
        private int size;
        private bool[,] grid;
        private bool[,] gridTemporary;
        private bool isActive = true;
        private int gameId;
        public int liveCells = 0;
        public int iterationCount = 1;
        private bool visualized = false; // if this is true it means that this game should be visible in the console
        private static int totalGames;
        public static int TotalGames
        {
            get { return totalGames; }
        }
        public int GameId
        {
            get { return gameId; }
        }

        /// <summary>
        /// Input the size of the grid
        /// </summary>
        /// <param name="size">size of the grid</param>
        public Game(int size)
        {
            gameId = totalGames;
            totalGames++;
            this.size = size;
            grid = new bool[size, size];
            gridTemporary = new bool[size, size];
            GenerateGrid();
        }

        private void GenerateGrid()
        {            
            var rand = new Random();
            for (int i = 0; i < size; i++)
            {
                for (int o = 0; o < size; o++)
                {
                    if (rand.Next(6) == 1)
                    {
                        grid[i, o] = true;
                        liveCells++;
                    }
                    else
                        grid[i, o] = false;
                    gridTemporary[i, o] = false;
                }
            }
        }
        public void SelectToVisualize()
        {
            visualized = true;
        }
        public void StopVisualizing()
        {
            visualized = false;
        }

        public void PlayGame()
        {
            while (isActive && iterationCount < 200)
            {
                if (liveCells <= 0)
                    GameLost();
                if(visualized)
                {
                    Console.Clear();
                    Visualizer.Visualize(grid, size);
                    Visualizer.ShowInfo(iterationCount, liveCells, gameId);
                }
                Iterate();
                Thread.Sleep(1000);
            }
        }
        public void Stop()
        {
            isActive = false;
        }
        public void Activate()
        {
            isActive = true;
        }
        private void GameLost()
        {
            isActive = false;
            Console.WriteLine("All cells dead");
        }
        private void Iterate()
        {
            if (liveCells > 0 && iterationCount <= 200)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int o = 0; o < size; o++)
                    {
                        byte liveNeighbors = CountLiveNeighbors(i, o);
                        if (grid[i, o] == true) // if the current cell is alive
                        {
                            if (liveNeighbors < 2 || liveNeighbors > 3)// cell dies
                            {
                                gridTemporary[i, o] = false;
                                liveCells--;
                            }
                            else// cell lives
                            {
                                gridTemporary[i, o] = true;
                            }
                        }
                        else// if the current cell is dead
                        {
                            if (liveNeighbors == 3)//cell repopulates
                            {
                                gridTemporary[i, o] = true;
                                liveCells++;
                            }
                            else// cell stays dead
                                gridTemporary[i, o] = false;
                        }
                    }
                }
                iterationCount++;
                grid = gridTemporary;
            }
            else
            {
                GameLost();
            }
        }
        // returns the count of live neighbors
        private byte CountLiveNeighbors(int width, int height)
        {
            byte liveNeighbors = 0;
            //2 for loops go trough a 3x3 area surrounding the given cell
            for (int x = width - 1; x < width + 2; x++)
            {
                for (int y = height - 1; y < height + 2; y++)
                {
                    //checks if it is within the array borders
                    if ((x >= 0 && y >= 0) && (x < size && y < size))
                    {
                        if (x == width && y == height)//checks if it is attempting to count itself
                            continue;
                        else
                        {
                            if (grid[x, y] == true)
                                liveNeighbors++;
                        }
                    }
                }
            }
            return liveNeighbors;
        }
    }
}
