using System;
using TheMaze.Models.GameObjects;

namespace TheMaze.Models
{
    public class Drawer
    {
        private GameObject[,] _points;
        private GameObject _player;

        public Drawer(GameObject[,] points)
        {
            _points = points; 
        }

        public void SetPoints(GameObject[,] points)
        {
            _points = points;
        }

        public void SetPlayer(GameObject player)
        {
            _player = player;
        }

        public void Draw()
        {
            for (var i = 0; i < _points.GetLength(0); i++)
            {
                for (int j = 0; j < _points.GetLength(1); j++)
                {
                    Console.ForegroundColor = _points[i, j].ColorForeground;
                    Console.BackgroundColor = _points[i, j].ColorBackground;
                    if (_points[i, j].IsActive)
                    {
                        Console.Write(_points[i, j].Symbol);
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
            Console.ForegroundColor = _player.ColorForeground; 
            Console.BackgroundColor = _player.ColorBackground; 
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
