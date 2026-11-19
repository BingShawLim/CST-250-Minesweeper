namespace MinesweeperGUIApp
{
    partial class FrmWinnerName
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
            lblWinMessage = new Label();
            lblScore = new Label();
            lblPrompt = new Label();
            txtWinnerName = new TextBox();
            lblNameError = new Label();
            btnOk = new Button();
            SuspendLayout();
            //
            // lblWinMessage
            //
            lblWinMessage.AutoSize = true;
            lblWinMessage.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblWinMessage.Location = new Point(20, 18);
            lblWinMessage.Name = "lblWinMessage";
            lblWinMessage.Size = new Size(80, 25);
            lblWinMessage.TabIndex = 0;
            lblWinMessage.Text = "Congratulations, you win!";
            //
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Location = new Point(20, 50);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(50, 20);
            lblScore.TabIndex = 5;
            lblScore.Text = "Score:";
            // 
            // lblPrompt
            //
            lblPrompt.AutoSize = true;
            lblPrompt.Location = new Point(20, 80);
            lblPrompt.Name = "lblPrompt";
            lblPrompt.Size = new Size(210, 20);
            lblPrompt.TabIndex = 1;
            lblPrompt.Text = "Enter your name for the high score list:";
            //
            // txtWinnerName
            //
            txtWinnerName.Location = new Point(20, 106);
            txtWinnerName.MaxLength = 20;
            txtWinnerName.Name = "txtWinnerName";
            txtWinnerName.Size = new Size(300, 27);
            txtWinnerName.TabIndex = 2;
            //
            // lblNameError
            //
            lblNameError.AutoSize = true;
            lblNameError.ForeColor = Color.Red;
            lblNameError.Location = new Point(20, 138);
            lblNameError.Name = "lblNameError";
            lblNameError.Size = new Size(0, 20);
            lblNameError.TabIndex = 3;
            //
            // btnOk
            //
            btnOk.Location = new Point(226, 166);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(94, 32);
            btnOk.TabIndex = 4;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += BtnOkClickEH;
            //
            // FrmWinnerName
            //
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(342, 216);
            Controls.Add(btnOk);
            Controls.Add(lblNameError);
            Controls.Add(txtWinnerName);
            Controls.Add(lblPrompt);
            Controls.Add(lblScore);
            Controls.Add(lblWinMessage);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmWinnerName";
            StartPosition = FormStartPosition.CenterParent;
            Text = "You Win!";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblWinMessage;
        private Label lblScore;
        private Label lblPrompt;
        private TextBox txtWinnerName;
        private Label lblNameError;
        private Button btnOk;
    }
}
