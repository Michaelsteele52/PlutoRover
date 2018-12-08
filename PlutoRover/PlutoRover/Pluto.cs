using System;
using System.Collections.Generic;
using System.Text;

namespace PlutoRover
{
    public class Pluto
    {
        public Grid[][] Grid;
        public int XSize { get; }
        public int YSize { get; }
        public Pluto(int xSize, int ySize)
        {
            XSize = xSize;
            YSize = ySize;
            Grid = new Grid[xSize][];
            for (int i = 0; i < xSize; i++)
            {
                Grid[i] = new Grid[ySize];
                for (int j = 0; j < ySize; j++)
                {
                    Grid[i][j] = new Grid();
                }
            }
        }
    }

    public class Grid
    {
        public bool Obstacle { get; set; }
    }
}
