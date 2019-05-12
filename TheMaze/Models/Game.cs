using System;
using TheMaze.ConsoleHelpers;
using TheMaze.Enums;
using TheMaze.Models.GameObjects;
using TheMaze.TextHelpers;

namespace TheMaze.Models
{
    public class Game
    {
        private GameObject playerInfo { get; set; }
        private GameField gameField { get; set; }
        private Drawer drawer { get; set; }
        private GameInfo gameInfo { get; set; }
        private TypeFinishGame typeFinishGame { get; set; }
        private ConsoleHelper consoleHelper { get; set; }

        public Game()
        {
            Initialize();
        }

        public void Run()
        {
            MenuHandler();
        }

        private void Initialize()
        {
            Console.CursorVisible = false;
            Console.Title = Configuration.TITLE;

            playerInfo = new Player();
            gameField = new GameField();
            drawer = new Drawer(gameField.Cells);
            gameInfo = new GameInfo();
            consoleHelper = new ConsoleHelper();
        }

        private void MenuHandler()
        {
            var isNeedRepeatMenu = true;
            while (isNeedRepeatMenu)
            {
                var key = consoleHelper.GameMenuHandler(gameInfo.GetGameMenu());
                Console.Clear();
                switch (key)
                {
                    case MenuItemType.Play:
                    case MenuItemType.QuickPlay:
                        isNeedRepeatMenu = false;
                        gameField.Build(key);
                        drawer.SetPoints(gameField.Cells);
                        drawer.SetPlayer(playerInfo);
                        drawer.Draw();
                        consoleHelper.ShowPlayerLifePoints((playerInfo as Player).CountLifePoints, Player.MAX_LIFE_POINTS);
                        Console.SetCursorPosition(playerInfo.PositionLeft, playerInfo.PositionTop);
                        (playerInfo as Player).StarTime = DateTime.Now;
                        MovementHandler();
                        ShowGameInfo();
                        break;                    
                    case MenuItemType.LoadGame:
                        if (!(playerInfo as Player).Load() | !gameField.Load())
                        {
                            Console.WriteLine("The version of last saved game is not available");
                            Console.ReadLine();
                        }
                        else
                        {
                            isNeedRepeatMenu = false;
                            drawer.SetPoints(gameField.Cells);
                            drawer.SetPlayer(playerInfo);
                            drawer.Draw();
                            consoleHelper.ShowPlayerLifePoints((playerInfo as Player).CountLifePoints, Player.MAX_LIFE_POINTS);
                            Console.SetCursorPosition(playerInfo.PositionLeft, playerInfo.PositionTop);
                            MovementHandler();
                            ShowGameInfo();
                        }
                        break;
                    case MenuItemType.EditPlayer:
                        Console.WriteLine("Write your name:");
                        (playerInfo as Player).PlayerName = Console.ReadLine();
                        consoleHelper.PlayerName = (playerInfo as Player).PlayerName;
                        gameInfo.PlayerName = (playerInfo as Player).PlayerName;
                        break;
                    case MenuItemType.Information:
                        Console.WriteLine(gameInfo.GetInstruction());
                        Console.ReadKey();
                        break;
                    case MenuItemType.Exit:
                        isNeedRepeatMenu = false;
                        Console.WriteLine(gameInfo.GetExitInfo());
                        break;
                }
            }
        }

