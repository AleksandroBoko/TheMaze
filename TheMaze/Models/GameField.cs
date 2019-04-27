using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using TheMaze.Enums;
using TheMaze.Interfaces;
using TheMaze.Models.GameObjects;

namespace TheMaze.Models
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
            Build();
        }

        public void Build()
        {
            BuildField();
            BuildRoute();
            BuildClosedDoors();
            BuildOpenedDoor();
            BuildCoins();
            BuildTraps();
            BuildDeadlyTraps();
            BuildKeys();
        }

        private void BuildField()
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

        private void BuildRoute()
        {
            var routeCells = new List<CellPosition>();
            routeCells.Add(new CellPosition() { RowIndex = 0, ColumnIndex = 0 });
            routeCells.Add(new CellPosition() { RowIndex = 0, ColumnIndex = 3 });
            routeCells.Add(new CellPosition() { RowIndex = 0, ColumnIndex = 4 });
            routeCells.Add(new CellPosition() { RowIndex = 0, ColumnIndex = 5 });

            routeCells.Add(new CellPosition() { RowIndex = 1, ColumnIndex = 0});
            routeCells.Add(new CellPosition() { RowIndex = 1, ColumnIndex = 2 });
            routeCells.Add(new CellPosition() { RowIndex = 1, ColumnIndex = 5 });
            routeCells.Add(new CellPosition() { RowIndex = 1, ColumnIndex = 7 });
            routeCells.Add(new CellPosition() { RowIndex = 1, ColumnIndex = 8 });
            routeCells.Add(new CellPosition() { RowIndex = 1, ColumnIndex = 9 });

            routeCells.Add(new CellPosition() { RowIndex = 2, ColumnIndex = 0 });
            routeCells.Add(new CellPosition() { RowIndex = 2, ColumnIndex = 1 });
            routeCells.Add(new CellPosition() { RowIndex = 2, ColumnIndex = 3 });
            routeCells.Add(new CellPosition() { RowIndex = 2, ColumnIndex = 5 });
            routeCells.Add(new CellPosition() { RowIndex = 2, ColumnIndex = 11 });
            routeCells.Add(new CellPosition() { RowIndex = 2, ColumnIndex = 12 });
            routeCells.Add(new CellPosition() { RowIndex = 2, ColumnIndex = 13 });
            routeCells.Add(new CellPosition() { RowIndex = 2, ColumnIndex = 14 });
            routeCells.Add(new CellPosition() { RowIndex = 2, ColumnIndex = 17 });

            routeCells.Add(new CellPosition() { RowIndex = 3, ColumnIndex = 3 });
            routeCells.Add(new CellPosition() { RowIndex = 3, ColumnIndex = 7 });
            routeCells.Add(new CellPosition() { RowIndex = 3, ColumnIndex = 9 });
            routeCells.Add(new CellPosition() { RowIndex = 3, ColumnIndex = 15 });
            routeCells.Add(new CellPosition() { RowIndex = 3, ColumnIndex = 16 });
            routeCells.Add(new CellPosition() { RowIndex = 3, ColumnIndex = 18 });

            routeCells.Add(new CellPosition() { RowIndex = 5, ColumnIndex = 4 });
            routeCells.Add(new CellPosition() { RowIndex = 5, ColumnIndex = 11 });

            routeCells.Add(new CellPosition() { RowIndex = 6, ColumnIndex = 1 });
            routeCells.Add(new CellPosition() { RowIndex = 6, ColumnIndex = 2 });
            routeCells.Add(new CellPosition() { RowIndex = 6, ColumnIndex = 5 });
            routeCells.Add(new CellPosition() { RowIndex = 6, ColumnIndex = 13 });
            routeCells.Add(new CellPosition() { RowIndex = 6, ColumnIndex = 14 });

            routeCells.Add(new CellPosition() { RowIndex = 7, ColumnIndex = 5 });
            routeCells.Add(new CellPosition() { RowIndex = 7, ColumnIndex = 8 });
            routeCells.Add(new CellPosition() { RowIndex = 7, ColumnIndex = 9 });

            routeCells.Add(new CellPosition() { RowIndex = 8, ColumnIndex = 3 });
            routeCells.Add(new CellPosition() { RowIndex = 8, ColumnIndex = 5 });
            routeCells.Add(new CellPosition() { RowIndex = 8, ColumnIndex = 15 });
            routeCells.Add(new CellPosition() { RowIndex = 8, ColumnIndex = 17 });

            routeCells.Add(new CellPosition() { RowIndex = 9, ColumnIndex = 9 });
            routeCells.Add(new CellPosition() { RowIndex = 9, ColumnIndex = 10 });

            routeCells.Add(new CellPosition() { RowIndex = 10, ColumnIndex = 3 });
            routeCells.Add(new CellPosition() { RowIndex = 10, ColumnIndex = 5 });
            routeCells.Add(new CellPosition() { RowIndex = 10, ColumnIndex = 6 });
            routeCells.Add(new CellPosition() { RowIndex = 10, ColumnIndex = 7 });
            routeCells.Add(new CellPosition() { RowIndex = 10, ColumnIndex = 13 });
            routeCells.Add(new CellPosition() { RowIndex = 10, ColumnIndex = 14 });

            routeCells.Add(new CellPosition() { RowIndex = 11, ColumnIndex = 11 });
            routeCells.Add(new CellPosition() { RowIndex = 11, ColumnIndex = 12 });

            routeCells.Add(new CellPosition() { RowIndex = 12, ColumnIndex = 2 });
            routeCells.Add(new CellPosition() { RowIndex = 12, ColumnIndex = 3 });
            routeCells.Add(new CellPosition() { RowIndex = 12, ColumnIndex = 5 });
            routeCells.Add(new CellPosition() { RowIndex = 12, ColumnIndex = 13 });
            routeCells.Add(new CellPosition() { RowIndex = 12, ColumnIndex = 14 });
            routeCells.Add(new CellPosition() { RowIndex = 12, ColumnIndex = 16 });
            routeCells.Add(new CellPosition() { RowIndex = 12, ColumnIndex = 17 });

            routeCells.Add(new CellPosition() { RowIndex = 13, ColumnIndex = 7 });
            routeCells.Add(new CellPosition() { RowIndex = 13, ColumnIndex = 10 });

            routeCells.Add(new CellPosition() { RowIndex = 14, ColumnIndex = 10 });
            routeCells.Add(new CellPosition() { RowIndex = 14, ColumnIndex = 12 });
            routeCells.Add(new CellPosition() { RowIndex = 14, ColumnIndex = 13 });

            routeCells.Add(new CellPosition() { RowIndex = 15, ColumnIndex = 2 });
            routeCells.Add(new CellPosition() { RowIndex = 15, ColumnIndex = 3 });
            routeCells.Add(new CellPosition() { RowIndex = 15, ColumnIndex = 5 });
            routeCells.Add(new CellPosition() { RowIndex = 15, ColumnIndex = 6 });
            routeCells.Add(new CellPosition() { RowIndex = 15, ColumnIndex = 9 });
            routeCells.Add(new CellPosition() { RowIndex = 15, ColumnIndex = 16 });
            routeCells.Add(new CellPosition() { RowIndex = 15, ColumnIndex = 18 });

            routeCells.Add(new CellPosition() { RowIndex = 16, ColumnIndex = 1 });
            routeCells.Add(new CellPosition() { RowIndex = 16, ColumnIndex = 8 });
            routeCells.Add(new CellPosition() { RowIndex = 16, ColumnIndex = 11 });
            routeCells.Add(new CellPosition() { RowIndex = 16, ColumnIndex = 13 });

            routeCells.Add(new CellPosition() { RowIndex = 17, ColumnIndex = 6 });
            routeCells.Add(new CellPosition() { RowIndex = 17, ColumnIndex = 8 });
            routeCells.Add(new CellPosition() { RowIndex = 17, ColumnIndex = 9 });
            routeCells.Add(new CellPosition() { RowIndex = 17, ColumnIndex = 16 });
            routeCells.Add(new CellPosition() { RowIndex = 17, ColumnIndex = 17 });

            routeCells.Add(new CellPosition() { RowIndex = 18, ColumnIndex = 1 });
            routeCells.Add(new CellPosition() { RowIndex = 18, ColumnIndex = 3 });
            routeCells.Add(new CellPosition() { RowIndex = 18, ColumnIndex = 5 });
            routeCells.Add(new CellPosition() { RowIndex = 18, ColumnIndex = 10 });
            routeCells.Add(new CellPosition() { RowIndex = 18, ColumnIndex = 11 });
            routeCells.Add(new CellPosition() { RowIndex = 18, ColumnIndex = 15 });

            routeCells.Add(new CellPosition() { RowIndex = 19, ColumnIndex = 6 });

            foreach (var routeCell in routeCells)
            {
                (Cells[routeCell.RowIndex, routeCell.ColumnIndex] as Cell).ColorBackground = ConsoleColor.Black;
                Cells[routeCell.RowIndex, routeCell.ColumnIndex].ColorForeground = ConsoleColor.White;
                (Cells[routeCell.RowIndex, routeCell.ColumnIndex] as Cell).FieldType = FieldTypes.Route;
                Cells[routeCell.RowIndex, routeCell.ColumnIndex].Symbol = ' ';
            }
        }

        private void BuildClosedDoors()
        {
            var countClosedDoors = 3;
            var addedClosedDoors = 0;
            var columnPosition = 0;
            var rowPosition = 0;

            while (addedClosedDoors < countClosedDoors)
            {
                switch (addedClosedDoors)
                {
                    case 0:
                        rowPosition = 5;
                        break;
                    case 1:
                        rowPosition = 14;
                        columnPosition = columnNumber - 1;
                        break;
                    case 2:
                        rowPosition = rowNumber - 1;
                        columnPosition = 7;
                        break;
                }

                (Cells[rowPosition, columnPosition] as Cell).ColorBackground = ConsoleColor.DarkGray;
                (Cells[rowPosition, columnPosition] as Cell).ColorForeground = ConsoleColor.Black;
                (Cells[rowPosition, columnPosition] as Cell).FieldType = FieldTypes.ClosedDoor;
                Cells[rowPosition, columnPosition].Symbol = '#';

                addedClosedDoors++;
            }
        }

        private void BuildOpenedDoor()
        {
            Cells [19, 17].ColorBackground = ConsoleColor.Green;
            Cells [19, 17].ColorForeground = ConsoleColor.White;
            (Cells [19, 17] as Cell).FieldType = FieldTypes.OpenedDoor;
            Cells [19, 17].Symbol = 'E';
        }

        private void BuildKeys()
        {
            var cellsKeys = new List<CellPosition>();

            cellsKeys.Add(new CellPosition() { RowIndex = 1, ColumnIndex = 18 });
            cellsKeys.Add(new CellPosition() { RowIndex = 13, ColumnIndex = 16 });
            cellsKeys.Add(new CellPosition() { RowIndex = 11, ColumnIndex = 1 });

            foreach (var cellKey in cellsKeys)
            {
                Cells[cellKey.RowIndex, cellKey.ColumnIndex].ColorBackground = ConsoleColor.Black;
                Cells[cellKey.RowIndex, cellKey.ColumnIndex].ColorForeground = ConsoleColor.Blue;
                (Cells[cellKey.RowIndex, cellKey.ColumnIndex] as Cell).FieldType = FieldTypes.Key;
                Cells[cellKey.RowIndex, cellKey.ColumnIndex].IsActive = true;
                Cells[cellKey.RowIndex, cellKey.ColumnIndex].Symbol = 'k';
            }
        }

        private void BuildCoins()
        {
            var cellsCoins = new List<CellPosition>();

            cellsCoins.Add(new CellPosition() { RowIndex = 0, ColumnIndex = 2 });

            cellsCoins.Add(new CellPosition() { RowIndex = 1, ColumnIndex = 10 });
            cellsCoins.Add(new CellPosition() { RowIndex = 1, ColumnIndex = 6 });
            cellsCoins.Add(new CellPosition() { RowIndex = 1, ColumnIndex = 17 });

            cellsCoins.Add(new CellPosition() { RowIndex = 2, ColumnIndex = 2 });
            cellsCoins.Add(new CellPosition() { RowIndex = 2, ColumnIndex = 10 });
            cellsCoins.Add(new CellPosition() { RowIndex = 2, ColumnIndex = 15 });

            cellsCoins.Add(new CellPosition() { RowIndex = 3, ColumnIndex = 5 });
            cellsCoins.Add(new CellPosition() { RowIndex = 3, ColumnIndex = 8 });
            cellsCoins.Add(new CellPosition() { RowIndex = 3, ColumnIndex = 10 });
            cellsCoins.Add(new CellPosition() { RowIndex = 3, ColumnIndex = 17 });

            cellsCoins.Add(new CellPosition() { RowIndex = 4, ColumnIndex = 3 });
            cellsCoins.Add(new CellPosition() { RowIndex = 4, ColumnIndex = 10 });
            cellsCoins.Add(new CellPosition() { RowIndex = 4, ColumnIndex = 18 });

            cellsCoins.Add(new CellPosition() { RowIndex = 5, ColumnIndex = 3 });
            cellsCoins.Add(new CellPosition() { RowIndex = 5, ColumnIndex = 10 });
            cellsCoins.Add(new CellPosition() { RowIndex = 5, ColumnIndex = 12 });
            cellsCoins.Add(new CellPosition() { RowIndex = 5, ColumnIndex = 18 });

            cellsCoins.Add(new CellPosition() { RowIndex = 6, ColumnIndex = 0 });
            cellsCoins.Add(new CellPosition() { RowIndex = 6, ColumnIndex = 4 });
            cellsCoins.Add(new CellPosition() { RowIndex = 6, ColumnIndex = 6 });
            cellsCoins.Add(new CellPosition() { RowIndex = 6, ColumnIndex = 12 });
            cellsCoins.Add(new CellPosition() { RowIndex = 6, ColumnIndex = 15 });
            cellsCoins.Add(new CellPosition() { RowIndex = 6, ColumnIndex = 18 });

            cellsCoins.Add(new CellPosition() { RowIndex = 7, ColumnIndex = 2 });
            cellsCoins.Add(new CellPosition() { RowIndex = 7, ColumnIndex = 7 });
            cellsCoins.Add(new CellPosition() { RowIndex = 7, ColumnIndex = 10 });
            cellsCoins.Add(new CellPosition() { RowIndex = 7, ColumnIndex = 13 });
            cellsCoins.Add(new CellPosition() { RowIndex = 7, ColumnIndex = 15 });
            cellsCoins.Add(new CellPosition() { RowIndex = 7, ColumnIndex = 18 });

            cellsCoins.Add(new CellPosition() { RowIndex = 8, ColumnIndex = 2 });
            cellsCoins.Add(new CellPosition() { RowIndex = 8, ColumnIndex = 4 });
            cellsCoins.Add(new CellPosition() { RowIndex = 8, ColumnIndex = 13 });
            cellsCoins.Add(new CellPosition() { RowIndex = 8, ColumnIndex = 16 });
            cellsCoins.Add(new CellPosition() { RowIndex = 8, ColumnIndex = 18 });

            cellsCoins.Add(new CellPosition() { RowIndex = 9, ColumnIndex = 5 });
            cellsCoins.Add(new CellPosition() { RowIndex = 9, ColumnIndex = 8 });
            cellsCoins.Add(new CellPosition() { RowIndex = 9, ColumnIndex = 11 });
            cellsCoins.Add(new CellPosition() { RowIndex = 9, ColumnIndex = 13 });
            cellsCoins.Add(new CellPosition() { RowIndex = 9, ColumnIndex = 17 });

            cellsCoins.Add(new CellPosition() { RowIndex = 10, ColumnIndex = 2 });
            cellsCoins.Add(new CellPosition() { RowIndex = 10, ColumnIndex = 4 });
            cellsCoins.Add(new CellPosition() { RowIndex = 10, ColumnIndex = 12 });
            cellsCoins.Add(new CellPosition() { RowIndex = 10, ColumnIndex = 15 });
            cellsCoins.Add(new CellPosition() { RowIndex = 10, ColumnIndex = 17 });

            cellsCoins.Add(new CellPosition() { RowIndex = 11, ColumnIndex = 2 });
            cellsCoins.Add(new CellPosition() { RowIndex = 11, ColumnIndex = 8 });
            cellsCoins.Add(new CellPosition() { RowIndex = 11, ColumnIndex = 10 });
            cellsCoins.Add(new CellPosition() { RowIndex = 11, ColumnIndex = 17 });

            cellsCoins.Add(new CellPosition() { RowIndex = 12, ColumnIndex = 1 });
            cellsCoins.Add(new CellPosition() { RowIndex = 12, ColumnIndex = 8 });
            cellsCoins.Add(new CellPosition() { RowIndex = 12, ColumnIndex = 10 });
            cellsCoins.Add(new CellPosition() { RowIndex = 12, ColumnIndex = 12 });
            cellsCoins.Add(new CellPosition() { RowIndex = 12, ColumnIndex = 15 });
            cellsCoins.Add(new CellPosition() { RowIndex = 12, ColumnIndex = 18 });

            cellsCoins.Add(new CellPosition() { RowIndex = 13, ColumnIndex = 1 });
            cellsCoins.Add(new CellPosition() { RowIndex = 13, ColumnIndex = 5 });
            cellsCoins.Add(new CellPosition() { RowIndex = 13, ColumnIndex = 8 });

            cellsCoins.Add(new CellPosition() { RowIndex = 14, ColumnIndex = 1 });
            cellsCoins.Add(new CellPosition() { RowIndex = 14, ColumnIndex = 5 });
            cellsCoins.Add(new CellPosition() { RowIndex = 14, ColumnIndex = 8 });
            cellsCoins.Add(new CellPosition() { RowIndex = 14, ColumnIndex = 11 });
            cellsCoins.Add(new CellPosition() { RowIndex = 14, ColumnIndex = 15 });
            cellsCoins.Add(new CellPosition() { RowIndex = 14, ColumnIndex = 18 });

            cellsCoins.Add(new CellPosition() { RowIndex = 15, ColumnIndex = 1 });
            cellsCoins.Add(new CellPosition() { RowIndex = 15, ColumnIndex = 4 });
            cellsCoins.Add(new CellPosition() { RowIndex = 15, ColumnIndex = 8 });
            cellsCoins.Add(new CellPosition() { RowIndex = 15, ColumnIndex = 13 });
            cellsCoins.Add(new CellPosition() { RowIndex = 15, ColumnIndex = 15 });
            cellsCoins.Add(new CellPosition() { RowIndex = 15, ColumnIndex = 17 });

            cellsCoins.Add(new CellPosition() { RowIndex = 16, ColumnIndex = 2 });
            cellsCoins.Add(new CellPosition() { RowIndex = 16, ColumnIndex = 6 });
            cellsCoins.Add(new CellPosition() { RowIndex = 16, ColumnIndex = 10 });
            cellsCoins.Add(new CellPosition() { RowIndex = 16, ColumnIndex = 12 });
            cellsCoins.Add(new CellPosition() { RowIndex = 16, ColumnIndex = 14 });

            cellsCoins.Add(new CellPosition() { RowIndex = 17, ColumnIndex = 1 });
            cellsCoins.Add(new CellPosition() { RowIndex = 17, ColumnIndex = 5 });
            cellsCoins.Add(new CellPosition() { RowIndex = 17, ColumnIndex = 7 });
            cellsCoins.Add(new CellPosition() { RowIndex = 17, ColumnIndex = 12 });
            cellsCoins.Add(new CellPosition() { RowIndex = 17, ColumnIndex = 14 });
            cellsCoins.Add(new CellPosition() { RowIndex = 17, ColumnIndex = 18 });

            cellsCoins.Add(new CellPosition() { RowIndex = 18, ColumnIndex = 2 });
            cellsCoins.Add(new CellPosition() { RowIndex = 18, ColumnIndex = 4 });
            cellsCoins.Add(new CellPosition() { RowIndex = 18, ColumnIndex = 12 });
            cellsCoins.Add(new CellPosition() { RowIndex = 18, ColumnIndex = 14 });
            cellsCoins.Add(new CellPosition() { RowIndex = 18, ColumnIndex = 16 });
            cellsCoins.Add(new CellPosition() { RowIndex = 18, ColumnIndex = 18 });

            cellsCoins.Add(new CellPosition() { RowIndex = 19, ColumnIndex = 5 });
            cellsCoins.Add(new CellPosition() { RowIndex = 19, ColumnIndex = 18 });

            foreach (var cellCoin in cellsCoins)
            {
                Cells[cellCoin.RowIndex, cellCoin.ColumnIndex].ColorBackground = ConsoleColor.Black;
                Cells[cellCoin.RowIndex, cellCoin.ColumnIndex].ColorForeground = ConsoleColor.Cyan;
                (Cells[cellCoin.RowIndex, cellCoin.ColumnIndex] as Cell).FieldType = FieldTypes.Coin;
                Cells[cellCoin.RowIndex, cellCoin.ColumnIndex].IsActive = true;
                Cells[cellCoin.RowIndex, cellCoin.ColumnIndex].Symbol = 'o';
            }
        }

        private void BuildTraps()
        {
            var cellsTraps = new List<CellPosition>();

            cellsTraps.Add(new CellPosition() { RowIndex = 2, ColumnIndex = 18 });
            cellsTraps.Add(new CellPosition() { RowIndex = 3, ColumnIndex = 6 });
            //cellsTraps.Add(new CellPosition() { RowIndex = 7, ColumnIndex = 6 });
            cellsTraps.Add(new CellPosition() { RowIndex = 8, ColumnIndex = 9 });
            cellsTraps.Add(new CellPosition() { RowIndex = 10, ColumnIndex = 1 });
            //cellsTraps.Add(new CellPosition() { RowIndex = 10, ColumnIndex = 8 });
            cellsTraps.Add(new CellPosition() { RowIndex = 12, ColumnIndex = 4 });
            cellsTraps.Add(new CellPosition() { RowIndex = 13, ColumnIndex = 15 });
            //cellsTraps.Add(new CellPosition() { RowIndex = 17, ColumnIndex = 4 });
            cellsTraps.Add(new CellPosition() { RowIndex = 18, ColumnIndex = 9 });

            foreach (var cellTrap in cellsTraps)
            {
                Cells[cellTrap.RowIndex, cellTrap.ColumnIndex].ColorBackground = ConsoleColor.Black;
                Cells[cellTrap.RowIndex, cellTrap.ColumnIndex].ColorForeground = ConsoleColor.Red;
                (Cells[cellTrap.RowIndex, cellTrap.ColumnIndex] as Cell).FieldType = FieldTypes.Trap;
                Cells[cellTrap.RowIndex, cellTrap.ColumnIndex].IsActive = true;
                Cells[cellTrap.RowIndex, cellTrap.ColumnIndex].Symbol = 'x';
            }

        }

        private void BuildDeadlyTraps()
        {
            var cellsTraps = new List<CellPosition>();

            //routePoints.Add(new CellPosition() { RowIndex = 2, ColumnIndex = 18 });
            //routePoints.Add(new CellPosition() { RowIndex = 3, ColumnIndex = 6 });
            cellsTraps.Add(new CellPosition() { RowIndex = 7, ColumnIndex = 6 });
            //routePoints.Add(new CellPosition() { RowIndex = 8, ColumnIndex = 9 });
            //routePoints.Add(new CellPosition() { RowIndex = 10, ColumnIndex = 1 });
            cellsTraps.Add(new CellPosition() { RowIndex = 10, ColumnIndex = 8 });
            //routePoints.Add(new CellPosition() { RowIndex = 12, ColumnIndex = 4 });
            //routePoints.Add(new CellPosition() { RowIndex = 13, ColumnIndex = 15 });
            cellsTraps.Add(new CellPosition() { RowIndex = 17, ColumnIndex = 4 });
            //routePoints.Add(new CellPosition() { RowIndex = 18, ColumnIndex = 9 });

            foreach (var cellTrap in cellsTraps)
            {
                Cells[cellTrap.RowIndex, cellTrap.ColumnIndex].ColorBackground = ConsoleColor.Black;
                Cells[cellTrap.RowIndex, cellTrap.ColumnIndex].ColorForeground = ConsoleColor.Red;
                (Cells[cellTrap.RowIndex, cellTrap.ColumnIndex] as Cell).FieldType = FieldTypes.DeadlyTrap;
                Cells[cellTrap.RowIndex, cellTrap.ColumnIndex].IsActive = true;
                Cells[cellTrap.RowIndex, cellTrap.ColumnIndex].Symbol = 'x';
            }

        }

        private struct CellPosition
        {
            public int RowIndex { get; set; }
            public int ColumnIndex { get; set; }
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
                    Cells[i,j] = cellsToSave[i][j];
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
                        var pointsBuilder = (GameField) pointsObject;
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
    }
}
