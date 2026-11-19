/*
 * Bing Shaw Lim
 * CST-250
 * 11/02/2026
 * Minesweeper - Milestone 5
 * Resources: CST-250 Project Milestone Guide (Grand Canyon University, 2025)
 */

using System.Text.RegularExpressions;
using MinesweeperClassLibrary.Models;
using MinesweeperClassLibrary.Services.DataAccessLayer;

namespace MinesweeperClassLibrary.Services.BusinessLogicLayer
{
    /// <summary>
    /// Business logic for game stats: validates names, gives ids and dates, sorts,
    /// and passes file work to the data access layer.
    /// </summary>
    public class GameStatService : IGameStatService
    {
        // Winner names: 1 to 20 letters, digits, or spaces (no commas, so the text file stays valid)
        private static readonly Regex NameRegex = new Regex(@"^[A-Za-z0-9 ]{1,20}$");

        // Class level variables (encapsulation: the list is private and only exposed read only)
        private List<GameStatModel> _stats;
        private readonly IGameStatDAO _statDAO;

        /// <summary>
        /// Every stored winning game, in the current sort order (read only)
        /// </summary>
        public IReadOnlyList<GameStatModel> Stats => _stats.AsReadOnly();

        /// <summary>
        /// Default constructor that stores stats in a text file
        /// </summary>
        public GameStatService() : this(new GameStatTextFileDAO())
        {
        }

        /// <summary>
        /// Constructor that takes any IGameStatDAO (used by the tests)
        /// </summary>
        /// <param name="statDAO">The data access object to use</param>
        public GameStatService(IGameStatDAO statDAO)
        {
            _stats = new List<GameStatModel>();
            _statDAO = statDAO;
        }

        /// <summary>
        /// Check that a winner's name is 1 to 20 letters, digits, or spaces and not only spaces
        /// </summary>
        /// <param name="name">The name typed on Form3</param>
        /// <returns>True when the name can be saved</returns>
        public bool IsValidName(string? name)
        {
            return !string.IsNullOrWhiteSpace(name) && NameRegex.IsMatch(name.Trim());
        }

        /// <summary>
        /// Add a winning game with the next id and today's date
        /// </summary>
        /// <param name="name">Winner's name from Form3</param>
        /// <param name="score">Final score from DetermineFinalScore</param>
        /// <param name="gameTime">How long the game took</param>
        /// <returns>The stored stat</returns>
        public GameStatModel AddStat(string name, int score, TimeSpan gameTime)
        {
            if (!IsValidName(name))
            {
                throw new ArgumentException("Name must be 1 to 20 letters, digits, or spaces.", nameof(name));
            }
            int nextId = _stats.Count == 0 ? 1 : _stats.Max(stat => stat.Id) + 1;
            GameStatModel stat = new GameStatModel(nextId, name.Trim(), score, gameTime, DateTime.Now);
            _stats.Add(stat);
            return stat;
        } // End of AddStat method

        /// <summary>
        /// Put the list in the order chosen from the Sort menu
        /// </summary>
        /// <param name="sorter">SortByName, SortByScore, or SortByDate</param>
        /// <returns>The sorted list</returns>
        public IReadOnlyList<GameStatModel> Sort(GameStatSorter sorter)
        {
            _stats = sorter.Sort(_stats);
            return Stats;
        } // End of Sort method

        /// <summary>
        /// File > Save: write the list to a text file
        /// </summary>
        /// <param name="filePath">Full path chosen in the save dialog</param>
        /// <returns>A message describing the result</returns>
        public string SaveStats(string filePath)
        {
            try
            {
                _statDAO.SaveStats(_stats, filePath);
                return $"Saved {_stats.Count} game(s) to {Path.GetFileName(filePath)}.";
            }
            catch (Exception ex)
            {
                return $"Could not save the file: {ex.Message}";
            }
        } // End of SaveStats method

        /// <summary>
        /// File > Load: replace the list with the stats in a text file
        /// </summary>
        /// <param name="filePath">Full path chosen in the open dialog</param>
        /// <returns>A message describing the result</returns>
        public string LoadStats(string filePath)
        {
            try
            {
                _stats = _statDAO.LoadStats(filePath);
                return $"Loaded {_stats.Count} game(s) from {Path.GetFileName(filePath)}.";
            }
            catch (Exception ex)
            {
                return $"Could not load the file: {ex.Message}";
            }
        } // End of LoadStats method
    }
}
