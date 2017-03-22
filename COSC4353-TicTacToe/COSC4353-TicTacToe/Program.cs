using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        //  User-Defined parameters
        const int BoardSize = 3;

        Console.WriteLine("Welcome to Tic-Tac-Toe!\n");
        
        //  Initialize game manager
        GameManager gameManager = new GameManager();

        while (true)
        {
            //  Initialize grid given board size
            Grid grid = new Grid(BoardSize);

            //  Evaluate board size. if the board size is less then 3... decide winner by default and set a new game.
            if (!gameManager.IsBoardSizeBigEnough())
            {
                Console.WriteLine("Ending game because board size is too small.");
                break;
            }

            //  Prompt user for difficulty level & evaluate input for validity, if not, repeat request
            PrintDifficultyPrompt();
            string difficultyInput = Console.ReadLine();
            while (!IsDifficultyInputValid(difficultyInput))
            {
                Console.WriteLine("\nInvalid input! \n");

                PrintDifficultyPrompt();

                difficultyInput = Console.ReadLine();
            }

            int difficultyLevel = int.Parse(difficultyInput);

            Console.WriteLine();    //  Skip a line

            PrintDifficulty(difficultyLevel);

            //  While the grid is not full (round not finished), loop
            while (grid.IsGridFull() == false)
            {
                //  Print current state of board
                grid.PrintCurrentGrid();

                //  Determine if player/computer goes first for the round
                if (gameManager.curTurn == GameManager.TurnState.PlayerTurn)
                {
                    //  Prompt user for tictactoe input & evaluate input for validity, if not, repeat request
                    Console.WriteLine("Your turn. What is your move?");
                    while(true)
                    {
                        #region Prompt for row number
                        //  Prompt for row
                        PromptTicTacToeInput(true);
                        string rowInput = Console.ReadLine();
                        while (!IsTicTacToeInputValid(rowInput))
                        {
                            Console.WriteLine("\nInvalid input!");
                            PromptTicTacToeInput(true);
                            rowInput = Console.ReadLine();
                        }
                        int rowNum = int.Parse(rowInput);
                        #endregion

                        #region Prompt for column number
                        PromptTicTacToeInput(false);
                        string columnInput = Console.ReadLine();
                        while (!IsTicTacToeInputValid(columnInput))
                        {
                            Console.WriteLine("\nInvalid input!");
                            PromptTicTacToeInput(false);
                            columnInput = Console.ReadLine();
                        }
                        int columnNum = int.Parse(columnInput);
                        #endregion

                        if (!Grid.GridPoints[columnNum, rowNum].isOccupied)
                        {
                            //  Input 'X' in the location for player
                            Grid.InputGridPoint(columnNum, rowNum, Grid.GridPoint.InputType.X);

                            //  Set the next turn to computer
                            gameManager.curTurn = GameManager.TurnState.ComputerTurn;

                            break;
                        }
                        else
                            Console.WriteLine("Space is occupied! Please try inputting again.");
                    }
                }
                else
                {
                    Console.WriteLine("My turn.");
                    switch (difficultyLevel)
                    {
                        case 1:
                            gameManager.ProcessComputerTurn(difficultyLevel);
                            break;
                        case 2:
                            gameManager.ProcessComputerTurn(difficultyLevel);
                            break;
                        case 3:
                            gameManager.ProcessComputerTurn(difficultyLevel);
                            break;
                    }

                    //  Set the next turn to Player
                    gameManager.curTurn = GameManager.TurnState.PlayerTurn;
                }
            }

            //  Prompt user for replay, if true, setup new game. Otherwise, break loop to end game.
            if (PromptUserForReplay())
                gameManager.SetupNextRound();
            else
                break;
        }

        Console.WriteLine("\nThank you for playing Tic-Tac-Toe.");

        Console.ReadLine();         //  Pause console so it doesn't close
    }

    #region Returns true if difficulty input is a integer and is within valid ranges. Otherwise, return false
    private static bool IsDifficultyInputValid(string input)
    {
        // determine if input is a integer 
        int difficultyLevel;
        bool isNumeric = int.TryParse(input, out difficultyLevel);
        if (isNumeric)
        {
            //  Determine if input is within valid range
            if (difficultyLevel > 0 && difficultyLevel <= 3)
                return true;
            return false;
        }
        return false;
    }
    #endregion

    #region Returns true if TicTacToe input is a integer and is within valid ranges. Otherwise, return false
    private static bool IsTicTacToeInputValid(string input)
    {
        // determine if input is a integer 
        int ticTacToeNum;
        bool isNumeric = int.TryParse(input, out ticTacToeNum);
        if (isNumeric)
        {
            //  Determine if input is within valid range
            if (ticTacToeNum >= 0 && ticTacToeNum < Grid.GridSize)
            {
                return true;
            }
            return false;
        }
        return false;
    }
    #endregion
    
    #region Prints the difficulty prompt
    private static void PrintDifficultyPrompt()
    {
        Console.Write("1. Easy\n" +
                "2. Medium\n" +
                "3. Hard\n" +
                "Input a difficulty level: " );
    }
    #endregion

    #region Prints the TicTacToe input prompt
    private static void PromptTicTacToeInput(bool promptRow)
    {
        if (promptRow)
            Console.Write("Enter a row number: "); //from 0 to " + (Grid.GridSize - 1) + ": ");
        else
            Console.Write("Enter a column number: "); //from 0 to " + (Grid.GridSize - 1) + ": ");
    }
    #endregion

    #region Prints the difficulty
    private static void PrintDifficulty(int difficultyLevel)
    {
        Console.Write("\nDifficulty level: ");
        switch (difficultyLevel)
        {
            case 1:
                Console.Write("Easy");
                break;
            case 2:
                Console.Write("Medium");
                break;
            case 3:
                Console.Write("Hard");
                break;
        }
        Console.WriteLine();
    }
    #endregion

    #region Returns true if user if they want to play again.
    private static bool PromptUserForReplay()
    {
        Console.Write("\nWould you like to play again? (1 = Yes, 2 = No): ");
        string replayInput = Console.ReadLine();
        int replayNum = int.Parse(replayInput);
        if (replayNum == 1)
            return true;
        else
            return false;
    }
    #endregion
}