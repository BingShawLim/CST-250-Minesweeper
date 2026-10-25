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
    /// One square on the Minesweeper board. Holds data only - all cell behavior
    /// lives in the business logic layer.
    /// </summary>
    public class CellModel
    {
        // Class level properties
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsVisited { get; set; }
        public bool IsBomb { get; set; }
        public bool IsFlagged { get; set; }
        public int NumberOfBombNeighbors { get; set; }
        public bool HasSpecialReward { get; set; }

        /// <summary>
        /// Default constructor - sets the starting values required by the milestone guide
        /// </summary>
        public CellModel()
        {
            Row = -1;
            Column = -1;
            IsVisited = false;
            IsBomb = false;
            IsFlagged = false;
            NumberOfBombNeighbors = 0;
            HasSpecialReward = false;
        }

        /// <summary>
        /// Parameterized constructor that places the cell at a row and column
        /// </summary>
        /// <param name="row">Row index of the cell</param>
        /// <param name="column">Column index of the cell</param>
        public CellModel(int row, int column) : this()
        {
            Row = row;
            Column = column;
        }
    }
}