        private void MovementHandler()
        {
            var isExit = false;
            var nextPointType = FieldTypes.Route;
            var isNextStepDone = false;
            while (!isExit)
            {
                var key = Console.ReadKey();
                var stepsDone = 0;
                var stepsNeedToDo = (playerInfo as Player).StepsPerTime;
                while (stepsDone < stepsNeedToDo)
                {
                    isNextStepDone = false;
                    switch (key.Key)
                    {
                        case ConsoleKey.DownArrow:
                            {
                                if (stepsDone == 0)
                                {
                                    Console.CursorLeft -= 1;
                                }
                                drawer.DrawRoute();
                                Console.CursorLeft -= 1;
                                if (Console.CursorTop < Configuration.ROW_NUMBER - 1)
                                {
                                    nextPointType = (gameField[Console.CursorTop + 1, Console.CursorLeft] as Cell)
                                        .FieldType;
                                    var canDoNextStep = NextStepHandler(nextPointType, Console.CursorTop + 1,
                                        Console.CursorLeft);
                                    if (canDoNextStep)
                                    {
                                        Console.CursorTop++;
                                        (playerInfo as Player).SetPosition(Console.CursorTop, Console.CursorLeft);
                                        isNextStepDone = true;
                                    }
                                }

                                drawer.DrawPlayer();
                                Console.CursorLeft -= 1;
                                (playerInfo as Player).IncreaseSteps();

                                break;
                            }
                        case ConsoleKey.UpArrow:
                            {
                                if (stepsDone == 0)
                                {
                                    Console.CursorLeft -= 1;
                                }
                                drawer.DrawRoute();
                                Console.CursorLeft -= 1;
                                if (Console.CursorTop > 0)
                                {
                                    nextPointType = (gameField[Console.CursorTop - 1, Console.CursorLeft] as Cell)
                                        .FieldType;
                                    if (NextStepHandler(nextPointType, Console.CursorTop - 1, Console.CursorLeft))
                                    {
                                        Console.CursorTop--;
                                        (playerInfo as Player).SetPosition(Console.CursorTop, Console.CursorLeft);
                                        isNextStepDone = true;
                                    }
                                }

                                drawer.DrawPlayer();
                                Console.CursorLeft -= 1;
                                (playerInfo as Player).IncreaseSteps();
                                break;
                            }
                        case ConsoleKey.LeftArrow:
                            {
                                if (stepsDone == 0)
                                {
                                    Console.CursorLeft -= 1;
                                }
                                drawer.DrawRoute();
                                Console.CursorLeft -= 1;
                                if (Console.CursorLeft > 0)
                                {
                                    nextPointType = (gameField[Console.CursorTop, Console.CursorLeft - 1] as Cell)
                                        .FieldType;
                                    if (NextStepHandler(nextPointType, Console.CursorTop, Console.CursorLeft - 1))
                                    {
                                        Console.CursorLeft--;
                                        (playerInfo as Player).SetPosition(Console.CursorTop, Console.CursorLeft);
                                        isNextStepDone = true;
                                    }
                                }

                                drawer.DrawPlayer();
                                Console.CursorLeft -= 1;
                                (playerInfo as Player).IncreaseSteps();
                                break;
                            }
                        case ConsoleKey.RightArrow:
                            {
                                if (stepsDone == 0)
                                {
                                    Console.CursorLeft -= 1;
                                }
                                drawer.DrawRoute();
                                Console.CursorLeft -= 1;
                                if (Console.CursorLeft < Configuration.COLUMN_NUMBER - 1)
                                {
                                    nextPointType = (gameField[Console.CursorTop, Console.CursorLeft + 1] as Cell)
                                        .FieldType;
                                    if (NextStepHandler(nextPointType, Console.CursorTop, Console.CursorLeft + 1))
                                    {
                                        Console.CursorLeft++;
                                        (playerInfo as Player).SetPosition(Console.CursorTop, Console.CursorLeft);
                                        isNextStepDone = true;
                                    }

                                }

                                drawer.DrawPlayer();
                                Console.CursorLeft -= 1;
                                (playerInfo as Player).IncreaseSteps();
                                break;
                            }
                        case ConsoleKey.Escape:
                            {
                                typeFinishGame = TypeFinishGame.Exit;
                                isExit = true;
                                break;
                            }
                        case ConsoleKey.F2:
                            {
                                SaveGame();
                                if (stepsDone == 0)
                                {
                                    Console.CursorLeft -= 1;
                                }
                                drawer.DrawPlayer();
                                Console.CursorLeft -= 1;
                                break;
                            }
                        case ConsoleKey.Enter:
                        {
                                Console.CursorLeft = (playerInfo as Player).PositionLeft;
                                Console.CursorTop = (playerInfo as Player).PositionTop;   
                                break;
                            }
                        default:
                            {
                                Console.CursorLeft -= 1;
                                drawer.DrawPlayer();
                                Console.CursorLeft -= 1;
                                break;
                            }
                    }

                    if (isNextStepDone &&
                        (nextPointType == FieldTypes.OpenedDoor || nextPointType == FieldTypes.ClosedDoor))
                    {
                        typeFinishGame = TypeFinishGame.Won;
                        isExit = true;
                    }
                    else if (isNextStepDone && (nextPointType == FieldTypes.Trap ||
                                                nextPointType == FieldTypes.DeadlyTrap)
                                            && (playerInfo as Player).CountLifePoints == 0)
                    {
                        typeFinishGame = TypeFinishGame.Lost;
                        isExit = true;
                    }
                    else if (isNextStepDone && nextPointType == FieldTypes.Portal
                                            && gameField[Console.CursorTop, Console.CursorLeft].IsActive)
                    {
                        gameField[Console.CursorTop, Console.CursorLeft].IsActive = false;
                        (playerInfo as Player).IncreaseGamePoints(Configuration.PORTAL_VALUE);
                        var random = new Random();
                        while (true)
                        {
                            var row = random.Next(0, Configuration.ROW_NUMBER - 1);
                            var column = random.Next(0, Configuration.COLUMN_NUMBER - 1);
                            if ((gameField[row, column] as Cell).FieldType == FieldTypes.Route)
                            {
                                drawer.DrawRoute();
                                Console.CursorLeft -= 1;

                                Console.SetCursorPosition(column, row);
                                (playerInfo as Player).SetPosition(row, column);

                                drawer.DrawPlayer();
                                Console.CursorLeft -= 1;
                                break;
                            }

                        }
                    }

                    stepsDone++;
                }
            }
        }

