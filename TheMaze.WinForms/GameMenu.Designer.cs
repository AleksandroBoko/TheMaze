using System;
using System.IO;
using System.Media;
using System.Threading;

namespace TheMaze.WinForms
{
    partial class GameMenu
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
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnQuickPlay = new System.Windows.Forms.Button();
            this.btnLoadGame = new System.Windows.Forms.Button();
            this.btnSetPlayerName = new System.Windows.Forms.Button();
            this.btnInstruction = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(144, 134);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(357, 23);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnQuickPlay
            // 
            this.btnQuickPlay.Location = new System.Drawing.Point(144, 163);
            this.btnQuickPlay.Name = "btnQuickPlay";
            this.btnQuickPlay.Size = new System.Drawing.Size(357, 23);
            this.btnQuickPlay.TabIndex = 1;
            this.btnQuickPlay.Text = "Quick Play";
            this.btnQuickPlay.UseVisualStyleBackColor = true;
            this.btnQuickPlay.Click += new System.EventHandler(this.btnQuickPlay_Click);
            // 
            // btnLoadGame
            // 
            this.btnLoadGame.Location = new System.Drawing.Point(144, 192);
            this.btnLoadGame.Name = "btnLoadGame";
            this.btnLoadGame.Size = new System.Drawing.Size(357, 23);
            this.btnLoadGame.TabIndex = 2;
            this.btnLoadGame.Text = "Load saved game";
            this.btnLoadGame.UseVisualStyleBackColor = true;
            this.btnLoadGame.Click += new System.EventHandler(this.btnLoadGame_Click);
            // 
            // btnSetPlayerName
            // 
            this.btnSetPlayerName.Location = new System.Drawing.Point(144, 221);
            this.btnSetPlayerName.Name = "btnSetPlayerName";
            this.btnSetPlayerName.Size = new System.Drawing.Size(357, 23);
            this.btnSetPlayerName.TabIndex = 3;
            this.btnSetPlayerName.Text = "Set player name";
            this.btnSetPlayerName.UseVisualStyleBackColor = true;
            this.btnSetPlayerName.Click += new System.EventHandler(this.btnSetPlayerName_Click);
            // 
            // btnInstruction
            // 
            this.btnInstruction.Location = new System.Drawing.Point(144, 250);
            this.btnInstruction.Name = "btnInstruction";
            this.btnInstruction.Size = new System.Drawing.Size(357, 23);
            this.btnInstruction.TabIndex = 4;
            this.btnInstruction.Text = "Instruction";
            this.btnInstruction.UseVisualStyleBackColor = true;
            this.btnInstruction.Click += new System.EventHandler(this.btnInstruction_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(144, 279);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(357, 23);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(293, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "Menu";
            // 
            // GameMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnInstruction);
            this.Controls.Add(this.btnSetPlayerName);
            this.Controls.Add(this.btnLoadGame);
            this.Controls.Add(this.btnQuickPlay);
            this.Controls.Add(this.btnPlay);
            this.Name = "GameMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.ResumeLayout(false);
            this.PerformLayout();

            var thread = new Thread(x =>
            {
                using (var soundPlayer = new SoundPlayer())
                {
                    soundPlayer.SoundLocation = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}/Resources/Audio/bensound-summer.wav";
                    soundPlayer.Play();
                }
            });

            thread.Start();

        }

        #endregion

        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnQuickPlay;
        private System.Windows.Forms.Button btnLoadGame;
        private System.Windows.Forms.Button btnSetPlayerName;
        private System.Windows.Forms.Button btnInstruction;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label1;
    }
}