using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using TheMaze.Enums;

namespace TheMaze.Models
{
    [DataContract]
    public class PointsBuilder
    {
        [NonSerialized]
        private int rowNumber = Configuration.ROW_NUMBER;

        [NonSerialized]
        private int columnNumber = Configuration.COLUMN_NUMBER;

        public Point[,] Points { get; private set; }

        [DataMember]
        private Point[][] pointsToSave { get; set; }

        public PointsBuilder()
        {
            Points = new Point[rowNumber, columnNumber];
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
            BuildKeys();
        }

        private void BuildField()
        {
            for (var i = 0; i < rowNumber; i++)
            {
                for (int j = 0; j < columnNumber; j++)
                {
                    Points[i, j] = new Point()
                    {
                        ColorBackground = ConsoleColor.Yellow,
                        ColorForground = ConsoleColor.Black,
                        PointType = PointTypes.Wall,
                        IsActive = true,
                        Symbol = 'I'
                    };
                }
            }
        }

        private void BuildRoute()
        {
            var routePoints = new List<RoutePoint>();
            routePoints.Add(new RoutePoint() { RowIndex = 0, ColumnIndex = 0 });
            routePoints.Add(new RoutePoint() { RowIndex = 0, ColumnIndex = 3 });
            routePoints.Add(new RoutePoint() { RowIndex = 0, ColumnIndex = 4 });
            routePoints.Add(new RoutePoint() { RowIndex = 0, ColumnIndex = 5 });

            routePoints.Add(new RoutePoint() { RowIndex = 1, ColumnIndex = 0});
            routePoints.Add(new RoutePoint() { RowIndex = 1, ColumnIndex = 2 });
            routePoints.Add(new RoutePoint() { RowIndex = 1, ColumnIndex = 5 });
            routePoints.Add(new RoutePoint() { RowIndex = 1, ColumnIndex = 7 });
            routePoints.Add(new RoutePoint() { RowIndex = 1, ColumnIndex = 8 });
            routePoints.Add(new RoutePoint() { RowIndex = 1, ColumnIndex = 9 });

            routePoints.Add(new RoutePoint() { RowIndex = 2, ColumnIndex = 0 });
            routePoints.Add(new RoutePoint() { RowIndex = 2, ColumnIndex = 1 });
            routePoints.Add(new RoutePoint() { RowIndex = 2, ColumnIndex = 3 });
            routePoints.Add(new RoutePoint() { RowIndex = 2, ColumnIndex = 5 });
            routePoints.Add(new RoutePoint() { RowIndex = 2, ColumnIndex = 11 });
            routePoints.Add(new RoutePoint() { RowIndex = 2, ColumnIndex = 12 });
            routePoints.Add(new RoutePoint() { RowIndex = 2, ColumnIndex = 13 });
            routePoints.Add(new RoutePoint() { RowIndex = 2, ColumnIndex = 14 });
            routePoints.Add(new RoutePoint() { RowIndex = 2, ColumnIndex = 17 });

            routePoints.Add(new RoutePoint() { RowIndex = 3, ColumnIndex = 3 });
            routePoints.Add(new RoutePoint() { RowIndex = 3, ColumnIndex = 7 });
            routePoints.Add(new RoutePoint() { RowIndex = 3, ColumnIndex = 9 });
            routePoints.Add(new RoutePoint() { RowIndex = 3, ColumnIndex = 15 });
            routePoints.Add(new RoutePoint() { RowIndex = 3, ColumnIndex = 16 });
            routePoints.Add(new RoutePoint() { RowIndex = 3, ColumnIndex = 18 });

            routePoints.Add(new RoutePoint() { RowIndex = 5, ColumnIndex = 4 });
            routePoints.Add(new RoutePoint() { RowIndex = 5, ColumnIndex = 11 });

            routePoints.Add(new RoutePoint() { RowIndex = 6, ColumnIndex = 1 });
            routePoints.Add(new RoutePoint() { RowIndex = 6, ColumnIndex = 2 });
            routePoints.Add(new RoutePoint() { RowIndex = 6, ColumnIndex = 5 });
            routePoints.Add(new RoutePoint() { RowIndex = 6, ColumnIndex = 13 });
            routePoints.Add(new RoutePoint() { RowIndex = 6, ColumnIndex = 14 });

            routePoints.Add(new RoutePoint() { RowIndex = 7, ColumnIndex = 5 });
            routePoints.Add(new RoutePoint() { RowIndex = 7, ColumnIndex = 8 });
            routePoints.Add(new RoutePoint() { RowIndex = 7, ColumnIndex = 9 });

            routePoints.Add(new RoutePoint() { RowIndex = 8, ColumnIndex = 3 });
            routePoints.Add(new RoutePoint() { RowIndex = 8, ColumnIndex = 5 });
            routePoints.Add(new RoutePoint() { RowIndex = 8, ColumnIndex = 15 });
            routePoints.Add(new RoutePoint() { RowIndex = 8, ColumnIndex = 17 });

            routePoints.Add(new RoutePoint() { RowIndex = 9, ColumnIndex = 9 });
            routePoints.Add(new RoutePoint() { RowIndex = 9, ColumnIndex = 10 });

            routePoints.Add(new RoutePoint() { RowIndex = 10, ColumnIndex = 3 });
            routePoints.Add(new RoutePoint() { RowIndex = 10, ColumnIndex = 5 });
            routePoints.Add(new RoutePoint() { RowIndex = 10, ColumnIndex = 6 });
            routePoints.Add(new RoutePoint() { RowIndex = 10, ColumnIndex = 7 });
            routePoints.Add(new RoutePoint() { RowIndex = 10, ColumnIndex = 13 });
            routePoints.Add(new RoutePoint() { RowIndex = 10, ColumnIndex = 14 });

            routePoints.Add(new RoutePoint() { RowIndex = 11, ColumnIndex = 11 });
            routePoints.Add(new RoutePoint() { RowIndex = 11, ColumnIndex = 12 });

            routePoints.Add(new RoutePoint() { RowIndex = 12, ColumnIndex = 2 });
            routePoints.Add(new RoutePoint() { RowIndex = 12, ColumnIndex = 3 });
            routePoints.Add(new RoutePoint() { RowIndex = 12, ColumnIndex = 5 });
            routePoints.Add(new RoutePoint() { RowIndex = 12, ColumnIndex = 13 });
            routePoints.Add(new RoutePoint() { RowIndex = 12, ColumnIndex = 14 });
            routePoints.Add(new RoutePoint() { RowIndex = 12, ColumnIndex = 16 });
            routePoints.Add(new RoutePoint() { RowIndex = 12, ColumnIndex = 17 });

            routePoints.Add(new RoutePoint() { RowIndex = 13, ColumnIndex = 7 });
            routePoints.Add(new RoutePoint() { RowIndex = 13, ColumnIndex = 10 });

            routePoints.Add(new RoutePoint() { RowIndex = 14, ColumnIndex = 10 });
            routePoints.Add(new RoutePoint() { RowIndex = 14, ColumnIndex = 12 });
            routePoints.Add(new RoutePoint() { RowIndex = 14, ColumnIndex = 13 });

            routePoints.Add(new RoutePoint() { RowIndex = 15, ColumnIndex = 2 });
            routePoints.Add(new RoutePoint() { RowIndex = 15, ColumnIndex = 3 });
            routePoints.Add(new RoutePoint() { RowIndex = 15, ColumnIndex = 5 });
            routePoints.Add(new RoutePoint() { RowIndex = 15, ColumnIndex = 6 });
            routePoints.Add(new RoutePoint() { RowIndex = 15, ColumnIndex = 9 });
            routePoints.Add(new RoutePoint() { RowIndex = 15, ColumnIndex = 16 });
            routePoints.Add(new RoutePoint() { RowIndex = 15, ColumnIndex = 18 });

            routePoints.Add(new RoutePoint() { RowIndex = 16, ColumnIndex = 1 });
            routePoints.Add(new RoutePoint() { RowIndex = 16, ColumnIndex = 8 });
            routePoints.Add(new RoutePoint() { RowIndex = 16, ColumnIndex = 11 });
            routePoints.Add(new RoutePoint() { RowIndex = 16, ColumnIndex = 13 });

            routePoints.Add(new RoutePoint() { RowIndex = 17, ColumnIndex = 6 });
            routePoints.Add(new RoutePoint() { RowIndex = 17, ColumnIndex = 8 });
            routePoints.Add(new RoutePoint() { RowIndex = 17, ColumnIndex = 9 });
            routePoints.Add(new RoutePoint() { RowIndex = 17, ColumnIndex = 16 });
            routePoints.Add(new RoutePoint() { RowIndex = 17, ColumnIndex = 17 });

            routePoints.Add(new RoutePoint() { RowIndex = 18, ColumnIndex = 1 });
            routePoints.Add(new RoutePoint() { RowIndex = 18, ColumnIndex = 3 });
            routePoints.Add(new RoutePoint() { RowIndex = 18, ColumnIndex = 5 });
            routePoints.Add(new RoutePoint() { RowIndex = 18, ColumnIndex = 10 });
            routePoints.Add(new RoutePoint() { RowIndex = 18, ColumnIndex = 11 });
            routePoints.Add(new RoutePoint() { RowIndex = 18, ColumnIndex = 15 });

            routePoints.Add(new RoutePoint() { RowIndex = 19, ColumnIndex = 6 });

            foreach (var routePoint in routePoints)
            {
                Points[routePoint.RowIndex, routePoint.ColumnIndex].ColorBackground = ConsoleColor.Black;
                Points[routePoint.RowIndex, routePoint.ColumnIndex].ColorForground = ConsoleColor.White;
                Points[routePoint.RowIndex, routePoint.ColumnIndex].PointType = PointTypes.Route;
                Points[routePoint.RowIndex, routePoint.ColumnIndex].Symbol = ' ';
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

                Points[rowPosition, columnPosition].ColorBackground = ConsoleColor.DarkGray;
                Points[rowPosition, columnPosition].ColorForground = ConsoleColor.Black;
                Points[rowPosition, columnPosition].PointType = PointTypes.ClosedDoor;
                Points[rowPosition, columnPosition].Symbol = '#';

                addedClosedDoors++;
            }
        }

