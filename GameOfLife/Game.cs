using System;
using System.Threading;

namespace GameOfLife
{
    class Game
    {
        private int size;
        private bool[,] grid;
        private bool[,] gridTemporary;
        private bool isActive = false;
        private int liveCells = 0;
        private int IterationCount = 0;
        private bool isSelected = false; // if this is true it means that this game should be visible in the console

        /// <summary>
        /// Input the size of the grid
        /// </summary>
        /// <param name="size">size of the grid</param>
        public Game(int size)
        {
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
        public void SelectGameToVisualize()
        {
            isSelected = true;
        }
        public void StartGame()
        {
            isActive = true;
            if (isSelected)
            {
                while (isActive)
                {
                    Visualize();
                    Iterate();
                    Thread.Sleep(1000);
                }
            }
            else
            {
                while (isActive)
                {
                    Iterate();
                    Thread.Sleep(1000);
                }
            }
        }
        public void StopGame()
        {
            isActive = false;
        }
        //Prints the field to the console
        public void Visualize()
        {
            Console.Clear();
            for (int i = 0; i < size; i++)
            {
                for (int o = 0; o < size; o++)
                {
                    if(grid[i, o] == true)
                        Console.Write("   X");//live cell
                    else
                        Console.Write("    ");//dead cell
                }
                Console.WriteLine("\n");
            }
            ShowInfo();
        }
        public void ShowInfo()
        {
            Console.Write($"Iteration #{IterationCount}, Live cells {liveCells}\n");
        }
        public void Iterate()
        {
            if (liveCells > 0)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int o = 0; o < size; o++)
                    {
                        byte liveNeighbors = CountLiveNeighbors(i, o);
                        if(grid[i, o] == true) // if the current cell is alive
                        {
                            if(liveNeighbors < 2 || liveNeighbors > 3)// cell dies
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
                IterationCount++;
                grid = gridTemporary;
            }
            else
            {
                isActive = false;
                Console.WriteLine("All cells dead");
            }
        }
        // returns the count of live neighbors
        public byte CountLiveNeighbors(int width, int height)
        {
            byte liveNeighbors = 0;
            //2 for loops go trough a 3x3 area surrounding the given cell
            for (int x = width - 1; x < width + 2; x++)
            {
                for (int y = height - 1; y < height + 2; y++)
                {
                    //checks if it is within the array borders
                    if((x >= 0 && y >= 0) && (x < this.size && y < this.size))
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
