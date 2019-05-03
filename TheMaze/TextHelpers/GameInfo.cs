using System;
using System.Text;

namespace TheMaze.TextHelpers
{
    public class GameInfo
    {
        private StringBuilder textInfo;
        public string PlayerName { private get; set; }

        public GameInfo()
        {
            textInfo = new StringBuilder();
        }

        public StringBuilder GetFinalResult(int coins, int lifePoints, int keys, int steps, int timeInSeconds)
        {
            textInfo.Clear();
            textInfo.AppendLine(new String('*', 35));
            textInfo.AppendLine("*         Congratulation!         *");
            textInfo.AppendLine("*          You have won!          *");
            textInfo.AppendLine(new String('*', 35));
            textInfo.AppendLine();
            textInfo.AppendLine("Game information: ");
            if (!String.IsNullOrEmpty(PlayerName))
            {
                textInfo.AppendLine($"Player name: {PlayerName}");
            }
            textInfo.AppendLine($"Coins: {coins}");
            textInfo.AppendLine($"Life points: {lifePoints}");
            textInfo.AppendLine($"Keys: {keys}");
            textInfo.AppendLine($"Steps: {steps}");
            textInfo.AppendLine($"Time: {timeInSeconds} sec");

            return textInfo;
        }

        public StringBuilder GetExitInfo()
        {
            textInfo.Clear();
            textInfo.AppendLine(new String('*', 35));
            if (!String.IsNullOrEmpty(PlayerName))
            {
                textInfo.AppendLine($"{PlayerName}, see you later!");
            }
            else
            {
                textInfo.AppendLine("*         See you later!          *");
            }
            textInfo.AppendLine(new String('*', 35));

            return textInfo;
        }

        public StringBuilder GetLoseInfo(int timeInSeconds)
        {
            textInfo.Clear();
            textInfo.AppendLine(new String('*', 35));
            textInfo.AppendLine("*         Unfortunately          *");
            textInfo.AppendLine("* Your life points have run out! *");
            textInfo.AppendLine(new String('*', 35));
            textInfo.AppendLine($"Time: {timeInSeconds} sec");
            textInfo.AppendLine(new String('*', 35));

            return textInfo;
        }

        public StringBuilder GetGameMenu()
        {
            textInfo.Clear();
            textInfo.AppendLine(new String('*', 25));
            textInfo.AppendLine("*         MENU          *");
            textInfo.AppendLine("*  Play             - 0 *");
            textInfo.AppendLine("*  Load saved game  - 1 *");
            textInfo.AppendLine("*  Set player name  - 2 *");
            textInfo.AppendLine("*  Instruction      - 3 *");
            textInfo.AppendLine("*  Exit             - 4 *");
            textInfo.AppendLine(new String('*', 25));

            return textInfo;
        }

        public StringBuilder GetInstruction()
        {
            textInfo.Clear();
            textInfo.AppendLine(new String('*', 35));
            textInfo.AppendLine("Controls: ");
            textInfo.AppendLine("To move just use keys UP, DOWN, LEFT, RIGHT");
            textInfo.AppendLine("To exit the game just press key ESC");

            textInfo.AppendLine("Game information: ");
            textInfo.AppendLine("Collect coins and keys. Keys help you to open the closed door.");
            textInfo.AppendLine("Be careful there are several traps which take of your life points.");
            textInfo.AppendLine("We count all your steps and you will be able to see this info in the end of the game");
            textInfo.AppendLine(new String('*', 35));

            return textInfo;
        }
    }
}
