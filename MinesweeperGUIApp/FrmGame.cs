/*
 * Bing Shaw Lim
 * CST-250
 * 11/07/2026
 * Minesweeper GUI - Milestone 4
 * Resources: CST-250 Project Milestone Guide (Grand Canyon University, 2025);
 *            CST-250 Activity 2 Guide (button grid technique from the Chessboard app)
 */

using MinesweeperClassLibrary.Models;
using MinesweeperClassLibrary.Services.BusinessLogicLayer;

namespace MinesweeperGUIApp
{
    /// <summary>
    /// Game form. A panel holds a grid of buttons that mirrors board.Cells. The form only
    /// draws the board and passes moves to IBoardService; every game rule stays in the BLL.
    /// Left click visits a cell (a cell with no bomb neighbors opens its whole empty area
    /// through the Milestone 3 recursive FloodFill), right click places or removes a flag.
    /// </summary>
    public partial class FrmGame : Form
    {
        // Class level variables
        private IBoardService _boardService;
        private BoardModel _board;
        private Button[,] _buttons;
        private int _rewardsUsed;
        private bool _isGameOver;

        // Button faces built from the game assets (null if an asset file is missing)
        private Image? _tileFace;
        private Image? _flagFace;
        private Image? _bombFace;
        private Image? _bombHitFace;

        /// <summary>
        /// Parameterized constructor for FrmGame
        /// </summary>
        /// <param name="size">Length and width of the board (from FrmStart)</param>
        /// <param name="difficulty">1 = easy, 2 = medium, 3 = hard (from FrmStart)</param>
        public FrmGame(int size, int difficulty)
        {
            InitializeComponent();

            // The form depends on the IBoardService abstraction, not the concrete class
            _boardService = new BoardService();
            _board = CreateBoard(size, difficulty);
            _buttons = new Button[size, size];
            _rewardsUsed = 0;
            _isGameOver = false;

            // Build the grid of buttons and the button faces, then draw the board
            SetUpButtons();
            LoadAssets();
            UpdateButtonFaces(false);
            UpdateStatusLabels();
            lblMessage.Text = "Left click to visit a cell. Right click to place or remove a flag.";

            // Start the clock
            _board.StartTime = DateTime.Now;
            tmrGame.Start();
        }

        /// <summary>
        /// Create a board and let the business logic layer place the bombs and reward
        /// and count the neighbors
        /// </summary>
        /// <param name="size">Length and width of the board</param>
        /// <param name="difficulty">1 = easy, 2 = medium, 3 = hard</param>
        /// <returns>A board that is ready to play</returns>
        private BoardModel CreateBoard(int size, int difficulty)
        {
            // Declare and initialize
            BoardModel board = new BoardModel(size);
            board.Difficulty = difficulty;

            // The BLL does all the setup work
            _boardService.SetupBombs(board);
            _boardService.SetupRewards(board, 1);
            _boardService.CountBombsNearby(board);
            return board;
        } // End of CreateBoard method

        /// <summary>
        /// Fill the panel with a grid of buttons that mirrors the board (same technique as
        /// the Activity 2 Chessboard app)
        /// </summary>
        private void SetUpButtons()
        {
            // Calculate the size of each button from the panel size
            int buttonSize = pnlBoard.Width / _board.Size;
            float fontSize = Math.Max(8F, buttonSize / 2.6F);

            // Shrink the panel so the grid fits exactly
            pnlBoard.Width = buttonSize * _board.Size;
            pnlBoard.Height = pnlBoard.Width;

            // Loop through the grid
            for (int row = 0; row < _board.Size; row++)
            {
                for (int col = 0; col < _board.Size; col++)
                {
                    Button button = new Button();
                    button.Width = buttonSize;
                    button.Height = buttonSize;
                    // Columns go across, rows go down
                    button.Left = col * buttonSize;
                    button.Top = row * buttonSize;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderColor = Color.DimGray;
                    button.Font = new Font("Segoe UI", fontSize, FontStyle.Bold);
                    button.BackgroundImageLayout = ImageLayout.Stretch;
                    button.TabStop = false;
                    // Store the location of the button in its Tag
                    button.Tag = new Point(row, col);
                    // MouseUp tells us which mouse button was used
                    button.MouseUp += BtnCellMouseUpEH;

                    _buttons[row, col] = button;
                    pnlBoard.Controls.Add(button);
                }
            }

            // Line the bottom buttons up under the board so nothing overlaps at any size or scaling
            btnUseHint.Top = pnlBoard.Bottom + 14;
            btnNewGame.Top = pnlBoard.Bottom + 14;
            btnNewGame.Left = pnlBoard.Right - btnNewGame.Width;
            ClientSize = new Size(Math.Max(ClientSize.Width, pnlBoard.Right + 20), btnNewGame.Bottom + 16);
        } // End of SetUpButtons method

