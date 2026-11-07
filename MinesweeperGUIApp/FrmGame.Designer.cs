namespace MinesweeperGUIApp
{
    partial class FrmGame
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
            components = new System.ComponentModel.Container();
            lblTime = new Label();
            lblBombs = new Label();
            lblRewards = new Label();
            lblMessage = new Label();
            pnlBoard = new Panel();
            btnUseHint = new Button();
            btnNewGame = new Button();
            tmrGame = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            //
            // lblTime
            //
            lblTime.AutoSize = true;
            lblTime.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTime.Location = new Point(20, 15);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(92, 20);
            lblTime.TabIndex = 0;
            lblTime.Text = "Time: 00:00";
            //
            // lblBombs
            //
            lblBombs.AutoSize = true;
            lblBombs.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblBombs.Location = new Point(190, 15);
            lblBombs.Name = "lblBombs";
            lblBombs.Size = new Size(152, 20);
            lblBombs.TabIndex = 1;
            lblBombs.Text = "Bombs: 0   Flags: 0";
            //
            // lblRewards
            //
            lblRewards.AutoSize = true;
            lblRewards.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblRewards.Location = new Point(440, 15);
            lblRewards.Name = "lblRewards";
            lblRewards.Size = new Size(63, 20);
            lblRewards.TabIndex = 2;
            lblRewards.Text = "Hints: 0";
            //
            // lblMessage
            //
            lblMessage.Font = new Font("Segoe UI", 10F);
            lblMessage.Location = new Point(20, 45);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(600, 25);
            lblMessage.TabIndex = 3;
            lblMessage.Text = "Message";
            lblMessage.TextAlign = ContentAlignment.MiddleLeft;
            //
            // pnlBoard
            //
            pnlBoard.Location = new Point(20, 78);
            pnlBoard.Name = "pnlBoard";
            pnlBoard.Size = new Size(600, 600);
            pnlBoard.TabIndex = 4;
            //
            // btnUseHint
            //
            btnUseHint.Enabled = false;
            btnUseHint.Location = new Point(20, 692);
            btnUseHint.Name = "btnUseHint";
            btnUseHint.Size = new Size(180, 40);
            btnUseHint.TabIndex = 5;
            btnUseHint.Text = "Use Hint";
            btnUseHint.UseVisualStyleBackColor = true;
            btnUseHint.Click += BtnUseHintClickEH;
            //
            // btnNewGame
            //
            btnNewGame.Location = new Point(440, 692);
            btnNewGame.Name = "btnNewGame";
            btnNewGame.Size = new Size(180, 40);
            btnNewGame.TabIndex = 6;
            btnNewGame.Text = "Close / New Game";
            btnNewGame.UseVisualStyleBackColor = true;
            btnNewGame.Click += BtnNewGameClickEH;
            //
            // tmrGame
            //
            tmrGame.Interval = 1000;
            tmrGame.Tick += TmrGameTickEH;
            //
            // FrmGame
            //
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(640, 748);
            Controls.Add(btnNewGame);
            Controls.Add(btnUseHint);
            Controls.Add(pnlBoard);
            Controls.Add(lblMessage);
            Controls.Add(lblRewards);
            Controls.Add(lblBombs);
            Controls.Add(lblTime);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmGame";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Minesweeper";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTime;
        private Label lblBombs;
        private Label lblRewards;
        private Label lblMessage;
        private Panel pnlBoard;
        private Button btnUseHint;
        private Button btnNewGame;
        private System.Windows.Forms.Timer tmrGame;
    }
}
