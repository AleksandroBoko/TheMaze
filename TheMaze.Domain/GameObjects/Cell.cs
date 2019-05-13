using System.Runtime.Serialization;

namespace TheMaze.Domain.GameObjects
{
    [KnownType(typeof(GameObject))]
    [DataContract]
    public class Cell : GameObject
    {
        [DataMember]
        public FieldTypes FieldType { get; set; }
    }
}
