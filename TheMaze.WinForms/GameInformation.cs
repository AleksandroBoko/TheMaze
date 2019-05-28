using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheMaze.Core.TextHelpers;

namespace TheMaze.WinForms
{
    public partial class GameInformation : Form
    {
        public GameInformation()
        {
            gameTextInfo = new GameInfo();
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
