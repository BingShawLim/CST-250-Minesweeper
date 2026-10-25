/*
 * Bing Shaw Lim
 * CST-250
 * 10/25/2026
 * Minesweeper - Milestone 1 xUnit Tests
 * Resources: CST-250 Project Milestone Guide (Grand Canyon University, 2025)
 */

using MinesweeperClassLibrary.Models;
using MinesweeperClassLibrary.Services.BusinessLogicLayer;

namespace MinesweeperClassLibrary.Tests
{
    /// <summary>
    /// xUnit tests for the business logic layer (BoardService and the difficulty classes)
    /// </summary>
    public class BoardServiceTests
    {
        // Helper: count the bombs on a board
        private static int CountBombs(BoardModel board)
        {
            int count = 0;
            foreach (CellModel cell in board.Cells)
            {
                if (cell.IsBomb)
                {
                    count++;
                }
            }
            return count;
        }

        // The BoardModel constructor must place a CellModel at every location with its row and column
        [Fact]
        public void BoardModel_Constructor_ShouldCreateCellAtEveryLocation()
        {
            // Arrange and Act
            BoardModel board = new BoardModel(8);

            // Assert
            Assert.Equal(8, board.Size);
            Assert.Equal(GameState.InProgress, board.GameState);
            Assert.Equal(0, board.RewardsRemaining);
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Assert.NotNull(board.Cells[row, col]);
                    Assert.Equal(row, board.Cells[row, col].Row);
                    Assert.Equal(col, board.Cells[row, col].Column);
                }
            }
        }

        // A new CellModel must start with the default values from the guide
        [Fact]
        public void CellModel_DefaultConstructor_ShouldSetGuideDefaults()
        {
            // Arrange and Act
            CellModel cell = new CellModel();

            // Assert
            Assert.Equal(-1, cell.Row);
            Assert.Equal(-1, cell.Column);
            Assert.False(cell.IsVisited);
            Assert.False(cell.IsBomb);
            Assert.False(cell.IsFlagged);
            Assert.Equal(0, cell.NumberOfBombNeighbors);
            Assert.False(cell.HasSpecialReward);
        }

        // SetupBombs must place exactly the number of bombs the difficulty asks for
        [Theory]
        [InlineData(10, 1)]
        [InlineData(10, 2)]
        [InlineData(15, 3)]
        public void SetupBombs_ShouldPlaceBombCountFromDifficulty(int size, int level)
        {
            // Arrange
            BoardService service = new BoardService(new Random(42));
            BoardModel board = new BoardModel(size) { Difficulty = level };
            int expected = DifficultyBase.FromLevel(level).CalculateBombCount(size);

            // Act
            service.SetupBombs(board);

            // Assert
            Assert.Equal(expected, CountBombs(board));
        }

        // Each difficulty subclass returns its own bomb count (polymorphism)
        [Fact]
        public void DifficultyBase_FromLevel_ShouldReturnCorrectSubclassAndCount()
        {
            // Arrange and Act
            DifficultyBase easy = DifficultyBase.FromLevel(1);
            DifficultyBase medium = DifficultyBase.FromLevel(2);
            DifficultyBase hard = DifficultyBase.FromLevel(3);

            // Assert: correct subclass behind the base reference
            Assert.IsType<EasyDifficulty>(easy);
            Assert.IsType<MediumDifficulty>(medium);
            Assert.IsType<HardDifficulty>(hard);
            // Assert: 100 cells -> 10%, 15%, 20% + 1
            Assert.Equal(10, easy.CalculateBombCount(10));
            Assert.Equal(15, medium.CalculateBombCount(10));
            Assert.Equal(21, hard.CalculateBombCount(10));
        }

        // CountBombsNearby must give 9 to bombs and the correct neighbor count to every other cell
        [Fact]
        public void CountBombsNearby_ShouldCountNeighbors_AndSetNineForBombs()
        {
            // Arrange: a 3 x 3 board with bombs at (0,0) and (2,2)
            BoardService service = new BoardService();
            BoardModel board = new BoardModel(3);
            board.Cells[0, 0].IsBomb = true;
            board.Cells[2, 2].IsBomb = true;

            // Act
            service.CountBombsNearby(board);

            // Assert
            Assert.Equal(9, board.Cells[0, 0].NumberOfBombNeighbors);
            Assert.Equal(9, board.Cells[2, 2].NumberOfBombNeighbors);
            Assert.Equal(2, board.Cells[1, 1].NumberOfBombNeighbors);
            Assert.Equal(1, board.Cells[0, 1].NumberOfBombNeighbors);
            Assert.Equal(0, board.Cells[0, 2].NumberOfBombNeighbors);
            Assert.Equal(0, board.Cells[2, 0].NumberOfBombNeighbors);
        }

        // Corner and edge cells must not count off-board locations (no IndexOutOfRange)
        [Fact]
        public void CountBombsNearby_ShouldHandleEdges_WhenBoardIsFullOfBombsExceptCorner()
        {
            // Arrange: every cell is a bomb except (0,0)
            BoardService service = new BoardService();
            BoardModel board = new BoardModel(4);
            foreach (CellModel cell in board.Cells)
            {
                cell.IsBomb = true;
            }
            board.Cells[0, 0].IsBomb = false;

            // Act
            service.CountBombsNearby(board);

            // Assert: a corner has only 3 neighbors
            Assert.Equal(3, board.Cells[0, 0].NumberOfBombNeighbors);
        }

        // SetupRewards must place rewards only on safe cells
        [Fact]
        public void SetupRewards_ShouldPlaceRewardOnSafeCell()
        {
            // Arrange
            BoardService service = new BoardService(new Random(7));
            BoardModel board = new BoardModel(10) { Difficulty = 3 };
            service.SetupBombs(board);

            // Act
            service.SetupRewards(board, 2);

            // Assert
            int rewards = 0;
            foreach (CellModel cell in board.Cells)
            {
                if (cell.HasSpecialReward)
                {
                    rewards++;
                    Assert.False(cell.IsBomb);
                }
            }
            Assert.Equal(2, rewards);
        }

        // A visited bomb means the game is lost
        [Fact]
        public void DetermineGameState_ShouldReturnLost_WhenBombVisited()
        {
            // Arrange
            BoardService service = new BoardService();
            BoardModel board = new BoardModel(3);
            board.Cells[1, 1].IsBomb = true;
            board.Cells[1, 1].IsVisited = true;

            // Act and Assert
            Assert.Equal(GameState.Lost, service.DetermineGameState(board));
        }

        // Visiting every safe cell wins the game
        [Fact]
        public void DetermineGameState_ShouldReturnWon_WhenAllSafeCellsVisited()
        {
            // Arrange
            BoardService service = new BoardService();
            BoardModel board = new BoardModel(3);
            board.Cells[1, 1].IsBomb = true;
            foreach (CellModel cell in board.Cells)
            {
                if (!cell.IsBomb)
                {
                    cell.IsVisited = true;
                }
            }

            // Act and Assert
            Assert.Equal(GameState.Won, service.DetermineGameState(board));
        }

        // A board with unvisited safe cells is still being played
        [Fact]
        public void DetermineGameState_ShouldReturnInProgress_WhenSafeCellsRemain()
        {
            // Arrange
            BoardService service = new BoardService();
            BoardModel board = new BoardModel(3);
            board.Cells[1, 1].IsBomb = true;

            // Act and Assert
            Assert.Equal(GameState.InProgress, service.DetermineGameState(board));
        }

        // Using a reward flags one bomb and spends the reward
        [Fact]
        public void UseSpecialBonus_ShouldRevealBomb_AndDecreaseRewards()
        {
            // Arrange
            BoardService service = new BoardService();
            BoardModel board = new BoardModel(3) { RewardsRemaining = 1 };
            board.Cells[2, 1].IsBomb = true;

            // Act
            CellModel? revealed = service.UseSpecialBonus(board);

            // Assert
            Assert.NotNull(revealed);
            Assert.True(revealed!.IsBomb);
            Assert.True(revealed.IsFlagged);
            Assert.Equal(0, board.RewardsRemaining);
            // No rewards left: nothing is revealed
            Assert.Null(service.UseSpecialBonus(board));
        }

        // A lost game scores 0, and a won game scores more on a harder difficulty
        [Fact]
        public void DetermineFinalScore_ShouldBeZeroWhenLost_AndHigherForHarderDifficulty()
        {
            // Arrange
            BoardService service = new BoardService();
            DateTime start = new DateTime(2026, 10, 25, 12, 0, 0);
            BoardModel easy = new BoardModel(10) { Difficulty = 1, StartTime = start, EndTime = start.AddSeconds(100), GameState = GameState.Won };
            BoardModel hard = new BoardModel(10) { Difficulty = 3, StartTime = start, EndTime = start.AddSeconds(100), GameState = GameState.Won };
            BoardModel lost = new BoardModel(10) { GameState = GameState.Lost };

            // Act and Assert: 10*10*1*10 + (1000 - 200) = 1800
            Assert.Equal(1800, service.DetermineFinalScore(easy, 0));
            Assert.True(service.DetermineFinalScore(hard, 0) > service.DetermineFinalScore(easy, 0));
            Assert.Equal(1750, service.DetermineFinalScore(easy, 1));
            Assert.Equal(0, service.DetermineFinalScore(lost, 0));
        }
    }
}
