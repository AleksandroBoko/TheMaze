using System;
using TheMaze.Core.Models.GameObjects;
using TheMaze.Mappers;

namespace TheMaze.Models
{
    public class Drawer
    {
        private GameObject[,] _cells;
        private GameObject _player;

        public Drawer(GameObject[,] cells)
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

        public void Draw()
        {
            for (var i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    Console.ResetColor();
                    if (_cells[i, j].IsActive)
                    {
                        Console.ForegroundColor = ConsoleColorMapper.MapToConsoleColor(_cells[i, j].ColorForeground);
                        Console.BackgroundColor = ConsoleColorMapper.MapToConsoleColor(_cells[i, j].ColorBackground);
                        Console.Write(_cells[i, j].Symbol);
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }

                Console.WriteLine();
            }

            Console.SetCursorPosition(_player.PositionLeft, _player.PositionTop);
            DrawPlayer();
            Console.SetCursorPosition(_player.PositionLeft, _player.PositionTop);
            SetDefaultColors();
        }

        public void DrawPlayer()
        {
            Console.ForegroundColor = ConsoleColorMapper.MapToConsoleColor(_player.ColorForeground);
            Console.BackgroundColor = ConsoleColorMapper.MapToConsoleColor(_player.ColorBackground);
            Console.Write(_player.Symbol);
        }

        public void DrawRoute()
        {
            SetDefaultColors();
            Console.Write(' ');
        }

        private void SetDefaultColors()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
