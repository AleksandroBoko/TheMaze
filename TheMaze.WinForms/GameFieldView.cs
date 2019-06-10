using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheMaze.Core.Configurations;
using TheMaze.Core.Enums;
using TheMaze.Core.Models.GameObjects;
using TheMaze.Core.TextHelpers;
using TheMaze.WinForms.Models;

namespace TheMaze.WinForms
{
    public partial class GameFieldView : Form
    {
        private WinDrawer winDrawer;
        private Player player;
        private GameObject[,] points;
        //private TypeFinishGame typeFinishGame;
        private readonly GameInfo gameInfo;

        public GameFieldView()
        {
            InitializeComponent();
            gameInfo = new GameInfo();
        }

        public void SetDrawer(WinDrawer drawer)
        {
            winDrawer = drawer;
        }

        public void SetPlayer(Player gamePlayer)
        {
            player = gamePlayer;
            SetPlayerData();
        }

        private void SetPlayerData()
        {
            if (player != null)
            {
                tbxPlayerName.Text = player.PlayerName;
                tbxLifePoints.Text = player.CountLifePoints.ToString();
                tbxCoins.Text = player.CountCoins.ToString();
                tbxCrystals.Text = player.CountCrystals.ToString();
                tbxKeys.Text = player.CountKeys.ToString();
                tbxSteps.Text = player.CountSteps.ToString();
            }
        }

        public void SetPoints(GameObject[,] gamePoints)
        {
            points = gamePoints;
        }

        public void RunDrawer()
        {
            if (winDrawer != null)
            {
                winDrawer.SetDataGrid(dgGame);
                winDrawer.Draw();
            }
        }

        private void dgGame_KeyDown(object sender, KeyEventArgs e)
        {
            //var key = e;
            //var send = sender;
            //var rows = dgGame.RowCount;
            //var colums = dgGame.ColumnCount;

            var result = MovementHandler(e);
            if (result != null && result.TypeFinishGame != TypeFinishGame.Continue)
            {
                HandleResultGame(result);
            }

            

            //if (key.KeyCode != Keys.Down)
            //{
            //    key.SuppressKeyPress = true;

            //}
        }

        private void HandleResultGame(StepResult stepResult)
        {
            var game = new GameInformation();
            switch (stepResult.TypeFinishGame)
            {
                case TypeFinishGame.Won:
                    game.Text = "Congratulation!";
                    game.SetInformationText(stepResult.Text);
                    Close();
                    game.Show();
                    break;
                case TypeFinishGame.Lost:
                    game.Text = "You have lost";
                    game.SetInformationText(stepResult.Text);
                    player.SetPosition(0, 0);
                    Close();
                    game.Show();
                    break;
                case TypeFinishGame.Exit:
                    game.Text = "Bye! See you later";
                    game.SetInformationText(stepResult.Text);
                    game.Show();
                    break;
            }
        }

