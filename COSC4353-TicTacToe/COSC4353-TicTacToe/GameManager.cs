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
        roundCounter = 0;
        playerScore = 0;
        computerScore = 0;
        curState = GameState.NULL;
        firstPick = GameState.PlayerTurn;
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
}
