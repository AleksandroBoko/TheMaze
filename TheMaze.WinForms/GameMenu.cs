using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheMaze.Core.Enums;
using TheMaze.Core.Models;
using TheMaze.Core.Models.GameObjects;

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
            var gameView = new GameFieldView();
            gameView.SetDrawer(drawer);
            gameView.RunDrawer();
            gameView.Show();
        }

        private void btnQuickPlay_Click(object sender, EventArgs e)
        {
            var gameField = new GameField();
            gameField.Build(MenuItemType.QuickPlay);
            var drawer = new WinDrawer(gameField.Cells);
            drawer.SetPlayer(_player);
            var gameView = new GameFieldView();
            gameView.SetDrawer(drawer);
            gameView.RunDrawer();
            gameView.Show();
        }

        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            //
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
            w.SetState(MenuItemType.Information);
            w.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
