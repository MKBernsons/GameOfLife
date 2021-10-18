using System;

namespace GameOfLife
{
    public class Visualizer
    {
        public static void Visualize(bool[,] grid, int size, int id, int cells, int iterations)
        {
            string printableGrid = "";

            for (int i = 0; i < size; i++)
            {
                for (int o = 0; o < size; o++)
                {
                    if (grid[i, o] == true)
                        printableGrid += " X";//live cell
                    else
                        printableGrid += "  ";//dead cell
                }
                printableGrid += "\n";
            }
            printableGrid += $"\n^ Id: {id}, Iteration #{iterations}, Live cells {cells} ^\n" +
                             $"-----------------------------------------------\n";
            Console.WriteLine(printableGrid);
        }
    }
}
