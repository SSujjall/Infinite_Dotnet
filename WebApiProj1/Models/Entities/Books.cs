using System.ComponentModel.DataAnnotations;

namespace WebApiProj1.Models.Entities
{
    public class Books
    {
        [Key]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookPrice { get; set; }
    }
}
