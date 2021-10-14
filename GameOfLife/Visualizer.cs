using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Visualizer
    {
        public static void Visualize(bool[,] grid, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int o = 0; o < size; o++)
                {
                    if (grid[i, o] == true)
                        Console.Write("   X");//live cell
                    else
                        Console.Write("    ");//dead cell
                }
                Console.WriteLine("\n");
            }
        }
        public static void ShowInfo(int iterations, int cells, int id)
        {
            Console.Write($"Id: {id}, Iteration #{iterations}, Live cells {cells}\n");
        }
    }
}
