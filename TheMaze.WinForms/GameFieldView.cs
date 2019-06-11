using System;
using System.Windows.Forms;
using TheMaze.Core.Configurations;
using TheMaze.Core.Enums;
using TheMaze.Core.Models;
using TheMaze.Core.Models.GameObjects;
using TheMaze.Core.TextHelpers;
using TheMaze.WinForms.Models;
using TheMaze.WinForms.Services;

namespace TheMaze.WinForms
{
    public partial class GameFieldView : Form
    {
        private WinDrawer winDrawer;
        private Player player;
        private GameField gameField;
        private readonly GameInfo gameInfo;
        private DateTime startTime;

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

        public void SetGameField(GameField field)
        {
            gameField = field;
        }

        public void RunDrawer()
        {
            if (winDrawer != null)
            {
                winDrawer.SetDataGrid(dgGame);
                winDrawer.Draw();
            }

            dgGame.CurrentCell = dgGame.Rows[player.PositionTop].Cells[player.PositionLeft];
            startTime = DateTime.Now;
        }

        private void dgGame_KeyDown(object sender, KeyEventArgs e)
        {
            var result = MovementHandler(e);
            if (result != null && result.TypeFinishGame != TypeFinishGame.Continue)
            {
                HandleResultGame(result);
            }
        }

        private void HandleResultGame(StepResult stepResult)
        {
            var game = new GameInformation();
            switch (stepResult.TypeFinishGame)
            {
                case TypeFinishGame.Won:
                    new SoundService().PlayWinSound();
                    game.Text = "Congratulation!";
                    game.SetInformationText(stepResult.Text);
                    Close();
                    game.Show();
                    break;
                case TypeFinishGame.Lost:
                    new SoundService().PlayLoseSound();
                    game.Text = "You have lost";
                    game.SetInformationText(stepResult.Text);
                    player.SetPosition(0, 0);
                    Close();
                    game.Show();
                    break;
                case TypeFinishGame.Exit:
                    new SoundService().PlayLoseSound();
                    game.Text = "Bye! See you later";
                    game.SetInformationText(stepResult.Text);
                    Close();
                    game.Show();
                    break;
            }
        }

        private bool NextStepHandler(FieldTypes fieldType, int nextRowPosition, int nextColumnPosition)
        {
            var result = true;
            switch (fieldType)
            {
                case FieldTypes.OpenedDoor:
                    if (player.CountGamePoints < Configuration.GAMEPOINTS_TO_EXIT
                        || player.CountSteps > Configuration.STEPS_TO_CLOSE_DOOR
                        || (DateTime.Now - player.StarTime).TotalSeconds > Configuration.TIME_TO_CLOSE_DOOR)
                    {
                        result = false;
                        new SoundService().PlayClosedSound();
                    }

                    break;
                case FieldTypes.Wall:
                    result = false;
                    new SoundService().PlayClosedSound();
                    break;
                case FieldTypes.ClosedDoor:                    
                    result = player.CountKeys > 0;
                    if (!result)
                    {
                        new SoundService().PlayClosedSound();
                    }
                    break;
                case FieldTypes.Coin:
                    if (gameField.Cells[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        player.IncreaseCoins();
                        gameField.Cells[nextRowPosition, nextColumnPosition].IsActive = false;
                        tbxCoins.Text = player.CountCoins.ToString();
                        tbxTotalGamePoints.Text = player.CountGamePoints.ToString();
                        new SoundService().PlayItemSound();
                    }

                    break;
                case FieldTypes.Key:
                    if (gameField.Cells[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        player.IncreaseKeys();
                        gameField.Cells[nextRowPosition, nextColumnPosition].IsActive = false;
                        tbxKeys.Text = player.CountKeys.ToString();
                        tbxTotalGamePoints.Text = player.CountGamePoints.ToString();
                        new SoundService().PlayKeySound();
                    }

                    break;
                case FieldTypes.Trap:
                    if (gameField.Cells[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        player.DecreaseLifePoints();
                        gameField.Cells[nextRowPosition, nextColumnPosition].IsActive = false;
                        tbxLifePoints.Text = player.CountLifePoints.ToString();
                        new SoundService().PlayTrapSound();
                    }

                    break;
                case FieldTypes.DeadlyTrap:
                    if (gameField.Cells[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        for (int i = 0; i < Player.MAX_LIFE_POINTS; i++)
                        {
                            player.DecreaseLifePoints();
                        }

                        gameField.Cells[nextRowPosition, nextColumnPosition].IsActive = false;
                        tbxLifePoints.Text = player.CountLifePoints.ToString();
                        new SoundService().PlayTrapSound();
                    }

                    break;
                case FieldTypes.Prize:
                    if (gameField.Cells[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        gameField.Cells[nextRowPosition, nextColumnPosition].IsActive = false;
                        player.IncreaseStepPerTime();
                        player.IncreaseGamePoints(Configuration.PRIZE_VALUE);
                        new SoundService().PlayItemSound();
                    }

                    break;
                case FieldTypes.Crystal:
                    if (gameField.Cells[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        gameField.Cells[nextRowPosition, nextColumnPosition].IsActive = false;
                        player.IncreaseCrystals();
                        tbxCrystals.Text = player.CountCrystals.ToString();
                        tbxTotalGamePoints.Text = player.CountGamePoints.ToString();
                        new SoundService().PlayItemSound();
                    }

                    break;
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
                                    nextPointType = (gameField.Cells[player.PositionTop + 1, player.PositionLeft] as Cell).FieldType;
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

                                break;
                            }
                        case Keys.Up:
                            {
                                //DrawRoute(stepsDone);
                                if (player.PositionTop > 0)
                                {
                                    nextPointType = (gameField.Cells[player.PositionTop - 1, player.PositionLeft] as Cell).FieldType;
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

                                break;
                            }
                        case Keys.Left:
                            {
                                //DrawRoute(stepsDone);
                                if (player.PositionLeft > 0)
                                {
                                    nextPointType = (gameField.Cells[player.PositionTop, player.PositionLeft - 1] as Cell)
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

                                break;
                            }
                        case Keys.Right:
                            {
                                //DrawRoute(stepsDone);
                                if (player.PositionLeft < Configuration.COLUMN_NUMBER - 1)
                                {
                                    nextPointType = (gameField.Cells[player.PositionTop, player.PositionLeft + 1] as Cell).FieldType;
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
                                gameField.Save();
                                player.Save();
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
                                        && gameField.Cells[player.PositionTop, player.PositionLeft].IsActive)
                {
                    new SoundService().PlayItemSound();
                    gameField.Cells[player.PositionTop, player.PositionLeft].IsActive = false;
                    winDrawer.DrawRoute(player.PositionTop, player.PositionLeft);
                    //player.IncreaseGamePoints(Configuration.PORTAL_VALUE);
                    var random = new Random();
                    while (true)
                    {
                        var row = random.Next(0, Configuration.ROW_NUMBER - 1);
                        var column = random.Next(0, Configuration.COLUMN_NUMBER - 1);
                        if ((gameField.Cells[row, column] as Cell).FieldType == FieldTypes.Route)
                        {
                            player.SetPosition(row, column);
                            winDrawer.DrawPlayer(player.PositionTop, player.PositionLeft);
                            dgGame.CurrentCell = dgGame.Rows[row].Cells[column];
                            dgGame.CurrentCell.Selected = true;
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
