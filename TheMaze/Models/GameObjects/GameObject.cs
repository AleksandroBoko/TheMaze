using System;
using System.Runtime.Serialization;

namespace TheMaze.Models.GameObjects
{
    [DataContract]
    public abstract class GameObject
    {
        [DataMember]
        public ConsoleColor ColorForeground { get; set; }
        [DataMember]
        public ConsoleColor ColorBackground { get; set; }
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
