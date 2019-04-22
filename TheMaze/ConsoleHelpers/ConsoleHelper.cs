using System;
using System.Text;
using TheMaze.Enums;
using TheMaze.Models;

namespace TheMaze.ConsoleHelpers
{
    public class ConsoleHelper
    {
        public string PlayerName { private get; set; }

        public MenuItemType GameMenuHandler(StringBuilder gameMenu)
        {
            MenuItemType choosedMenuItem = MenuItemType.Exit;
            var isRightKey = false;
            while (!isRightKey)
            {
                Console.Clear();
                Console.WriteLine(gameMenu);
                Console.WriteLine("Choose the menu point from 0 to 3:");
                var key = Console.ReadLine();
                switch (key)
                {
                    case "0":
                        choosedMenuItem = MenuItemType.Play;
                        isRightKey = true;
                        break;
                    case "1":
                        choosedMenuItem = MenuItemType.LoadGame;
                        isRightKey = true;
                        break;
                    case "2": 
                        choosedMenuItem = MenuItemType.EditPlayer;
                        isRightKey = true;
                        break;
                    case "3": 
                        choosedMenuItem = MenuItemType.Information;
                        isRightKey = true;
                        break;
                    case "4":
                        choosedMenuItem = MenuItemType.Exit;
                        isRightKey = true;
                        break;
                }
            }

            return choosedMenuItem;
        }

        public void ShowPlayerLifePoints(int lifePoints, int maxLifePoint)
        {
            Console.SetCursorPosition(0, Configuration.ROW_NUMBER);
            Console.WriteLine(new String('-', Configuration.COLUMN_NUMBER));
            if (!String.IsNullOrEmpty(PlayerName))
            {
                Console.WriteLine($"Player name: {PlayerName}");
            }
            Console.WriteLine($"Life points: {new String('o', lifePoints)}{new String(' ', maxLifePoint - lifePoints)}");
        }
    }
}
