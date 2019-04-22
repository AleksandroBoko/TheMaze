using System;

namespace TheMaze.Models
{
    public class Drawer
    {
        private Point[,] points;
        private Player player;

        public Drawer(Point[,] points)
        {
            this.points = points; 
        }

        public void SetPoints(Point[,] points)
        {
            this.points = points;
        }

        public void SetPlayer(Player player)
        {
            this.player = player;
        }

        public void Draw()
        {
            for (var i = 0; i < points.GetLength(0); i++)
            {
                for (int j = 0; j < points.GetLength(1); j++)
                {
                    Console.ForegroundColor = points[i, j].ColorForground;
                    Console.BackgroundColor = points[i, j].ColorBackground;
                    if (points[i, j].IsActive)
                    {
                        Console.Write(points[i, j].Symbol);
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }

                Console.WriteLine();
            }

            Console.SetCursorPosition(player.PositionLeft, player.PositionTop);
            DrawPlayer();
            Console.SetCursorPosition(player.PositionLeft, player.PositionTop);

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void DrawPlayer()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write('0');
        }

        public void DrawRoute()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(' ');
        }
    }
}
