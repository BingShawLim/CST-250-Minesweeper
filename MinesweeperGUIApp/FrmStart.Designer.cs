namespace MinesweeperGUIApp
{
    partial class FrmStart
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblSize = new Label();
            trbSize = new TrackBar();
            lblSizeValue = new Label();
            grpDifficulty = new GroupBox();
            rdoHard = new RadioButton();
            rdoMedium = new RadioButton();
            rdoEasy = new RadioButton();
            lblBombPreview = new Label();
            btnStartGame = new Button();
            btnExit = new Button();
            ((System.ComponentModel.ISupportInitialize)trbSize).BeginInit();
            grpDifficulty.SuspendLayout();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(178, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Minesweeper";
            //
            // lblSize
            //
            lblSize.AutoSize = true;
            lblSize.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSize.Location = new Point(22, 72);
            lblSize.Name = "lblSize";
            lblSize.Size = new Size(80, 19);
            lblSize.TabIndex = 1;
            lblSize.Text = "Board Size";
            //
            // trbSize
            //
            trbSize.LargeChange = 2;
            trbSize.Location = new Point(18, 96);
            trbSize.Maximum = 20;
            trbSize.Minimum = 5;
            trbSize.Name = "trbSize";
            trbSize.Size = new Size(280, 45);
            trbSize.TabIndex = 2;
            trbSize.Value = 10;
            trbSize.ValueChanged += TrbSizeValueChangedEH;
            //
            // lblSizeValue
            //
            lblSizeValue.Font = new Font("Segoe UI", 10F);
            lblSizeValue.Location = new Point(305, 98);
            lblSizeValue.Name = "lblSizeValue";
            lblSizeValue.Size = new Size(70, 23);
            lblSizeValue.TabIndex = 3;
            lblSizeValue.Text = "10 x 10";
            lblSizeValue.TextAlign = ContentAlignment.MiddleLeft;
            //
            // grpDifficulty
            //
            grpDifficulty.Controls.Add(rdoHard);
            grpDifficulty.Controls.Add(rdoMedium);
            grpDifficulty.Controls.Add(rdoEasy);
            grpDifficulty.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpDifficulty.Location = new Point(22, 150);
            grpDifficulty.Name = "grpDifficulty";
            grpDifficulty.Size = new Size(350, 70);
            grpDifficulty.TabIndex = 4;
            grpDifficulty.TabStop = false;
            grpDifficulty.Text = "Difficulty";
            //
            // rdoEasy
            //
            rdoEasy.AutoSize = true;
            rdoEasy.Checked = true;
            rdoEasy.Font = new Font("Segoe UI", 10F);
            rdoEasy.Location = new Point(15, 30);
            rdoEasy.Name = "rdoEasy";
            rdoEasy.Size = new Size(52, 23);
            rdoEasy.TabIndex = 0;
            rdoEasy.TabStop = true;
            rdoEasy.Text = "Easy";
            rdoEasy.UseVisualStyleBackColor = true;
            rdoEasy.CheckedChanged += RdoDifficultyCheckedChangedEH;
            //
            // rdoMedium
            //
            rdoMedium.AutoSize = true;
            rdoMedium.Font = new Font("Segoe UI", 10F);
            rdoMedium.Location = new Point(125, 30);
            rdoMedium.Name = "rdoMedium";
            rdoMedium.Size = new Size(76, 23);
            rdoMedium.TabIndex = 1;
            rdoMedium.Text = "Medium";
            rdoMedium.UseVisualStyleBackColor = true;
            rdoMedium.CheckedChanged += RdoDifficultyCheckedChangedEH;
            //
            // rdoHard
            //
            rdoHard.AutoSize = true;
            rdoHard.Font = new Font("Segoe UI", 10F);
            rdoHard.Location = new Point(250, 30);
            rdoHard.Name = "rdoHard";
            rdoHard.Size = new Size(55, 23);
            rdoHard.TabIndex = 2;
            rdoHard.Text = "Hard";
            rdoHard.UseVisualStyleBackColor = true;
            rdoHard.CheckedChanged += RdoDifficultyCheckedChangedEH;
            //
            // lblBombPreview
            //
            lblBombPreview.AutoSize = true;
            lblBombPreview.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblBombPreview.ForeColor = Color.DimGray;
            lblBombPreview.Location = new Point(22, 232);
            lblBombPreview.Name = "lblBombPreview";
            lblBombPreview.Size = new Size(190, 19);
            lblBombPreview.TabIndex = 5;
            lblBombPreview.Text = "Easy: 10 bombs on 100 cells";
            //
            // btnStartGame
            //
            btnStartGame.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStartGame.Location = new Point(22, 270);
            btnStartGame.Name = "btnStartGame";
            btnStartGame.Size = new Size(170, 42);
            btnStartGame.TabIndex = 6;
            btnStartGame.Text = "Start Game";
            btnStartGame.UseVisualStyleBackColor = true;
            btnStartGame.Click += BtnStartGameClickEH;
            //
            // btnExit
            //
            btnExit.Location = new Point(202, 270);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(170, 42);
            btnExit.TabIndex = 7;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += BtnExitClickEH;
            //
            // FrmStart
            //
            AcceptButton = btnStartGame;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(394, 330);
            Controls.Add(btnExit);
            Controls.Add(btnStartGame);
            Controls.Add(lblBombPreview);
            Controls.Add(grpDifficulty);
            Controls.Add(lblSizeValue);
            Controls.Add(trbSize);
            Controls.Add(lblSize);
            Controls.Add(lblTitle);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmStart";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Minesweeper - New Game";
            ((System.ComponentModel.ISupportInitialize)trbSize).EndInit();
            grpDifficulty.ResumeLayout(false);
            grpDifficulty.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblSize;
        private TrackBar trbSize;
        private Label lblSizeValue;
        private GroupBox grpDifficulty;
        private RadioButton rdoEasy;
        private RadioButton rdoMedium;
        private RadioButton rdoHard;
        private Label lblBombPreview;
        private Button btnStartGame;
        private Button btnExit;
    }
}
