/*
 * Bing Shaw Lim
 * CST-250
 * 11/07/2026
 * Minesweeper - Milestone 4
 * Resources: CST-250 Project Milestone Guide (Grand Canyon University, 2025)
 */

using MinesweeperClassLibrary.Models;

namespace MinesweeperClassLibrary.Services.BusinessLogicLayer
{
    /// <summary>
    /// Abstraction for all board behavior. The console app and the Milestone 4 GUI only
    /// depend on this contract, not on how the rules are implemented.
    /// </summary>
    public interface IBoardService
    {
        /// <summary>Randomly place bombs using the board's difficulty</summary>
        void SetupBombs(BoardModel board);

        /// <summary>Place the special reward cells on safe squares</summary>
        void SetupRewards(BoardModel board, int numberOfRewards);

        /// <summary>Calculate NumberOfBombNeighbors for every cell (9 for a bomb)</summary>
        void CountBombsNearby(BoardModel board);

        /// <summary>Check whether a row and column are inside the board</summary>
        bool IsOnBoard(BoardModel board, int row, int col);

        /// <summary>Visit (clear) one cell and report what happened</summary>
        MoveResult VisitCell(BoardModel board, int row, int col);

        /// <summary>
        /// Recursively reveal the block of connected cells with no bomb neighbors, plus the
        /// numbered cells around its edge. Returns how many cells were revealed.
        /// </summary>
        int FloodFill(BoardModel board, int row, int col);

        /// <summary>Place a flag on a cell, or remove the flag if one is already there</summary>
        MoveResult ToggleFlag(BoardModel board, int row, int col);

        /// <summary>Inspect the board and return Won, Lost, or InProgress (StillPlaying)</summary>
        GameState DetermineGameState(BoardModel board);

        /// <summary>Use a collected reward (Hint: reveal one random bomb)</summary>
        CellModel? UseSpecialBonus(BoardModel board);

        /// <summary>Calculate the final score from difficulty, time, size, and rewards used</summary>
        int DetermineFinalScore(BoardModel board, int rewardsUsed);

        /// <summary>Milestone 4: count the bombs on the board (for the GUI status label)</summary>
        int CountBombs(BoardModel board);

        /// <summary>Milestone 4: count the flags the player has placed (for the GUI status label)</summary>
        int CountFlags(BoardModel board);
    }
}
