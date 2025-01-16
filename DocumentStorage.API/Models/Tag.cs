namespace DocumentStorage.API.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public required string Name { get; set; }



        public virtual ICollection<Document>? Documents { get; set; }
    }
}
