/*
 * Bing Shaw Lim
 * CST-250
 * 10/25/2026
 * Minesweeper - Milestone 1
 * Resources: CST-250 Project Milestone Guide (Grand Canyon University, 2025)
 */

using MinesweeperClassLibrary.Models;

namespace MinesweeperClassLibrary.Services.BusinessLogicLayer
{
    /// <summary>
    /// Business logic for the Minesweeper board. Implements IBoardService so every rule
    /// about bombs, neighbors, rewards, game state, and score lives here and not in the models.
    /// </summary>
    public class BoardService : IBoardService
    {
        // Random number generator (injectable so tests are repeatable)
        private readonly Random _random;

        /// <summary>
        /// Default constructor - uses a new random number generator
        /// </summary>
        public BoardService() : this(new Random())
        {
        }

        /// <summary>
        /// Parameterized constructor - lets unit tests pass a seeded Random
        /// </summary>
        /// <param name="random">Random number generator to use</param>
        public BoardService(Random random)
        {
            _random = random;
        }

        /// <summary>
        /// Randomly place live bombs on the board. The board's Difficulty is turned into a
        /// DifficultyBase subclass, which decides how many bombs to place (polymorphism).
        /// </summary>
        /// <param name="board">The board to fill with bombs</param>
        public void SetupBombs(BoardModel board)
        {
            // Get the difficulty rules for this board
            DifficultyBase difficulty = DifficultyBase.FromLevel(board.Difficulty);
            int bombsToPlace = difficulty.CalculateBombCount(board.Size);
            int bombsPlaced = 0;

            // Keep choosing random cells until the right number of bombs are placed
            while (bombsPlaced < bombsToPlace)
            {
                int row = _random.Next(board.Size);
                int col = _random.Next(board.Size);
                // Only place a bomb on a cell that does not already have one
                if (!board.Cells[row, col].IsBomb)
                {
                    board.Cells[row, col].IsBomb = true;
                    bombsPlaced++;
                }
            }
        } // End of SetupBombs method

        /// <summary>
        /// Place special reward cells (Hint reward) on random cells that are not bombs
        /// </summary>
        /// <param name="board">The board to place rewards on</param>
        /// <param name="numberOfRewards">How many reward cells to create</param>
        public void SetupRewards(BoardModel board, int numberOfRewards)
        {
            // Count the safe cells so the loop can never run forever
            int safeCells = 0;
            foreach (CellModel cell in board.Cells)
            {
                if (!cell.IsBomb && !cell.HasSpecialReward)
                {
                    safeCells++;
                }
            }
            int rewardsToPlace = Math.Min(numberOfRewards, safeCells);
            int rewardsPlaced = 0;

            // Choose random safe cells for the rewards
            while (rewardsPlaced < rewardsToPlace)
            {
                CellModel cell = board.Cells[_random.Next(board.Size), _random.Next(board.Size)];
                if (!cell.IsBomb && !cell.HasSpecialReward)
                {
                    cell.HasSpecialReward = true;
                    rewardsPlaced++;
                }
            }
        } // End of SetupRewards method

        /// <summary>
        /// Calculate the number of bomb neighbors for every cell. A bomb cell is set to 9.
        /// </summary>
        /// <param name="board">The board to count</param>
        public void CountBombsNearby(BoardModel board)
        {
            for (int row = 0; row < board.Size; row++)
            {
                for (int col = 0; col < board.Size; col++)
                {
                    CellModel cell = board.Cells[row, col];
                    // A live cell is marked with 9
                    if (cell.IsBomb)
                    {
                        cell.NumberOfBombNeighbors = 9;
                    }
                    else
                    {
                        cell.NumberOfBombNeighbors = CountNeighborBombs(board, row, col);
                    }
                }
            }
        } // End of CountBombsNearby method

