using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions<AppDb> options) : base(options) { }

            //This initialises a connection to the database
            // This method works with dependency injection to configure the database connection

        public DbSet<UserModel> Users { get; set; }
    }
}
