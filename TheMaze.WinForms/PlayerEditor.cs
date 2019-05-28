using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheMaze.Core.Models.GameObjects;

namespace TheMaze.WinForms
{
    public partial class PlayerEditor : Form
    {
        private GameObject _player;

        public PlayerEditor()
        {
            InitializeComponent();
        }

        public void SetPlayer(GameObject player)
        {
            _player = player;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tbPlayerName.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            (_player as Player).PlayerName = tbPlayerName.Text;
            Close();
        }
    }
}
