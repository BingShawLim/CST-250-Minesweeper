# CST-250 Minesweeper - Milestone 5 (Bing Shaw Lim)

| Project | Purpose |
|---|---|
| MinesweeperClassLibrary | Models (CellModel, BoardModel, GameState, MoveResult, GameStatModel), Services/BusinessLogicLayer (IBoardService, BoardService, DifficultyBase + Easy/Medium/Hard, IGameStatService, GameStatService, GameStatSorter + SortByName/SortByScore/SortByDate), and Services/DataAccessLayer (IGameStatDAO, GameStatTextFileDAO) |
| MinesweeperClassLibrary.Tests | xUnit tests for the business logic and data access layers (62 test cases) |
| MinesweeperGUIApp | Windows Forms game: FrmStart picks the size and difficulty, FrmGame is the clickable board, FrmWinnerName (Form3) and FrmGameStats (Form4) handle the high scores |
| MinesweeperConsoleApp | Playable console game: choose a size and difficulty, then Visit, Flag/Unflag, or Use Reward each turn until you win or hit a bomb |

**Milestone 5 features:** game statistics and high scores for winning games.
- `GameStatModel` (the guide's GameStat class): Id, Name, Score, GameTime, and Date.
- Form3 (`FrmWinnerName`) opens after a win, shows the score and time, and gets the winner's name (1 to 20 letters, digits, or spaces, checked by `GameStatService.IsValidName`).
- The game form passes the name and score to Form4 (`FrmGameStats`), which adds the game to the list and shows every winner in a DataGridView.
- Form4 menus: File > Save, Load, Exit (text file through `GameStatTextFileDAO`, one line per game) and Sort > By Name, By Score, By Date (polymorphism: each menu passes a different `GameStatSorter` subclass to `GameStatService.Sort`).
- `FrmStart` keeps one `GameStatService`, so the list lasts for every game while the app is open; its High Scores button opens Form4 at any time (for example, to load a saved file).

**Milestone 4 features:** a Windows Forms version of the game on the same class library.
- `FrmStart` (startup form in `Program.cs`): a TrackBar picks the board size (5 to 20) and radio buttons pick Easy, Medium, or Hard; a label previews the bomb count. Start Game passes both values to `FrmGame`.
- `FrmGame`: a panel holds a grid of buttons (the Activity 2 Chessboard technique). Left click visits a cell (empty areas open with flood fill), right click places or removes a flag. Button faces come from `board.Cells`: gray tile = not visited, flag = `F`, bomb = `B`, a colored number or a blank for visited cells.
- Labels show the time, bombs and flags, and Hints. Use Hint spends a reward to flag one bomb. A win shows the time and the score from `DetermineFinalScore`; a loss shows every bomb. Close / New Game returns to `FrmStart`.
- Game assets in `MinesweeperGUIApp/Assets` (tile, flag, bomb, reward) were drawn for this project.
- New business logic: `CountBombs` and `CountFlags`, so the form shows counts without doing the counting itself.

**Milestone 3 features:** a recursive `FloodFill(board, row, col)` method in `BoardService`. Visiting a cell with no bomb neighbors opens the whole connected empty area plus the numbered cells around its edge. The fill stops at the edge of the board, at visited, flagged, and bomb cells, and at numbered cells. A reward found by the fill is collected for the player.

**Milestone 2 features:** interactive game loop, `PrintBoard` (shows `?` for unvisited cells and `F` for flags), `DetermineGameState` (Won / Lost / InProgress), `VisitCell` and `ToggleFlag` in the business logic layer, and a working **Hint** reward (visit the `r` cell, then choose option 3 to reveal and flag a random bomb).

Open `Minesweeper.sln` in Visual Studio 2022, set MinesweeperGUIApp (or MinesweeperConsoleApp) as the startup project, and press F5. Run the tests from Test Explorer.
