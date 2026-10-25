/*
 * Bing Shaw Lim
 * CST-250
 * 10/25/2026
 * Minesweeper - Milestone 1
 * Resources: CST-250 Project Milestone Guide (Grand Canyon University, 2025)
 */

namespace MinesweeperClassLibrary.Services.BusinessLogicLayer
{
    /// <summary>Easy level: 10% of the cells are bombs</summary>
    public class EasyDifficulty : DifficultyBase
    {
        public override string Name => "Easy";
        public override double BombPercentage => 0.10;
    }
}