        /// <summary>
        /// Load the game assets and build one face image for each kind of cell
        /// </summary>
        private void LoadAssets()
        {
            // Declare and initialize
            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets");
            int faceSize = _buttons[0, 0].Width;

            try
            {
                using (Image tile = Image.FromFile(Path.Combine(folder, "tile.png")))
                using (Image flag = Image.FromFile(Path.Combine(folder, "flag.png")))
                using (Image bomb = Image.FromFile(Path.Combine(folder, "bomb.png")))
                {
                    _tileFace = BuildFace(faceSize, tile, null, Color.Gray);
                    _flagFace = BuildFace(faceSize, tile, flag, Color.Gray);
                    _bombFace = BuildFace(faceSize, null, bomb, Color.Gainsboro);
                    _bombHitFace = BuildFace(faceSize, null, bomb, Color.IndianRed);
                }
            }
            catch (Exception)
            {
                // If an asset is missing the game still works with the text scheme (F, B, numbers)
                _tileFace = null;
                _flagFace = null;
                _bombFace = null;
                _bombHitFace = null;
            }
        } // End of LoadAssets method

        /// <summary>
        /// Draw a square face: a background color, an optional tile image, and an optional icon
        /// </summary>
        /// <param name="size">Width and height of the face in pixels</param>
        /// <param name="background">Tile image to stretch over the face, or null</param>
        /// <param name="icon">Icon to draw in the middle, or null</param>
        /// <param name="backColor">Color behind everything</param>
        /// <returns>The finished face image</returns>
        private static Image BuildFace(int size, Image? background, Image? icon, Color backColor)
        {
            Bitmap face = new Bitmap(size, size);
            using (Graphics graphics = Graphics.FromImage(face))
            {
                graphics.Clear(backColor);
                if (background != null)
                {
                    graphics.DrawImage(background, 0, 0, size, size);
                }
                if (icon != null)
                {
                    // Leave a small margin around the icon
                    int margin = size / 7;
                    graphics.DrawImage(icon, margin, margin, size - (2 * margin), size - (2 * margin));
                }
            }
            return face;
        } // End of BuildFace method

        /// <summary>
        /// Update every button face from the board.Cells properties.
        /// Unvisited = gray tile, flagged = flag (F), bomb = bomb (B),
        /// visited = blank or the number of neighboring bombs.
        /// </summary>
        /// <param name="revealAll">True when the game is over, so every bomb is shown</param>
        private void UpdateButtonFaces(bool revealAll)
        {
            for (int row = 0; row < _board.Size; row++)
            {
                for (int col = 0; col < _board.Size; col++)
                {
                    CellModel cell = _board.Cells[row, col];
                    Button button = _buttons[row, col];
                    button.Text = "";
                    button.BackgroundImage = null;

                    if (cell.IsVisited && cell.IsBomb)
                    {
                        // The bomb the player hit
                        ShowFace(button, _bombHitFace, "B", Color.IndianRed, Color.Black);
                    }
                    else if (revealAll && cell.IsBomb && !cell.IsFlagged)
                    {
                        // Game over: show the bombs that were still hidden
                        ShowFace(button, _bombFace, "B", Color.Gainsboro, Color.Black);
                    }
                    else if (cell.IsFlagged)
                    {
                        // A flag placed by the player (or by the Hint reward)
                        ShowFace(button, _flagFace, "F", Color.Gray, Color.Red);
                    }
                    else if (!cell.IsVisited)
                    {
                        // Not visited yet: gray
                        ShowFace(button, _tileFace, "", Color.Gray, Color.Black);
                    }
                    else
                    {
                        // A cleared cell: blank or the number of neighboring bombs
                        button.BackColor = Color.Gainsboro;
                        if (cell.NumberOfBombNeighbors > 0)
                        {
                            button.Text = cell.NumberOfBombNeighbors.ToString();
                            button.ForeColor = GetNumberColor(cell.NumberOfBombNeighbors);
                        }
                    }
                }
            }
        } // End of UpdateButtonFaces method

