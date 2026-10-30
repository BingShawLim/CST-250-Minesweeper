# CST-250 Minesweeper - Milestone 2 (Bing Shaw Lim)

| Project | Purpose |
|---|---|
| MinesweeperClassLibrary | Models (CellModel, BoardModel, GameState, MoveResult) and Services/BusinessLogicLayer (IBoardService, BoardService, DifficultyBase + Easy/Medium/Hard) |
| MinesweeperClassLibrary.Tests | xUnit tests for the business logic layer (27 tests) |
| MinesweeperConsoleApp | Playable console game: choose a size and difficulty, then Visit, Flag/Unflag, or Use Reward each turn until you win or hit a bomb |

**Milestone 2 features:** interactive game loop, `PrintBoard` (shows `?` for unvisited cells and `F` for flags), `DetermineGameState` (Won / Lost / InProgress), `VisitCell` and `ToggleFlag` in the business logic layer, and a working **Hint** reward (visit the `r` cell, then choose option 3 to reveal and flag a random bomb).

Open `Minesweeper.sln` in Visual Studio 2022, set MinesweeperConsoleApp as the startup project, and press F5. Run the tests from Test Explorer.
