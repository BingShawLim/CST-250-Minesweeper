/*
 * Bing Shaw Lim
 * CST-250
 * 11/18/2026
 * Minesweeper GUI - Milestone 5 (Form3)
 * Resources: CST-250 Project Milestone Guide (Grand Canyon University, 2025)
 */

using MinesweeperClassLibrary.Services.BusinessLogicLayer;

namespace MinesweeperGUIApp
{
    /// <summary>
    /// Form3: shown after a winning game to capture the winner's name
    /// </summary>
    public partial class FrmWinnerName : Form
    {
        // Class level variables
        private readonly IGameStatService _statService;

        // The validated name, read by the game form after this dialog closes
        public string WinnerName { get; private set; }

        /// <summary>
        /// Constructor that shows the winning score and keeps the service used to check the name
        /// </summary>
        /// <param name="score">The final score</param>
        /// <param name="gameTime">How long the game took</param>
        /// <param name="statService">Business logic that validates names</param>
        public FrmWinnerName(int score, TimeSpan gameTime, IGameStatService statService)
        {
            InitializeComponent();
            _statService = statService;
            WinnerName = string.Empty;
            lblScore.Text = $"Score: {score}   Time: {(int)gameTime.TotalSeconds} seconds";
            lblNameError.Visible = false;
        }

        /// <summary>
        /// Click Event Handler for btnOk
        /// Checks the name in the business layer and closes the form when it is valid
        /// </summary>
        /// <param name="sender">btnOk</param>
        /// <param name="e">Event arguments</param>
        private void BtnOkClickEH(object sender, EventArgs e)
        {
            if (!_statService.IsValidName(txtWinnerName.Text))
            {
                lblNameError.Text = "Use 1 to 20 letters, digits, or spaces.";
                lblNameError.Visible = true;
                txtWinnerName.Focus();
                return;
            }
            WinnerName = txtWinnerName.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        } // End of BtnOkClickEH method
    }
}
