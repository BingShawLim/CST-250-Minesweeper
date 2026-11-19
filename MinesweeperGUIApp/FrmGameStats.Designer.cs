namespace MinesweeperGUIApp
{
    partial class FrmGameStats
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mnsMain = new MenuStrip();
            tsmFile = new ToolStripMenuItem();
            tsmSave = new ToolStripMenuItem();
            tsmLoad = new ToolStripMenuItem();
            tsmExit = new ToolStripMenuItem();
            tsmSort = new ToolStripMenuItem();
            tsmSortByName = new ToolStripMenuItem();
            tsmSortByScore = new ToolStripMenuItem();
            tsmSortByDate = new ToolStripMenuItem();
            lblTitle = new Label();
            dgvStats = new DataGridView();
            btnOk = new Button();
            mnsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStats).BeginInit();
            SuspendLayout();
            //
            // mnsMain
            //
            mnsMain.ImageScalingSize = new Size(20, 20);
            mnsMain.Items.AddRange(new ToolStripItem[] { tsmFile, tsmSort });
            mnsMain.Location = new Point(0, 0);
            mnsMain.Name = "mnsMain";
            mnsMain.Size = new Size(582, 28);
            mnsMain.TabIndex = 0;
            //
            // tsmFile
            //
            tsmFile.DropDownItems.AddRange(new ToolStripItem[] { tsmSave, tsmLoad, tsmExit });
            tsmFile.Name = "tsmFile";
            tsmFile.Size = new Size(46, 24);
            tsmFile.Text = "File";
            //
            // tsmSave
            //
            tsmSave.Name = "tsmSave";
            tsmSave.Size = new Size(224, 26);
            tsmSave.Text = "Save";
            tsmSave.Click += TsmSaveClickEH;
            //
            // tsmLoad
            //
            tsmLoad.Name = "tsmLoad";
            tsmLoad.Size = new Size(224, 26);
            tsmLoad.Text = "Load";
            tsmLoad.Click += TsmLoadClickEH;
            //
            // tsmExit
            //
            tsmExit.Name = "tsmExit";
            tsmExit.Size = new Size(224, 26);
            tsmExit.Text = "Exit";
            tsmExit.Click += TsmExitClickEH;
            //
            // tsmSort
            //
            tsmSort.DropDownItems.AddRange(new ToolStripItem[] { tsmSortByName, tsmSortByScore, tsmSortByDate });
            tsmSort.Name = "tsmSort";
            tsmSort.Size = new Size(51, 24);
            tsmSort.Text = "Sort";
            //
            // tsmSortByName
            //
            tsmSortByName.Name = "tsmSortByName";
            tsmSortByName.Size = new Size(224, 26);
            tsmSortByName.Text = "By Name";
            tsmSortByName.Click += TsmSortByNameClickEH;
            //
            // tsmSortByScore
            //
            tsmSortByScore.Name = "tsmSortByScore";
            tsmSortByScore.Size = new Size(224, 26);
            tsmSortByScore.Text = "By Score";
            tsmSortByScore.Click += TsmSortByScoreClickEH;
            //
            // tsmSortByDate
            //
            tsmSortByDate.Name = "tsmSortByDate";
            tsmSortByDate.Size = new Size(224, 26);
            tsmSortByDate.Text = "By Date";
            tsmSortByDate.Click += TsmSortByDateClickEH;
            //
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.Location = new Point(12, 34);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(100, 25);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "High Scores";
            // 
            // dgvStats
            //
            dgvStats.AllowUserToAddRows = false;
            dgvStats.AllowUserToDeleteRows = false;
            dgvStats.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvStats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStats.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStats.Location = new Point(12, 66);
            dgvStats.Name = "dgvStats";
            dgvStats.ReadOnly = true;
            dgvStats.RowHeadersVisible = false;
            dgvStats.RowHeadersWidth = 51;
            dgvStats.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStats.Size = new Size(558, 280);
            dgvStats.TabIndex = 1;
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.Location = new Point(476, 356);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(94, 32);
            btnOk.TabIndex = 3;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += BtnOkClickEH;
            // 
            // FrmGameStats
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 400);
            MinimumSize = new Size(500, 330);
            Controls.Add(btnOk);
            Controls.Add(dgvStats);
            Controls.Add(lblTitle);
            Controls.Add(mnsMain);
            MainMenuStrip = mnsMain;
            Name = "FrmGameStats";
            StartPosition = FormStartPosition.CenterParent;
            Text = "High Scores";
            Load += FrmGameStatsLoadEH;
            mnsMain.ResumeLayout(false);
            mnsMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStats).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip mnsMain;
        private ToolStripMenuItem tsmFile;
        private ToolStripMenuItem tsmSave;
        private ToolStripMenuItem tsmLoad;
        private ToolStripMenuItem tsmExit;
        private ToolStripMenuItem tsmSort;
        private ToolStripMenuItem tsmSortByName;
        private ToolStripMenuItem tsmSortByScore;
        private ToolStripMenuItem tsmSortByDate;
        private Label lblTitle;
        private DataGridView dgvStats;
        private Button btnOk;
    }
}
