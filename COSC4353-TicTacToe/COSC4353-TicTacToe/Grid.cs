﻿using System;
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
    }

    //  Constructor
    public Grid(int gridSize)
    {
        GridSize = gridSize;
        GenerateGrid(GridSize);

        EmptyGridPoints = GridPoints.Cast<GridPoint>().ToList();
        OccupiedGridPoints = new List<GridPoint>();
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

    //  InputGridpoint
    public static void InputGridPoint(int x, int y, GridPoint.InputType inputType)
    {
        GridPoints[x,y].input = inputType;
        GridPoints[x, y].isOccupied = true;

        //  Update empty gridpoints
        EmptyGridPoints.Remove(GridPoints[x,y]);

        //  Update taken gridpoints
        OccupiedGridPoints.Add(GridPoints[x,y]);
    }

    #region returns true if grid no longer has any spaces left. Otherwise, return false
    public bool IsGridFull()
    {
        if (EmptyGridPoints.Count <= 0)
        {
            return true;
        }
        return false;
    }
    #endregion
}
