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

    public GameState curState;
    public GameState firstPick;
    public enum GameState
    {
        PlayerTurn,
        ComputerTurn,
        NULL
    };

    public GameManager()
    {
        roundCounter = 1;
        playerScore = 0;
        computerScore = 0;
        curState = GameState.NULL;
        firstPick = GameState.PlayerTurn;
    }

    //  returns true if the board size is greater then 3... decide winner by default and set a new game.
    public bool CheckBoardSizeConditions()
    {
        //  if the board size is less then 3... decide winner by default and set a new game.
        if (Grid.GridSize < 3)
        {
            if (firstPick == GameManager.GameState.PlayerTurn)
                ProcessPlayerWin();
            else
                ProcessComputerWin();

            SetupNextRound();

            return false;
        }
        return true;
    }

    public void SetupNextRound()
    {
        roundCounter++;

        //  Alternate who goes first next round
        if (firstPick == GameState.PlayerTurn)
        {
            firstPick = GameState.ComputerTurn;
            curState = GameState.ComputerTurn;
        }
        else
        {
            firstPick = GameState.PlayerTurn;
            curState = GameState.PlayerTurn;
        }
    }
    
    public void ProcessComputerTurn()
    {

    }

    //  Process things when player wins
    public void ProcessPlayerWin()
    {
        playerScore++;
    }

    //  Process things when computer wins
    public void ProcessComputerWin()
    {
        computerScore++;
    }

}
