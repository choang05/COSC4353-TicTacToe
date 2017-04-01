using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Grid
{
    public static int GridSize;
    public static GridPoint[,] GridPoints;
    public static List<GridPoint> EmptyGridPoints;
    public static List<GridPoint> OccupiedGridPoints;

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

        public GridPoint GetTopNeighbor()
        {
            return GridPoints[xCoord, yCoord - 1];
        }
        public GridPoint GetRightNeighbor()
        {
            return GridPoints[xCoord + 1, yCoord];
        }
        public GridPoint GetBottomNeighbor()
        {
            return GridPoints[xCoord, yCoord + 1];
        }
        public GridPoint GetLeftNeighbor()
        {
            return GridPoints[xCoord - 1, yCoord];
        }
        public GridPoint GetTopToBottomRightNeighbor()
        {
            return GridPoints[xCoord + 1, yCoord + 1];
        }
        public GridPoint GetTopToBottomLeftNeighbor()
        {
            return GridPoints[xCoord - 1, yCoord + 1];
        }
        public GridPoint GetBottomToTopRightNeighbor()
        {
            return GridPoints[xCoord + 1, yCoord - 1];
        }
        public GridPoint GetBottomToTopLeftNeighbor()
        {
            return GridPoints[xCoord - 1, yCoord - 1];
        }
    }

    //  Constructor
    public Grid(int gridSize)
    {
        if (gridSize < 3)
            return;

        GridSize = gridSize;
        GenerateGrid(GridSize);

        EmptyGridPoints = GridPoints.Cast<GridPoint>().ToList();
        OccupiedGridPoints = new List<GridPoint>();
    }

    #region GenerateGrid(): Generate the grid points with default values
    public void GenerateGrid(int gridSize)
    {
        GridPoints = new GridPoint[gridSize, gridSize];

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                GridPoints[x, y] = new GridPoint(x, y, false, GridPoint.InputType.NULL);
            }
        }
    }
    #endregion

    #region PrintCurrentGrid(): Prints the current state of the grid
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
                if (GridPoints[x, y].input == GridPoint.InputType.NULL)
                {
                    Console.Write("[" + " " + "]");
                    //Console.Write("(" + GridPoints[x, y].xCoord + GridPoints[x, y].yCoord + ")");
                }
                else
                {
                    Console.Write("[" + GridPoints[x, y].input + "]");
                    //Console.Write("(" + GridPoints[x, y].xCoord + GridPoints[x, y].yCoord + ")");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();    //  Skip a line
    }
    #endregion

    #region InputGridPoint(): Inputs a inputtype in the GridPoint at the given x, y, and the inputtype 
    public static void InputGridPoint(int x, int y, GridPoint.InputType inputType)
    {
        GridPoints[x, y].input = inputType;
        GridPoints[x, y].isOccupied = true;

        EmptyGridPoints = GetEmptyGridPoints();
        //Console.WriteLine(EmptyGridPoints.Count);

        //  Update taken gridpoints
        OccupiedGridPoints.Add(GridPoints[x, y]);
    }
    #endregion 

    #region IsGridFull(): returns true if grid no longer has any spaces left. Otherwise, return false
    public static bool IsGridFull()
    {
        if (EmptyGridPoints.Count <= 0)
        {
            return true;
        }
        return false;
    }
    #endregion

    #region GetEmptyGridPoints(): Returns all gridpoints that are empty
    public static List<GridPoint> GetEmptyGridPoints()
    {
        List<GridPoint> emptyGridPoints = new List<GridPoint>();
        for (int y = 0; y < GridSize; y++)
        {
            for (int x = 0; x < GridSize; x++)
            {
                if (!GridPoints[x,y].isOccupied)
                {
                    emptyGridPoints.Add(GridPoints[x, y]);
                }
            }
        }
        return emptyGridPoints;
    }
    #endregion
}