        private bool NextStepHandler(FieldTypes fieldType, int nextRowPosition, int nextColumnPosition)
        {
            var result = true;
            //var points = gameField.Cells;
            switch (fieldType)
            {
                case FieldTypes.OpenedDoor:
                    if ((player as Player).CountGamePoints < Configuration.GAMEPOINTS_TO_EXIT
                        || (player as Player).CountSteps > Configuration.STEPS_TO_CLOSE_DOOR)
                    {
                        result = false;
                    }

                    break;
                case FieldTypes.Wall:
                    result = false;
                    break;
                case FieldTypes.ClosedDoor:
                    result = (player as Player).CountKeys > 0;
                    break;
                case FieldTypes.Coin:
                    if (points[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        player.IncreaseCoins();
                        points[nextRowPosition, nextColumnPosition].IsActive = false;
                        tbxCoins.Text = player.CountCoins.ToString();
                        tbxTotalGamePoints.Text = player.CountGamePoints.ToString();
                    }

                    break;
                case FieldTypes.Key:
                    if (points[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        player.IncreaseKeys();
                        points[nextRowPosition, nextColumnPosition].IsActive = false;
                        tbxKeys.Text = player.CountKeys.ToString();
                        tbxTotalGamePoints.Text = player.CountGamePoints.ToString();
                    }

                    break;
                case FieldTypes.Trap:
                    if (points[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        player.DecreaseLifePoints();
                        points[nextRowPosition, nextColumnPosition].IsActive = false;
                        tbxLifePoints.Text = player.CountLifePoints.ToString();
                        //var currentRow = Console.CursorTop;
                        //var currentColumn = Console.CursorLeft;
                        ////consoleHelper.ShowPlayerLifePoints((player as Player).CountLifePoints,
                        ////    Player.MAX_LIFE_POINTS);
                        //Console.SetCursorPosition(currentColumn, currentRow);
                    }

                    break;
                case FieldTypes.DeadlyTrap:
                    if (points[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        for (int i = 0; i < Player.MAX_LIFE_POINTS; i++)
                        {
                            player.DecreaseLifePoints();
                        }

                        points[nextRowPosition, nextColumnPosition].IsActive = false;
                        tbxLifePoints.Text = player.CountLifePoints.ToString();
                        //var currentRow = Console.CursorTop;
                        //var currentColumn = Console.CursorLeft;
                        ////consoleHelper.ShowPlayerLifePoints((player as Player).CountLifePoints,
                        ////    Player.MAX_LIFE_POINTS);
                        //Console.SetCursorPosition(currentColumn, currentRow);
                    }

                    break;
                case FieldTypes.Prize:
                    if (points[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        points[nextRowPosition, nextColumnPosition].IsActive = false;
                        player.IncreaseStepPerTime();
                        player.IncreaseGamePoints(Configuration.PRIZE_VALUE);
                    }

                    break;
                case FieldTypes.Crystal:
                    if (points[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        points[nextRowPosition, nextColumnPosition].IsActive = false;
                        player.IncreaseCrystals();
                        tbxCrystals.Text = player.CountCrystals.ToString();
                        tbxTotalGamePoints.Text = player.CountGamePoints.ToString();
                    }

                    break;
                //case FieldTypes.Portal:
                //    if (points[nextRowPosition, nextColumnPosition].IsActive)
                //    {
                //        points[nextRowPosition, nextColumnPosition].IsActive = false;
                //    }

                //    break;
            }

            return result;
        }

        private StepResult MovementHandler(KeyEventArgs key)
        {
            var typeFinishGame = TypeFinishGame.Continue;
            var isExit = false;
            var nextPointType = FieldTypes.Route;
            var isNextStepDone = false;
            //while (!isExit)
            //{
                var stepsDone = 0;
                var stepsNeedToDo = player.StepsPerTime;
                while (stepsDone < stepsNeedToDo)
                {
                    isNextStepDone = false;
                    switch (key.KeyCode)
                    {
                        case Keys.Down:
                            {
                            //DrawRoute(stepsDone);
                                
                                if (player.PositionTop < Configuration.ROW_NUMBER - 1)
                                {
                                    nextPointType = (points[player.PositionTop + 1, player.PositionLeft] as Cell).FieldType;
                                    var canDoNextStep = NextStepHandler(nextPointType, player.PositionTop + 1, player.PositionLeft);
                                    if (canDoNextStep)
                                    {
                                        winDrawer.DrawRoute(player.PositionTop, player.PositionLeft);
                                        player.SetPosition(player.PositionTop + 1, player.PositionLeft);
                                        winDrawer.DrawPlayer(player.PositionTop, player.PositionLeft);
                                        isNextStepDone = true;
                                        player.IncreaseSteps();
                                        tbxSteps.Text = player.CountSteps.ToString();
                                }
                                else
                                    {
                                        key.SuppressKeyPress = true;
                                    }
                                }
                                else
                                {
                                    key.SuppressKeyPress = true;
                                }

                                //DrawPlayer();
                                //player.IncreaseSteps();

                                break;
                            }
                        case Keys.Up:
                            {
                                //DrawRoute(stepsDone);
                                if (player.PositionTop > 0)
                                {
                                    nextPointType = (points[player.PositionTop - 1, player.PositionLeft] as Cell).FieldType;
                                    if (NextStepHandler(nextPointType, player.PositionTop - 1, player.PositionLeft))
                                    {
                                        winDrawer.DrawRoute(player.PositionTop, player.PositionLeft);
                                        player.SetPosition(player.PositionTop - 1, player.PositionLeft);
                                        winDrawer.DrawPlayer(player.PositionTop, player.PositionLeft);
                                        isNextStepDone = true;
                                        player.IncreaseSteps();
                                        tbxSteps.Text = player.CountSteps.ToString();
                                }
                                    else
                                    {
                                        key.SuppressKeyPress = true;
                                    }
                                }
                                else
                                {
                                    key.SuppressKeyPress = true;
                                }

                            //DrawPlayer();
                            //player.IncreaseSteps();
                                break;
                            }
                        case Keys.Left:
                            {
                                //DrawRoute(stepsDone);
                                if (player.PositionLeft > 0)
                                {
                                    nextPointType = (points[player.PositionTop, player.PositionLeft - 1] as Cell)
                                        .FieldType;
                                    if (NextStepHandler(nextPointType, player.PositionTop, player.PositionLeft - 1))
                                    {
                                        winDrawer.DrawRoute(player.PositionTop, player.PositionLeft);
                                        player.SetPosition(player.PositionTop, player.PositionLeft-1);
                                        winDrawer.DrawPlayer(player.PositionTop, player.PositionLeft);
                                        isNextStepDone = true;
                                        player.IncreaseSteps();
                                        tbxSteps.Text = player.CountSteps.ToString();
                                }
                                    else
                                    {
                                        key.SuppressKeyPress = true;
                                    }
                                }
                                else
                                {
                                    key.SuppressKeyPress = true;
                                }

                            //DrawPlayer();
                            //player.IncreaseSteps();
                                break;
                            }
                        case Keys.Right:
                            {
                                //DrawRoute(stepsDone);
                                if (player.PositionLeft < Configuration.COLUMN_NUMBER - 1)
                                {
                                    nextPointType = (points[player.PositionTop, player.PositionLeft + 1] as Cell).FieldType;
                                    if (NextStepHandler(nextPointType, player.PositionTop, player.PositionLeft + 1))
                                    {
                                        winDrawer.DrawRoute(player.PositionTop, player.PositionLeft);
                                        player.SetPosition(player.PositionTop, player.PositionLeft + 1);
                                        winDrawer.DrawPlayer(player.PositionTop, player.PositionLeft);
                                        isNextStepDone = true;
                                        player.IncreaseSteps();
                                        tbxSteps.Text = player.CountSteps.ToString();
                                }
                                    else
                                    {
                                        key.SuppressKeyPress = true;
                                    }
                                }
                                else
                                {
                                    key.SuppressKeyPress = true;
                                }

                                //DrawPlayer();
                                //player.IncreaseSteps();
                                break;
                            }
                        case Keys.Escape:
                            {
                                typeFinishGame = TypeFinishGame.Exit;
                                isExit = true;
                                break;
                            }
                        case Keys.F2:
                            {
                                //SaveGame();
                                //DrawPlayer(stepsDone == 0);
                                break;
                            }
                        //case ConsoleKey.Enter:
                        //    {
                        //        Console.CursorLeft = player.PositionLeft;
                        //        Console.CursorTop = player.PositionTop;
                        //        break;
                        //    }
                        //default:
                        //    {
                        //        DrawRoute(0);
                        //        break;
                        //    }
                    }

                    if (isNextStepDone &&
                        (nextPointType == FieldTypes.OpenedDoor || nextPointType == FieldTypes.ClosedDoor))
                    {
                        typeFinishGame = TypeFinishGame.Won;
                        isExit = true;
                    }
                    else if (isNextStepDone && (nextPointType == FieldTypes.Trap ||
                                                nextPointType == FieldTypes.DeadlyTrap)
                                            && player.CountLifePoints == 0)
                    {
                        typeFinishGame = TypeFinishGame.Lost;
                        isExit = true;
                    }
                else if (isNextStepDone && nextPointType == FieldTypes.Portal
                                        && points[player.PositionTop, player.PositionLeft].IsActive)
                {
                    points[player.PositionTop, player.PositionLeft].IsActive = false;
                    winDrawer.DrawRoute(player.PositionTop, player.PositionLeft);
                    
                    
                    //dgGame.Rows[player.PositionTop].Cells[player.PositionLeft].Selected = false;
                    //player.IncreaseGamePoints(Configuration.PORTAL_VALUE);
                    var random = new Random();
                    while (true)
                    {
                        var row = random.Next(0, Configuration.ROW_NUMBER - 1);
                        var column = random.Next(0, Configuration.COLUMN_NUMBER - 1);
                        if ((points[row, column] as Cell).FieldType == FieldTypes.Route)
                        {
                            player.SetPosition(row, column);
                            winDrawer.DrawPlayer(player.PositionTop, player.PositionLeft);
                            var cell = dgGame.Rows[row].Cells[column]; 
                            cell.Selected = true;
                            dgGame.CurrentCell = dgGame.Rows[row].Cells[column];

                            //drawer.DrawRoute();
                            //Console.CursorLeft -= 1;

                            //Console.SetCursorPosition(column, row);
                            //player.SetPosition(row, column);

                            //drawer.DrawPlayer();
                            //Console.CursorLeft -= 1;
                            break;
                        }

                    }
                }

                stepsDone++;
                }

            return GetStepResult(typeFinishGame);

            //}
        }

        private StepResult GetStepResult(TypeFinishGame typeFinishGame)
        {
            var time = (int)(DateTime.Now - player.StarTime).TotalSeconds;
            var resultText = String.Empty;
            //TypeFinishGame gameStatus = TypeFinishGame.Continue;

            switch (typeFinishGame)
            {
                case TypeFinishGame.Won:
                    resultText = gameInfo.GetFinalResult(player, time).ToString();
                    break;
                case TypeFinishGame.Lost:
                    resultText = gameInfo.GetLoseInfo(time).ToString();
                    break;
                case TypeFinishGame.Exit:
                    resultText = gameInfo.GetExitInfo().ToString();
                    break;
            }

            return new StepResult
            {
                TypeFinishGame = typeFinishGame,
                Text = resultText
            };
        }
    }
}
