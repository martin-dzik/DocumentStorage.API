namespace DocumentStorage.API.DTOs
{
    public abstract class DocumentDtoBase
    {
        public required Dictionary<string, object> Data { get; set; }

        public ICollection<string>? Tags { get; set; }
    }
}
