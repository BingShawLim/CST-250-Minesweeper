/*
 * Bing Shaw Lim
 * CST-250
 * 11/02/2026
 * Minesweeper - Milestone 5
 * Resources: CST-250 Project Milestone Guide (Grand Canyon University, 2025)
 */

using System.Globalization;
using MinesweeperClassLibrary.Models;

namespace MinesweeperClassLibrary.Services.DataAccessLayer
{
    /// <summary>
    /// Stores game stats in a comma-separated text file, one winning game per line:
    /// Id,Name,Score,GameTimeSeconds,Date
    /// </summary>
    public class GameStatTextFileDAO : IGameStatDAO
    {
        // Format used for the date column so files read the same on any computer
        private const string DateFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// Write every stat to the given file, replacing what was there
        /// </summary>
        /// <param name="stats">The stats to save</param>
        /// <param name="filePath">Full path of the text file</param>
        public void SaveStats(IEnumerable<GameStatModel> stats, string filePath)
        {
            List<string> lines = new List<string>();
            foreach (GameStatModel stat in stats)
            {
                lines.Add(string.Join(",",
                    stat.Id.ToString(CultureInfo.InvariantCulture),
                    stat.Name,
                    stat.Score.ToString(CultureInfo.InvariantCulture),
                    ((int)stat.GameTime.TotalSeconds).ToString(CultureInfo.InvariantCulture),
                    stat.Date.ToString(DateFormat, CultureInfo.InvariantCulture)));
            }
            File.WriteAllLines(filePath, lines);
        } // End of SaveStats method

        /// <summary>
        /// Read every stat from the given file. Damaged lines are skipped.
        /// </summary>
        /// <param name="filePath">Full path of the text file</param>
        /// <returns>The stats in the file</returns>
        public List<GameStatModel> LoadStats(string filePath)
        {
            List<GameStatModel> stats = new List<GameStatModel>();
            foreach (string line in File.ReadAllLines(filePath))
            {
                string[] values = line.Split(',');
                if (values.Length != 5)
                {
                    continue;
                }
                bool valid = int.TryParse(values[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out int id)
                    & int.TryParse(values[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out int score)
                    & int.TryParse(values[3], NumberStyles.Integer, CultureInfo.InvariantCulture, out int seconds)
                    & DateTime.TryParseExact(values[4], DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date);
                if (valid)
                {
                    stats.Add(new GameStatModel(id, values[1], score, TimeSpan.FromSeconds(seconds), date));
                }
            }
            return stats;
        } // End of LoadStats method
    }
}
