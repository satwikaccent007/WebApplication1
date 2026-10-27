using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class AppDb:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=BDC7-L-2156R7Q\\SQLEXPRESS;Initial Catalog=MyAppDb;Integrated Security=true;TrustServerCertificate=true;");
        
        }
        public DbSet<UserModel> user { get; set; } //this represents the table;
        // so the model class also represents the schema;
        public DbSet<OrderModel> order { get; set; }
        public DbSet<BookModel> books { get; set; }
    }
}
