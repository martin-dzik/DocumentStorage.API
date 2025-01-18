using System.Runtime.Serialization;

namespace DocumentStorage.API.DTOs
{
    [DataContract]
    public abstract class DocumentDtoBase
    {

        [DataMember]
        public required string Data { get; set; }

        [DataMember]
        public ICollection<string>? Tags { get; set; }
    }
}
