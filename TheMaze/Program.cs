using System;
using TheMaze.Models;

namespace TheMaze
{
    class Program
    {
        private static Game game { get; set; }

        static void Main(string[] args)
        {
            game = new Game();
            game.Run();

            Console.ReadKey();
        }

    }
}
