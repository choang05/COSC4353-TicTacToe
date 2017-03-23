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

    Direction direction;
    enum Direction
    {
        Top, Right, Bottom, Left
    };

    public GameManager()
    {
        roundCounter = 1;
        playerScore = 0;
        computerScore = 0;
        curTurn = TurnState.PlayerTurn;
        firstPick = TurnState.PlayerTurn;
    }

    //  returns true if the board size is greater then 3... decide winner by default and set a new game.
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

    #region Process the computer's turn. Determine actions based on difficulty
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

        #region  MEDIUM
        else if (compDifficulty == 2)
        {

        }
        #endregion

        #region HARD
        else if (compDifficulty == 3)
        {

        }
        #endregion
    }
    #endregion

    #region Returns the game condition
    public GameConditions GetWinCondition()
    {
        //Console.WriteLine(GetLength(1, Grid.GridPoints[0,0], Direction.Right, Grid.GridPoints[0,0].input));

        //  Loop through grid and check for completed inputs
        for (int i = 0; i < Grid.OccupiedGridPoints.Count; i++)
        {
            //  Check board for completed 'X's
            if (GetLength(1, Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.Top, Grid.GridPoint.InputType.X) >= Grid.GridSize-1
                || GetLength(1, Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.Right, Grid.GridPoint.InputType.X) >= Grid.GridSize - 1
                || GetLength(1, Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.Bottom, Grid.GridPoint.InputType.X) >= Grid.GridSize - 1
                || GetLength(1, Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.Left, Grid.GridPoint.InputType.X) >= Grid.GridSize - 1)
            {
                return GameConditions.PlayerWins;
            }
            //  Check board for completed 'O's
            else if (GetLength(1, Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.Top, Grid.GridPoint.InputType.O) >= Grid.GridSize - 1
                || GetLength(1, Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.Right, Grid.GridPoint.InputType.O) >= Grid.GridSize - 1
                || GetLength(1, Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.Bottom, Grid.GridPoint.InputType.O) >= Grid.GridSize - 1
                || GetLength(1, Grid.GridPoints[Grid.OccupiedGridPoints[i].xCoord, Grid.OccupiedGridPoints[i].yCoord], Direction.Left, Grid.GridPoint.InputType.O) >= Grid.GridSize - 1)
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

    //  Recursivly return the length of the input type in a given direction and input type
    private int GetLength(int length, Grid.GridPoint gridPoint, Direction dir, Grid.GridPoint.InputType inputType)
    {
        //  Recursivly count top neighbors
        if (dir == Direction.Top && gridPoint.yCoord - 1 >= 0 && Grid.GridPoints[gridPoint.xCoord, gridPoint.yCoord-1].input == inputType)
            return length + GetLength(length, gridPoint.GetTopNeighbor(), dir, inputType);

        //  Recursivly count right neighbors
        else if (dir == Direction.Right && gridPoint.xCoord + 1 < Grid.GridSize && Grid.GridPoints[gridPoint.xCoord + 1, gridPoint.yCoord].input == inputType)
            return length + GetLength(length, gridPoint.GetRightNeighbor(), dir, inputType);

        //  Recursivly count bottom neighbors
        else if (dir == Direction.Bottom && gridPoint.yCoord + 1 < Grid.GridSize && Grid.GridPoints[gridPoint.xCoord, gridPoint.yCoord + 1].input == inputType)
            return length + GetLength(length, gridPoint.GetBottomNeighbor(), dir, inputType);

        //  Recursivly count left neighbors
        else if (dir == Direction.Left && gridPoint.xCoord - 1 >= 0 && Grid.GridPoints[gridPoint.xCoord - 1, gridPoint.yCoord].input == inputType)
            return length + GetLength(length, gridPoint.GetLeftNeighbor(), dir, inputType);
        return 0;
    }

    //  Process things when player wins
    public void ProcessPlayerWin()
    {
        Console.WriteLine("\nYou win!");
        playerScore++;
    }

    //  Process things when computer wins
    public void ProcessComputerWin()
    {
        Console.WriteLine("\nI win!");
        computerScore++;
    }

    //  Process things when round ties
    public void ProcessTie()
    {
        Console.WriteLine("\nIt's a tie!");
    }
}