        /// <summary>
        /// Show an image face on a button, or fall back to text when the asset is missing
        /// </summary>
        /// <param name="button">The button to draw</param>
        /// <param name="face">The face image, or null</param>
        /// <param name="fallbackText">Text to show if there is no image</param>
        /// <param name="backColor">Background color</param>
        /// <param name="foreColor">Text color</param>
        private static void ShowFace(Button button, Image? face, string fallbackText, Color backColor, Color foreColor)
        {
            button.BackColor = backColor;
            button.ForeColor = foreColor;
            if (face != null)
            {
                button.BackgroundImage = face;
            }
            else
            {
                button.Text = fallbackText;
            }
        } // End of ShowFace method

        /// <summary>
        /// Pick the classic Minesweeper color for a neighbor count
        /// </summary>
        /// <param name="number">Number of neighboring bombs (1 - 8)</param>
        /// <returns>The text color for that number</returns>
        private static Color GetNumberColor(int number)
        {
            return number switch
            {
                1 => Color.Blue,
                2 => Color.Green,
                3 => Color.Red,
                4 => Color.Navy,
                5 => Color.Maroon,
                6 => Color.Teal,
                7 => Color.Black,
                _ => Color.DimGray
            };
        } // End of GetNumberColor method

        /// <summary>
        /// Mouse up event handler for every cell button: left click visits, right click flags
        /// </summary>
        /// <param name="sender">The cell button that was clicked</param>
        /// <param name="e">Mouse event arguments (which mouse button)</param>
        private void BtnCellMouseUpEH(object? sender, MouseEventArgs e)
        {
            // Ignore clicks after the game has ended
            if (_isGameOver || sender is not Button button || button.Tag is not Point location)
            {
                return;
            }
            int row = location.X;
            int col = location.Y;
            MoveResult result;

            // Send the move to the business logic layer
            if (e.Button == MouseButtons.Right)
            {
                result = _boardService.ToggleFlag(_board, row, col);
            }
            else if (e.Button == MouseButtons.Left)
            {
                result = _boardService.VisitCell(_board, row, col);
            }
            else
            {
                return;
            }

            // Show what happened, redraw the board, and check for a win or loss
            ShowMoveMessage(result, row, col);
            UpdateButtonFaces(false);
            UpdateStatusLabels();
            CheckGameState();
        } // End of BtnCellMouseUpEH method

