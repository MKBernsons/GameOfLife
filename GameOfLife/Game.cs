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
        private int liveCells = 0;
        private int iterationCount = 1;
        private bool visualized = false; // if this is true it means that this game should be visible in the console
        private static int totalLiveCells = 0;
        private static int totalGames = 0;

        public static int TotalLiveCells
        {
            get { return totalLiveCells; }
            set { totalLiveCells = value; }
        }
        public static int TotalGames
        {
            get { return totalGames; }
            set { totalGames = value; }
        }
        public int GameId
        {
            get { return gameId; }
        }
        public int Size { get => size; set => size = value; }
        public bool[,] Grid { get => grid; set => grid = value; }
        public bool[,] GridTemporary { get => gridTemporary; set => gridTemporary = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public bool Visualized { get => visualized; set => visualized = value; }
        public int LiveCells { get => liveCells; set => liveCells = value; }
        public int IterationCount { get => iterationCount; set => iterationCount = value; }

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
                        LiveCells++;
                        totalLiveCells++;
                    }
                    else
                        grid[i, o] = false;
                    gridTemporary[i, o] = false;
                }
            }
        }

        public void StartVisualizing()
        {
            visualized = true;
        }

        public void StopVisualizing()
        {
            visualized = false;
        }

        public void PlayGame()
        {
            bool lost = false;
            while (isActive && IterationCount <= 200)
            {
                if (LiveCells <= 0)
                {
                    IsActive = false;
                    lost = true;
                }
                    
                if(visualized)
                {
                    Console.Clear();
                    Visualizer.Visualize(grid, size);
                    Visualizer.ShowInfo(IterationCount, LiveCells, gameId);
                }
                Iterate();
                Thread.Sleep(1000);
            }
            if (lost)
                GameLost();
            else
                Stop();
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
            Stop();
            Console.WriteLine("All cells died");
        }

        private void Iterate()
        {
            if (LiveCells > 0 && IterationCount <= 200)
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
                                LiveCells--;
                                totalLiveCells--;
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
                                LiveCells++;
                                totalLiveCells++;
                            }
                            else// cell stays dead
                                gridTemporary[i, o] = false;
                        }
                    }
                }
                IterationCount++;
                grid = gridTemporary;
            }
        }

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
