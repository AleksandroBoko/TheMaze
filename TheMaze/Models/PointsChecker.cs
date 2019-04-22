using TheMaze.Enums;

namespace TheMaze.Models
{
    public class PointsChecker
    {
        private Point[,] points { get; set; }

        public PointsChecker(Point[,] points)
        {
            this.points = points;
        }

        public PointTypes GetPointType(int row, int column)
        {
            var point = points[row, column];
            return point.PointType;
        }

    }
}
