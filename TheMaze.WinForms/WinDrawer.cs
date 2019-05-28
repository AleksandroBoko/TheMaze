using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheMaze.Core.Configurations;
using TheMaze.Core.Models.GameObjects;

namespace TheMaze.WinForms
{
    public class WinDrawer
    {
        private GameObject[,] _cells;
        private GameObject _player;
        private DataGridView _dataGameGrid;

        public WinDrawer(GameObject[,] cells)
        {
            _cells = cells;
        }

        public void SetPoints(GameObject[,] cells)
        {
            _cells = cells;
        }

        public void SetPlayer(GameObject player)
        {
            _player = player;
        }

        public void SetDataGrid(DataGridView dataGrid)
        {
            _dataGameGrid = dataGrid;
        }

        public void Draw()
        {
            _dataGameGrid.AutoSize = true;

            for (var i = 0; i < _cells.GetLength(0); i++)
            {
                var col = new DataGridViewColumn();
                col.CellTemplate = new DataGridViewTextBoxCell();
                // col.Width == 100;
                col.Width = 20;
                var columnIndex = _dataGameGrid.Columns.Add(col);

                //if (i < _cells.GetLength(0) - 1)
                //{
                //    _dataGameGrid.Rows.Add();
                //}

                for (int j = 0; j < _cells.GetLength(1) - 1 && i == 0; j++)
                {
                    _dataGameGrid.Rows.Add();



                    //Console.ResetColor();
                    //if (_cells[i, j].IsActive)
                    //{
                    //    Console.ForegroundColor = _cells[i, j].ColorForeground;
                    //    Console.BackgroundColor = _cells[i, j].ColorBackground;
                    //    Console.Write(_cells[i, j].Symbol);
                    //}
                    //else
                    //{
                    //    Console.Write(' ');
                    //}
                }
                
                //_dataGameGrid.Rows[0].Cells[0].Value = "H";

                //break;

                //Console.WriteLine();


            }

            for (var i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    if (_cells[i, j].IsActive)
                    {
                        //_dataGameGrid.Rows[i].Cells[j].Style.ForeColor = Color. _cells[i, j].ColorForeground.;
                        //Console.BackgroundColor = _cells[i, j].ColorBackground;
                        _dataGameGrid.Rows[i].Cells[j].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        _dataGameGrid.Rows[i].Cells[j].Value = _cells[i, j].Symbol;
                    }
                    //else
                    //{
                    //    Console.Write(' ');
                    //}

                    //_dataGameGrid.Rows[i].Cells[j].Value = "H";
                }
            }



            //_dataGameGrid.Rows[0].Cells[0].Value = "H";

            }
    }
}
