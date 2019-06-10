using System;
using System.Windows.Forms;
using TheMaze.Core.Enums;
using TheMaze.Core.Models;
using TheMaze.Core.Models.GameObjects;
using TheMaze.Core.TextHelpers;

namespace TheMaze.WinForms
{
    public partial class GameMenu : Form
    {
        private GameObject _player { get; set; }


        public GameMenu()
        {
            InitializeComponent();
            _player = new Player();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            var gameField = new GameField();
            gameField.Build(MenuItemType.Play);
            var drawer = new WinDrawer(gameField.Cells);

            (_player as Player).ResetData();
            (_player as Player).StarTime = DateTime.Now;

            drawer.SetPlayer(_player);
            var gameView = new GameFieldView();

            gameView.SetDrawer(drawer);
            gameView.SetPlayer(_player as Player);
            gameView.SetGameField(gameField);
            gameView.RunDrawer();
            gameView.Show();
        }

        private void btnQuickPlay_Click(object sender, EventArgs e)
        {
            var gameField = new GameField();
            gameField.Build(MenuItemType.QuickPlay);
            var drawer = new WinDrawer(gameField.Cells);

            (_player as Player).ResetData();
            (_player as Player).StarTime = DateTime.Now;

            drawer.SetPlayer(_player);
            var gameView = new GameFieldView();

            gameView.SetDrawer(drawer);
            gameView.SetPlayer(_player as Player);
            gameView.SetGameField(gameField);
            gameView.RunDrawer();
            gameView.Show();
        }

        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            var gameField = new GameField();
            if (!(_player as Player).Load() | !gameField.Load())
            {
                MessageBox.Show("The version of last saved game is not available");
            }
            else
            {
                var drawer = new WinDrawer(gameField.Cells);
                drawer.SetPlayer(_player);

                var gameView = new GameFieldView();

                gameView.SetDrawer(drawer);
                gameView.SetPlayer(_player as Player);
                gameView.SetGameField(gameField);
                gameView.RunDrawer();
                gameView.Show();
            }
        }

        private void btnSetPlayerName_Click(object sender, EventArgs e)
        {
            var playerEditor = new PlayerEditor();
            playerEditor.SetPlayer(_player);
            playerEditor.Show();
        }

        private void btnInstruction_Click(object sender, EventArgs e)
        {
            var w = new GameInformation();
            w.Text = "Instruction";
            w.SetInformationText(new GameInfo().GetInstruction().ToString());
            w.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
