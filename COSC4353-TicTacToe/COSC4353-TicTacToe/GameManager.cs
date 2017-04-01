using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameManager
{
    public int roundCounter;
    public int playerScore;
    public int computerScore;

    public TurnState curTurn;
    public TurnState firstPick;
    public enum TurnState
    {
        PlayerTurn, ComputerTurn, NULL
    };

    public enum GameConditions
    {
        PlayerWins, ComputerWins, Tie, NULL
    };

    enum Direction
    {
        Top, Right, Bottom, Left, 
        DiagonalTopToBottomLeft, DiagonalTopToBottomRight, DiagonalBottomToTopLeft, DiagonalBottomToTopRight
    };

    //  Constructor
    public GameManager()
    {
        roundCounter = 1;
        playerScore = 0;
        computerScore = 0;
        curTurn = TurnState.PlayerTurn;
        firstPick = TurnState.PlayerTurn;
    }

    #region IsBoardSizeBigEnough(): returns true if the board size is greater then 3... decide winner by default and set a new game.
    public bool IsBoardSizeBigEnough()
    {
        //  if the board size is less then 3... decide winner by default and set a new game.
        if (Grid.GridSize < 3)
        {
            if (firstPick == TurnState.PlayerTurn)
                ProcessPlayerWin();
            else
                ProcessComputerWin();

            //SetupNextRound();

            return false;
        }
        return true;
    }
    #endregion

    #region SetupNextRound(): Sets up the game for another round and alternates first pick
    public void SetupNextRound()
    {
        roundCounter++;

        //  Alternate who goes first next round
        if (firstPick == TurnState.PlayerTurn)
        {
            firstPick = TurnState.ComputerTurn;
            curTurn = TurnState.ComputerTurn;
        }
        else
        {
            firstPick = TurnState.PlayerTurn;
            curTurn = TurnState.PlayerTurn;
        }
    }
    #endregion

    #region ProcessComputerTurn(): Process the computer's turn. Determine actions based on difficulty
    public void ProcessComputerTurn(int compDifficulty)
    {
        #region  EASY - randomly input empty space
        if (compDifficulty == 1)
        {
            Random random = new Random();
            int randomIndex = random.Next(0, Grid.EmptyGridPoints.Count);

            Console.WriteLine("I choose Row " + Grid.EmptyGridPoints[randomIndex].yCoord + " and Column " + Grid.EmptyGridPoints[randomIndex].xCoord);

            Grid.InputGridPoint(Grid.EmptyGridPoints[randomIndex].xCoord, Grid.EmptyGridPoints[randomIndex].yCoord, Grid.GridPoint.InputType.O);
        }
        #endregion

        #region  MEDIUM - Computer checks if it can win next move
        else if (compDifficulty == 2)
        {
            //  Go through each empty cell
            Grid.GridPoint mostPotentialGridPoint = Grid.EmptyGridPoints[0];
            int curHighestWeight = 0;

            //  For each empty space, find the a gridpoint with neighboring O's
            for (int i = 0; i < Grid.EmptyGridPoints.Count; i++)
            {
                //  Create a weight of the gridpoint based on neighboring inputs
                int weight = 0;
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.Top, Grid.GridPoint.InputType.O);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.Right, Grid.GridPoint.InputType.O);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.Bottom, Grid.GridPoint.InputType.O);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.Left, Grid.GridPoint.InputType.O);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.DiagonalTopToBottomRight, Grid.GridPoint.InputType.O);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.DiagonalTopToBottomLeft, Grid.GridPoint.InputType.O);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.DiagonalBottomToTopRight, Grid.GridPoint.InputType.O);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.DiagonalBottomToTopLeft, Grid.GridPoint.InputType.O);

                //  if the gridpoints weight is higher then the current, update the most potential gridpoint and current weight
                if (weight > curHighestWeight)
                {
                    mostPotentialGridPoint = Grid.EmptyGridPoints[i];
                    curHighestWeight = weight;
                }
            }

            //  Input the new gridpoint
            Grid.InputGridPoint(mostPotentialGridPoint.xCoord, mostPotentialGridPoint.yCoord, Grid.GridPoint.InputType.O);
        }
        #endregion

        #region HARD - Computer always tries to pick spots to win game & block player cells
        else if (compDifficulty == 3)
        {
            //  Go through each empty cell
            Grid.GridPoint mostPotentialGridPoint = Grid.EmptyGridPoints[0];
            int curHighestWeight = 0;

            //  For each empty space, find the a gridpoint with neighboring O's
            for (int i = 0; i < Grid.EmptyGridPoints.Count; i++)
            {
                //  Create a weight of the gridpoint based on neighboring inputs
                int weight = 0;
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.Top, Grid.GridPoint.InputType.O);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.Right, Grid.GridPoint.InputType.O);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.Bottom, Grid.GridPoint.InputType.O);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.Left, Grid.GridPoint.InputType.O);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.DiagonalTopToBottomRight, Grid.GridPoint.InputType.O);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.DiagonalTopToBottomLeft, Grid.GridPoint.InputType.O);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.DiagonalBottomToTopRight, Grid.GridPoint.InputType.O);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.DiagonalBottomToTopLeft, Grid.GridPoint.InputType.O);
                //  Add weights for empty spaces
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.Top, Grid.GridPoint.InputType.NULL);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.Right, Grid.GridPoint.InputType.NULL);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.Bottom, Grid.GridPoint.InputType.NULL);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.Left, Grid.GridPoint.InputType.NULL);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.DiagonalTopToBottomRight, Grid.GridPoint.InputType.NULL);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.DiagonalTopToBottomLeft, Grid.GridPoint.InputType.NULL);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.DiagonalBottomToTopRight, Grid.GridPoint.InputType.NULL);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.DiagonalBottomToTopLeft, Grid.GridPoint.InputType.NULL);
                //  Add weights for player spaces to block them
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.Top, Grid.GridPoint.InputType.X);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.Right, Grid.GridPoint.InputType.X);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.Bottom, Grid.GridPoint.InputType.X);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.Left, Grid.GridPoint.InputType.X);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.DiagonalTopToBottomRight, Grid.GridPoint.InputType.X);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.DiagonalTopToBottomLeft, Grid.GridPoint.InputType.X);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.DiagonalBottomToTopRight, Grid.GridPoint.InputType.X);
                weight += GetLength(Grid.GridPoints[Grid.EmptyGridPoints[i].xCoord, Grid.EmptyGridPoints[i].yCoord], Direction.DiagonalBottomToTopLeft, Grid.GridPoint.InputType.X);

                //  if the gridpoints weight is higher then the current, update the most potential gridpoint and current weight
                if (weight > curHighestWeight)
                {
                    mostPotentialGridPoint = Grid.EmptyGridPoints[i];
                    curHighestWeight = weight;
                }
            }

            //  Input the new gridpoint
            Grid.InputGridPoint(mostPotentialGridPoint.xCoord, mostPotentialGridPoint.yCoord, Grid.GridPoint.InputType.O);

        }
        #endregion
    }
    #endregion

    #region GetWinCondition(): Returns the game's current win condition if any
    public GameConditions GetWinCondition()
    {
        //Console.WriteLine(GetLength(1, Grid.GridPoints[0,0], Direction.Right, Grid.GridPoints[0,0].input) + " ");
        //  Loop through grid and check for completed inputs
        for (int i = 0; i < Grid.OccupiedGridPoints.Count; i++)
        {
            //Console.WriteLine("(" + Grid.OccupiedGridPoints[i].xCoord + "," + Grid.OccupiedGridPoints[i].yCoord + "): " + Grid.OccupiedGridPoints[i].input);
            //if (Grid.OccupiedGridPoints[i].input == Grid.GridPoint.InputType.X)
            //{
            //    Console.WriteLine("  Top: " + GetLength(Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.Top, Grid.GridPoint.InputType.X));
            //    Console.WriteLine("  Right: " + GetLength(Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.Right, Grid.GridPoint.InputType.X));
            //    Console.WriteLine("  Bottom: " + GetLength(Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.Bottom, Grid.GridPoint.InputType.X));
            //    Console.WriteLine("  Left: " + GetLength(Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.Left, Grid.GridPoint.InputType.X));
            //}

            //  Check board for completed 'X's
            if ((Grid.OccupiedGridPoints[i].input == Grid.GridPoint.InputType.X && GetLength(Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.Top, Grid.GridPoint.InputType.X) >= Grid.GridSize - 1)
                || (Grid.OccupiedGridPoints[i].input == Grid.GridPoint.InputType.X && GetLength(Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.Right, Grid.GridPoint.InputType.X) >= Grid.GridSize - 1)
                || (Grid.OccupiedGridPoints[i].input == Grid.GridPoint.InputType.X && GetLength(Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.Bottom, Grid.GridPoint.InputType.X) >= Grid.GridSize - 1)
                || (Grid.OccupiedGridPoints[i].input == Grid.GridPoint.InputType.X && GetLength(Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.Left, Grid.GridPoint.InputType.X) >= Grid.GridSize - 1)
                || (Grid.OccupiedGridPoints[i].input == Grid.GridPoint.InputType.X && GetLength(Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.DiagonalTopToBottomRight, Grid.GridPoint.InputType.X) >= Grid.GridSize - 1)
                || (Grid.OccupiedGridPoints[i].input == Grid.GridPoint.InputType.X && GetLength(Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.DiagonalTopToBottomLeft, Grid.GridPoint.InputType.X) >= Grid.GridSize - 1)
                || (Grid.OccupiedGridPoints[i].input == Grid.GridPoint.InputType.X && GetLength(Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.DiagonalBottomToTopRight, Grid.GridPoint.InputType.X) >= Grid.GridSize - 1)
                || (Grid.OccupiedGridPoints[i].input == Grid.GridPoint.InputType.X && GetLength(Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.DiagonalBottomToTopLeft, Grid.GridPoint.InputType.X) >= Grid.GridSize - 1))
            {
                return GameConditions.PlayerWins;
            }
            //  Check board for completed 'O's
            else if ((Grid.OccupiedGridPoints[i].input == Grid.GridPoint.InputType.O && GetLength(Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.Top, Grid.GridPoint.InputType.O) >= Grid.GridSize - 1)
                || (Grid.OccupiedGridPoints[i].input == Grid.GridPoint.InputType.O && GetLength(Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.Right, Grid.GridPoint.InputType.O) >= Grid.GridSize - 1)
                || (Grid.OccupiedGridPoints[i].input == Grid.GridPoint.InputType.O && GetLength(Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.Bottom, Grid.GridPoint.InputType.O) >= Grid.GridSize - 1)
                || (Grid.OccupiedGridPoints[i].input == Grid.GridPoint.InputType.O && GetLength(Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.Left, Grid.GridPoint.InputType.O) >= Grid.GridSize - 1)
                || (Grid.OccupiedGridPoints[i].input == Grid.GridPoint.InputType.O && GetLength(Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.DiagonalTopToBottomRight, Grid.GridPoint.InputType.O) >= Grid.GridSize - 1)
                || (Grid.OccupiedGridPoints[i].input == Grid.GridPoint.InputType.O && GetLength(Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.DiagonalTopToBottomLeft, Grid.GridPoint.InputType.O) >= Grid.GridSize - 1)
                || (Grid.OccupiedGridPoints[i].input == Grid.GridPoint.InputType.O && GetLength(Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.DiagonalBottomToTopRight, Grid.GridPoint.InputType.O) >= Grid.GridSize - 1)
                || (Grid.OccupiedGridPoints[i].input == Grid.GridPoint.InputType.O && GetLength(Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.DiagonalBottomToTopLeft, Grid.GridPoint.InputType.O) >= Grid.GridSize - 1))
            {
                return GameConditions.ComputerWins;
            }
            //  Else if the board is full and all the points have been checked... then its a tie
            else if (i >= Grid.GridPoints.Length-1 && Grid.IsGridFull())
            {
                return GameConditions.Tie;
            }
        }

        return GameConditions.NULL;
    }
    #endregion

    #region GetLength(): Recursivly return the length of the input type in a given direction and input type
    private int GetLength(Grid.GridPoint gridPoint, Direction dir, Grid.GridPoint.InputType inputType)
    {
        #region Check Sides
        //  Recursivly count top neighbors
        if (dir == Direction.Top && gridPoint.yCoord - 1 >= 0 && Grid.GridPoints[gridPoint.xCoord, gridPoint.yCoord-1].input == inputType)
            return 1 + GetLength(gridPoint.GetTopNeighbor(), dir, inputType);

        //  Recursivly count right neighbors
        else if (dir == Direction.Right && gridPoint.xCoord + 1 < Grid.GridSize && Grid.GridPoints[gridPoint.xCoord + 1, gridPoint.yCoord].input == inputType)
            return 1 + GetLength(gridPoint.GetRightNeighbor(), dir, inputType);

        //  Recursivly count bottom neighbors
        else if (dir == Direction.Bottom && gridPoint.yCoord + 1 < Grid.GridSize && Grid.GridPoints[gridPoint.xCoord, gridPoint.yCoord + 1].input == inputType)
            return 1 + GetLength(gridPoint.GetBottomNeighbor(), dir, inputType);

        //  Recursivly count left neighbors
        else if (dir == Direction.Left && gridPoint.xCoord - 1 >= 0 && Grid.GridPoints[gridPoint.xCoord - 1, gridPoint.yCoord].input == inputType)
            return 1 + GetLength(gridPoint.GetLeftNeighbor(), dir, inputType);
        #endregion

        #region Check Digonals
        //  Recursivly count diagonal top-to-bottom right neighbors
        else if (dir == Direction.DiagonalTopToBottomRight 
            && gridPoint.xCoord + 1 < Grid.GridSize && gridPoint.yCoord + 1 < Grid.GridSize 
            && Grid.GridPoints[gridPoint.xCoord + 1, gridPoint.yCoord + 1].input == inputType)
            return 1 + GetLength(gridPoint.GetTopToBottomRightNeighbor(), dir, inputType);
        
        //  Recursivly count diagonal top-to-bottom left neighbors
        else if (dir == Direction.DiagonalTopToBottomLeft
            && gridPoint.xCoord - 1 >= 0 && gridPoint.yCoord + 1 < Grid.GridSize
            && Grid.GridPoints[gridPoint.xCoord - 1, gridPoint.yCoord + 1].input == inputType)
            return 1 + GetLength(gridPoint.GetTopToBottomLeftNeighbor(), dir, inputType);
        
        //  Recursivly count diagonal bottom-to-top right neighbors
        else if (dir == Direction.DiagonalBottomToTopRight 
            && gridPoint.xCoord + 1 < Grid.GridSize && gridPoint.yCoord - 1 >= 0
            && Grid.GridPoints[gridPoint.xCoord + 1, gridPoint.yCoord - 1].input == inputType)
            return 1 + GetLength(gridPoint.GetBottomToTopRightNeighbor(), dir, inputType);

        //  Recursivly count diagonal bottom-to-top left neighbors
        else if (dir == Direction.DiagonalBottomToTopLeft 
            && gridPoint.xCoord - 1 >= 0 && gridPoint.yCoord - 1 >= 0
            && Grid.GridPoints[gridPoint.xCoord - 1, gridPoint.yCoord - 1].input == inputType)
            return 1 + GetLength(gridPoint.GetBottomToTopLeftNeighbor(), dir, inputType);
        #endregion

        else
            return 0;
    }
    #endregion

    #region ProcessPlayerWin(): Process things when player wins
    public void ProcessPlayerWin()
    {
        Console.WriteLine("\nYou win!");
        playerScore++;
    }
    #endregion

    #region ProcessComputerWin(): Process things when computer wins
    public void ProcessComputerWin()
    {
        Console.WriteLine("\nI win!");
        computerScore++;
    }
    #endregion

    #region ProcessTie(): Process things when round ties
    public void ProcessTie()
    {
        Console.WriteLine("\nIt's a tie!");
    }
    #endregion
}
