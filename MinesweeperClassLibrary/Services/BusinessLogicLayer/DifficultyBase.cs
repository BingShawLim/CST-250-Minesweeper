/*
 * Bing [Last Name]
 * CST-250
 * 10/25/2026
 * Minesweeper - Milestone 1
 * Resources: CST-250 Project Milestone Guide (Grand Canyon University, 2025)
 */

namespace MinesweeperClassLibrary.Services.BusinessLogicLayer
{
    /// <summary>
    /// Abstract base for difficulty rules. Each level overrides BombPercentage, and
    /// BoardService calls CalculateBombCount without knowing which level it has (polymorphism).
    /// </summary>
    public abstract class DifficultyBase
    {
        /// <summary>Display name of the level</summary>
        public abstract string Name { get; }

        /// <summary>Fraction of the cells that become bombs (0.0 - 0.25)</summary>
        public abstract double BombPercentage { get; }

        /// <summary>
        /// Number of bombs for a board of the given size (always at least 1 and
        /// always leaves at least one safe cell)
        /// </summary>
        /// <param name="size">Length and width of the board</param>
        /// <returns>The number of bombs to place</returns>
        public virtual int CalculateBombCount(int size)
        {
            int totalCells = size * size;
            int bombs = (int)Math.Round(totalCells * BombPercentage);
            return Math.Clamp(bombs, 1, Math.Max(1, totalCells - 1));
        }

        /// <summary>
        /// Factory method that turns a difficulty number into the matching subclass
        /// </summary>
        /// <param name="level">1 = easy, 2 = medium, 3 = hard</param>
        /// <returns>A DifficultyBase reference to the correct subclass</returns>
        public static DifficultyBase FromLevel(int level)
        {
            return level switch
            {
                2 => new MediumDifficulty(),
                3 => new HardDifficulty(),
                _ => new EasyDifficulty()
            };
        }
    }
}
