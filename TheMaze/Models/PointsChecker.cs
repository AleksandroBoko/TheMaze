using TheMaze.Enums;
using TheMaze.Models.GameObjects;

namespace TheMaze.Models
{
    public class PointsChecker
    {
        private GameObject[,] points { get; set; }

        public PointsChecker(GameObject[,] points)
        {
            this.points = points;
        }

        public FieldTypes GetPointType(int row, int column)
        {
            var point = points[row, column];
            return (point as Field).FieldType;
        }

    }
}
