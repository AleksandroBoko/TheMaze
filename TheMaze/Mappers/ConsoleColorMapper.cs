using System;
using TheMaze.Core.Enums;

namespace TheMaze.Mappers
{
    public static class ConsoleColorMapper
    {
        public static ConsoleColor MapToConsoleColor(CellColor cellColor)
        {
            ConsoleColor color = ConsoleColor.Black;
            switch (cellColor)
            {
                case CellColor.WallBackground:
                    color = ConsoleColor.Yellow;
                    break;
                case CellColor.WallForeground:
                    color = ConsoleColor.Black;
                    break;
                case CellColor.CoinBackground:
                    color = ConsoleColor.Black;
                    break;
                case CellColor.CoinForeground:
                    color = ConsoleColor.Cyan;
                    break;
                case CellColor.ClosedDoorBackground:
                    color = ConsoleColor.DarkGray;
                    break;
                case CellColor.ClosedDoorForeground:
                    color = ConsoleColor.Black;
                    break;
                case CellColor.DeadlyTrapBackground:
                    color = ConsoleColor.Black;
                    break;
                case CellColor.DeadlyTrapForeground:
                    color = ConsoleColor.Red;
                    break;
                case CellColor.OpenDoorBackground:
                    color = ConsoleColor.Green;
                    break;
                case CellColor.OpenDoorForeground:
                    color = ConsoleColor.White;
                    break;
                case CellColor.KeyBackground:
                    color = ConsoleColor.Black;
                    break;
                case CellColor.KeyForeground:
                    color = ConsoleColor.Blue;
                    break;
                case CellColor.CrystalBackground:
                    color = ConsoleColor.Blue;
                    break;
                case CellColor.CrystalForeground:
                    color = ConsoleColor.White;
                    break;
                case CellColor.TrapBackground:
                    color = ConsoleColor.Black;
                    break;
                case CellColor.TrapForeground:
                    color = ConsoleColor.Red;
                    break;
                case CellColor.PortalBackground:
                    color = ConsoleColor.Blue;
                    break;
                case CellColor.PortalForeground:
                    color = ConsoleColor.White;
                    break;
                case CellColor.PrizeBackground:
                    color = ConsoleColor.Green;
                    break;
                case CellColor.PrizeForeground:
                    color = ConsoleColor.Black;
                    break;
                case CellColor.PlayerBackground:
                    color = ConsoleColor.White;
                    break;
                case CellColor.PlayerForeground:
                    color = ConsoleColor.Blue;
                    break;

            }

            return color;
        }
    }
}
