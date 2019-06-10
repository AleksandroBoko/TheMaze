namespace TheMaze.WinForms
{
    partial class GameFieldView
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
            this.dgGame = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxPlayerName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxCoins = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxCrystals = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxKeys = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxLifePoints = new System.Windows.Forms.TextBox();
            this.tbxSteps = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxTotalGamePoints = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgGame)).BeginInit();
            this.SuspendLayout();
            // 
            // dgGame
            // 
            this.dgGame.AllowUserToDeleteRows = false;
            this.dgGame.AllowUserToResizeColumns = false;
            this.dgGame.AllowUserToResizeRows = false;
            this.dgGame.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgGame.ColumnHeadersVisible = false;
            this.dgGame.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dgGame.Location = new System.Drawing.Point(12, 12);
            this.dgGame.Name = "dgGame";
            this.dgGame.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgGame.RowHeadersVisible = false;
            this.dgGame.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgGame.Size = new System.Drawing.Size(304, 210);
            this.dgGame.TabIndex = 1;
            this.dgGame.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgGame_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(474, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Player name";
            // 
            // tbxPlayerName
            // 
            this.tbxPlayerName.Location = new System.Drawing.Point(477, 26);
            this.tbxPlayerName.Name = "tbxPlayerName";
            this.tbxPlayerName.ReadOnly = true;
            this.tbxPlayerName.Size = new System.Drawing.Size(237, 20);
            this.tbxPlayerName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(474, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Coins";
            // 
            // tbxCoins
            // 
            this.tbxCoins.Location = new System.Drawing.Point(477, 140);
            this.tbxCoins.Name = "tbxCoins";
            this.tbxCoins.ReadOnly = true;
            this.tbxCoins.Size = new System.Drawing.Size(237, 20);
            this.tbxCoins.TabIndex = 5;
            this.tbxCoins.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(474, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Crystals";
            // 
            // tbxCrystals
            // 
            this.tbxCrystals.Location = new System.Drawing.Point(477, 200);
            this.tbxCrystals.Name = "tbxCrystals";
            this.tbxCrystals.ReadOnly = true;
            this.tbxCrystals.Size = new System.Drawing.Size(237, 20);
            this.tbxCrystals.TabIndex = 7;
            this.tbxCrystals.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(474, 244);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Keys";
            // 
            // tbxKeys
            // 
            this.tbxKeys.Location = new System.Drawing.Point(477, 260);
            this.tbxKeys.Name = "tbxKeys";
            this.tbxKeys.ReadOnly = true;
            this.tbxKeys.Size = new System.Drawing.Size(237, 20);
            this.tbxKeys.TabIndex = 9;
            this.tbxKeys.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(474, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Life points";
            // 
            // tbxLifePoints
            // 
            this.tbxLifePoints.Location = new System.Drawing.Point(477, 83);
            this.tbxLifePoints.Name = "tbxLifePoints";
            this.tbxLifePoints.ReadOnly = true;
            this.tbxLifePoints.Size = new System.Drawing.Size(237, 20);
            this.tbxLifePoints.TabIndex = 11;
            // 
            // tbxSteps
            // 
            this.tbxSteps.Location = new System.Drawing.Point(477, 386);
            this.tbxSteps.Name = "tbxSteps";
            this.tbxSteps.ReadOnly = true;
            this.tbxSteps.Size = new System.Drawing.Size(237, 20);
            this.tbxSteps.TabIndex = 13;
            this.tbxSteps.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(474, 370);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Steps";
            // 
            // tbxTotalGamePoints
            // 
            this.tbxTotalGamePoints.Location = new System.Drawing.Point(477, 325);
            this.tbxTotalGamePoints.Name = "tbxTotalGamePoints";
            this.tbxTotalGamePoints.ReadOnly = true;
            this.tbxTotalGamePoints.Size = new System.Drawing.Size(237, 20);
            this.tbxTotalGamePoints.TabIndex = 15;
            this.tbxTotalGamePoints.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(474, 309);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Total game points";
            // 
            // GameFieldView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(725, 450);
            this.Controls.Add(this.tbxTotalGamePoints);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbxSteps);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbxLifePoints);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbxKeys);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbxCrystals);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxCoins);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxPlayerName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgGame);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "GameFieldView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game";
            ((System.ComponentModel.ISupportInitialize)(this.dgGame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgGame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxPlayerName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxCoins;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxCrystals;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxKeys;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxLifePoints;
        private System.Windows.Forms.TextBox tbxSteps;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxTotalGamePoints;
        private System.Windows.Forms.Label label7;
    }
}