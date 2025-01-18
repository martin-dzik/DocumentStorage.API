using System.Runtime.Serialization;

namespace DocumentStorage.API.DTOs
{
    [DataContract]
    public class TagDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public required string Name { get; set; }
    }
}
