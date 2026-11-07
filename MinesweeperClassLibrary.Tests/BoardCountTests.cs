/*
 * Bing Shaw Lim
 * CST-250
 * 11/07/2026
 * Minesweeper - Milestone 4 xUnit Tests
 * Resources: CST-250 Project Milestone Guide (Grand Canyon University, 2025)
 */

using MinesweeperClassLibrary.Models;
using MinesweeperClassLibrary.Services.BusinessLogicLayer;

namespace MinesweeperClassLibrary.Tests
{
    /// <summary>
    /// xUnit tests for the Milestone 4 counting methods the GUI status label uses
    /// </summary>
    public class BoardCountTests
    {
        // A new board has no bombs and no flags
        [Fact]
        public void CountBombsAndFlags_NewBoard_ShouldBeZero()
        {
            // Arrange
            BoardService service = new BoardService();
            BoardModel board = new BoardModel(5);

            // Act and Assert
            Assert.Equal(0, service.CountBombs(board));
            Assert.Equal(0, service.CountFlags(board));
        }

        // CountBombs must match the number SetupBombs placed for each difficulty
        [Theory]
        [InlineData(10, 1)]
        [InlineData(10, 2)]
        [InlineData(20, 3)]
        public void CountBombs_AfterSetupBombs_ShouldMatchDifficulty(int size, int difficulty)
        {
            // Arrange
            BoardService service = new BoardService(new Random(7));
            BoardModel board = new BoardModel(size) { Difficulty = difficulty };
            int expected = DifficultyBase.FromLevel(difficulty).CalculateBombCount(size);

            // Act
            service.SetupBombs(board);

            // Assert
            Assert.Equal(expected, service.CountBombs(board));
        }

        // Placing and removing flags with ToggleFlag must change the flag count
        [Fact]
        public void CountFlags_ShouldFollowToggleFlag()
        {
            // Arrange
            BoardService service = new BoardService();
            BoardModel board = new BoardModel(5);

            // Act
            service.ToggleFlag(board, 0, 0);
            service.ToggleFlag(board, 1, 1);
            service.ToggleFlag(board, 2, 2);
            int afterThree = service.CountFlags(board);
            service.ToggleFlag(board, 1, 1);

            // Assert
            Assert.Equal(3, afterThree);
            Assert.Equal(2, service.CountFlags(board));
        }

        // A flag placed by the Hint reward is counted too
        [Fact]
        public void CountFlags_ShouldIncludeHintFlag()
        {
            // Arrange
            BoardService service = new BoardService();
            BoardModel board = new BoardModel(4);
            board.Cells[3, 3].IsBomb = true;
            service.CountBombsNearby(board);
            board.RewardsRemaining = 1;

            // Act
            CellModel? revealed = service.UseSpecialBonus(board);

            // Assert
            Assert.NotNull(revealed);
            Assert.Equal(1, service.CountFlags(board));
            Assert.Equal(1, service.CountBombs(board));
        }
    }
}
