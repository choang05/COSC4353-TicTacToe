using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Grid
{
    public static int GridSize;
    public static GridPoint[,] GridPoints;

    public struct GridPoint
    {
        public int xCoord;
        public int yCoord;
        public bool isOccupied;

        public InputType input;
        public enum InputType  {   X, O, NULL    };

        //  Constructor
        public GridPoint(int xCoord, int yCoord, bool isOccupied, InputType input)
        {
            this.xCoord = xCoord;
            this.yCoord = yCoord;
            this.isOccupied = isOccupied;
            this.input = input;
        }
    }

    //  Constructor
    public Grid(int gridSize)
    {
        GridSize = gridSize;
        GenerateGrid(GridSize);
    }

    //  Generate the grid points with default values
    public void GenerateGrid(int gridSize)
    {
        GridPoints = new GridPoint[gridSize, gridSize];

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                GridPoints[x, y] = new GridPoint(x, y, false, GridPoint.InputType.NULL);
            }
        }
    }

    //  Prints the current state of the grid
    public void PrintCurrentGrid()
    {
        Console.WriteLine();    //  Skip a line

        Console.Write("   ");
        for (int row = 0; row < GridSize; row++)
            Console.Write(row + "  ");

        Console.WriteLine();

        for (int y = 0; y < GridSize; y++)
        {
            Console.Write(y + " ");
            for (int x = 0; x < GridSize; x++)
            {
                if (GridPoints[y, x].input == GridPoint.InputType.NULL)
                {
                    Console.Write("[" + " " + "]");
                    //Console.Write("[" + GridPoints[x, y].xCoord + GridPoints[x, y].yCoord + "]");
                }
                else
                    Console.Write("[" + GridPoints[x,y].input + "]");
            }
            Console.WriteLine();
        }
        Console.WriteLine();    //  Skip a line
    }
}