        private void SaveGame()
        {
            gameField.Save();
            (playerInfo as Player).Save();
        }

        private bool NextStepHandler(FieldTypes fieldTypes, int nextRowPosition, int nextColumnPosition)
        {
            var result = true;
            var points = gameField.Cells;
            switch (fieldTypes)
            {
                case FieldTypes.OpenedDoor:
                    if ((playerInfo as Player).CountGamePoints < Configuration.GAMEPOINTS_TO_EXIT
                        || (playerInfo as Player).CountSteps > Configuration.STEPS_TO_CLOSE_DOOR)
                    {
                        result = false;
                    }

                    break;
                case FieldTypes.Wall:
                    result = false;
                    break;
                case FieldTypes.ClosedDoor:
                    result = (playerInfo as Player).CountKeys > 0;
                    break;
                case FieldTypes.Coin:
                    if (points[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        (playerInfo as Player).IncreaseCoins();
                        points[nextRowPosition, nextColumnPosition].IsActive = false;
                    }

                    break;
                case FieldTypes.Key:
                    if (points[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        (playerInfo as Player).IncreaseKeys();
                        points[nextRowPosition, nextColumnPosition].IsActive = false;
                    }

                    break;
                case FieldTypes.Trap:
                    if (points[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        (playerInfo as Player).DecreaseLifePoints();
                        points[nextRowPosition, nextColumnPosition].IsActive = false;
                        var currentRow = Console.CursorTop;
                        var currentColumn = Console.CursorLeft;
                        consoleHelper.ShowPlayerLifePoints((playerInfo as Player).CountLifePoints,
                            Player.MAX_LIFE_POINTS);
                        Console.SetCursorPosition(currentColumn, currentRow);
                    }

                    break;
                case FieldTypes.DeadlyTrap:
                    if (points[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        for (int i = 0; i < Player.MAX_LIFE_POINTS; i++)
                        {
                            (playerInfo as Player).DecreaseLifePoints();
                        }

                        points[nextRowPosition, nextColumnPosition].IsActive = false;
                        var currentRow = Console.CursorTop;
                        var currentColumn = Console.CursorLeft;
                        consoleHelper.ShowPlayerLifePoints((playerInfo as Player).CountLifePoints,
                            Player.MAX_LIFE_POINTS);
                        Console.SetCursorPosition(currentColumn, currentRow);
                    }

                    break;
                case FieldTypes.Prize:
                    if (points[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        points[nextRowPosition, nextColumnPosition].IsActive = false;
                        (playerInfo as Player).IncreaseStepPerTime();
                        (playerInfo as Player).IncreaseGamePoints(Configuration.PRIZE_VALUE);
                    }

                    break;
                case FieldTypes.Crystal:
                    if (points[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        points[nextRowPosition, nextColumnPosition].IsActive = false;
                        (playerInfo as Player).IncreaseCrystals();
                    }
                    break;
            }

            return result;
        }

        private void ShowGameInfo()
        {
            switch (typeFinishGame)
            {
                case TypeFinishGame.Won:
                    Console.Clear();
                    var time = (int)(DateTime.Now - (playerInfo as Player).StarTime).TotalSeconds;
                    Console.WriteLine(gameInfo.GetFinalResult(playerInfo as Player, time));
                    break;
                case TypeFinishGame.Lost:
                    Console.Clear();
                    Console.WriteLine(gameInfo.GetLoseInfo((int)(DateTime.Now - (playerInfo as Player).StarTime).TotalSeconds));
                    break;
                case TypeFinishGame.Exit:
                    Console.Clear();
                    Console.WriteLine(gameInfo.GetExitInfo());
                    break;
            }
        }
    }
}
