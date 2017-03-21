using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    //  User-defined board size
    private const int BoardSize = 1;

    static void Main(string[] args)
    {
        //  Initialize grid
        Grid grid = new Grid(BoardSize);

        //  Prompt user for grid size
        Console.WriteLine("Welcome to Tic-Tac-Toe!");

        Console.WriteLine();    //  Skip a line

        Console.Write("Please enter a difficulty number from 1-3: ");

        string difficultyInput = Console.ReadLine();

        //  Evaluate input for validity, if not, repeat request
        while (!IsDifficultyInputValid(difficultyInput))
        {
            Console.WriteLine("Invalid input!");
            Console.WriteLine();    //  Skip a line
            Console.WriteLine("Please enter a difficulty number from 1-3:");

            difficultyInput = Console.ReadLine();
        }

        int difficultyLevel = int.Parse(difficultyInput);

        Console.WriteLine("Difficulty level: " + difficultyLevel);

        Console.WriteLine();    //  Skip a line

        //  Pause console so it doesn't close
        Console.ReadLine();
    }

    //  Returns true if given string is a integer. Otherwise, return false
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
}