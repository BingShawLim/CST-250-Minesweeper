/*
 * Bing Shaw Lim
 * CST-250
 * 11/03/2026
 * Minesweeper - Milestone 3
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
        FlagRemoved,
        // Milestone 3: the cell had no bomb neighbors, so flood fill opened the area around it
        AreaRevealed,
        // Milestone 3: flood fill opened an area and found the special reward in it
        AreaRevealedWithReward
    }
}
