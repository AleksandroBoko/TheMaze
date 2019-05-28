using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheMaze.WinForms
{
    public partial class GameFieldView : Form
    {
        public WinDrawer winDrawer { get; set; }

        public GameFieldView()
        {
            InitializeComponent();
        }

        public void SetDrawer(WinDrawer drawer)
        {
            winDrawer = drawer;
        }

        public void RunDrawer()
        {
            if (winDrawer != null)
            {
                winDrawer.SetDataGrid(dgGame);
                winDrawer.Draw();
            }
        }
    }
}
