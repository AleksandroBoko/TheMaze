using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Linq;
using TheMaze.Domain.GameObjects;
using TheMaze.Enums;
using TheMaze.Interfaces;
using TheMaze.Models.GameObjects;

namespace TheMaze.Domain
{
    [KnownType(typeof(Cell))]
    [KnownType(typeof(GameObject))]
    [DataContract]
    public class GameField : IGameSerialization
    {
        [NonSerialized]
        private int rowNumber = Configuration.ROW_NUMBER;

        [NonSerialized]
        private int columnNumber = Configuration.COLUMN_NUMBER;

        public GameObject[,] Cells { get; private set; }

        [DataMember]
        private GameObject[][] cellsToSave { get; set; }

        public GameObject this[int row, int column]
        {
            get => Cells[row, column];
            set => Cells[row, column] = value;
        }

        public GameField()
        {
            Cells = new GameObject[rowNumber, columnNumber];
        }

        public void BuildFieldForQuickGame()
        {
            BuildWalls();
            LoadStaticGameField();
        }

        public void BuildFieldForGeneratedGame()
        {
            BuildWalls();
            GenerateField();
        }

        public void Build(MenuItemType gameType)
        {
            switch (gameType)
            {
                case MenuItemType.QuickPlay:
                    BuildFieldForQuickGame();
                    break;
                case MenuItemType.Play:
                    BuildFieldForGeneratedGame();
                    break;
            }
        }

        private void BuildWalls()
        {
            for (var i = 0; i < rowNumber; i++)
            {
                for (int j = 0; j < columnNumber; j++)
                {
                    Cells[i, j] = new Cell()
                    {
                        ColorBackground = ConsoleColor.Yellow,
                        ColorForeground = ConsoleColor.Black,
                        FieldType = FieldTypes.Wall,
                        IsActive = true,
                        Symbol = 'I'
                    };
                }
            }
        }

        private void ConvertCellsToArrayToArrays()
        {
            cellsToSave = new GameObject[Configuration.ROW_NUMBER][];
            for (int i = 0; i < Configuration.ROW_NUMBER; i++)
            {
                cellsToSave[i] = new GameObject[Configuration.COLUMN_NUMBER];
                for (int j = 0; j < Configuration.COLUMN_NUMBER; j++)
                {
                    cellsToSave[i][j] = Cells[i, j];
                }
            }
        }

        private void ConvertCellsToMultidimensionalArray()
        {
            Cells = new GameObject[Configuration.ROW_NUMBER, Configuration.COLUMN_NUMBER];

            for (int i = 0; i < Configuration.ROW_NUMBER; i++)
            {
                for (int j = 0; j < Configuration.COLUMN_NUMBER; j++)
                {
                    Cells[i, j] = cellsToSave[i][j];
                }
            }
        }

        public void Save()
        {
            ConvertCellsToArrayToArrays();
            var serializer = new DataContractJsonSerializer(typeof(GameField));
            var path = $"{Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory)}Cells.json";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (var stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                serializer.WriteObject(stream, (object)this);
            }
        }

