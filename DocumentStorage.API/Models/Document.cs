namespace DocumentStorage.API.Models
{
    public class Document
    {
        public int Id { get; set; }

        public required string Data { get; set; }




        public virtual ICollection<Tag>? Tags { get; set; }
    }
}
