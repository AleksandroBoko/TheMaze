using System.Runtime.Serialization;
using TheMaze.Enums;

namespace TheMaze.Models.GameObjects
{
    [KnownType(typeof(GameObject))]
    [DataContract]
    public class Point : GameObject
    {
        [DataMember]
        public FieldTypes FieldType { get; set; }
    }
}
