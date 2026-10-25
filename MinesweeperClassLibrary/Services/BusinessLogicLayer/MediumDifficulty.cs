/*
 * Bing Shaw Lim
 * CST-250
 * 10/25/2026
 * Minesweeper - Milestone 1
 * Resources: CST-250 Project Milestone Guide (Grand Canyon University, 2025)
 */

namespace MinesweeperClassLibrary.Services.BusinessLogicLayer
{
    /// <summary>Medium level: 15% of the cells are bombs</summary>
    public class MediumDifficulty : DifficultyBase
    {
        public override string Name => "Medium";
        public override double BombPercentage => 0.15;
    }
}
