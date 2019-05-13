using System.Runtime.Serialization;
using TheMaze.Core.Enums;

namespace TheMaze.Core.Models.GameObjects
{
    [KnownType(typeof(GameObject))]
    [DataContract]
    public class Cell : GameObject
    {
        [DataMember]
        public FieldTypes FieldType { get; set; }
    }
}
