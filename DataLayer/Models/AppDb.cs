using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Models
{
    public class AppDb:DbContext
    {
        public AppDb(DbContextOptions<AppDb> options) : base(options) { }

        public DbSet<UserModel> User { get; set; }
        public DbSet<Dept> Dept { get; set; }
        public DbSet<Student> Student { get; set; }
    }
}
