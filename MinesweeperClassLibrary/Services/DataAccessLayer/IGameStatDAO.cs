/*
 * Bing Shaw Lim
 * CST-250
 * 11/02/2026
 * Minesweeper - Milestone 5
 * Resources: CST-250 Project Milestone Guide (Grand Canyon University, 2025)
 */

using MinesweeperClassLibrary.Models;

namespace MinesweeperClassLibrary.Services.DataAccessLayer
{
    /// <summary>
    /// Abstraction for storing game stats. The business layer only knows this contract,
    /// so the text file could be swapped for another store without changing it.
    /// </summary>
    public interface IGameStatDAO
    {
        /// <summary>Write every stat to the given file</summary>
        void SaveStats(IEnumerable<GameStatModel> stats, string filePath);

        /// <summary>Read every stat from the given file</summary>
        List<GameStatModel> LoadStats(string filePath);
    }
}