        private void BuildOpenedDoor()
        {
            Points [19, 17].ColorBackground = ConsoleColor.Green;
            Points [19, 17].ColorForground = ConsoleColor.White;
            Points [19, 17].PointType = PointTypes.OpenedDoor;
            Points [19, 17].Symbol = 'E';
        }

        private void BuildKeys()
        {
            var routePoints = new List<RoutePoint>();

            routePoints.Add(new RoutePoint() { RowIndex = 1, ColumnIndex = 18 });
            routePoints.Add(new RoutePoint() { RowIndex = 13, ColumnIndex = 16 });
            routePoints.Add(new RoutePoint() { RowIndex = 11, ColumnIndex = 1 });

            foreach (var routePoint in routePoints)
            {
                Points[routePoint.RowIndex, routePoint.ColumnIndex].ColorBackground = ConsoleColor.Black;
                Points[routePoint.RowIndex, routePoint.ColumnIndex].ColorForground = ConsoleColor.Blue;
                Points[routePoint.RowIndex, routePoint.ColumnIndex].PointType = PointTypes.Key;
                Points[routePoint.RowIndex, routePoint.ColumnIndex].IsActive = true;
                Points[routePoint.RowIndex, routePoint.ColumnIndex].Symbol = 'k';
            }
        }

        private void BuildCoins()
        {
            var routePoints = new List<RoutePoint>();

            routePoints.Add(new RoutePoint() { RowIndex = 0, ColumnIndex = 2 });

            routePoints.Add(new RoutePoint() { RowIndex = 1, ColumnIndex = 10 });
            routePoints.Add(new RoutePoint() { RowIndex = 1, ColumnIndex = 6 });
            routePoints.Add(new RoutePoint() { RowIndex = 1, ColumnIndex = 17 });

            routePoints.Add(new RoutePoint() { RowIndex = 2, ColumnIndex = 2 });
            routePoints.Add(new RoutePoint() { RowIndex = 2, ColumnIndex = 10 });
            routePoints.Add(new RoutePoint() { RowIndex = 2, ColumnIndex = 15 });

            routePoints.Add(new RoutePoint() { RowIndex = 3, ColumnIndex = 5 });
            routePoints.Add(new RoutePoint() { RowIndex = 3, ColumnIndex = 8 });
            routePoints.Add(new RoutePoint() { RowIndex = 3, ColumnIndex = 10 });
            routePoints.Add(new RoutePoint() { RowIndex = 3, ColumnIndex = 17 });

            routePoints.Add(new RoutePoint() { RowIndex = 4, ColumnIndex = 3 });
            routePoints.Add(new RoutePoint() { RowIndex = 4, ColumnIndex = 10 });
            routePoints.Add(new RoutePoint() { RowIndex = 4, ColumnIndex = 18 });

            routePoints.Add(new RoutePoint() { RowIndex = 5, ColumnIndex = 3 });
            routePoints.Add(new RoutePoint() { RowIndex = 5, ColumnIndex = 10 });
            routePoints.Add(new RoutePoint() { RowIndex = 5, ColumnIndex = 12 });
            routePoints.Add(new RoutePoint() { RowIndex = 5, ColumnIndex = 18 });

            routePoints.Add(new RoutePoint() { RowIndex = 6, ColumnIndex = 0 });
            routePoints.Add(new RoutePoint() { RowIndex = 6, ColumnIndex = 4 });
            routePoints.Add(new RoutePoint() { RowIndex = 6, ColumnIndex = 6 });
            routePoints.Add(new RoutePoint() { RowIndex = 6, ColumnIndex = 12 });
            routePoints.Add(new RoutePoint() { RowIndex = 6, ColumnIndex = 15 });
            routePoints.Add(new RoutePoint() { RowIndex = 6, ColumnIndex = 18 });

            routePoints.Add(new RoutePoint() { RowIndex = 7, ColumnIndex = 2 });
            routePoints.Add(new RoutePoint() { RowIndex = 7, ColumnIndex = 7 });
            routePoints.Add(new RoutePoint() { RowIndex = 7, ColumnIndex = 10 });
            routePoints.Add(new RoutePoint() { RowIndex = 7, ColumnIndex = 13 });
            routePoints.Add(new RoutePoint() { RowIndex = 7, ColumnIndex = 15 });
            routePoints.Add(new RoutePoint() { RowIndex = 7, ColumnIndex = 18 });

            routePoints.Add(new RoutePoint() { RowIndex = 8, ColumnIndex = 2 });
            routePoints.Add(new RoutePoint() { RowIndex = 8, ColumnIndex = 4 });
            routePoints.Add(new RoutePoint() { RowIndex = 8, ColumnIndex = 13 });
            routePoints.Add(new RoutePoint() { RowIndex = 8, ColumnIndex = 16 });
            routePoints.Add(new RoutePoint() { RowIndex = 8, ColumnIndex = 18 });

            routePoints.Add(new RoutePoint() { RowIndex = 9, ColumnIndex = 5 });
            routePoints.Add(new RoutePoint() { RowIndex = 9, ColumnIndex = 8 });
            routePoints.Add(new RoutePoint() { RowIndex = 9, ColumnIndex = 11 });
            routePoints.Add(new RoutePoint() { RowIndex = 9, ColumnIndex = 13 });
            routePoints.Add(new RoutePoint() { RowIndex = 9, ColumnIndex = 17 });

            routePoints.Add(new RoutePoint() { RowIndex = 10, ColumnIndex = 2 });
            routePoints.Add(new RoutePoint() { RowIndex = 10, ColumnIndex = 4 });
            routePoints.Add(new RoutePoint() { RowIndex = 10, ColumnIndex = 12 });
            routePoints.Add(new RoutePoint() { RowIndex = 10, ColumnIndex = 15 });
            routePoints.Add(new RoutePoint() { RowIndex = 10, ColumnIndex = 17 });

            routePoints.Add(new RoutePoint() { RowIndex = 11, ColumnIndex = 2 });
            routePoints.Add(new RoutePoint() { RowIndex = 11, ColumnIndex = 8 });
            routePoints.Add(new RoutePoint() { RowIndex = 11, ColumnIndex = 10 });
            routePoints.Add(new RoutePoint() { RowIndex = 11, ColumnIndex = 17 });

            routePoints.Add(new RoutePoint() { RowIndex = 12, ColumnIndex = 1 });
            routePoints.Add(new RoutePoint() { RowIndex = 12, ColumnIndex = 8 });
            routePoints.Add(new RoutePoint() { RowIndex = 12, ColumnIndex = 10 });
            routePoints.Add(new RoutePoint() { RowIndex = 12, ColumnIndex = 12 });
            routePoints.Add(new RoutePoint() { RowIndex = 12, ColumnIndex = 15 });
            routePoints.Add(new RoutePoint() { RowIndex = 12, ColumnIndex = 18 });

            routePoints.Add(new RoutePoint() { RowIndex = 13, ColumnIndex = 1 });
            routePoints.Add(new RoutePoint() { RowIndex = 13, ColumnIndex = 5 });
            routePoints.Add(new RoutePoint() { RowIndex = 13, ColumnIndex = 8 });

            routePoints.Add(new RoutePoint() { RowIndex = 14, ColumnIndex = 1 });
            routePoints.Add(new RoutePoint() { RowIndex = 14, ColumnIndex = 5 });
            routePoints.Add(new RoutePoint() { RowIndex = 14, ColumnIndex = 8 });
            routePoints.Add(new RoutePoint() { RowIndex = 14, ColumnIndex = 11 });
            routePoints.Add(new RoutePoint() { RowIndex = 14, ColumnIndex = 15 });
            routePoints.Add(new RoutePoint() { RowIndex = 14, ColumnIndex = 18 });

            routePoints.Add(new RoutePoint() { RowIndex = 15, ColumnIndex = 1 });
            routePoints.Add(new RoutePoint() { RowIndex = 15, ColumnIndex = 4 });
            routePoints.Add(new RoutePoint() { RowIndex = 15, ColumnIndex = 8 });
            routePoints.Add(new RoutePoint() { RowIndex = 15, ColumnIndex = 13 });
            routePoints.Add(new RoutePoint() { RowIndex = 15, ColumnIndex = 15 });
            routePoints.Add(new RoutePoint() { RowIndex = 15, ColumnIndex = 17 });

            routePoints.Add(new RoutePoint() { RowIndex = 16, ColumnIndex = 2 });
            routePoints.Add(new RoutePoint() { RowIndex = 16, ColumnIndex = 6 });
            routePoints.Add(new RoutePoint() { RowIndex = 16, ColumnIndex = 10 });
            routePoints.Add(new RoutePoint() { RowIndex = 16, ColumnIndex = 12 });
            routePoints.Add(new RoutePoint() { RowIndex = 16, ColumnIndex = 14 });

            routePoints.Add(new RoutePoint() { RowIndex = 17, ColumnIndex = 1 });
            routePoints.Add(new RoutePoint() { RowIndex = 17, ColumnIndex = 5 });
            routePoints.Add(new RoutePoint() { RowIndex = 17, ColumnIndex = 7 });
            routePoints.Add(new RoutePoint() { RowIndex = 17, ColumnIndex = 12 });
            routePoints.Add(new RoutePoint() { RowIndex = 17, ColumnIndex = 14 });
            routePoints.Add(new RoutePoint() { RowIndex = 17, ColumnIndex = 18 });

            routePoints.Add(new RoutePoint() { RowIndex = 18, ColumnIndex = 2 });
            routePoints.Add(new RoutePoint() { RowIndex = 18, ColumnIndex = 4 });
            routePoints.Add(new RoutePoint() { RowIndex = 18, ColumnIndex = 12 });
            routePoints.Add(new RoutePoint() { RowIndex = 18, ColumnIndex = 14 });
            routePoints.Add(new RoutePoint() { RowIndex = 18, ColumnIndex = 16 });
            routePoints.Add(new RoutePoint() { RowIndex = 18, ColumnIndex = 18 });

            routePoints.Add(new RoutePoint() { RowIndex = 19, ColumnIndex = 5 });
            routePoints.Add(new RoutePoint() { RowIndex = 19, ColumnIndex = 18 });

            foreach (var routePoint in routePoints)
            {
                Points[routePoint.RowIndex, routePoint.ColumnIndex].ColorBackground = ConsoleColor.Black;
                Points[routePoint.RowIndex, routePoint.ColumnIndex].ColorForground = ConsoleColor.Cyan;
                Points[routePoint.RowIndex, routePoint.ColumnIndex].PointType = PointTypes.Coin;
                Points[routePoint.RowIndex, routePoint.ColumnIndex].IsActive = true;
                Points[routePoint.RowIndex, routePoint.ColumnIndex].Symbol = 'o';
            }
        }

