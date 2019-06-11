using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TheMaze.Core.Enums;
using TheMaze.Core.Models.GameObjects;
using TheMaze.WinForms.Mappers;

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
                var col = new DataGridViewImageColumn();
                col.CellTemplate = new DataGridViewImageCell();
                col.Width = 20;
                _dataGameGrid.Columns.Add(col);

                for (int j = 0; j < _cells.GetLength(1) - 1 && i == 0; j++)
                {
                    _dataGameGrid.Rows.Add();
                }
            }

            for (var i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    if (_cells[i, j].IsActive)
                    {
                        _dataGameGrid.Rows[i].Cells[j].Style.ForeColor = WinColorMapper.MapToWinColor(_cells[i, j].ColorForeground);
                        _dataGameGrid.Rows[i].Cells[j].Style.BackColor = WinColorMapper.MapToWinColor(_cells[i, j].ColorBackground);
                        _dataGameGrid.Rows[i].Cells[j].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        _dataGameGrid.Rows[i].Cells[j].Value =
                            WinImageMapper.GetCellImageByType((_cells[i, j] as Cell).FieldType);
                    }
                    else
                    {
                        _dataGameGrid.Rows[i].Cells[j].Style.ForeColor = WinColorMapper.MapToWinColor(CellColor.RouteForeground);
                        _dataGameGrid.Rows[i].Cells[j].Style.BackColor = WinColorMapper.MapToWinColor(CellColor.RouteBackground);
                    }
                }
            }

            DrawPlayer(_player.PositionTop, _player.PositionLeft);
            _dataGameGrid.Rows[_player.PositionTop].Cells[_player.PositionLeft].Selected = true;
        }

        public void DrawRoute(int row, int column)
        {
            _dataGameGrid.Rows[row].Cells[column].Style.ForeColor = WinColorMapper.MapToWinColor(CellColor.RouteForeground);
            _dataGameGrid.Rows[row].Cells[column].Style.BackColor = WinColorMapper.MapToWinColor(CellColor.RouteBackground);
            _dataGameGrid.Rows[row].Cells[column].Style.SelectionBackColor = WinColorMapper.MapToWinColor(CellColor.RouteBackground);
            _dataGameGrid.Rows[row].Cells[column].Selected = false;
            _dataGameGrid.Rows[row].Cells[column].Value = WinImageMapper.GetCellImageByType(FieldTypes.Route);;
        }

        public void DrawPlayer(int row, int column)
        {
            _dataGameGrid.Rows[row].Cells[column].Style.ForeColor = WinColorMapper.MapToWinColor(CellColor.PlayerForeground);
            _dataGameGrid.Rows[row].Cells[column].Style.BackColor = WinColorMapper.MapToWinColor(CellColor.PlayerBackground);
            _dataGameGrid.Rows[row].Cells[column].Style.SelectionForeColor = WinColorMapper.MapToWinColor(CellColor.PlayerBackground);
            _dataGameGrid.Rows[row].Cells[column].Style.SelectionBackColor = WinColorMapper.MapToWinColor(CellColor.PlayerForeground);
            _dataGameGrid.Rows[row].Cells[column].Selected = true;
            _dataGameGrid.Rows[row].Cells[column].Value = Image.FromFile(
                $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}/Resources/Images/player.png");
        }
    }
}
