using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            printableGrid += $"^ Id: {id}, Iteration #{iterations}, Live cells {cells} ^\n" +
                             $"-----------------------------------------------\n";
            Console.WriteLine(printableGrid);
        }
    }
}