        public bool Load()
        {
            var result = true;
            var serializer = new DataContractJsonSerializer(typeof(GameField));
            var path = $"{Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory)}Cells.json";

            if (!File.Exists(path))
            {
                return false;
            }

            using (var stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                if (stream.Length > 0)
                {
                    var pointsObject = serializer.ReadObject(stream);
                    if (pointsObject != null)
                    {
                        var pointsBuilder = (GameField)pointsObject;
                        cellsToSave = pointsBuilder.cellsToSave;

                        if (cellsToSave != null && cellsToSave.Any())
                        {
                            ConvertCellsToMultidimensionalArray();
                        }
                    }
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }

        public void GenerateField()
        {
            var random = new Random();

            if (random.Next(0, 10) < 5)
            {
                GenerateHorizontalDirectionRoute();
            }
            else
            {
                GenerateVerticalDirectionRoute();
            }

            GenerateCoins();
            GenerateClosedDoors();
            GenerateOpenDoors();
            GenerateKeys();
            GenerateTraps();
            GenerateDeadlyTraps();
            GeneratePortals();
            GeneratePrizes();
            GenerateCrystals();
        }

        private void GenerateHorizontalDirectionRoute()
        {
            var random = new Random();

            for (var i = 0; i < rowNumber; i++)
            {
                for (var j = 0; j < columnNumber; j++)
                {
                    if (j % 2 != 0)
                    {
                        Cells[i, j] = CreateRouteCell();
                    }
                    else if (random.Next(0, 2) < 1)
                    {
                        Cells[i, j] = CreateRouteCell();
                    }
                }
            }
        }

        private void GenerateVerticalDirectionRoute()
        {
            var random = new Random();

            for (var i = 0; i < rowNumber; i++)
            {
                for (var j = 0; j < columnNumber; j++)
                {
                    if (i % 2 != 0)
                    {
                        Cells[i, j] = CreateRouteCell();
                    }
                    else if (random.Next(0, 2) < 1)
                    {
                        Cells[i, j] = CreateRouteCell();
                    }
                }
            }
        }

        private void GenerateClosedDoors()
        {
            var closedDoorCount = 0;
            var random = new Random();

            while (closedDoorCount < Configuration.COUNT_CLOSED_DOORS)
            {
                var indexRow = random.Next(0, Configuration.ROW_NUMBER - 1);
                var indexColumn = random.Next(0, Configuration.COLUMN_NUMBER - 1);

                if ((Cells[indexRow, 0] as Cell).FieldType == FieldTypes.Route)
                {
                    Cells[indexRow, 0] = CreateClosedDoorCell();
                    closedDoorCount++;
                }
                else if ((Cells[indexRow, Configuration.COLUMN_NUMBER - 1] as Cell).FieldType == FieldTypes.Route)
                {
                    Cells[indexRow, Configuration.COLUMN_NUMBER - 1] = CreateClosedDoorCell();
                    closedDoorCount++;
                }
                else if ((Cells[Configuration.ROW_NUMBER - 1, indexColumn] as Cell).FieldType == FieldTypes.Route)
                {
                    Cells[Configuration.ROW_NUMBER - 1, indexColumn] = CreateClosedDoorCell();
                    closedDoorCount++;
                }
            }
        }

        private void GenerateOpenDoors()
        {
            var openDoorCount = 0;
            var random = new Random();

            while (openDoorCount < Configuration.COUNT_OPEN_DOORS)
            {
                var indexColumn = random.Next(0, Configuration.COLUMN_NUMBER - 1);
                if ((Cells[Configuration.ROW_NUMBER - 1, indexColumn] as Cell).FieldType == FieldTypes.Route)
                {
                    Cells[Configuration.ROW_NUMBER - 1, indexColumn] = CreateOpenDoorCell();
                    openDoorCount++;
                }
            }
        }

        private void GenerateCoins()
        {
            var coinCount = 0;
            var random = new Random();

            while (coinCount < Configuration.COUNT_COINS)
            {
                var found = false;
                while (!found)
                {
                    var indexRow = random.Next(0, Configuration.ROW_NUMBER - 1);
                    var indexColumn = random.Next(0, Configuration.COLUMN_NUMBER - 1);
                    var cell = Cells[indexRow, indexColumn];
                    if ((cell as Cell).FieldType == FieldTypes.Route)
                    {
                        found = true;
                        Cells[indexRow, indexColumn] = CreateCoinCell();
                    }
                }

                coinCount++;
            }
        }

        private void GenerateKeys()
        {
            var keyCount = 0;
            var random = new Random();

            while (keyCount < Configuration.COUNT_KEYS)
            {
                var indexRow = random.Next(0, Configuration.ROW_NUMBER - 1);
                var indexColumn = random.Next(0, Configuration.COLUMN_NUMBER - 1);
                var cell = Cells[indexRow, indexColumn];
                if ((cell as Cell).FieldType == FieldTypes.Route)
                {
                    Cells[indexRow, indexColumn] = CreateKeyCell();
                    keyCount++;
                }
            }
        }

        private void GenerateTraps()
        {
            var trapCount = 0;
            var random = new Random();

            while (trapCount < Configuration.COUNT_TRAPS)
            {
                var indexRow = random.Next(0, Configuration.ROW_NUMBER - 1);
                var indexColumn = random.Next(0, Configuration.COLUMN_NUMBER - 1);
                var cell = Cells[indexRow, indexColumn];
                if ((cell as Cell).FieldType == FieldTypes.Route)
                {
                    Cells[indexRow, indexColumn] = CreateTrapCell();
                    trapCount++;
                }
            }
        }

        private void GenerateDeadlyTraps()
        {
            var trapCount = 0;
            var random = new Random();

            while (trapCount < Configuration.COUNT_DEADLY_TRAPS)
            {
                var indexRow = random.Next(0, Configuration.ROW_NUMBER - 1);
                var indexColumn = random.Next(0, Configuration.COLUMN_NUMBER - 1);
                var cell = Cells[indexRow, indexColumn];
                if ((cell as Cell).FieldType == FieldTypes.Route)
                {
                    Cells[indexRow, indexColumn] = CreateDeadlyTrapCell();
                    trapCount++;
                }
            }
        }

        private void GeneratePrizes()
        {
            var prizesCount = 0;
            var random = new Random();

            while (prizesCount < Configuration.COUNT_PRIZES)
            {
                var indexRow = random.Next(0, Configuration.ROW_NUMBER - 1);
                var indexColumn = random.Next(0, Configuration.COLUMN_NUMBER - 1);
                var cell = Cells[indexRow, indexColumn];
                if ((cell as Cell).FieldType == FieldTypes.Route)
                {
                    Cells[indexRow, indexColumn] = CreatePrizeCell();
                    prizesCount++;
                }
            }
        }

        private void GeneratePortals()
        {
            var portalCount = 0;
            var random = new Random();

            while (portalCount < Configuration.COUNT_PORTALS)
            {
                var indexRow = random.Next(0, Configuration.ROW_NUMBER - 1);
                var indexColumn = random.Next(0, Configuration.COLUMN_NUMBER - 1);
                var cell = Cells[indexRow, indexColumn];
                if ((cell as Cell).FieldType == FieldTypes.Route)
                {
                    Cells[indexRow, indexColumn] = CreatePortalCell();
                    portalCount++;
                }
            }
        }

        private void GenerateCrystals()
        {
            var crystalCount = 0;
            var random = new Random();

            while (crystalCount < Configuration.COUNT_CRYSTAL)
            {
                var indexRow = random.Next(0, Configuration.ROW_NUMBER - 1);
                var indexColumn = random.Next(0, Configuration.COLUMN_NUMBER - 1);
                var cell = Cells[indexRow, indexColumn];
                if ((cell as Cell).FieldType == FieldTypes.Route)
                {
                    Cells[indexRow, indexColumn] = CreateCrystalCell();
                    crystalCount++;
                }
            }
        }

        #region CREATION PART
        private Cell CreateRouteCell()
        {
            var cell = new Cell
            {
                ColorBackground = ConsoleColor.Black,
                ColorForeground = ConsoleColor.White,
                FieldType = FieldTypes.Route,
                IsActive = true,
                Symbol = ' '
            };

            return cell;
        }

        private Cell CreateCoinCell()
        {
            var cell = new Cell
            {
                ColorBackground = ConsoleColor.Black,
                ColorForeground = ConsoleColor.Cyan,
                FieldType = FieldTypes.Coin,
                IsActive = true,
                Symbol = 'o'
            };

            return cell;
        }

        private Cell CreateClosedDoorCell()
        {
            var cell = new Cell
            {
                ColorBackground = ConsoleColor.DarkGray,
                ColorForeground = ConsoleColor.Black,
                FieldType = FieldTypes.ClosedDoor,
                IsActive = true,
                Symbol = '#'
            };

            return cell;
        }

        private Cell CreateOpenDoorCell()
        {
            var cell = new Cell
            {
                ColorBackground = ConsoleColor.Green,
                ColorForeground = ConsoleColor.White,
                FieldType = FieldTypes.OpenedDoor,
                IsActive = true,
                Symbol = 'E'
            };

            return cell;
        }

        private Cell CreateKeyCell()
        {
            var cell = new Cell
            {
                ColorBackground = ConsoleColor.Black,
                ColorForeground = ConsoleColor.Blue,
                FieldType = FieldTypes.Key,
                IsActive = true,
                Symbol = 'k'
            };

            return cell;
        }

        private Cell CreateTrapCell()
        {
            var cell = new Cell()
            {
                ColorBackground = ConsoleColor.Black,
                ColorForeground = ConsoleColor.Red,
                FieldType = FieldTypes.Trap,
                IsActive = true,
                Symbol = 'x'
            };

            return cell;
        }

        private Cell CreateDeadlyTrapCell()
        {
            var cell = new Cell()
            {
                ColorBackground = ConsoleColor.Black,
                ColorForeground = ConsoleColor.Red,
                FieldType = FieldTypes.DeadlyTrap,
                IsActive = true,
                Symbol = 'x'
            };

            return cell;
        }

        private Cell CreatePortalCell()
        {
            var cell = new Cell()
            {
                ColorBackground = ConsoleColor.Blue,
                ColorForeground = ConsoleColor.White,
                FieldType = FieldTypes.Portal,
                IsActive = true,
                Symbol = 'P'
            };

            return cell;
        }

        private Cell CreatePrizeCell()
        {
            var cell = new Cell()
            {
                ColorBackground = ConsoleColor.Green,
                ColorForeground = ConsoleColor.Black,
                FieldType = FieldTypes.Prize,
                IsActive = true,
                Symbol = '*'
            };

            return cell;
        }

        private Cell CreateCrystalCell()
        {
            var cell = new Cell()
            {
                ColorBackground = ConsoleColor.Blue,
                ColorForeground = ConsoleColor.White,
                FieldType = FieldTypes.Crystal,
                IsActive = true,
                Symbol = '^'
            };

            return cell;
        }
        #endregion
        
        private void LoadStaticGameField()
        {
            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            var path = $"{projectDirectory}{Configuration.STATIC_GAME_FIELD_PATH}";

            using (var stream = new StreamReader(Path.GetFullPath(path)))
            {
                var jsonText = stream.ReadToEnd();
                var json = JObject.Parse(jsonText);

                if (json != null && json.HasValues)
                {
                    BuildCellFromJson(json, FieldTypes.Route);
                    BuildCellFromJson(json, FieldTypes.Coin);
                    BuildCellFromJson(json, FieldTypes.OpenedDoor);
                    BuildCellFromJson(json, FieldTypes.ClosedDoor);
                    BuildCellFromJson(json, FieldTypes.Key);
                    BuildCellFromJson(json, FieldTypes.Trap);
                    BuildCellFromJson(json, FieldTypes.DeadlyTrap);
                    BuildCellFromJson(json, FieldTypes.Portal);
                    BuildCellFromJson(json, FieldTypes.Prize);
                    BuildCellFromJson(json, FieldTypes.Crystal);
                }
            }
        }

        private void BuildCellFromJson(JObject json, FieldTypes fieldType)
        {
            if (json[$"{fieldType}"].HasValues)
            {
                foreach (var jToken in json[$"{fieldType}"])
                {
                    var row = jToken["RowIndex"].Value<int>();
                    var column = jToken["ColumnIndex"].Value<int>();
                    Cells[row, column].IsActive = true;
                    (Cells[row, column] as Cell).FieldType = fieldType;
                    SetCellStyle(Cells[row, column], fieldType);
                }
            }
        }

        private void SetCellStyle(GameObject gameObject, FieldTypes fieldType)
        {
            if (gameObject == null)
            {
                return;
            }

            switch (fieldType)
            {
                case FieldTypes.Route:
                    gameObject.ColorForeground = ConsoleColor.Black;
                    gameObject.ColorBackground = ConsoleColor.Black;
                    gameObject.Symbol = ' ';
                    break;
                case FieldTypes.Coin:
                    gameObject.ColorForeground = ConsoleColor.Cyan;
                    gameObject.ColorBackground = ConsoleColor.Black;
                    gameObject.Symbol = 'o';
                    break;
                case FieldTypes.OpenedDoor:
                    gameObject.ColorForeground = ConsoleColor.White;
                    gameObject.ColorBackground = ConsoleColor.Green;
                    gameObject.Symbol = 'E';
                    break;
                case FieldTypes.ClosedDoor:
                    gameObject.ColorForeground = ConsoleColor.Black;
                    gameObject.ColorBackground = ConsoleColor.DarkGray;
                    gameObject.Symbol = '#';
                    break;
                case FieldTypes.Key:
                    gameObject.ColorForeground = ConsoleColor.Blue;
                    gameObject.ColorBackground = ConsoleColor.Black;
                    gameObject.Symbol = 'k';
                    break;
                case FieldTypes.Trap:
                case FieldTypes.DeadlyTrap:
                    gameObject.ColorForeground = ConsoleColor.Red;
                    gameObject.ColorBackground = ConsoleColor.Black;
                    gameObject.Symbol = 'x';
                    break;
                case FieldTypes.Portal:
                    gameObject.ColorForeground = ConsoleColor.White;
                    gameObject.ColorBackground = ConsoleColor.Blue;
                    gameObject.Symbol = 'P';
                    break;
                case FieldTypes.Prize:
                    gameObject.ColorForeground = ConsoleColor.Black;
                    gameObject.ColorBackground = ConsoleColor.Green;
                    gameObject.Symbol = '*';
                    break;
                case FieldTypes.Crystal:
                    gameObject.ColorForeground = ConsoleColor.White;
                    gameObject.ColorBackground = ConsoleColor.Blue;
                    gameObject.Symbol = '^';
                    break;
            }
        }
    }
}
