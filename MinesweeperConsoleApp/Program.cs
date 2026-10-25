/*
 * Bing Shaw Lim
 * CST-250
 * 10/25/2026
 * Minesweeper Console App - Milestone 1
 * Resources: CST-250 Project Milestone Guide (Grand Canyon University, 2025)
 */

using MinesweeperClassLibrary.Models;
using MinesweeperClassLibrary.Services.BusinessLogicLayer;

//----------------------------------------------------------
// Start of the Main Method
//----------------------------------------------------------

// The program depends on the IBoardService abstraction, not the concrete class
IBoardService boardService = new BoardService();

Console.WriteLine("Hello, welcome to Minesweeper");

// First board: size 10 (10 or less), easy difficulty
BoardModel board = CreateBoard(boardService, 10, 1);
Console.WriteLine("Here is the answer key for the first board (size 10, Easy)");
PrintAnswers(board);

// Second board: size 15 (greater than 10), medium difficulty
BoardModel secondBoard = CreateBoard(boardService, 15, 2);
Console.WriteLine("Here is the answer key for the second board (size 15, Medium)");
PrintAnswers(secondBoard);

Console.ResetColor();
Console.WriteLine("Press Enter to exit...");
Console.ReadLine();

//----------------------------------------------------------
// End of the Main Method
//----------------------------------------------------------

// Create a board, then let the business logic layer set up bombs, rewards, and neighbor counts
static BoardModel CreateBoard(IBoardService boardService, int size, int difficulty)
{
    // Create an instance of the BoardModel class
    BoardModel board = new BoardModel(size);
    board.Difficulty = difficulty;
    // Call SetupBombs and CountBombsNearby to initialize the grid
    boardService.SetupBombs(board);
    boardService.SetupRewards(board, 1);
    boardService.CountBombsNearby(board);
    return board;
}

// Print the full answer key: B for bombs, a number for neighboring bombs, "." for none
static void PrintAnswers(BoardModel board)
{
    // Print the column numbers
    Console.Write("     ");
    for (int col = 0; col < board.Size; col++)
    {
        Console.Write($"{col,2}  ");
    }
    Console.WriteLine();
    PrintDivider(board.Size);

    for (int row = 0; row < board.Size; row++)
    {
        // Print the row number and the left border
        Console.Write($"{row,3} |");
        for (int col = 0; col < board.Size; col++)
        {
            CellModel cell = board.Cells[row, col];
            string symbol = GetSymbol(cell);
            // Highlight the special reward cell with a dark yellow background
            if (cell.HasSpecialReward)
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
            }
            Console.ForegroundColor = GetColor(cell);
            Console.Write($" {symbol} ");
            Console.ResetColor();
            Console.Write("|");
        }
        Console.WriteLine();
        PrintDivider(board.Size);
    }
    Console.WriteLine("Legend: B = bomb, number = bombs next to the cell, . = no bombs nearby, " +
        "yellow background = special reward (Hint)");
    Console.WriteLine();
}

// Print a divider line such as    +---+---+---+
static void PrintDivider(int size)
{
    Console.Write("    +");
    for (int col = 0; col < size; col++)
    {
        Console.Write("---+");
    }
    Console.WriteLine();
}

// Choose the symbol to show for a cell
static string GetSymbol(CellModel cell)
{
    if (cell.IsBomb)
    {
        return "B";
    }
    if (cell.NumberOfBombNeighbors > 0)
    {
        return cell.NumberOfBombNeighbors.ToString();
    }
    return ".";
}

// Choose the print color for a cell's symbol
static ConsoleColor GetColor(CellModel cell)
{
    if (cell.IsBomb)
    {
        return ConsoleColor.Red;
    }
    return cell.NumberOfBombNeighbors switch
    {
        0 => ConsoleColor.Gray,
        1 => ConsoleColor.Cyan,
        2 => ConsoleColor.Green,
        3 => ConsoleColor.Magenta,
        4 => ConsoleColor.Yellow,
        _ => ConsoleColor.DarkRed
    };
}
