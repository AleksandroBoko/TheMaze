using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMaze.Enums;

namespace TheMaze.Models.GameObjects
{
    public abstract class GameElement
    {
        public ConsoleColor ColorForeground { get; set; }
        public ConsoleColor ColorBackground { get; set; }
        public bool IsActive { get; set; }
        public char Symbol { get; set; }
        public int PositionTop { get; set; }
        public int PositionLeft { get; set; }
    }
}
