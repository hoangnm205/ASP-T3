using System;
using ASP_T3.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASP_T3
{
    public class ApplicationDBContext : DbContext
    {
        private string connectionString = @"Data Source=172.16.2.164,1433;Initial Catalog=ASP-T3;User Id=sa;Password=123123;TrustServerCertificate=True";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<User> Users { get; set; }
    }
}

