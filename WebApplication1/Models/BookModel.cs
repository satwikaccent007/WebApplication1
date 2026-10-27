using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class BookModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }
    }
}
