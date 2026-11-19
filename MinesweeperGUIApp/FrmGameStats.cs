/*
 * Bing Shaw Lim
 * CST-250
 * 11/18/2026
 * Minesweeper GUI - Milestone 5 (Form4)
 * Resources: CST-250 Project Milestone Guide (Grand Canyon University, 2025)
 */

using MinesweeperClassLibrary.Models;
using MinesweeperClassLibrary.Services.BusinessLogicLayer;

namespace MinesweeperGUIApp
{
    /// <summary>
    /// Form4: shows the winners' scores in a data grid view with File and Sort menus.
    /// After a win it receives the winner's name and score from the game form and adds
    /// them to the list through the business logic layer.
    /// </summary>
    public partial class FrmGameStats : Form
    {
        // File types offered by the save and load dialogs
        private const string FileFilter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

        // Class level variables
        private readonly IGameStatService _statService;
        private readonly BindingSource _statsBindingSource;

        // The new winner passed in from the game form (empty name = just viewing the list)
        private readonly string _winnerName;
        private readonly int _score;
        private readonly TimeSpan _gameTime;
        private int _newStatId;

        /// <summary>
        /// Constructor used to view the list without adding a new score
        /// </summary>
        /// <param name="statService">Business logic that holds and sorts the stats</param>
        public FrmGameStats(IGameStatService statService)
            : this(statService, string.Empty, 0, TimeSpan.Zero)
        {
        }

        /// <summary>
        /// Constructor used after a win: the game form passes the winner's name and score
        /// </summary>
        /// <param name="statService">Business logic that holds and sorts the stats</param>
        /// <param name="winnerName">Name entered on Form3</param>
        /// <param name="score">Final score of the winning game</param>
        /// <param name="gameTime">How long the winning game took</param>
        public FrmGameStats(IGameStatService statService, string winnerName, int score, TimeSpan gameTime)
        {
            InitializeComponent();
            _statService = statService;
            _statsBindingSource = new BindingSource();
            dgvStats.DataSource = _statsBindingSource;
            _winnerName = winnerName;
            _score = score;
            _gameTime = gameTime;
            _newStatId = 0;
        }

        /// <summary>
        /// Load Event Handler for FrmGameStats
        /// Adds the new winner (if any) and shows the stats sorted by score
        /// </summary>
        /// <param name="sender">FrmGameStats</param>
        /// <param name="e">Event arguments</param>
        private void FrmGameStatsLoadEH(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_winnerName))
            {
                GameStatModel newStat = _statService.AddStat(_winnerName, _score, _gameTime);
                _newStatId = newStat.Id;
            }
            RefreshGrid(_statService.Sort(new SortByScore()));
        } // End of FrmGameStatsLoadEH method

        /// <summary>
        /// Click Event Handler for File > Save
        /// Writes the stats to a text file chosen by the user
        /// </summary>
        /// <param name="sender">tsmSave</param>
        /// <param name="e">Event arguments</param>
        private void TsmSaveClickEH(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save game stats";
                saveFileDialog.Filter = FileFilter;
                saveFileDialog.FileName = "MinesweeperStats.txt";
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    MessageBox.Show(_statService.SaveStats(saveFileDialog.FileName), "Save");
                }
            }
        } // End of TsmSaveClickEH method

        /// <summary>
        /// Click Event Handler for File > Load
        /// Replaces the list with the stats in a text file chosen by the user
        /// </summary>
        /// <param name="sender">tsmLoad</param>
        /// <param name="e">Event arguments</param>
        private void TsmLoadClickEH(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Load game stats";
                openFileDialog.Filter = FileFilter;
                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    MessageBox.Show(_statService.LoadStats(openFileDialog.FileName), "Load");
                    _newStatId = 0;
                    RefreshGrid(_statService.Stats);
                }
            }
        } // End of TsmLoadClickEH method

        /// <summary>
        /// Click Event Handler for File > Exit
        /// Closes Form4
        /// </summary>
        /// <param name="sender">tsmExit</param>
        /// <param name="e">Event arguments</param>
        private void TsmExitClickEH(object sender, EventArgs e)
        {
            Close();
        } // End of TsmExitClickEH method

        /// <summary>
        /// Click Event Handler for Sort > By Name
        /// </summary>
        /// <param name="sender">tsmSortByName</param>
        /// <param name="e">Event arguments</param>
        private void TsmSortByNameClickEH(object sender, EventArgs e)
        {
            RefreshGrid(_statService.Sort(new SortByName()));
        } // End of TsmSortByNameClickEH method

        /// <summary>
        /// Click Event Handler for Sort > By Score
        /// </summary>
        /// <param name="sender">tsmSortByScore</param>
        /// <param name="e">Event arguments</param>
        private void TsmSortByScoreClickEH(object sender, EventArgs e)
        {
            RefreshGrid(_statService.Sort(new SortByScore()));
        } // End of TsmSortByScoreClickEH method

        /// <summary>
        /// Click Event Handler for Sort > By Date
        /// </summary>
        /// <param name="sender">tsmSortByDate</param>
        /// <param name="e">Event arguments</param>
        private void TsmSortByDateClickEH(object sender, EventArgs e)
        {
            RefreshGrid(_statService.Sort(new SortByDate()));
        } // End of TsmSortByDateClickEH method

        /// <summary>
        /// Click Event Handler for btnOk
        /// Closes Form4
        /// </summary>
        /// <param name="sender">btnOk</param>
        /// <param name="e">Event arguments</param>
        private void BtnOkClickEH(object sender, EventArgs e)
        {
            Close();
        } // End of BtnOkClickEH method

        /// <summary>
        /// Show a list of stats in the data grid view and select the new winner's row
        /// </summary>
        /// <param name="stats">The stats in the order to show</param>
        private void RefreshGrid(IReadOnlyList<GameStatModel> stats)
        {
            _statsBindingSource.DataSource = stats.ToList();
            _statsBindingSource.ResetBindings(false);
            if (dgvStats.Columns.Contains("GameTime"))
            {
                dgvStats.Columns["GameTime"]!.HeaderText = "Game Time";
                dgvStats.Columns["GameTime"]!.DefaultCellStyle.Format = @"mm\:ss";
                dgvStats.Columns["Date"]!.DefaultCellStyle.Format = "MM/dd/yyyy h:mm tt";
            }

            // Highlight the game that was just won; otherwise select nothing
            dgvStats.ClearSelection();
            for (int i = 0; i < stats.Count; i++)
            {
                if (_newStatId != 0 && stats[i].Id == _newStatId)
                {
                    dgvStats.Rows[i].Selected = true;
                }
            }
        } // End of RefreshGrid method
    }
}
