using System;
using System.Runtime.Serialization;
using TheMaze.Core.Enums;

namespace TheMaze.Core.Models.GameObjects
{
    [DataContract]
    public class GameObject
    {
        [DataMember]
        public CellColor ColorForeground { get; set; }
        [DataMember]
        public CellColor ColorBackground { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public char Symbol { get; set; }
        [DataMember]
        public int PositionTop { get; set; }
        [DataMember]
        public int PositionLeft { get; set; }
    }
}
