/*
 * Bing Shaw Lim
 * CST-250
 * 11/02/2026
 * Minesweeper - Milestone 5 xUnit Tests
 * Resources: CST-250 Project Milestone Guide (Grand Canyon University, 2025)
 */

using MinesweeperClassLibrary.Models;
using MinesweeperClassLibrary.Services.BusinessLogicLayer;
using MinesweeperClassLibrary.Services.DataAccessLayer;

namespace MinesweeperClassLibrary.Tests
{
    /// <summary>
    /// xUnit tests for game stats: GameStatService, the sorters, and the text file DAO
    /// </summary>
    public class GameStatServiceTests : IDisposable
    {
        // Each test gets its own temporary stats file
        private readonly string _filePath;

        /// <summary>
        /// Create a unique temporary file path for this test
        /// </summary>
        public GameStatServiceTests()
        {
            _filePath = Path.Combine(Path.GetTempPath(), $"MinesweeperStats_{Guid.NewGuid():N}.txt");
        }

        /// <summary>
        /// Delete the temporary file after the test
        /// </summary>
        public void Dispose()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        /// <summary>
        /// In-memory DAO used to prove the service depends only on IGameStatDAO (abstraction)
        /// </summary>
        private class FakeGameStatDAO : IGameStatDAO
        {
            public List<GameStatModel> Saved { get; } = new List<GameStatModel>();

            public void SaveStats(IEnumerable<GameStatModel> stats, string filePath)
            {
                Saved.Clear();
                Saved.AddRange(stats);
            }

            public List<GameStatModel> LoadStats(string filePath)
            {
                return new List<GameStatModel>(Saved);
            }
        }

        // Helper: a service with three winners added a day apart
        private static GameStatService MakeServiceWithThreeStats()
        {
            GameStatService service = new GameStatService(new FakeGameStatDAO());
            service.AddStat("Charlie", 300, TimeSpan.FromSeconds(90)).Date = new DateTime(2026, 11, 1);
            service.AddStat("alice", 500, TimeSpan.FromSeconds(60)).Date = new DateTime(2026, 11, 3);
            service.AddStat("Bob", 400, TimeSpan.FromSeconds(75)).Date = new DateTime(2026, 11, 2);
            return service;
        }

        // GameStatModel's default constructor sets the default values
        [Fact]
        public void GameStatModel_DefaultConstructor_ShouldSetDefaults()
        {
            // Arrange and Act
            GameStatModel stat = new GameStatModel();

            // Assert
            Assert.Equal(0, stat.Id);
            Assert.Equal(string.Empty, stat.Name);
            Assert.Equal(0, stat.Score);
            Assert.Equal(TimeSpan.Zero, stat.GameTime);
        }

        // Winner names are checked with a regular expression
        [Theory]
        [InlineData("Bing", true)]
        [InlineData("Player 2", true)]
        [InlineData("", false)]
        [InlineData("   ", false)]
        [InlineData("Smith, Jo", false)]
        [InlineData("ThisNameIsWayTooLongToFit", false)]
        public void IsValidName_ShouldMatchRules(string name, bool expected)
        {
            // Arrange
            GameStatService service = new GameStatService(new FakeGameStatDAO());

            // Act and Assert
            Assert.Equal(expected, service.IsValidName(name));
        }

        // AddStat gives ids 1, 2, 3 and trims the name
        [Fact]
        public void AddStat_ShouldAssignIdsInOrder()
        {
            // Arrange
            GameStatService service = new GameStatService(new FakeGameStatDAO());

            // Act
            GameStatModel first = service.AddStat("  Bing ", 820, TimeSpan.FromSeconds(95));
            GameStatModel second = service.AddStat("Jason", 610, TimeSpan.FromSeconds(120));

            // Assert
            Assert.Equal(1, first.Id);
            Assert.Equal(2, second.Id);
            Assert.Equal("Bing", first.Name);
            Assert.Equal(2, service.Stats.Count);
        }

        // AddStat refuses a name that fails validation
        [Fact]
        public void AddStat_WithInvalidName_ShouldThrow()
        {
            // Arrange
            GameStatService service = new GameStatService(new FakeGameStatDAO());

            // Act and Assert
            Assert.Throws<ArgumentException>(() => service.AddStat("", 100, TimeSpan.FromSeconds(10)));
        }

