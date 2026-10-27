using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Models
{
    public class OrderModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

    }
}