        private void BuildTraps()
        {
            var routePoints = new List<RoutePoint>();

            routePoints.Add(new RoutePoint() { RowIndex = 2, ColumnIndex = 18 });
            routePoints.Add(new RoutePoint() { RowIndex = 3, ColumnIndex = 6 });
            routePoints.Add(new RoutePoint() { RowIndex = 7, ColumnIndex = 6 });
            routePoints.Add(new RoutePoint() { RowIndex = 8, ColumnIndex = 9 });
            routePoints.Add(new RoutePoint() { RowIndex = 10, ColumnIndex = 1 });
            routePoints.Add(new RoutePoint() { RowIndex = 10, ColumnIndex = 8 });
            routePoints.Add(new RoutePoint() { RowIndex = 12, ColumnIndex = 4 });
            routePoints.Add(new RoutePoint() { RowIndex = 13, ColumnIndex = 15 });
            routePoints.Add(new RoutePoint() { RowIndex = 17, ColumnIndex = 4 });
            routePoints.Add(new RoutePoint() { RowIndex = 18, ColumnIndex = 9 });

            foreach (var routePoint in routePoints)
            {
                Points[routePoint.RowIndex, routePoint.ColumnIndex].ColorBackground = ConsoleColor.Black;
                Points[routePoint.RowIndex, routePoint.ColumnIndex].ColorForground = ConsoleColor.Red;
                Points[routePoint.RowIndex, routePoint.ColumnIndex].PointType = PointTypes.Trap;
                Points[routePoint.RowIndex, routePoint.ColumnIndex].IsActive = true;
                Points[routePoint.RowIndex, routePoint.ColumnIndex].Symbol = 'x';
            }

        }

