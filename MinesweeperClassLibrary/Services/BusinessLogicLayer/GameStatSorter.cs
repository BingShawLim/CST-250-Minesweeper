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
    /// Abstract base for the Sort menu choices. Each subclass overrides Sort, and
    /// GameStatService calls Sort without knowing which order it gets (polymorphism).
    /// </summary>
    public abstract class GameStatSorter
    {
        /// <summary>Menu text for this sort order</summary>
        public abstract string Name { get; }

        /// <summary>
        /// Return the stats in this sorter's order
        /// </summary>
        /// <param name="stats">Stats to sort</param>
        /// <returns>A new sorted list</returns>
        public abstract List<GameStatModel> Sort(IEnumerable<GameStatModel> stats);
    }

    /// <summary>
    /// Sort > By Name: alphabetical, ignoring upper and lower case, best score first for the same name
    /// </summary>
    public class SortByName : GameStatSorter
    {
        /// <inheritdoc/>
        public override string Name => "By Name";

        /// <inheritdoc/>
        public override List<GameStatModel> Sort(IEnumerable<GameStatModel> stats)
        {
            return stats.OrderBy(stat => stat.Name, StringComparer.OrdinalIgnoreCase)
                .ThenByDescending(stat => stat.Score)
                .ToList();
        }
    }

    /// <summary>
    /// Sort > By Score: highest score first, faster game first on a tie
    /// </summary>
    public class SortByScore : GameStatSorter
    {
        /// <inheritdoc/>
        public override string Name => "By Score";

        /// <inheritdoc/>
        public override List<GameStatModel> Sort(IEnumerable<GameStatModel> stats)
        {
            return stats.OrderByDescending(stat => stat.Score)
                .ThenBy(stat => stat.GameTime)
                .ToList();
        }
    }

    /// <summary>
    /// Sort > By Date: most recent game first
    /// </summary>
    public class SortByDate : GameStatSorter
    {
        /// <inheritdoc/>
        public override string Name => "By Date";

        /// <inheritdoc/>
        public override List<GameStatModel> Sort(IEnumerable<GameStatModel> stats)
        {
            return stats.OrderByDescending(stat => stat.Date).ToList();
        }
    }
}
