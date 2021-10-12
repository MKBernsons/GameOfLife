using System;
using System.Threading;

namespace GameOfLife
{
    class Game
    {
        private int width;
        private int height;
        private bool[,] grid;
        private bool[,] gridTemporary;
        private bool isActive = false;
        private int liveCells = 0;
        private bool isSelected = false; // if this is true it means that this game should be visible in the console

        /// <summary>
        /// Input the size of the grid
        /// </summary>
        /// <param name="width">Width of the grid</param>
        /// <param name="height">Height of the grid</param>
        public Game(int width, int height)
        {
            this.width = width;
            this.height = height;
            GenerateGrid();
        }

        private void GenerateGrid()
        {
            grid = new bool[width, height];
            gridTemporary = new bool[width, height];
            var rand = new Random();

            for (int i = 0; i < width; i++)
            {
                for (int o = 0; o < height; o++)
                {
                    if (rand.Next(4) == 1)
                    {
                        grid[i, o] = true;
                        liveCells++;
                    }
                    else
                        grid[i, o] = false;
                    gridTemporary[i, o] = false;
                }
            }
            Console.WriteLine($"Generated with {liveCells} live cells");
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

        //Prints the field to the console
        public void Visualize()
        {
            Console.Clear();
            for (int i = 0; i < width; i++)
            {
                for (int o = 0; o < height; o++)
                {
                    if(grid[i, o] == true)
                        Console.Write("   X");//live cell
                    else
                        Console.Write("    ");//dead cell
                }
                Console.WriteLine("\n");
            }
        }

        public void Iterate()
        {
            if (liveCells > 2)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int o = 0; o < height; o++)
                    {
                        byte liveNeighbors = CountLiveNeighbors(i, o);

                        if(grid[i, o] == true) // if the current cell is alive
                        {
                            if (liveNeighbors < 2) // cell dies due to underpopulation
                            {
                                gridTemporary[i, o] = false;
                                liveCells--;
                            }
                            else if(liveNeighbors == 2 || liveNeighbors == 3) // cell lives
                            {
                                gridTemporary[i, o] = true;
                            }
                            else if(liveNeighbors > 3) // cell dies due to overpopulation
                            {
                                gridTemporary[i, o] = false;
                                liveCells--;
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
                grid = gridTemporary;
            }
            else
            {
                isActive = false;
                Console.WriteLine($"Live cells {liveCells}\n" +
                                  $"isActive {isActive}\n" +
                                  $"isSelected {isSelected}");
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
                    if((x >= 0 && y >= 0) && (x < this.width && y < this.height))
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
