/*
 * Bing Shaw Lim
 * CST-250
 * 11/07/2026
 * Minesweeper GUI - Milestone 4
 * Resources: CST-250 Project Milestone Guide (Grand Canyon University, 2025)
 */

namespace MinesweeperGUIApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            // The start form collects the board size and difficulty first
            Application.Run(new FrmStart());
        }
    }
}