        /// <summary>
        /// Turn a MoveResult from the BLL into a message for the player
        /// </summary>
        /// <param name="result">What the move did</param>
        /// <param name="row">Row of the cell</param>
        /// <param name="col">Column of the cell</param>
        private void ShowMoveMessage(MoveResult result, int row, int col)
        {
            lblMessage.Text = result switch
            {
                MoveResult.SafeCellVisited => $"Cell {row}, {col} is safe.",
                MoveResult.AreaRevealed => $"Cell {row}, {col} had no bombs nearby, so the empty area opened up.",
                MoveResult.AreaRevealedWithReward => $"The empty area around {row}, {col} opened up and held a special reward! You earned a Hint.",
                MoveResult.RewardCollected => $"Cell {row}, {col} had a special reward! You earned a Hint.",
                MoveResult.BombHit => $"Cell {row}, {col} was a bomb!",
                MoveResult.AlreadyVisited => $"Cell {row}, {col} is already cleared.",
                MoveResult.CellIsFlagged => $"Cell {row}, {col} is flagged. Right click to remove the flag first.",
                MoveResult.FlagPlaced => $"Flag placed on {row}, {col}.",
                MoveResult.FlagRemoved => $"Flag removed from {row}, {col}.",
                _ => "That cell is not on the board."
            };

            // A reward is worth a pop-up so the player notices the Hint button
            if (result == MoveResult.RewardCollected || result == MoveResult.AreaRevealedWithReward)
            {
                MessageBox.Show("You found the special reward! Click Use Hint to have one bomb flagged for you.",
                    "Reward", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        } // End of ShowMoveMessage method

        /// <summary>
        /// Ask the BLL for the game state and end the game on a win or loss
        /// </summary>
        private void CheckGameState()
        {
            GameState state = _boardService.DetermineGameState(_board);
            if (state != GameState.InProgress)
            {
                EndGame(state);
            }
        } // End of CheckGameState method

        /// <summary>
        /// Stop the game: stop the clock, show every bomb, and show the result and score
        /// </summary>
        /// <param name="state">Won or Lost</param>
        private void EndGame(GameState state)
        {
            // Declare and initialize
            _isGameOver = true;
            tmrGame.Stop();
            btnUseHint.Enabled = false;
            TimeSpan elapsed = _board.EndTime - _board.StartTime;
            int score = _boardService.DetermineFinalScore(_board, _rewardsUsed);

            UpdateButtonFaces(true);
            lblTime.Text = $"Time: {elapsed:mm\\:ss}";

            if (state == GameState.Won)
            {
                lblMessage.Text = $"You win! Score: {score}";
                MessageBox.Show($"You cleared the board in {elapsed.TotalSeconds:F0} seconds.\nScore: {score}",
                    "You Win!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lblMessage.Text = "Game over. You hit a bomb.";
                MessageBox.Show("You hit a bomb. Better luck next time!",
                    "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        } // End of EndGame method

        /// <summary>
        /// Update the bombs, flags, and rewards labels and the Use Hint button.
        /// The counts come from the business logic layer.
        /// </summary>
        private void UpdateStatusLabels()
        {
            lblBombs.Text = $"Bombs: {_boardService.CountBombs(_board)}   Flags: {_boardService.CountFlags(_board)}";
            lblRewards.Text = $"Hints: {_board.RewardsRemaining}";
            btnUseHint.Enabled = !_isGameOver && _board.RewardsRemaining > 0;
        } // End of UpdateStatusLabels method

        /// <summary>
        /// Click event handler for btnUseHint: spend a reward to flag one random bomb
        /// </summary>
        /// <param name="sender">btnUseHint</param>
        /// <param name="e">Event arguments</param>
        private void BtnUseHintClickEH(object sender, EventArgs e)
        {
            CellModel? revealed = _boardService.UseSpecialBonus(_board);
            if (revealed == null)
            {
                lblMessage.Text = "No hint to use. Find the reward cell first.";
                return;
            }
            _rewardsUsed++;
            lblMessage.Text = $"Hint used: cell {revealed.Row}, {revealed.Column} is a bomb. It has been flagged.";
            UpdateButtonFaces(false);
            UpdateStatusLabels();
            // Flagging the last bomb can win the game
            CheckGameState();
        } // End of BtnUseHintClickEH method

        /// <summary>
        /// Tick event handler for the game clock (every second)
        /// </summary>
        /// <param name="sender">tmrGame</param>
        /// <param name="e">Event arguments</param>
        private void TmrGameTickEH(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - _board.StartTime;
            lblTime.Text = $"Time: {elapsed:mm\\:ss}";
        } // End of TmrGameTickEH method

        /// <summary>
        /// Click event handler for btnNewGame: close this form and return to the start form
        /// </summary>
        /// <param name="sender">btnNewGame</param>
        /// <param name="e">Event arguments</param>
        private void BtnNewGameClickEH(object sender, EventArgs e)
        {
            Close();
        } // End of BtnNewGameClickEH method
    }
}