        /// <summary>
        /// Count the bombs in the (up to) 8 cells around one cell
        /// </summary>
        /// <param name="board">The board being counted</param>
        /// <param name="row">Row of the cell</param>
        /// <param name="col">Column of the cell</param>
        /// <returns>Number of neighboring bombs (0 - 8)</returns>
        private int CountNeighborBombs(BoardModel board, int row, int col)
        {
            int count = 0;
            // Check the 3 x 3 block around the cell
            for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
            {
                for (int colOffset = -1; colOffset <= 1; colOffset++)
                {
                    // Skip the cell itself
                    if (rowOffset == 0 && colOffset == 0)
                    {
                        continue;
                    }
                    int neighborRow = row + rowOffset;
                    int neighborCol = col + colOffset;
                    // Only count neighbors that are on the board
                    if (IsOnBoard(board, neighborRow, neighborCol) && board.Cells[neighborRow, neighborCol].IsBomb)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Check whether a row and column are inside the board
        /// </summary>
        /// <param name="board">The board to check against</param>
        /// <param name="row">Row index</param>
        /// <param name="col">Column index</param>
        /// <returns>True when the location is on the board</returns>
        public bool IsOnBoard(BoardModel board, int row, int col)
        {
            return row >= 0 && row < board.Size && col >= 0 && col < board.Size;
        }

        /// <summary>
        /// Inspect the board and decide the game state.
        /// Lost: a bomb was visited. Won: every safe cell is visited (or every bomb correctly
        /// flagged with no wrong flags). Otherwise the game is still in progress (StillPlaying).
        /// Full gameplay use arrives in Milestone 2.
        /// </summary>
        /// <param name="board">The board to inspect</param>
        /// <returns>The current GameState</returns>
        public GameState DetermineGameState(BoardModel board)
        {
            bool allSafeVisited = true;
            bool allBombsFlagged = true;
            bool anyWrongFlag = false;

            foreach (CellModel cell in board.Cells)
            {
                // A visited bomb ends the game
                if (cell.IsBomb && cell.IsVisited)
                {
                    board.GameState = GameState.Lost;
                    board.EndTime = DateTime.Now;
                    return board.GameState;
                }
                if (!cell.IsBomb && !cell.IsVisited)
                {
                    allSafeVisited = false;
                }
                if (cell.IsBomb && !cell.IsFlagged)
                {
                    allBombsFlagged = false;
                }
                if (!cell.IsBomb && cell.IsFlagged)
                {
                    anyWrongFlag = true;
                }
            }

            if (allSafeVisited || (allBombsFlagged && !anyWrongFlag))
            {
                board.GameState = GameState.Won;
                board.EndTime = DateTime.Now;
            }
            else
            {
                board.GameState = GameState.InProgress;
            }
            return board.GameState;
        } // End of DetermineGameState method

        /// <summary>
        /// Use one collected Hint reward: reveal one random, unflagged bomb.
        /// Full gameplay use arrives in Milestone 2.
        /// </summary>
        /// <param name="board">The board in play</param>
        /// <returns>The revealed bomb cell, or null when no reward or no hidden bomb is left</returns>
        public CellModel? UseSpecialBonus(BoardModel board)
        {
            // The player must have collected a reward first
            if (board.RewardsRemaining <= 0)
            {
                return null;
            }

            // Gather the bombs the player has not flagged yet
            List<CellModel> hiddenBombs = new List<CellModel>();
            foreach (CellModel cell in board.Cells)
            {
                if (cell.IsBomb && !cell.IsFlagged)
                {
                    hiddenBombs.Add(cell);
                }
            }
            if (hiddenBombs.Count == 0)
            {
                return null;
            }

            // Spend the reward and flag one random bomb for the player
            board.RewardsRemaining--;
            CellModel revealed = hiddenBombs[_random.Next(hiddenBombs.Count)];
            revealed.IsFlagged = true;
            return revealed;
        } // End of UseSpecialBonus method

        /// <summary>
        /// Score formula: (cells x difficulty x 10) + time bonus - 50 per reward used.
        /// The time bonus starts at 1000 and loses 2 points per second. A lost game scores 0.
        /// </summary>
        /// <param name="board">The finished board</param>
        /// <param name="rewardsUsed">How many rewards the player used</param>
        /// <returns>The final score (never negative)</returns>
        public int DetermineFinalScore(BoardModel board, int rewardsUsed)
        {
            if (board.GameState == GameState.Lost)
            {
                return 0;
            }
            DateTime end = board.EndTime == DateTime.MinValue ? DateTime.Now : board.EndTime;
            int seconds = (int)Math.Max(0, (end - board.StartTime).TotalSeconds);
            int baseScore = board.Size * board.Size * board.Difficulty * 10;
            int timeBonus = Math.Max(0, 1000 - (seconds * 2));
            int penalty = rewardsUsed * 50;
            return Math.Max(0, baseScore + timeBonus - penalty);
        } // End of DetermineFinalScore method
    }
}
