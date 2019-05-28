namespace TheMaze.Core.Configurations
{
    public static class Configuration
    {
        public const string TITLE = "The Maze";

        public const int STEPS_TO_CLOSE_DOOR = 500;

        public const int COINS_TO_EXIT = 10;
        public const int CRYSTALS_TO_EXIT = 10;
        public const int GAMEPOINTS_TO_EXIT = 30;

        public const int COUNT_COINS = 100;
        public const int COUNT_CLOSED_DOORS = 3;
        public const int COUNT_OPEN_DOORS = 1;
        public const int COUNT_KEYS = 3;
        public const int COUNT_TRAPS = 3;
        public const int COUNT_DEADLY_TRAPS = 3;
        public const int COUNT_PORTALS = 3;
        public const int COUNT_PRIZES = 3;
        public const int COUNT_CRYSTAL = 30;

        public const int ROW_NUMBER = 20;
        public const int COLUMN_NUMBER = 20;

        public const int COIN_VALUE = 1;
        public const int CRYSTAL_VALUE = 1;
        public const int KEY_VALUE = 1;
        public const int PORTAL_VALUE = 1;
        public const int PRIZE_VALUE = 1;

        public const string STATIC_GAME_FIELD_PATH = "/TheMaze.Core/Resources/GameField.json";
    }
}