        // Sort > By Name is alphabetical and ignores upper and lower case
        [Fact]
        public void Sort_ByName_ShouldBeAlphabetical()
        {
            // Arrange
            GameStatService service = MakeServiceWithThreeStats();

            // Act
            IReadOnlyList<GameStatModel> sorted = service.Sort(new SortByName());

            // Assert
            Assert.Equal("alice", sorted[0].Name);
            Assert.Equal("Bob", sorted[1].Name);
            Assert.Equal("Charlie", sorted[2].Name);
        }

        // Sort > By Score puts the highest score first
        [Fact]
        public void Sort_ByScore_ShouldBeHighestFirst()
        {
            // Arrange
            GameStatService service = MakeServiceWithThreeStats();

            // Act
            IReadOnlyList<GameStatModel> sorted = service.Sort(new SortByScore());

            // Assert
            Assert.Equal(500, sorted[0].Score);
            Assert.Equal(400, sorted[1].Score);
            Assert.Equal(300, sorted[2].Score);
        }

        // Sort > By Date puts the most recent game first
        [Fact]
        public void Sort_ByDate_ShouldBeNewestFirst()
        {
            // Arrange
            GameStatService service = MakeServiceWithThreeStats();

            // Act
            IReadOnlyList<GameStatModel> sorted = service.Sort(new SortByDate());

            // Assert
            Assert.Equal("alice", sorted[0].Name);
            Assert.Equal("Bob", sorted[1].Name);
            Assert.Equal("Charlie", sorted[2].Name);
        }

        // Polymorphism: every sorter is used through a GameStatSorter reference
        [Fact]
        public void Sorters_ShouldWorkThroughBaseClassReference()
        {
            // Arrange
            GameStatSorter[] sorters = { new SortByName(), new SortByScore(), new SortByDate() };
            GameStatService service = MakeServiceWithThreeStats();

            // Act and Assert
            foreach (GameStatSorter sorter in sorters)
            {
                Assert.Equal(3, service.Sort(sorter).Count);
            }
            Assert.Equal("By Name", sorters[0].Name);
            Assert.Equal("By Score", sorters[1].Name);
            Assert.Equal("By Date", sorters[2].Name);
        }

        // File > Save then File > Load with the real text file DAO brings back the same games
        [Fact]
        public void SaveStats_ThenLoadStats_ShouldRoundTrip()
        {
            // Arrange
            GameStatService writer = new GameStatService();
            writer.AddStat("Bing", 820, TimeSpan.FromSeconds(95));
            writer.AddStat("Jason", 610, TimeSpan.FromSeconds(120));

            // Act
            string saveMessage = writer.SaveStats(_filePath);
            GameStatService reader = new GameStatService();
            string loadMessage = reader.LoadStats(_filePath);

            // Assert
            Assert.Contains("Saved 2 game(s)", saveMessage);
            Assert.Contains("Loaded 2 game(s)", loadMessage);
            Assert.Equal(2, reader.Stats.Count);
            Assert.Equal("Bing", reader.Stats[0].Name);
            Assert.Equal(820, reader.Stats[0].Score);
            Assert.Equal(TimeSpan.FromSeconds(120), reader.Stats[1].GameTime);
        }

        // Loading a file that does not exist reports an error message instead of crashing
        [Fact]
        public void LoadStats_MissingFile_ShouldReturnErrorMessage()
        {
            // Arrange
            GameStatService service = new GameStatService();

            // Act
            string message = service.LoadStats(_filePath);

            // Assert
            Assert.Contains("Could not load", message);
            Assert.Empty(service.Stats);
        }

        // Damaged lines in the text file are skipped
        [Fact]
        public void TextFileDAO_LoadStats_ShouldSkipDamagedLines()
        {
            // Arrange
            File.WriteAllLines(_filePath, new[]
            {
                "1,Bing,820,95,2026-11-20 21:15:00",
                "not a valid line",
                "2,Jason,abc,120,2026-11-21 08:00:00"
            });
            GameStatTextFileDAO dao = new GameStatTextFileDAO();

            // Act
            List<GameStatModel> stats = dao.LoadStats(_filePath);

            // Assert
            Assert.Single(stats);
            Assert.Equal("Bing", stats[0].Name);
        }
    }
}
