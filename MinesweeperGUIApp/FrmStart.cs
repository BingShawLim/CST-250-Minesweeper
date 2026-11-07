/*
 * Bing Shaw Lim
 * CST-250
 * 11/07/2026
 * Minesweeper GUI - Milestone 4
 * Resources: CST-250 Project Milestone Guide (Grand Canyon University, 2025)
 */

using MinesweeperClassLibrary.Services.BusinessLogicLayer;

namespace MinesweeperGUIApp
{
    /// <summary>
    /// Start form. Gets the board size (track bar) and difficulty (radio buttons) from the
    /// player, then passes them to FrmGame. Closing FrmGame brings the player back here.
    /// </summary>
    public partial class FrmStart : Form
    {
        /// <summary>
        /// Default constructor for FrmStart
        /// </summary>
        public FrmStart()
        {
            InitializeComponent();
            // Show the starting size and bomb count
            UpdateBombPreview();
        }

        /// <summary>
        /// Value changed event handler for the size track bar
        /// </summary>
        /// <param name="sender">trbSize</param>
        /// <param name="e">Event arguments</param>
        private void TrbSizeValueChangedEH(object sender, EventArgs e)
        {
            UpdateBombPreview();
        } // End of TrbSizeValueChangedEH method

        /// <summary>
        /// Checked changed event handler for the three difficulty radio buttons
        /// </summary>
        /// <param name="sender">rdoEasy, rdoMedium, or rdoHard</param>
        /// <param name="e">Event arguments</param>
        private void RdoDifficultyCheckedChangedEH(object sender, EventArgs e)
        {
            UpdateBombPreview();
        } // End of RdoDifficultyCheckedChangedEH method

        /// <summary>
        /// Show the chosen size and how many bombs that size and difficulty will have.
        /// The bomb count comes from the business logic layer (DifficultyBase subclasses).
        /// </summary>
        private void UpdateBombPreview()
        {
            // Declare and initialize
            int size = trbSize.Value;
            DifficultyBase difficulty = DifficultyBase.FromLevel(GetSelectedDifficulty());

            lblSizeValue.Text = $"{size} x {size}";
            lblBombPreview.Text = $"{difficulty.Name}: {difficulty.CalculateBombCount(size)} bombs on {size * size} cells";
        } // End of UpdateBombPreview method

        /// <summary>
        /// Turn the checked radio button into a difficulty level
        /// </summary>
        /// <returns>1 = easy, 2 = medium, 3 = hard</returns>
        private int GetSelectedDifficulty()
        {
            if (rdoHard.Checked)
            {
                return 3;
            }
            if (rdoMedium.Checked)
            {
                return 2;
            }
            return 1;
        } // End of GetSelectedDifficulty method

        /// <summary>
        /// Click event handler for btnStartGame: pass the size and difficulty to FrmGame
        /// </summary>
        /// <param name="sender">btnStartGame</param>
        /// <param name="e">Event arguments</param>
        private void BtnStartGameClickEH(object sender, EventArgs e)
        {
            // Create the game form with the player's choices
            using (FrmGame frmGame = new FrmGame(trbSize.Value, GetSelectedDifficulty()))
            {
                // Hide this form while the game is played
                Hide();
                frmGame.ShowDialog();
            }
            // The game form closed, so come back to the start form
            Show();
        } // End of BtnStartGameClickEH method

        /// <summary>
        /// Click event handler for btnExit: close the application
        /// </summary>
        /// <param name="sender">btnExit</param>
        /// <param name="e">Event arguments</param>
        private void BtnExitClickEH(object sender, EventArgs e)
        {
            Close();
        } // End of BtnExitClickEH method
    }
}
