/*
 * Bing Shaw Lim
 * CST-250
 * 11/02/2026
 * Minesweeper - Milestone 5
 * Resources: CST-250 Project Milestone Guide (Grand Canyon University, 2025)
 */

using MinesweeperClassLibrary.Models;

namespace MinesweeperClassLibrary.Services.BusinessLogicLayer
{
    /// <summary>
    /// Abstraction for the high score list. Form3 and Form4 only depend on this contract.
    /// </summary>
    public interface IGameStatService
    {
        /// <summary>Every stored winning game, in the current sort order (read only)</summary>
        IReadOnlyList<GameStatModel> Stats { get; }

        /// <summary>Check that a winner's name can be saved</summary>
        bool IsValidName(string? name);

        /// <summary>Add a winning game and return the stored stat</summary>
        GameStatModel AddStat(string name, int score, TimeSpan gameTime);

        /// <summary>Put the list in the order chosen from the Sort menu</summary>
        IReadOnlyList<GameStatModel> Sort(GameStatSorter sorter);

        /// <summary>File > Save: write the list to a text file</summary>
        string SaveStats(string filePath);

        /// <summary>File > Load: replace the list with the stats in a text file</summary>
        string LoadStats(string filePath);
    }
}
