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
            ((System.ComponentModel.ISupportInitialize)(this.dgGame)).BeginInit();
            this.SuspendLayout();
            // 
            // dgGame
            // 
            this.dgGame.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgGame.ColumnHeadersVisible = false;
            this.dgGame.Location = new System.Drawing.Point(12, 12);
            this.dgGame.Name = "dgGame";
            this.dgGame.RowHeadersVisible = false;
            this.dgGame.Size = new System.Drawing.Size(304, 210);
            this.dgGame.TabIndex = 1;
            this.dgGame.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgGame_KeyDown);
            // 
            // GameFieldView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgGame);
            this.Name = "GameFieldView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game";
            ((System.ComponentModel.ISupportInitialize)(this.dgGame)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgGame;
    }
}