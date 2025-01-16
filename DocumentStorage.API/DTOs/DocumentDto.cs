namespace DocumentStorage.API.DTOs
{
    public class DocumentDto
    {
        public required Dictionary<string, string> Data { get; set; }

        public ICollection<string>? Tags { get; set; }
    }
}
