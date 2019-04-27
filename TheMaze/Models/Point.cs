using System;
using TheMaze.Enums;

namespace TheMaze.Models
{
    public struct Point
    {
        public ConsoleColor ColorForeground { get; set; }
        public ConsoleColor ColorBackground { get; set; }
        public FieldTypes FieldTypes { get; set; }
        public bool IsActive { get; set; }
        public char Symbol { get; set; }  
    }
}
