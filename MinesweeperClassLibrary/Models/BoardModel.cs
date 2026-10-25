/*
 * Bing Shaw Lim
 * CST-250
 * 10/25/2026
 * Minesweeper - Milestone 1
 * Resources: CST-250 Project Milestone Guide (Grand Canyon University, 2025)
 */

namespace MinesweeperClassLibrary.Models
{
    /// <summary>
    /// The square Minesweeper game board. Holds data only and is composed of a
    /// two-dimensional array of CellModel objects (composition).
    /// </summary>
    public class BoardModel
    {
        // Class level properties
        public int Size { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public CellModel[,] Cells { get; set; }
        // Difficulty level: 1 = easy, 2 = medium, 3 = hard
        public int Difficulty { get; set; }
        public int RewardsRemaining { get; set; }
        public GameState GameState { get; set; }

        /// <summary>
        /// Constructor that builds a size x size grid with a CellModel at every location
        /// </summary>
        /// <param name="size">Length and width of the square board</param>
        public BoardModel(int size)
        {
            Size = size;
            Difficulty = 1;
            RewardsRemaining = 0;
            GameState = GameState.InProgress;
            StartTime = DateTime.Now;
            EndTime = DateTime.MinValue;
            // Create the grid and store a new cell at each location
            Cells = new CellModel[size, size];
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Cells[row, col] = new CellModel(row, col);
                }
            }
        }
    }
}
