/*
 * Bing Shaw Lim
 * CST-250
 * 11/02/2026
 * Minesweeper - Milestone 5
 * Resources: CST-250 Project Milestone Guide (Grand Canyon University, 2025)
 */

namespace MinesweeperClassLibrary.Models
{
    /// <summary>
    /// Statistics for one winning game. Holds data only.
    /// </summary>
    public class GameStatModel
    {
        // Class level properties
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public TimeSpan GameTime { get; set; }
        public DateTime Date { get; set; }

        /// <summary>
        /// Default constructor that sets every property to a default value
        /// </summary>
        public GameStatModel()
        {
            Id = 0;
            Name = string.Empty;
            Score = 0;
            GameTime = TimeSpan.Zero;
            Date = DateTime.Now;
        }

        /// <summary>
        /// Parameterized constructor that sets every property
        /// </summary>
        /// <param name="id">Unique id in the stats list</param>
        /// <param name="name">Winner's name</param>
        /// <param name="score">Final score</param>
        /// <param name="gameTime">How long the game took</param>
        /// <param name="date">Date and time the game was won</param>
        public GameStatModel(int id, string name, int score, TimeSpan gameTime, DateTime date)
        {
            Id = id;
            Name = name;
            Score = score;
            GameTime = gameTime;
            Date = date;
        }
    }
}
