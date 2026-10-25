/*
 * Bing [Last Name]
 * CST-250
 * 10/25/2026
 * Minesweeper - Milestone 1
 * Resources: CST-250 Project Milestone Guide (Grand Canyon University, 2025)
 */

namespace MinesweeperClassLibrary.Services.BusinessLogicLayer
{
    /// <summary>Hard level: 20% of the cells are bombs, plus one extra bomb</summary>
    public class HardDifficulty : DifficultyBase
    {
        public override string Name => "Hard";
        public override double BombPercentage => 0.20;

        /// <summary>
        /// Hard overrides the base calculation to add one extra bomb
        /// </summary>
        /// <param name="size">Length and width of the board</param>
        /// <returns>The number of bombs to place</returns>
        public override int CalculateBombCount(int size)
        {
            int totalCells = size * size;
            return Math.Min(base.CalculateBombCount(size) + 1, Math.Max(1, totalCells - 1));
        }
    }
}
