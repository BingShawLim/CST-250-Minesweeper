/*
 * Bing Shaw Lim
 * CST-250
 * 10/28/2026
 * Minesweeper Console App - Milestone 2
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

// Let the player choose the board size and difficulty
int size = GetNumberInRange("Enter the board size (4 - 20): ", 4, 20);
int difficulty = GetNumberInRange("Enter the difficulty (1 = Easy, 2 = Medium, 3 = Hard): ", 1, 3);
BoardModel board = CreateBoard(boardService, size, difficulty);

// Show the answer key first (technically this is cheating, but it helps with testing)
Console.WriteLine("Here is the answer key for testing:");
PrintAnswers(board);

bool victory = false; // player wins
bool death = false;   // player loses
int rewardsUsed = 0;  // used by the score formula
PrintBoard(board);

// Repeat until the game is over
while (!victory && !death)
{
    // 1. Ask the user for a row and column number
    int row = GetNumberInRange($"Enter the row number (0 - {board.Size - 1}): ", 0, board.Size - 1);
    int col = GetNumberInRange($"Enter the column number (0 - {board.Size - 1}): ", 0, board.Size - 1);

    // 2. Ask if this is for Visit, Flag, or Use Reward
    int choice = GetNumberInRange(
        $"Choose 1 to Visit, 2 to Flag/Unflag, 3 to Use Reward (rewards available: {board.RewardsRemaining}): ", 1, 3);

    // 3. Update the status of the chosen cell according to the move
    switch (choice)
    {
        case 1:
            ShowMoveMessage(boardService.VisitCell(board, row, col), row, col);
            break;
        case 2:
            ShowMoveMessage(boardService.ToggleFlag(board, row, col), row, col);
            break;
        case 3:
            if (UseReward(boardService, board))
            {
                rewardsUsed++;
            }
            break;
    }

    // 4. Determine if victory == true or if death == true
    GameState state = boardService.DetermineGameState(board);
    victory = state == GameState.Won;
    death = state == GameState.Lost;

    // 5. Print the grid
    PrintBoard(board);
} // end game

// Print a victory or failure message
if (victory)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("You win! Every safe cell has been cleared.");
    Console.ResetColor();
    TimeSpan elapsed = board.EndTime - board.StartTime;
    Console.WriteLine($"Time: {elapsed.TotalSeconds:F0} seconds. " +
        $"Score: {boardService.DetermineFinalScore(board, rewardsUsed)}");
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("BOOM! You visited a bomb. Game over.");
    Console.ResetColor();
    Console.WriteLine("Here is where all the bombs were:");
    PrintAnswers(board);
}

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

// Use a Hint reward: the business logic layer picks a random hidden bomb and flags it.
// Returns true when a reward was actually used.
static bool UseReward(IBoardService boardService, BoardModel board)
{
    CellModel? revealed = boardService.UseSpecialBonus(board);
    if (revealed == null)
    {
        Console.WriteLine("You have no rewards to use. Find the reward cell first!");
        return false;
    }
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"Hint used! There is a bomb at row {revealed.Row}, column {revealed.Column}. " +
        "It has been flagged for you.");
    Console.ResetColor();
    return true;
}

// Show a message for the result of a Visit or Flag move
static void ShowMoveMessage(MoveResult result, int row, int col)
{
    string message = result switch
    {
        MoveResult.SafeCellVisited => $"Cell {row},{col} is safe.",
        MoveResult.RewardCollected => $"Cell {row},{col} had a special reward! You earned a Hint. " +
            "Choose option 3 on a later turn to reveal a bomb.",
        MoveResult.BombHit => $"Cell {row},{col} was a bomb!",
        MoveResult.AlreadyVisited => $"Cell {row},{col} was already cleared. Try another cell.",
        MoveResult.CellIsFlagged => $"Cell {row},{col} is flagged. Unflag it before visiting.",
        MoveResult.FlagPlaced => $"Flag placed on cell {row},{col}.",
        MoveResult.FlagRemoved => $"Flag removed from cell {row},{col}.",
        _ => "That cell is not on the board."
    };
    Console.ForegroundColor = result == MoveResult.RewardCollected ? ConsoleColor.Yellow : ConsoleColor.White;
    Console.WriteLine(message);
    Console.ResetColor();
}

// Keep asking until the user types a whole number between min and max
static int GetNumberInRange(string prompt, int min, int max)
{
    while (true)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();
        if (int.TryParse(input, out int number) && number >= min && number <= max)
        {
            return number;
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Please enter a whole number from {min} to {max}.");
        Console.ResetColor();
    }
}

// Print the full answer key: B for bombs, a number for neighboring bombs, "." for none
static void PrintAnswers(BoardModel board)
{
    PrintColumnNumbers(board.Size);
    for (int row = 0; row < board.Size; row++)
    {
        Console.Write($"{row,3} |");
        for (int col = 0; col < board.Size; col++)
        {
            CellModel cell = board.Cells[row, col];
            // The reward cell shows as "r" in the answer key
            if (cell.HasSpecialReward)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" r ");
            }
            else
            {
                Console.ForegroundColor = GetColor(cell);
                Console.Write($" {GetSymbol(cell)} ");
            }
            Console.ResetColor();
            Console.Write("|");
        }
        Console.WriteLine();
        PrintDivider(board.Size);
    }
    Console.WriteLine("Legend: B = bomb, number = bombs next to the cell, . = no bombs nearby, r = special reward");
    Console.WriteLine();
}

// Print the board during game play. Same as PrintAnswers, but it uses IsVisited:
// a cell that has not been visited shows "?" (or "F" when the player flagged it)
static void PrintBoard(BoardModel board)
{
    PrintColumnNumbers(board.Size);
    for (int row = 0; row < board.Size; row++)
    {
        Console.Write($"{row,3} |");
        for (int col = 0; col < board.Size; col++)
        {
            CellModel cell = board.Cells[row, col];
            if (cell.IsFlagged)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(" F ");
            }
            else if (!cell.IsVisited)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(" ? ");
            }
            else
            {
                Console.ForegroundColor = GetColor(cell);
                Console.Write($" {GetSymbol(cell)} ");
            }
            Console.ResetColor();
            Console.Write("|");
        }
        Console.WriteLine();
        PrintDivider(board.Size);
    }
    Console.WriteLine();
}

// Print the column numbers and the top divider line
static void PrintColumnNumbers(int size)
{
    Console.Write("     ");
    for (int col = 0; col < size; col++)
    {
        Console.Write($"{col,2}  ");
    }
    Console.WriteLine();
    PrintDivider(size);
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
