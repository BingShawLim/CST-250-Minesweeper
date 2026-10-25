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
    /// Abstraction for all board behavior. The console app (and later the GUI) only
    /// depends on this contract, not on how the rules are implemented.
    /// </summary>
    public interface IBoardService
    {
        /// <summary>Randomly place bombs using the board's difficulty</summary>
        void SetupBombs(BoardModel board);

        /// <summary>Place the special reward cells on safe squares</summary>
        void SetupRewards(BoardModel board, int numberOfRewards);

        /// <summary>Calculate NumberOfBombNeighbors for every cell (9 for a bomb)</summary>
        void CountBombsNearby(BoardModel board);

        /// <summary>Inspect the board and return Won, Lost, or InProgress (StillPlaying)</summary>
        GameState DetermineGameState(BoardModel board);

        /// <summary>Use a collected reward (Hint: reveal one random bomb)</summary>
        CellModel? UseSpecialBonus(BoardModel board);

        /// <summary>Calculate the final score from difficulty, time, size, and rewards used</summary>
        int DetermineFinalScore(BoardModel board, int rewardsUsed);
    }
}
