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

        //  Prompt user for grid size
        PrintBoardSizePromt();
        string boardSizeInput = Console.ReadLine();

        //  Evaluate input for validity, if not, repeat request
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

        //  if the board size is less then 3... decide winner by default and set a new game.
        if (boardSize < 3)
        {
            if (gameManager.firstPick == GameManager.GameState.PlayerTurn)
                gameManager.playerScore++;
            else
                gameManager.computerScore++;

            gameManager.SetupNextRound();
        }

        //  Prompt user for difficulty level
        PrintDifficultyPromt();

        string difficultyInput = Console.ReadLine();

        //  Evaluate input for validity, if not, repeat request
        while (!IsDifficultyInputValid(difficultyInput))
        {
            Console.WriteLine("\nInvalid input! \n");

            PrintDifficultyPromt();

            difficultyInput = Console.ReadLine();
        }

        int difficultyLevel = int.Parse(difficultyInput);

        Console.WriteLine();    //  Skip a line

        PrintDifficulty(difficultyLevel);

        grid.PrintCurrentGrid();

        Console.WriteLine("\nThank you for playing Tic-Tac - Toe.");

        //  Pause console so it doesn't close
        Console.ReadLine();
    }

    //  Returns true if given string is a integer. Otherwise, return false
    private static bool IsBoardSizeInputValid(string input)
    {
        // determine if input is a integer 
        int boardSize;
        bool isNumeric = int.TryParse(input, out boardSize);
        if (isNumeric)
            return true;
        else
            return false;
    }

    //  Returns true if given string is a integer and is within valid ranges. Otherwise, return false
    private static bool IsDifficultyInputValid(string input)
    {
        // determine if input is a integer 
        int difficultyLevel;
        bool isNumeric = int.TryParse(input, out difficultyLevel);
        if (isNumeric)
        {
            //  Determine if input is within valid range
            if (difficultyLevel > 0 && difficultyLevel <= 3)
            {
                return true;
            }

            return false;
        }

        return false;
    }

    //  Prints the difficulty promt
    private static void PrintDifficultyPromt()
    {
        Console.WriteLine("Input a difficulty level \n" +
                "1. Easy\n" +
                "2. Medium\n" +
                "3. Hard\n");
    }

    //  Prints the board size promt
    private static void PrintBoardSizePromt()
    {
        Console.Write("Input board size: ");
    }

    //  Prints the difficulty
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
}