using System.Runtime.Serialization;

namespace DocumentStorage.API.DTOs
{
    [DataContract]
    public class DocumentDto : DocumentDtoBase
    {
        [DataMember]
        public int Id { get; set; }
    }
}
