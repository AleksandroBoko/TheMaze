using TheMaze.Core.Enums;
using TheMaze.Core.TextHelpers;

namespace TheMaze.WinForms
{
    partial class GameInformation
    {
        private readonly GameInfo gameTextInfo;

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
            this.rtbGameInformation = new System.Windows.Forms.RichTextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbGameInformation
            // 
            this.rtbGameInformation.Location = new System.Drawing.Point(12, 12);
            this.rtbGameInformation.MinimumSize = new System.Drawing.Size(400, 400);
            this.rtbGameInformation.Name = "rtbGameInformation";
            this.rtbGameInformation.Size = new System.Drawing.Size(400, 400);
            this.rtbGameInformation.TabIndex = 0;
            this.rtbGameInformation.Text = "";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(174, 418);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // GameInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 454);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.rtbGameInformation);
            this.Name = "GameInformation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Information";
            this.ResumeLayout(false);

        }

        #endregion

        public void SetState(MenuItemType menuItemType)
        {
            switch (menuItemType)
            {
                case MenuItemType.Information:
                    rtbGameInformation.Text = gameTextInfo.GetInstruction().ToString();
                    break;
            }
        }

        private System.Windows.Forms.RichTextBox rtbGameInformation;
        private System.Windows.Forms.Button btnOk;
    }
}