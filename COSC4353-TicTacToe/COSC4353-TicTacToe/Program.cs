using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Tic-Tac-Toe!\n");
        
        //  Initialize game manager
        GameManager gameManager = new GameManager();

        //  Prompt user for grid size & evaluate input for validity, if not, repeat request
        PrintBoardSizePromt();
        string boardSizeInput = Console.ReadLine();
        while (!IsBoardSizeInputValid(boardSizeInput))
        {
            Console.WriteLine("\nInvalid input!\n");

            PrintBoardSizePromt();

            boardSizeInput = Console.ReadLine();
        }

        Console.WriteLine();    // Skip a line

        //  Initialize grid given inputted board size
        int boardSize = int.Parse(boardSizeInput);
        Grid grid = new Grid(boardSize);

        //  Evaluate board size. if the board size is less then 3... decide winner by default and set a new game.
        gameManager.CheckBoardSizeConditions();

        //  Prompt user for difficulty level & evaluate input for validity, if not, repeat request
        PrintDifficultyPromt();
        string difficultyInput = Console.ReadLine();
        while (!IsDifficultyInputValid(difficultyInput))
        {
            Console.WriteLine("\nInvalid input! \n");

            PrintDifficultyPromt();

            difficultyInput = Console.ReadLine();
        }

        int difficultyLevel = int.Parse(difficultyInput);

        Console.WriteLine();    //  Skip a line

        PrintDifficulty(difficultyLevel);

        //  Print current state of board
        grid.PrintCurrentGrid();

        //  Prompt user for tictactoe input & evaluate input for validity, if not, repeat request
        Console.WriteLine("What is your move?");
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

        //  Prompt for column
        PromptTicTacToeInput(false);
        string columnInput = Console.ReadLine();
        while (!IsTicTacToeInputValid(columnInput))
        {
            Console.WriteLine("\nInvalid input!");
            PromptTicTacToeInput(false);
            columnInput = Console.ReadLine();
        }
        int columnNum = int.Parse(columnInput);

        //  Determine if player/computer goes first for the round
        if (gameManager.firstPick == GameManager.GameState.PlayerTurn && gameManager.roundCounter > 1)
        {
            //PromptTicTacToeInput();
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Computer goes first");
            gameManager.ProcessComputerTurn();
        }

        Console.WriteLine("\nThank you for playing Tic-Tac - Toe.");

        Console.ReadLine();         //  Pause console so it doesn't close
    }

    #region Returns true if board size input is a integer. Otherwise, return false
    private static bool IsBoardSizeInputValid(string input)
    {
        // determine if input is a integer 
        int boardSize;
        bool isNumeric = int.TryParse(input, out boardSize);
        if (isNumeric)
        {
            //  Determine if input is within valid range
            if (boardSize > 0)
                return true;
            return false;
        }
        else
            return false;
    }
    #endregion

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
    
    #region Prints the difficulty promt
    private static void PrintDifficultyPromt()
    {
        Console.WriteLine("Input a difficulty level \n" +
                "1. Easy\n" +
                "2. Medium\n" +
                "3. Hard\n");
    }
    #endregion

    #region Prints the board size promt
    private static void PrintBoardSizePromt()
    {
        Console.Write("Input board size: ");
    }
    #endregion

    #region Prints the TicTacToe input prompt
    private static void PromptTicTacToeInput(bool promptRow)
    {
        if (promptRow)
            Console.Write("Enter a row number from 0 to " + (Grid.GridSize - 1) + ": ");
        else
            Console.Write("Enter a column number from 0 to " + (Grid.GridSize-1) + ": ");
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
}