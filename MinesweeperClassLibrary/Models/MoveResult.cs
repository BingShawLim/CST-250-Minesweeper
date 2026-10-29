/*
 * Bing Shaw Lim
 * CST-250
 * 10/28/2026
 * Minesweeper - Milestone 2
 * Resources: CST-250 Project Milestone Guide (Grand Canyon University, 2025)
 */

namespace MinesweeperClassLibrary.Models
{
    /// <summary>
    /// The outcome of one player move. The business logic layer returns one of these
    /// values so the user interface knows which message to show without knowing the rules.
    /// </summary>
    public enum MoveResult
    {
        // The row or column is off the board
        InvalidCell,
        // A safe cell was cleared
        SafeCellVisited,
        // A safe cell was cleared and it held a special reward
        RewardCollected,
        // A bomb was visited (the game is lost)
        BombHit,
        // The cell was already cleared earlier
        AlreadyVisited,
        // The cell is flagged, so it cannot be visited until the flag is removed
        CellIsFlagged,
        // A flag was placed on the cell
        FlagPlaced,
        // A flag was removed from the cell
        FlagRemoved
    }
}