        private struct RoutePoint
        {
            public int RowIndex { get; set; }
            public int ColumnIndex { get; set; }
        }

        private void ConvertPointsToArrayToArrays()
        {
            pointsToSave = new Point[Configuration.ROW_NUMBER][];
            for (int i = 0; i < Configuration.ROW_NUMBER; i++)
            {
                pointsToSave[i] = new Point[Configuration.COLUMN_NUMBER];
                for (int j = 0; j < Configuration.COLUMN_NUMBER; j++)
                {
                    pointsToSave[i][j] = Points[i, j];
                }
            }
        }

        private void ConvertPointsToMultidimensionalArray()
        {
            Points = new Point[Configuration.ROW_NUMBER, Configuration.COLUMN_NUMBER];

            for (int i = 0; i < Configuration.ROW_NUMBER; i++)
            {
                for (int j = 0; j < Configuration.COLUMN_NUMBER; j++)
                {
                    Points[i,j] = pointsToSave[i][j];
                }
            }
        }

        public void Save()
        {
            ConvertPointsToArrayToArrays();
            var serializer = new DataContractJsonSerializer(typeof(PointsBuilder));
            var path = $"{Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory)}Points.json";
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
            var serializer = new DataContractJsonSerializer(typeof(PointsBuilder));
            var path = $"{Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory)}Points.json";

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
                        var pointsBuilder = (PointsBuilder) pointsObject;
                        pointsToSave = pointsBuilder.pointsToSave;

                        if (pointsToSave != null && pointsToSave.Any())
                        {
                            ConvertPointsToMultidimensionalArray();
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
