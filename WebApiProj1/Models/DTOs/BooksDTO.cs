namespace WebApiProj1.Models.DTOs
{
    public class AddBooksDTO
    {
        public string BookName { get; set; }
        public string BookPrice { get; set; }
    }

    public class UpdateBookDTO
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookPrice { get; set; }
    }
}
